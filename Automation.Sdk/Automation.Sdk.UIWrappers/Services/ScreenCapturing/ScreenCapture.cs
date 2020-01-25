
namespace Automation.Sdk.UIWrappers.Services.ScreenCapturing
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Automation.Sdk.UIWrappers.Aspects;
    using Automation.Sdk.UIWrappers.Services.Logging;

    [AutoRegister]
    public class ScreenCapture : IScreenCapture
    {
        private readonly Logger _logger;
        private readonly bool _screenshotsEnabled;
        private SortedDictionary<string, Bitmap> _screenList;
        private readonly int _maxScreenshotsCount;

        private int _screenshotId = 1;

        public ScreenCapture(Logger logger)
        {
            _logger = logger;

            var val = ConfigurationManager.AppSettings.Get("screenshots");
            _screenshotsEnabled = string.IsNullOrWhiteSpace(val) ? false : bool.Parse(val);

            val = ConfigurationManager.AppSettings.Get("maxScreenshotsCount");
            _maxScreenshotsCount = string.IsNullOrWhiteSpace(val) ? 5 : int.Parse(val);
        }

        public void TakeScreenshot(string path)
        {
            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, new Size(width, height));
            }

            var folder = Path.GetDirectoryName(path);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            bmp.Save(path, ImageFormat.Png);
        }

        public void StoreScreenshot(string screenName)
        {
            if (!_screenshotsEnabled)
            {
                return;
            }

            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;

            if (width <= 0 || height <= 0)
            {
                _logger.Write($"Someone left CI VM unproperly? Width={width}, Height={height}");
                return;
            }

            _logger.Write($"Attempt to take ScreenShot {screenName}: Width={width}, Height={height}");
            Bitmap bmp;
            try
            {
                bmp = new Bitmap(width, height);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(0, 0, 0, 0, new Size(width, height));
                }
            }
            catch (Exception e)
            {
                _logger.Write("Exception while taking screenshot. Someone left CI VM unproperly?");
                _logger.Write(e);

                return;
            }

            StoreImage(bmp, screenName);
        }

        public void StoreImage(Bitmap image, string name)
        {
            if (image == null)
            {
                _logger.Write($"Image {name} is null.");
                return;
            }

            if (_screenList == null)
            {
                _screenList = new SortedDictionary<string, Bitmap>();
            }

            name = ConvertFromTo.RemoveRestrictedChars(name);
            _screenList.Add($"{_screenshotId:D3} - {name}", image);
            _screenshotId++;

            if (_screenList.Count() > _maxScreenshotsCount)
            {
                var imageToDispose = _screenList.First();
                imageToDispose.Value.Dispose();

                _screenList.Remove(imageToDispose.Key);
            }
        }

        public void SaveScreenshots(string path, bool onlyLast = false)
        {
            if (_screenList == null || _screenList.Count < 1)
            {
                return;
            }

            if (_screenList.Count > 0 && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var screenshotsToSave = onlyLast ? _screenList.Skip(_screenList.Count - 1) : _screenList;

            foreach (var screenshot in screenshotsToSave)
            {
                if (screenshot.Value != null)
                {
                    var savedPath = Path.Combine(path, screenshot.Key.Replace("?", "_") + ".png");
                    try
                    {
                        screenshot.Value.Save(savedPath, ImageFormat.Png);
                    }
                    catch (Exception e)
                    {
                        _logger.Write($"Cannot save screenshots to path: {path}. Error: {e.Message}");
                    }
                }
            }

            ClearScreenshots();
        }

        public void ClearScreenshots()
        {
            if (_screenList == null)
            {
                return;
            }

            try
            {
                foreach (var image in _screenList)
                {
                    image.Value.Dispose();
                }
            }
            catch (Exception e)
            {
                _logger.Write(e);
            }

            _screenshotId = 1;
            _screenList.Clear();
        }

        public void Dispose()
        {
            ClearScreenshots();
        }
    }
}