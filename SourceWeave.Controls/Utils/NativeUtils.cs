using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace SourceWeave.Controls.Utils
{
    internal static class NativeUtils
    {
        internal const uint TPM_LEFTALIGN = 0;
        internal const  uint TPM_RETURNCMD = 256;

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        internal static extern IntPtr PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        internal static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        internal static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);
    }

    [Serializable]
    internal struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
        public int Height
        {
            get
            {
                return Bottom - Top;
            }
            set
            {
                Bottom = Top + value;
            }
        }

        public Point Position
        {
            get
            {
                return new Point(Left, Top);
            }
        }

        public Size Size
        {
            get
            {
                return new Size(Width, Height);
            }
        }

        public int Width
        {
            get
            {
                return Right - Left;
            }
            set
            {
                Right = Left + value;
            }
        }

        public Rect(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public Rect(Rect rect)
        {
            Left = rect.Left;
            Top = rect.Top;
            Right = rect.Right;
            Bottom = rect.Bottom;
        }

        public void Offset(int dx, int dy)
        {
            Left += dx;
            Right += dx;
            Top += dy;
            Bottom += dy;
        }

        public Int32Rect ToInt32Rect()
        {
            return new Int32Rect(Left, Top, Width, Height);
        }
    }
}
