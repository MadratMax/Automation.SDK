namespace Automation.Sdk.UIWrappers.Services.ControlConversion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Windows.Automation;
    using Automation.Sdk.UIWrappers.ControlDrivers;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using JetBrains.Annotations;
    using Constants;

    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    // Can be inherited in test suites
    public class ControlTypeConverter
    {
        private readonly ILogger _logger;
        private readonly Dictionary<string, IControlTypeMap<Element>> _map;

        public ControlTypeConverter([NotNull] ILogger logger)
        {
            _logger = logger;
            _map = new Dictionary<string, IControlTypeMap<Element>>();
            _map.Add(ControlTypeNames.Button, new ControlTypeMap<UIAButton>(ControlType.Button));
            _map.Add(ControlTypeNames.CheckBox, new ControlTypeMap<UIACheckBox>(ControlType.CheckBox));
            _map.Add(ControlTypeNames.ComboBox, new ControlTypeMap<UIAComboBox>(ControlType.ComboBox));
            _map.Add(ControlTypeNames.DataGrid, new ControlTypeMap<UIADataGrid>(ControlType.DataGrid));
            _map.Add(ControlTypeNames.Document, new ControlTypeMap<UIADocument>(ControlType.Document));
            _map.Add(ControlTypeNames.DataItem, new ControlTypeMap<UIADataItem>(ControlType.DataItem));
            _map.Add(ControlTypeNames.TextBox, new ControlTypeMap<UIAEdit>(ControlType.Edit));
            _map.Add(ControlTypeNames.GroupBox, new ControlTypeMap<UIAGroup>(ControlType.Group));
            _map.Add(ControlTypeNames.Header, new ControlTypeMap<UIAHeader>(ControlType.Header));
            _map.Add(ControlTypeNames.HeaderItem, new ControlTypeMap<UIAHeaderItem>(ControlType.HeaderItem));
            _map.Add(ControlTypeNames.Link, new ControlTypeMap<UIAHyperlink>(ControlType.Hyperlink));
            _map.Add(ControlTypeNames.Image, new ControlTypeMap<UIAImage>(ControlType.Image));
            _map.Add(ControlTypeNames.ListBox, new ControlTypeMap<UIAList>(ControlType.List));
            _map.Add(ControlTypeNames.ListItem, new ControlTypeMap<UIAListItem>(ControlType.ListItem));
            _map.Add(ControlTypeNames.MenuBar, new ControlTypeMap<Element>(ControlType.MenuBar));
            _map.Add(ControlTypeNames.Menu, new ControlTypeMap<UIAMenu>(ControlType.Menu));
            _map.Add(ControlTypeNames.MenuItem, new ControlTypeMap<UIAMenuItem>(ControlType.MenuItem));
            _map.Add(ControlTypeNames.Tooltip, new ControlTypeMap<UIAToolTip>(ControlType.ToolTip));
            _map.Add(ControlTypeNames.Toolbar, new ControlTypeMap<UIAToolTip>(ControlType.ToolBar));
            _map.Add(ControlTypeNames.Panel, new ControlTypeMap<UIAPane>(ControlType.Pane));
            _map.Add(ControlTypeNames.ProgressBar, new ControlTypeMap<UIAProgressBar>(ControlType.ProgressBar));
            _map.Add(ControlTypeNames.RadioButton, new ControlTypeMap<UIARadioButton>(ControlType.RadioButton));
            _map.Add(ControlTypeNames.ScrollBar, new ControlTypeMap<UIAScrollBar>(ControlType.ScrollBar));
            _map.Add(ControlTypeNames.Slider, new ControlTypeMap<UIASlider>(ControlType.Slider));
            _map.Add(ControlTypeNames.Thumb, new ControlTypeMap<UIAThumb>(ControlType.Thumb));
            _map.Add(ControlTypeNames.Spinner, new ControlTypeMap<UIASpinner>(ControlType.Spinner));
            _map.Add(ControlTypeNames.SplitButton, new ControlTypeMap<UIASplitButton>(ControlType.SplitButton));
            _map.Add(ControlTypeNames.Tab, new ControlTypeMap<UIATab>(ControlType.Tab));
            _map.Add(ControlTypeNames.TabItem, new ControlTypeMap<UIATabItem>(ControlType.TabItem));
            _map.Add(ControlTypeNames.Table, new ControlTypeMap<UIATable>(ControlType.Table));
            _map.Add(ControlTypeNames.Label, new ControlTypeMap<UIAText>(ControlType.Text));
            _map.Add(ControlTypeNames.TreeItem, new ControlTypeMap<UIATreeItem>(ControlType.TreeItem));
            _map.Add(ControlTypeNames.Window, new ControlTypeMap<UIAWindow>(ControlType.Window));
            _map.Add(ControlTypeNames.Custom, new ControlTypeMap<UIACustom>(ControlType.Custom));
        }

        public ControlType Convert<TElement>() where TElement : Element
        {
            return _map.Single(x => x.Value.UIMapType == typeof(TElement)).Value.ControlType;
        }

        [NotNull]
        public ControlType Resolve([NotNull] string controlTypeName)
        {
            var mapItem = _map.Single(x => x.Key.Equals(controlTypeName));

            return mapItem.Value.ControlType;
        }

        public Element Create(AutomationElement element)
        {
            var controlType = element.Current.ControlType;
            var controlMap = _map.SingleOrDefault(x => Equals(x.Value.ControlType, controlType));
            if (controlMap.Key == null)
            {
                return new Element(element);
            }
            var constructor = CreateConstructor(controlMap.Value.UIMapType, typeof(AutomationElement));
            return (Element) constructor(new object[] {element});
        }

        private static Func<object[], object> CreateConstructor(Type type, params Type[] parameters)
        {
            var constructorInfo = type.GetConstructor(parameters);
            var paramExpr = Expression.Parameter(typeof(object[]));
            var constructorParameters = parameters.Select((paramType, index) =>
                Expression.Convert(
                    Expression.ArrayAccess(paramExpr, Expression.Constant(index)),
                    paramType)).ToArray();

            var body = Expression.New(constructorInfo, constructorParameters);

            var constructor = Expression.Lambda<Func<object[], object>>(body, paramExpr);
            return constructor.Compile();
        }
    }
}
