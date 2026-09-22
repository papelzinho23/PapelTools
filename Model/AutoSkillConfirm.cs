using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Newtonsoft.Json;
using _4RTools.Utils;

namespace _4RTools.Model
{
    /// <summary>
    /// Some skills arm on key press and only fire once you click the target
    /// (the game swaps the mouse cursor to a "confirm" icon while armed).
    /// This watches for that cursor and auto-fires the confirm click, so the
    /// player only has to press the skill key.
    /// </summary>
    public class AutoSkillConfirm : Action
    {
        private const string ACTION_NAME = "AutoSkillConfirm";
        private _4RThread thread;

        /// <summary>Cursor handles captured while the "confirm skill target" cursor is showing.</summary>
        public List<long> TargetCursorHandles { get; set; } = new List<long>();
        public int CooldownMs { get; set; } = 150;
        public bool RequireForeground { get; set; } = true;

        [JsonIgnore]
        private bool wasMatching = false;
        [JsonIgnore]
        private DateTime lastClickAt = DateTime.MinValue;

        public void Start()
        {
            Stop();
            Client roClient = ClientSingleton.GetClient();
            if (roClient != null)
            {
                this.wasMatching = false;
                this.thread = new _4RThread(_ => Execute(roClient));
                _4RThread.Start(this.thread);
            }
        }

        private int Execute(Client roClient)
        {
            IntPtr current = CursorUtils.GetCurrentCursorHandle();
            bool isMatching = current != IntPtr.Zero && TargetCursorHandles.Contains(current.ToInt64());
            bool foregroundOk = !RequireForeground || Interop.GetForegroundWindow() == roClient.process.MainWindowHandle;

            // Edge-triggered: fire once when the cursor switches TO the confirm icon,
            // not on every poll while it stays that way.
            if (isMatching && !wasMatching && foregroundOk &&
                (DateTime.Now - lastClickAt).TotalMilliseconds >= CooldownMs)
            {
                Point p = CursorUtils.GetCursorClientPosition(roClient.process.MainWindowHandle);
                int lParam = (p.Y << 16) | (p.X & 0xFFFF);
                Interop.PostMessageCoord(roClient.process.MainWindowHandle, Constants.WM_LBUTTONDOWN, 1, lParam);
                Thread.Sleep(1);
                Interop.PostMessageCoord(roClient.process.MainWindowHandle, Constants.WM_LBUTTONUP, 0, lParam);
                lastClickAt = DateTime.Now;
            }

            wasMatching = isMatching;
            Thread.Sleep(20);
            return 0;
        }

        public void Stop()
        {
            _4RThread.Stop(this.thread);
        }

        public string GetConfiguration()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string GetActionName()
        {
            return ACTION_NAME;
        }
    }
}
