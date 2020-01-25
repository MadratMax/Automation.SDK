
namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.Runtime.InteropServices;
    using InputLib = Microsoft.Test.Input;
    using MouseButton = Enums.MouseButton;
    using System.Threading;

    /// <summary>
    /// Class for Mouse test input
    /// </summary>
    public static class Mouse
    {
        /// <summary>
        /// Simulates scrolling of the mouse wheel up or down.
        /// </summary>
        /// <param name="x">The number of lines to scroll. Use positive numbers to scroll up and negative numbers to scroll down.</param>
        public static void ScrollMouseWheel(double x)
        {
            InputLib.Mouse.Scroll(x);
        }

        public static void Click(MouseButton button = MouseButton.Left)
        {
            InputLib.Mouse.Click((InputLib.MouseButton)button);
        }

        public static void Down(MouseButton button)
        {
            InputLib.Mouse.Down((InputLib.MouseButton)button);
        }

        public static void Up(MouseButton button)
        {
            InputLib.Mouse.Up((InputLib.MouseButton)button);
        }

        public static void Reset()
        {
            InputLib.Mouse.Reset();
            Thread.Sleep(50);
        }

        public static void DragTo(MouseButton button, System.Drawing.Point pt)
        {
            InputLib.Mouse.DragTo((InputLib.MouseButton)button, pt);
        }

        public static void DragTo(MouseButton button, System.Windows.Point pt)
        {
            DragTo(button, new System.Drawing.Point((int)pt.X, (int)pt.Y));
        }

        public static void Freeze()
        {
            BlockInput(true);
        }

        public static bool MoveTo(int x, int y)
        {
            if (y < 0)
            {
                y = 0;
            }

            if (x < 0)
            {
                x = 0;
            }

            Console.WriteLine($"Moving mouse to {x}:{y}");
            InputLib.Mouse.MoveTo(new System.Drawing.Point(x,y));
            Thread.Sleep(1);
            return true;
        }

        public static bool MoveTo(double x, double y)
        {
            return MoveTo((int)x, (int)y);
        }

        public static bool MoveTo(System.Windows.Point pt)
        {
            return MoveTo(pt.X, pt.Y);
        }

        public static bool MoveTo(System.Drawing.Point pt)
        {
            return MoveTo(pt.X, pt.Y);
        }

        public static void MoveMouseAround(int count, int wait)
        {
            System.Windows.Point pt = GetCursorPosition();

            for (int j = 0; j < count; ++j )
            { 
                for (int i = -6; i < 6; i += 2)
                {
                    MoveTo(pt.X + i, pt.Y + i);
                    Thread.Sleep(wait);
                }
            }

            Thread.Sleep(50);
        }

        public static bool SendClick(double x, double y)
        {
            if (!MoveTo(x, y))
            {
                return false;
            }

            InputLib.Mouse.Click(InputLib.MouseButton.Left);

            return true;
        }

        public static void Unfreeze()
        {
            BlockInput(false);
        }

        [DllImport("user32.dll")]
        private static extern bool BlockInput(bool block);

        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator System.Windows.Point(POINT point)
            {
                return new System.Windows.Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        public static System.Windows.Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }

        public static void ClickOn(System.Windows.Point point, MouseButton btn = MouseButton.Left)
        {
            MoveTo(point);
            Click(btn);
        }
    }
}
