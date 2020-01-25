namespace Automation.Sdk.UIWrappers.Enums
{
    using System.Diagnostics.CodeAnalysis;
    using InputLib = Microsoft.Test.Input;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "Self-explanatory code")]
    public enum MouseButton
    {
        Left = InputLib.MouseButton.Left,
        Right = InputLib.MouseButton.Right,
        Middle = InputLib.MouseButton.Middle
    }
}
