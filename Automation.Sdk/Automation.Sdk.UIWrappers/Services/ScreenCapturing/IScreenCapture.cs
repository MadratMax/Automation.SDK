
namespace Automation.Sdk.UIWrappers.Services.ScreenCapturing
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Provides functions to capture the entire screen, or a particular window, and save it to a file.
    /// </summary>
    public interface IScreenCapture : IDisposable
    {
        /// <summary>
        /// Do the screenshot, and save it to disk
        /// </summary>
        /// <param name="path">Path to file</param>
        void TakeScreenshot(string path);

        /// <summary>
        /// Gets the screenshot and store it to ScreenList dictionary
        /// </summary>
        /// <param name="screenName">Name of the screenshot. Will be saved as "{Screenshot Number} - {screenName}"</param>
        void StoreScreenshot(string screenName);

        /// <summary>
        /// Store image to ScreenList dictionary
        /// </summary>
        /// <param name="image">image data to store</param>
        /// <param name="name">Name of the image</param>
        void StoreImage(Bitmap image, string name);

        /// <summary>
        /// Writes All screenshots stored in ScreenList dictionary and clears it after
        /// </summary>
        /// <param name="path">Output folder</param>
        /// <param name="onlyLast">True to save only last screenshot</param>
        void SaveScreenshots(string path, bool onlyLast = false);

        /// <summary>
        /// Clears the ScreenList dictionary
        /// </summary>
        void ClearScreenshots();
    }
}