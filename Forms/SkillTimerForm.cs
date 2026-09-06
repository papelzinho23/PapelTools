using System;
using System.Windows.Forms;
using _4RTools.Utils;
using _4RTools.Model;
using System.Windows.Input;

namespace _4RTools.Forms
{
    public partial class SkillTimerForm : Form, IObserver
    {
        private NumericUpDown[] delayInputs;
        private TextBox[] keyInputs;

        public SkillTimerForm(Subject subject)
        {
            InitializeComponent();
            subject.Attach(this);

            this.delayInputs = new[]
            {
                txtAutoRefreshDelay, txtAutoRefreshDelay2, txtAutoRefreshDelay3,
                txtAutoRefreshDelay4, txtAutoRefreshDelay5
            };
            this.keyInputs = new[]
            {
                txtSkillTimerKey, txtSkillTimerKey2, txtSkillTimerKey3,
                txtSkillTimerKey4, txtSkillTimerKey5
            };

            for (int i = 0; i < keyInputs.Length; i++)
            {
                int slot = i;
                keyInputs[i].KeyDown += new System.Windows.Forms.KeyEventHandler(FormUtils.OnKeyDown);
                keyInputs[i].KeyPress += new System.Windows.Forms.KeyPressEventHandler(FormUtils.OnKeyPress);
                keyInputs[i].TextChanged += (s, e) => OnKeyChange(slot);
                delayInputs[i].ValueChanged += (s, e) => OnDelayChange(slot);
            }
        }

        private static AutoRefreshSpammer Spammer(int slot)
        {
            Profile p = ProfileSingleton.GetCurrent();
            switch (slot)
            {
                case 0: return p.AutoRefreshSpammer1;
                case 1: return p.AutoRefreshSpammer2;
                case 2: return p.AutoRefreshSpammer3;
                case 3: return p.AutoRefreshSpammer4;
                default: return p.AutoRefreshSpammer5;
            }
        }

        public void Update(ISubject subject)
        {
            switch ((subject as Subject).Message.code)
            {
                case MessageCode.PROFILE_CHANGED:
                    FormUtils.ResetForm(this);
                    for (int i = 0; i < keyInputs.Length; i++)
                    {
                        AutoRefreshSpammer s = Spammer(i);
                        keyInputs[i].Text = s.RefreshKey.ToString();
                        delayInputs[i].Value = ClampToInput(delayInputs[i], (decimal)s.RefreshDelay);
                    }
                    break;
                case MessageCode.TURN_ON:
                    for (int i = 0; i < keyInputs.Length; i++) Spammer(i).Start();
                    break;
                case MessageCode.TURN_OFF:
                    for (int i = 0; i < keyInputs.Length; i++) Spammer(i).Stop();
                    break;
            }
        }

        private static decimal ClampToInput(NumericUpDown input, decimal value)
        {
            if (value < input.Minimum) return input.Minimum;
            if (value > input.Maximum) return input.Maximum;
            return value;
        }

        private void OnKeyChange(int slot)
        {
            try
            {
                Key key = (Key)Enum.Parse(typeof(Key), keyInputs[slot].Text);
                AutoRefreshSpammer spammer = Spammer(slot);
                spammer.RefreshKey = key;
                ProfileSingleton.SetConfiguration(spammer);
            }
            catch { /* partial / invalid key text while typing */ }
        }

        private void OnDelayChange(int slot)
        {
            try
            {
                AutoRefreshSpammer spammer = Spammer(slot);
                spammer.RefreshDelay = (double)delayInputs[slot].Value;
                ProfileSingleton.SetConfiguration(spammer);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[SkillTimer] {ex.Message}", "PapelTools", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
