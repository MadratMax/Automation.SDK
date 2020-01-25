namespace Automation.Sdk.UIWrappers.Services.ControlsFacade.Fluency.Controls.Desktop
{
    /// <summary>
    /// In order to support all required entities within one namespace and cut UNSUPPORTED
    /// ones we have this.
    /// </summary>
    /// <example>
    /// How this works:
    /// Imagine the following visual UI structure where search context element is "Element 1"
    /// Element 1
    /// |
    /// |--Element 1.1
    /// |
    /// |--Element 1.2
    /// |  |
    /// |  |--Element 1.2.1
    /// |  |
    /// |  |--Element 1.2.2
    /// |
    /// |--Element 1.3
    ///    |
    ///    ---Element 1.3.1
    /// </example>
    public enum Scope
    {
        /// <summary>
        /// Search through only direct child elements of the search context element
        /// 
        /// Will perform search throug following elements from above example:
        /// Element 1.1, 1.2, 1.3
        /// </summary>
        Children = 2,

        /// <summary>
        /// Search through all descendants elements of the search context element
        /// 
        /// Will perform search throug following elements from above example:
        /// Element 1.1, 1.2, 1.2.1, 1.2.2, 1.3, 1.3.1
        /// </summary>
        Descendants = 4,

        /// <summary>
        /// Search through all descendants elements of the search context element including element itself
        /// 
        /// Will perform search throug all elements from above example:
        /// Element 1, 1.1, 1.2, 1.2.1, 1.2.2, 1.3, 1.3.1
        /// </summary>
        Subtree = 7,
    }
}