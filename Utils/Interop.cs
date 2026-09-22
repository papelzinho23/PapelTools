using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace _4RTools.Utils
{
    internal class Interop
    {
        // PINVOKES
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, int Msg, Keys wParam, int lParam);

        // Distinct name (same entry point) so mouse-message calls with an int wParam
        // don't collide with the Keys overload above.
        [DllImport("user32.dll", SetLastError = true, EntryPoint = "PostMessage")]
        public static extern bool PostMessageCoord(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
    }
}
