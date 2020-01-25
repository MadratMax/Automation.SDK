namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop
{
    using Constants;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// This is the Microsoft UI Automation supported control types
    /// Unfortunatelly, MS UIA control types are runtime defined static propertiess
    /// 
    /// This enum exist in order to:
    /// 1) Support fluent mapping
    /// 2) list available choices
    /// 3) Support all required entities within one namespace
    /// </summary>
    public enum ControlTypes
    {
        [Display(Name = ControlTypeNames.Button)]
        Button,

        [Display(Name = ControlTypeNames.CheckBox)]
        CheckBox,

        [Display(Name = ControlTypeNames.ComboBox)]
        ComboBox,

        [Display(Name = ControlTypeNames.DataGrid)]
        DataGrid,

        [Display(Name = ControlTypeNames.Document)]
        Document,

        [Display(Name = ControlTypeNames.DataItem)]
        DataItem,

        [Display(Name = ControlTypeNames.TextBox)]
        TextBox,

        [Display(Name = ControlTypeNames.GroupBox)]
        GroupBox,

        [Display(Name = ControlTypeNames.Header)]
        Header,

        [Display(Name = ControlTypeNames.HeaderItem)]
        HeaderItem,

        [Display(Name = ControlTypeNames.Link)]
        Link,

        [Display(Name = ControlTypeNames.Image)]
        Image,

        [Display(Name = ControlTypeNames.ListBox)]
        ListBox,

        [Display(Name = ControlTypeNames.ListItem)]
        ListItem,

        [Display(Name = ControlTypeNames.MenuBar)]
        MenuBar,

        [Display(Name = ControlTypeNames.Menu)]
        Menu,

        [Display(Name = ControlTypeNames.MenuItem)]
        MenuItem,

        [Display(Name = ControlTypeNames.Tooltip)]
        Tooltip,

        [Display(Name = ControlTypeNames.Toolbar)]
        Toolbar,

        [Display(Name = ControlTypeNames.Panel)]
        Panel,

        [Display(Name = ControlTypeNames.ProgressBar)]
        ProgressBar,

        [Display(Name = ControlTypeNames.RadioButton)]
        RadioButton,

        [Display(Name = ControlTypeNames.ScrollBar)]
        ScrollBar,

        [Display(Name = ControlTypeNames.Slider)]
        Slider,

        [Display(Name = ControlTypeNames.Thumb)]
        Thumb,

        [Display(Name = ControlTypeNames.Spinner)]
        Spinner,

        [Display(Name = ControlTypeNames.SplitButton)]
        SplitButton,

        [Display(Name = ControlTypeNames.Tab)]
        Tab,

        [Display(Name = ControlTypeNames.TabItem)]
        TabItem,

        [Display(Name = ControlTypeNames.Table)]
        Table,

        [Display(Name = ControlTypeNames.Label)]
        Label,

        [Display(Name = ControlTypeNames.TreeItem)]
        TreeItem,

        [Display(Name = ControlTypeNames.Window)]
        Window,

        [Display(Name = ControlTypeNames.Custom)]
        Custom,
    }
}
