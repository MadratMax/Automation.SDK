namespace Automation.Sdk.Bindings.Transformations
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class TablePropertyAttribute : Attribute
    {
        /// <summary>
        /// Name of the column in table for this property
        /// </summary>
        public string TableColumnName;

        /// <summary>
        /// If true then this field belongs to a sense fields collection for current SpecFlow step
        /// </summary>
        public bool PartOfSenseFields = false;

        /// <summary>
        /// If true then table should contain column with this property
        /// </summary>
        public bool Mandatory = true;

        /// <summary>
        /// If true then this property will be skipped while parsing table
        /// </summary>
        public bool Skip = true;
    }
}
