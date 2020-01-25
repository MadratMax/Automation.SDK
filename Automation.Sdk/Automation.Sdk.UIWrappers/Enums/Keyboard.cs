
namespace Automation.Sdk.UIWrappers.Enums
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using InputLib = Microsoft.Test.Input;

    /// <summary>
    /// Keyboard key code
    /// </summary>
    public enum Key
    {
        /// <summary>
        /// Indicates the key is part of a dead-key composition
        /// </summary>
        DeadCharProcessed = 0, 

        /// <summary>
        /// No key pressed.
        /// </summary>
        None = 0, 

        /// <summary>
        /// Cancel key
        /// </summary>
        Cancel = 3, 

        /// <summary>
        /// The BACKSPACE key.
        /// </summary>
        Back = 8, 

        /// <summary>
        /// Tab key
        /// </summary>
        Tab = 9, 

        /// <summary>
        /// LineFeed
        /// </summary>
        LineFeed = 10, 

        /// <summary>
        /// Clear
        /// </summary>
        Clear = 12, 

        /// <summary>
        /// Enter
        /// </summary>
        Enter = 13, 

        /// <summary>
        /// Shift
        /// </summary>
        Shift = 16, 

        /// <summary>
        /// Ctrl
        /// </summary>
        Ctrl = 17, 

        /// <summary>
        /// Alt
        /// </summary>
        Alt = 18, 

        /// <summary>
        /// Pause
        /// </summary>
        Pause = 19, 

        /// <summary>
        /// CapLock
        /// </summary>
        CapsLock = 20, 

        /// <summary>
        /// Escape
        /// </summary>
        Escape = 27, 

        /// <summary>
        /// Space
        /// </summary>
        Space = 32, 

        /// <summary>
        /// PageUp
        /// </summary>
        PageUp = 33, 

        /// <summary>
        /// PageDown
        /// </summary>
        PageDown = 34, 

        /// <summary>
        /// End
        /// </summary>
        End = 35, 

        /// <summary>
        /// Home
        /// </summary>
        Home = 36, 

        /// <summary>
        /// Left
        /// </summary>
        Left = 37, 

        /// <summary>
        /// Up
        /// </summary>
        Up = 38, 

        /// <summary>
        /// Right
        /// </summary>
        Right = 39, 

        /// <summary>
        /// Down
        /// </summary>
        Down = 40, 

        /// <summary>
        /// Select
        /// </summary>
        Select = 41, 

        /// <summary>
        /// Print
        /// </summary>
        Print = 42, 

        /// <summary>
        /// Execute
        /// </summary>
        Execute = 43, 

        /// <summary>
        /// PrintScreen
        /// </summary>
        PrintScreen = 44, 

        /// <summary>
        /// SnapShot
        /// </summary>        
        Snapshot = 44, 

        /// <summary>
        /// Insert
        /// </summary>
        Insert = 45, 

        /// <summary>
        /// Delete
        /// </summary>
        Delete = 46, 

        /// <summary>
        /// Help
        /// </summary>
        Help = 47, 

        /// <summary>
        /// D0
        /// </summary>
        D0 = 48, 

        /// <summary>
        /// D1
        /// </summary>
        D1 = 49, 

        /// <summary>
        /// D2
        /// </summary>
        D2 = 50, 

        /// <summary>
        /// D3
        /// </summary>
        D3 = 51, 

        /// <summary>
        /// D4
        /// </summary>
        D4 = 52, 

        /// <summary>
        /// D5
        /// </summary>
        D5 = 53, 

        /// <summary>
        /// D6
        /// </summary>
        D6 = 54, 

        /// <summary>
        /// D7
        /// </summary>
        D7 = 55, 

        /// <summary>
        /// D8
        /// </summary>
        D8 = 56, 

        /// <summary>
        /// D9
        /// </summary>
        D9 = 57, 

        /// <summary>
        /// A
        /// </summary>
        A = 65, 

        /// <summary>
        /// B
        /// </summary>
        B = 66, 

        /// <summary>
        /// C
        /// </summary>
        C = 67, 

        /// <summary>
        /// D
        /// </summary>
        D = 68, 

        /// <summary>
        /// E
        /// </summary>
        E = 69, 

        /// <summary>
        /// F
        /// </summary>
        F = 70, 

        /// <summary>
        /// G
        /// </summary>
        G = 71, 

        /// <summary>
        /// H
        /// </summary>
        H = 72, 

        /// <summary>
        /// I
        /// </summary>
        I = 73, 

        /// <summary>
        /// J
        /// </summary>
        J = 74, 

        /// <summary>
        /// K
        /// </summary>
        K = 75, 

        /// <summary>
        /// L
        /// </summary>
        L = 76, 

        /// <summary>
        /// M
        /// </summary>
        M = 77, 

        /// <summary>
        /// N
        /// </summary>
        N = 78, 

        /// <summary>
        /// O
        /// </summary>
        O = 79, 

        /// <summary>
        /// P
        /// </summary>
        P = 80, 

        /// <summary>
        /// Q
        /// </summary>
        Q = 81, 

        /// <summary>
        /// R
        /// </summary>
        R = 82, 

        /// <summary>
        /// S
        /// </summary>
        S = 83, 

        /// <summary>
        /// T
        /// </summary>
        T = 84, 

        /// <summary>
        /// U
        /// </summary>
        U = 85, 

        /// <summary>
        /// V
        /// </summary>
        V = 86, 

        /// <summary>
        /// W
        /// </summary>
        W = 87, 

        /// <summary>
        /// X
        /// </summary>
        X = 88, 

        /// <summary>
        /// Y
        /// </summary>
        Y = 89, 

        /// <summary>
        /// Z
        /// </summary>
        Z = 90, 

        /// <summary>
        /// The left Windows logo key (Microsoft Natural Keyboard).
        /// </summary>
        LWin = 91, 

        /// <summary>
        /// The right Windows logo key (Microsoft Natural Keyboard).
        /// </summary>
        RWin = 92, 

        /// <summary>
        /// The Application key (Microsoft Natural Keyboard).
        /// </summary>
        Apps = 93, 

        /// <summary>
        /// The Computer Sleep key.
        /// </summary>
        Sleep = 95, 

        /// <summary>
        /// NumPad0
        /// </summary>
        NumPad0 = 96, 

        /// <summary>
        /// NumPad1
        /// </summary>
        NumPad1 = 97, 

        /// <summary>
        /// NumPad2
        /// </summary>
        NumPad2 = 98, 

        /// <summary>
        /// NumPad3
        /// </summary>
        NumPad3 = 99, 

        /// <summary>
        /// NumPad4
        /// </summary>
        NumPad4 = 100, 

        /// <summary>
        /// NumPad5
        /// </summary>
        NumPad5 = 101, 

        /// <summary>
        /// NumPad6
        /// </summary>
        NumPad6 = 102, 

        /// <summary>
        /// NumPad7
        /// </summary>
        NumPad7 = 103, 

        /// <summary>
        /// NumPad8
        /// </summary>
        NumPad8 = 104, 

        /// <summary>
        /// NumPad9
        /// </summary>
        NumPad9 = 105, 

        /// <summary>
        /// Multiply
        /// </summary>
        Multiply = 106, 

        /// <summary>
        /// Add
        /// </summary>
        Add = 107, 

        /// <summary>
        /// Separator
        /// </summary>
        Separator = 108, 

        /// <summary>
        /// Subtract
        /// </summary>
        Subtract = 109, 

        /// <summary>
        /// Decimal
        /// </summary>
        Decimal = 110, 

        /// <summary>
        /// Divide
        /// </summary>
        Divide = 111, 

        /// <summary>
        /// F1 Key
        /// </summary>
        F1 = 112, 

        /// <summary>
        /// F2 Key
        /// </summary>
        F2 = 113, 

        /// <summary>
        /// F3 Key
        /// </summary>
        F3 = 114, 

        /// <summary>
        /// F4 Key
        /// </summary>
        F4 = 115, 

        /// <summary>
        /// F5 Key
        /// </summary>
        F5 = 116, 

        /// <summary>
        /// F6 Key
        /// </summary>
        F6 = 117, 

        /// <summary>
        /// F7 Key
        /// </summary>
        F7 = 118, 

        /// <summary>
        /// F8 Key
        /// </summary>
        F8 = 119, 

        /// <summary>
        /// F9 Key
        /// </summary>
        F9 = 120, 

        /// <summary>
        /// F10 Key
        /// </summary>
        F10 = 121, 

        /// <summary>
        /// F11 Key
        /// </summary>
        F11 = 122, 

        /// <summary>
        /// F12 Key
        /// </summary>
        F12 = 123, 

        /// <summary>
        /// F13 Key
        /// </summary>
        F13 = 124, 

        /// <summary>
        /// F14 Key
        /// </summary>
        F14 = 125, 

        /// <summary>
        /// F15 Key
        /// </summary>
        F15 = 126, 

        /// <summary>
        /// F16 Key
        /// </summary>
        F16 = 127, 

        /// <summary>
        /// F17 Key
        /// </summary>
        F17 = 128, 

        /// <summary>
        /// F18 Key
        /// </summary>
        F18 = 129, 

        /// <summary>
        /// F19 Key
        /// </summary>
        F19 = 130, 

        /// <summary>
        /// F20 Key
        /// </summary>
        F20 = 131, 

        /// <summary>
        /// F21 Key
        /// </summary>
        F21 = 132, 

        /// <summary>
        /// F22 Key
        /// </summary>
        F22 = 133, 

        /// <summary>
        /// F23 Key
        /// </summary>
        F23 = 134, 

        /// <summary>
        /// F24 Key
        /// </summary>
        F24 = 135, 

        /// <summary>
        /// NumLock
        /// </summary>
        NumLock = 144, 

        /// <summary>
        /// The SCROLL LOCK key.
        /// </summary>
        Scroll = 145, 

        /// <summary>
        /// LeftShift
        /// </summary>
        LeftShift = 160, 

        /// <summary>
        /// RightShift
        /// </summary>
        RightShift = 161, 

        /// <summary>
        /// LeftCtrl
        /// </summary>
        LeftCtrl = 162, 

        /// <summary>
        /// RightCtrl
        /// </summary>
        RightCtrl = 163, 

        /// <summary>
        /// LeftAlt
        /// </summary>
        LeftAlt = 164, 

        /// <summary>
        /// RightAlt
        /// </summary>
        RightAlt = 165, 

        /// <summary>
        /// LaunchApplication1
        /// </summary>
        LaunchApplication1 = 182, 

        /// <summary>
        /// LaunchApplication2
        /// </summary>
        LaunchApplication2 = 183, 

        /// <summary>
        /// The Oem 1 key. ',:' for US
        /// </summary>
        Oem1 = 186, 

        /// <summary>
        /// The Oem Semicolon key.
        /// </summary>
        OemSemicolon = 186, 

        /// <summary>
        /// The Oem plus key. '+' any country
        /// </summary>
        OemPlus = 187, 

        /// <summary>
        ///  The Oem comma key. ',' any country
        /// </summary>
        OemComma = 188, 

        /// <summary>
        /// The Oem Minus key. '-' any country
        /// </summary>
        OemMinus = 189, 

        /// <summary>
        /// The Oem Period key. '.' any country
        /// </summary>
        OemPeriod = 190, 

        /// <summary>
        /// The Oem Question key.
        /// </summary>
        OemQuestion = 191, 

        /// <summary>
        /// The Oem 2 key. '/?' for US
        /// </summary>
        Oem2 = 191, 

        /// <summary>
        /// The Oem tilde key.
        /// </summary>
        OemTilde = 192, 

        /// <summary>
        /// The Oem 3 key. '`~' for US
        /// </summary>
        Oem3 = 192, 

        /// <summary>
        /// The ABNT_C1 (Brazilian) key.
        /// </summary>
        AbntC1 = 193, 

        /// <summary>
        /// The ABNT_C2 (Brazilian) key.
        /// </summary>
        AbntC2 = 194, 

        /// <summary>
        /// The Oem Open Brackets key.
        /// </summary>
        OemOpenBrackets = 219, 

        /// <summary>
        /// The Oem 4 key.
        /// </summary>
        Oem4 = 219, 

        /// <summary>
        /// The Oem 5 key.
        /// </summary>
        Oem5 = 220, 

        /// <summary>
        /// The Oem Pipe key.
        /// </summary>
        OemPipe = 220, 

        /// <summary>
        /// The Oem Close Brackets key.
        /// </summary>
        OemCloseBrackets = 221, 

        /// <summary>
        /// The Oem 6 key.
        /// </summary>
        Oem6 = 221, 

        /// <summary>
        /// The Oem Quotes key.
        /// </summary>
        OemQuotes = 222, 

        /// <summary>
        /// The Oem 7 key.
        /// </summary>
        Oem7 = 222, 

        /// <summary>
        /// The Oem8 key.
        /// </summary>
        Oem8 = 223, 

        /// <summary>
        /// The Oem Backslash key.
        /// </summary>
        OemBackslash = 226, 

        /// <summary>
        /// The Oem 102 key.
        /// </summary>
        Oem102 = 226, 

        /// <summary>
        /// A special key masking the real key being processed by an IME.
        /// </summary>
        ImeProcessed = 229, 

        /// <summary>
        /// A special key masking the real key being processed as a system key.
        /// </summary>
        System = 230, 

        /// <summary>
        /// The OEM_ATTN key.
        /// </summary>
        OemAttn = 240, 

        /// <summary>
        /// The DBE_ALPHANUMERIC key.
        /// </summary>
        DbeAlphanumeric = 240, 

        /// <summary>
        /// The DBE_KATAKANA key.
        /// </summary>
        DbeKatakana = 241, 

        /// <summary>
        /// The OEM_FINISH key.
        /// </summary>
        OemFinish = 241, 

        /// <summary>
        /// The DBE_HIRAGANA key.
        /// </summary>
        DbeHiragana = 242, 

        /// <summary>
        /// The OEM_COPY key.
        /// </summary>
        OemCopy = 242, 

        /// <summary>
        /// The DBE_SBCSCHAR key.
        /// </summary>
        DbeSbcsChar = 243, 

        /// <summary>
        /// The OEM_AUTO key.
        /// </summary>
        OemAuto = 243, 

        /// <summary>
        /// The DBE_DBCSCHAR key.
        /// </summary>
        DbeDbcsChar = 244, 

        /// <summary>
        /// The OEM_ENLW key.
        /// </summary>
        OemEnlw = 244, 

        /// <summary>
        /// The DBE_ROMAN key.
        /// </summary>
        DbeRoman = 245, 

        /// <summary>
        /// The OEM_BACKTAB key.
        /// </summary>
        OemBackTab = 245, 

        /// <summary>
        /// Zoom
        /// </summary>
        Zoom = 251, 

        /// <summary>
        /// DbeEnterDialogConversionMode
        /// </summary>
        DbeEnterDialogConversionMode = 253, 

        /// <summary>
        /// Pa1
        /// </summary>
        Pa1 = 253, 

        /// <summary>
        /// OemClear
        /// </summary>
        OemClear = 254, 
    }

    /// <summary>
    /// The Keyboard class
    /// </summary>    
    public class Keyboard
    {
        /// <summary>
        /// The Press method
        /// </summary>
        /// <param name="keys">
        /// The keys parameter
        /// </param>
        /// <param name="releaseAfter">
        /// The releaseAfter parameter
        /// </param>
        public static void Press(string keys, bool releaseAfter = true)
        {
            keys = keys.ToLower().Replace(" ", string.Empty);

            var key = keys.Split('+');

            foreach (var k in key)
            {
                if (new Regex(@"f\d").IsMatch(k))
                {
                    HitFunctionKey(k);
                }
                else if (new Regex(@"[\w+]{2,}").IsMatch(k))
                {
                    switch (k.ToLower())
                    {
                        case "alt":
                        case "ctrl":
                        case "shift":
                        case "insert":
                        case ".":
                            PressKey(k);
                            break;
                        case "tab":
                            HitTab();
                            break;
                        case "enter":
                            HitEnterKey();
                            break;
                        case "backspace":
                            HitBackspaceKey();
                            break;
                        case "shiftenter":
                            HitShiftEnter();
                            break;
                        case "shiftenterdelayedrelease":
                            HitShiftEnter(false);
                            break;
                        case "right":
                        case "left":
                        case "down":
                        case "up":
                        case "home":
                        case "end":
                            HitArrowKey(k);
                            break;
                        case "pageup":
                            PageUp();
                            break;
                        case "pagedown":
                            PageDown();
                            break;
                        case "space":
                            Space();
                            break;
                        case "esc":
                        case "escape":
                            HitESCKey();
                            break;
                        case "tilde":
                            HitTildeKey();
                            break;
                        case "delete":
                            Delete();
                            break;
                        case "zoom in":
                        case "zoomin":
                            PressKey("ctrl");
                            PressKey("+");
                            break;
                        case "zoom out":
                        case "zoomout":
                            PressKey("ctrl");
                            PressKey("-");
                            break;
                        case "nextdocument":
                            PressKey("ctrl");
                            HitAlphaKey("M");
                            break;
                        case "prevdocument":
                            PressKey("ctrl");
                            HitAlphaKey("U");
                            break;
                        case "nextpage":
                            PressKey("shift");
                            PressKey("ctrl");
                            HitAlphaKey("k");
                            break;
                        case "prevpage":
                            PressKey("shift");
                            PressKey("ctrl");
                            HitAlphaKey("j");
                            break;
                        case "plus":
                            PressKey("+");
                            break;
                        case "minus":
                            PressKey("-");
                            break;
                    }
                }
                else if (new Regex(@"^[a-z]$").IsMatch(k))
                {
                    HitAlphaKey(k);
                }
                else if (new Regex(@"\d").IsMatch(k))
                {
                    HitDigitKey(k);
                }
                else if (new Regex(@"\W").IsMatch(k))
                {
                    PressKey(k);
                }

                Thread.Sleep(200);
            }

            if (releaseAfter)
            {
                ReleaseKeyboard();
            }
        }

        /// <summary>
        /// The Press method
        /// </summary>
        /// <param name="key">
        /// The key parameter
        /// </param>
        public static void Press(Key key)
        {
            InputLib.Keyboard.Press((InputLib.Key)key);
        }

        /// <summary>
        /// The Release method
        /// </summary>
        /// <param name="key">
        /// The key parameter
        /// </param>
        public static void Release(Key key)
        {
            InputLib.Keyboard.Release((InputLib.Key)key);
        }

        /// <summary>
        /// The PressKey method
        /// </summary>
        /// <param name="keyName">
        /// The keyName parameter
        /// </param>
        public static void PressKey(string keyName)
        {
            switch (keyName.ToLower())
            {
                case "ctrl":
                case "shift":
                case "alt":
                case "insert":
                    Press((Key)Enum.Parse(typeof(Key), keyName, true));
                    break;
                case "*":
                    Press(Key.Multiply);
                    break;
                case "+":
                    Press(Key.Add);
                    break;
                case "-":
                    Press(Key.Subtract);
                    break;
                case ",":
                    Press(Key.OemComma);
                    break;
                case ".":
                    Press(Key.OemPeriod);
                    break;
                case "~":
                    Press(Key.OemTilde);
                    break;
                default:
                    throw new Exception("Not implemented");
            }
        }

        /// <summary>
        /// The HitDownKey method
        /// </summary>        
        public static void HitDownKey()
        {
            Hit(Key.Down);
        }

        /// <summary>
        /// The HitAltPlusKey method
        /// </summary>
        /// <param name="key">
        /// The key parameter
        /// </param>
        public static void HitAltPlusKey(Key key)
        {
            Press(Key.LeftAlt);
            Press(key);
            Release(Key.LeftAlt);
        }

        /// <summary>
        /// Releases the keyboard to a clean state
        /// </summary>
        public static void ReleaseKeyboard()
        {
            InputLib.Keyboard.Reset();
        }

        /// <summary>
        /// Sending an Enter key
        /// </summary>
        public static void HitEnterKey()
        {
            Hit(Key.Enter);
        }

        /// <summary>
        /// Sending an Backspace key
        /// </summary>
        public static void HitBackspaceKey()
        {
            Hit(Key.Back);
        }

        /// <summary>
        /// The HitArrowKey method
        /// </summary>
        /// <param name="arrow">
        /// The arrow parameter
        /// </param>
        public static void HitArrowKey(string arrow)
        {
            Hit((Key)Enum.Parse(typeof(Key), arrow, true));
        }

        /// <summary>
        /// Sending an tab key
        /// </summary>
        public static void HitTab()
        {
            Hit(Key.Tab);
        }

        /// <summary>
        /// Sending cute command
        /// </summary>
        public static void Cut()
        {
            Press(Key.Ctrl);
            Hit(Key.X);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Sending paste command
        /// </summary>
        public static void Paste()
        {
            Press(Key.Ctrl);
            Hit(Key.V);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Sending a CTRL + Delete
        /// </summary>
        public static void HitCtrlDelete()
        {
            Press(Key.Ctrl);
            Hit(Key.Delete);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Enters the keys.
        /// </summary>
        /// <param name="keys">
        /// The keys.
        /// </param>
        public static void EnterKeys(params Key[] keys)
        {
            foreach (Key key in keys)
            {
                Hit(key);
            }
        }

        /// <summary>
        /// Sending CTRL + Insert
        /// </summary>
        public static void HitCtrlInsert()
        {
            Press(Key.Ctrl);
            Hit(Key.Insert);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Sending CTRL + Shift + Delete
        /// </summary>
        public static void HitCtrlShiftDelete()
        {
            Press(Key.Ctrl);
            Press(Key.Shift);
            Press(Key.Delete);
            Release(Key.Delete);
            Release(Key.Shift);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Close Window
        /// </summary>
        public static void CloseWindow()
        {
            HitAltFunctionKey("F4");
        }

        /// <summary>
        /// Highlight a specific text
        /// </summary>
        /// <param name="strText">
        /// The strText parameter
        /// </param>
        /// <param name="startIndex">
        /// The startIndex parameter
        /// </param>
        /// <param name="endIndex">
        /// The endIndex parameter
        /// </param>
        /// <returns>
        /// The bool type object
        /// </returns>
        public static bool HighlightText(string strText, int startIndex, int endIndex)
        {
            int intTextLength = strText.Length;

            if (endIndex > intTextLength)
            {
                return false;
            }

            Press(Key.Home);

            for (int i = 0; i < intTextLength; i++)
            {
                if (i == startIndex)
                {
                    Press(Key.Shift);
                    for (int y = startIndex; y < endIndex; y++)
                    {
                        Press(Key.Right);
                    }

                    Release(Key.Shift);
                    i = endIndex;
                }

                if (i < intTextLength)
                {
                    Press(Key.Right);
                }
            }

            return true;
        }

        /// <summary>
        /// Sending an Esc
        /// </summary>
        public static void HitESCKey()
        {
            Hit(Key.Escape);
        }

        /// <summary>
        /// Sending a Delete
        /// </summary>
        public static void Delete()
        {
            Hit(Key.Delete);
        }

        /// <summary>
        /// Sending a Page Up
        /// </summary>
        public static void PageUp()
        {
            Hit(Key.PageUp);
        }

        /// <summary>
        /// Sending a Page Down
        /// </summary>
        public static void PageDown()
        {
            Hit(Key.PageDown);
        }

        /// <summary>
        /// Sending CTRL + C
        /// </summary>
        public static void Copy()
        {
            Press(Key.Ctrl);
            Hit(Key.C);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Sending CTRL + Z
        /// </summary>
        public static void Undo()
        {
            Press(Key.Ctrl);
            Hit(Key.Z);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Sending CTRL + Z
        /// </summary>
        public static void Redo()
        {
            Press(Key.Ctrl);
            Hit(Key.Y);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Sending Space
        /// </summary>
        public static void Space()
        {
            Hit(Key.Space);
        }

        /// <summary>
        /// Sending ctrl+Key command
        /// </summary>
        /// <param name="key">
        /// The key parameter
        /// </param>
        public static void HitCtrlPlusKey(Key key)
        {
            Press(Key.Ctrl);
            Hit(key);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Sending ctrl-tab command
        /// </summary>
        public static void HitCtrlTab()
        {
            Press(Key.Ctrl);
            Hit(Key.Tab);
            Release(Key.Ctrl);
        }
        
        /// <summary>
        /// Sending ctrl-shift-tab command
        /// </summary>
        public static void HitCtrlShiftTab()
        {
            Press(Key.Ctrl);
            Press(Key.Shift);
            Hit(Key.Tab);
            Release(Key.Shift);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Sending shift-enter command
        /// </summary>
        /// <param name="release">
        /// The release parameter
        /// </param>
        public static void HitShiftEnter(bool release = true)
        {
            Press(Key.Shift);
            Press(Key.Enter);

            // Fix for @TestID_45251_45252
            if (release)
            {
                Release(Key.Shift);
            }
        }

        /// <summary>
        /// Sending shift-tab command
        /// </summary>
        public static void HitShiftTab()
        {
            Press(Key.Shift);
            Hit(Key.Tab);
            Release(Key.Shift);
        }

        /// <summary>
        /// The Type method
        /// </summary>
        /// <param name="keys">
        /// The keys parameter
        /// </param>
        public static void Type(string keys)
        {
            // Much faster than Keyboard.Type(keys);

            /* From: https://msdn.microsoft.com/ru-ru/library/system.windows.forms.sendkeys(v=vs.110).aspx
             * The plus sign (+), caret (^), percent sign (%), tilde (~), and parentheses () have special meanings to SendKeys. 
             * To specify one of these characters, enclose it within braces ({}). For example, to specify the plus sign, use "{+}". 
             * To specify brace characters, use "{{}" and "{}}". 
             * Brackets ([ ]) have no special meaning to SendKeys, but you must enclose them in braces.
             */

            SendKeys.SendWait(keys);
        }

        /// <summary>
        /// The Type method
        /// </summary>
        /// <param name="keys">
        /// The keys parameter
        /// </param>
        public static void TypeSlowly(string keys)
        {
            InputLib.Keyboard.Type(keys);
        }

        /// <summary>
        /// Sending Alt command
        /// </summary>
        public static void HitAltKey()
        {
            Hit(Key.Alt);
        }

        /// <summary>
        /// Sending specified alphabet key
        /// </summary>
        /// <param name="strAlphaKey">
        /// The strAlphaKey parameter
        /// </param>
        public static void HitAlphaKey(string strAlphaKey)
        {
            Hit((Key)Enum.Parse(typeof(Key), strAlphaKey, true));
        }

        /// <summary>
        /// Sending a control alphabet key
        /// </summary>
        /// <param name="strAlphaKey">
        /// The strAlphaKey parameter
        /// </param>
        public static void HitControlAlphaKey(string strAlphaKey)
        {
            Press(Key.Ctrl);
            HitAlphaKey(strAlphaKey);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Sending Ctrl-Function key
        /// </summary>
        /// <param name="strFunctionKey">
        /// The strFunctionKey parameter
        /// </param>
        public static void HitCtrlFunctionKey(string strFunctionKey)
        {
            Press(Key.Ctrl);
            HitFunctionKey(strFunctionKey);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Sending specified function key
        /// </summary>
        /// <param name="strFunctionKey">
        /// The strFunctionKey parameter
        /// </param>
        public static void HitFunctionKey(string strFunctionKey)
        {
            Hit((Key)Enum.Parse(typeof(Key), strFunctionKey, true));
        }

        /// <summary>
        /// Sending alt function key
        /// </summary>
        /// <param name="strFunctionKey">
        /// The strFunctionKey parameter
        /// </param>
        public static void HitAltFunctionKey(string strFunctionKey)
        {
            Press(Key.Alt);
            HitFunctionKey(strFunctionKey);
            Release(Key.Alt);
        }

        /// <summary>
        /// Sending control digit key
        /// </summary>
        /// <param name="strDigitKey">
        /// The strDigitKey parameter
        /// </param>
        public static void HitControlDigitKey(string strDigitKey)
        {
            Press(Key.Ctrl);
            HitDigitKey(strDigitKey);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// Sending digit key
        /// </summary>
        /// <param name="strDigitKey">
        /// The strDigitKey parameter
        /// </param>
        public static void HitDigitKey(string strDigitKey)
        {
            switch (strDigitKey)
            {
                case "1":
                    Press(Key.D1);
                    break;
                case "2":
                    Press(Key.D2);
                    break;
                case "3":
                    Press(Key.D3);
                    break;
                case "4":
                    Press(Key.D4);
                    break;
                case "5":
                    Press(Key.D5);
                    break;
                case "6":
                    Press(Key.D6);
                    break;
                case "7":
                    Press(Key.D7);
                    break;
                case "8":
                    Press(Key.D8);
                    break;
                case "9":
                    Press(Key.D9);
                    break;
                default:
                    Press(Key.D0);
                    break;
            }
        }

        /// <summary>
        /// Sending a tilde key
        /// </summary>
        public static void HitTildeKey()
        {
            Hit(Key.OemTilde);
        }

        /// <summary>
        /// Sending select all text command
        /// </summary>
        public static void SelectAllText()
        {
            Press(Key.Ctrl);
            Hit(Key.A);
            Release(Key.Ctrl);
        }

        /// <summary>
        /// The Hit method
        /// </summary>
        /// <param name="key">
        /// The key parameter
        /// </param>
        public static void Hit(Key key)
        {
            Press(key);
            Release(key);
        }
    }
}
