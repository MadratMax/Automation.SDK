namespace Automation.Sdk.Bindings.Transformations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Automation.Sdk.UIWrappers.Services;
    using JetBrains.Annotations;
    using TechTalk.SpecFlow;

    public static class TableAutoTransformations
    {
        [UsedImplicitly]
        public static List<TListType> CreateList<TListType>(this Table table)
            where TListType : ITableAutoTranform
        {
            var type = typeof(TListType);

            var properties = type.GetProperties();
            var mandatoryFields = new List<string>();
            var senseFields = new List<string>();
            var allFields = new List<string>();
            var resultObjects = new List<TListType>();

            FillLists(properties, mandatoryFields, senseFields, allFields);
            Methods.CheckSpecFlowTable(table, mandatoryFields, senseFields, ScenarioContext.Current.StepContext.StepInfo.Text);

            foreach (var tableRow in table.Rows)
            {
                var newObject = CreateNewObject<TListType>(allFields, properties, tableRow);
                resultObjects.Add(newObject);
            }

            return resultObjects;
        }

        [UsedImplicitly]
        public static TObject CreateObject<TObject>(this Table table)
            where TObject : ITableAutoTranform
        {
            return CreateList<TObject>(table).First();
        }

        private static TListType CreateNewObject<TListType>(List<string> allFields, PropertyInfo[] properties, TableRow tableRow) 
            where TListType : ITableAutoTranform
        {
            var newObject = Activator.CreateInstance<TListType>();

            for (int i = 0; i < allFields.Count; i++)
            {
                var fieldName = allFields[i];
                var property = properties[i];

                if (tableRow.ContainsKey(fieldName))
                {
                    var value = new FormattedString(tableRow[fieldName]).ToString();

                    if (property.PropertyType == typeof(int))
                    {
                        var intValue = int.Parse(value);
                        property.SetMethod.Invoke(newObject, new object[] {intValue});
                    }
                    else if (property.PropertyType == typeof(bool))
                    {
                        var boolValue = value.Equals("true", StringComparison.InvariantCultureIgnoreCase);
                        property.SetMethod.Invoke(newObject, new object[] {boolValue});
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        property.SetMethod.Invoke(newObject, new object[] {value});
                    }
                }
            }

            newObject.BuildSelf();
            return newObject;
        }

        private static void FillLists(PropertyInfo[] properties, List<string> mandatoryFields, List<string> senseFields, List<string> allFields)
        {
            foreach (var propertyInfo in properties)
            {
                var columnName = propertyInfo.Name;

                var attribute = propertyInfo.GetCustomAttribute<TablePropertyAttribute>();
                if (attribute != null)
                {
                    if (attribute.Skip)
                    {
                        continue;
                    }

                    columnName = attribute.TableColumnName;

                    if (attribute.Mandatory)
                    {
                        mandatoryFields.Add(columnName);
                    }

                    if (attribute.PartOfSenseFields)
                    {
                        senseFields.Add(columnName);
                    }
                }
                else
                {
                    mandatoryFields.Add(columnName);
                }

                allFields.Add(columnName);
            }
        }
    }
}