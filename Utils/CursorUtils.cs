using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace _4RTools.Utils
{
    /// <summary>
    /// Reads the current Windows cursor. RO clients swap the real system cursor
    /// (not a custom-drawn sprite) when a skill is armed and waiting for the
    /// confirm click, so its handle can be captured once and compared later.
    /// </summary>
    internal static class CursorUtils
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hCursor;
            public POINT ptScreenPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        private const int CURSOR_SHOWING = 0x00000001;

        [DllImport("user32.dll")]
        private static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll")]
        private static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

        /// <summary>Current system cursor handle, or IntPtr.Zero if hidden/unavailable.</summary>
        public static IntPtr GetCurrentCursorHandle()
        {
            CURSORINFO ci = new CURSORINFO { cbSize = Marshal.SizeOf(typeof(CURSORINFO)) };
            if (!GetCursorInfo(out ci)) return IntPtr.Zero;
            if ((ci.flags & CURSOR_SHOWING) == 0) return IntPtr.Zero;
            return ci.hCursor;
        }

        /// <summary>Current cursor position translated to the client coordinates of the given window.</summary>
        public static Point GetCursorClientPosition(IntPtr hWnd)
        {
            CURSORINFO ci = new CURSORINFO { cbSize = Marshal.SizeOf(typeof(CURSORINFO)) };
            GetCursorInfo(out ci);
            POINT p = ci.ptScreenPos;
            ScreenToClient(hWnd, ref p);
            return new Point(p.X, p.Y);
        }
    }
}
