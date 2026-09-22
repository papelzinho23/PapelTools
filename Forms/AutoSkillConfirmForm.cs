using System;
using System.Windows.Forms;
using _4RTools.Utils;
using _4RTools.Model;

namespace _4RTools.Forms
{
    public partial class AutoSkillConfirmForm : Form, IObserver
    {
        private Timer countdownTimer;
        private int countdownTicks;

        public AutoSkillConfirmForm(Subject subject)
        {
            InitializeComponent();
            subject.Attach(this);

            this.btnCapture.Click += new EventHandler(this.btnCapture_Click);
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.nudCooldown.ValueChanged += new EventHandler(this.nudCooldown_ValueChanged);
            this.chkForeground.CheckedChanged += new EventHandler(this.chkForeground_CheckedChanged);

            this.countdownTimer = new Timer { Interval = 1000 };
            this.countdownTimer.Tick += new EventHandler(this.countdownTimer_Tick);
        }

        public void Update(ISubject subject)
        {
            switch ((subject as Subject).Message.code)
            {
                case MessageCode.PROFILE_CHANGED:
                    RefreshFromProfile();
                    break;
                case MessageCode.TURN_ON:
                    ProfileSingleton.GetCurrent().AutoSkillConfirm.Start();
                    break;
                case MessageCode.TURN_OFF:
                    ProfileSingleton.GetCurrent().AutoSkillConfirm.Stop();
                    break;
            }
        }

        private void RefreshFromProfile()
        {
            AutoSkillConfirm model = ProfileSingleton.GetCurrent().AutoSkillConfirm;
            this.lblSampleCount.Text = $"Amostras capturadas: {model.TargetCursorHandles.Count}";
            this.nudCooldown.Value = Math.Min(Math.Max(model.CooldownMs, (int)this.nudCooldown.Minimum), (int)this.nudCooldown.Maximum);
            this.chkForeground.Checked = model.RequireForeground;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            this.btnCapture.Enabled = false;
            this.countdownTicks = 3;
            this.lblStatus.Text = $"Capturando em {countdownTicks}... (aperte a magia no jogo agora)";
            this.countdownTimer.Start();
        }

        private void countdownTimer_Tick(object sender, EventArgs e)
        {
            this.countdownTicks--;
            if (this.countdownTicks > 0)
            {
                this.lblStatus.Text = $"Capturando em {countdownTicks}...";
                return;
            }

            this.countdownTimer.Stop();
            this.btnCapture.Enabled = true;

            IntPtr handle = CursorUtils.GetCurrentCursorHandle();
            AutoSkillConfirm model = ProfileSingleton.GetCurrent().AutoSkillConfirm;

            if (handle == IntPtr.Zero)
            {
                this.lblStatus.Text = "Não consegui ler o cursor. Tente de novo.";
                return;
            }

            long value = handle.ToInt64();
            if (!model.TargetCursorHandles.Contains(value))
            {
                model.TargetCursorHandles.Add(value);
                ProfileSingleton.SetConfiguration(model);
            }

            this.lblSampleCount.Text = $"Amostras capturadas: {model.TargetCursorHandles.Count}";
            this.lblStatus.Text = "Capturado! Repita 2-3x segurando o cursor sobre o alvo pra pegar variações.";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AutoSkillConfirm model = ProfileSingleton.GetCurrent().AutoSkillConfirm;
            model.TargetCursorHandles.Clear();
            ProfileSingleton.SetConfiguration(model);
            this.lblSampleCount.Text = "Amostras capturadas: 0";
            this.lblStatus.Text = "Amostras limpas.";
        }

        private void nudCooldown_ValueChanged(object sender, EventArgs e)
        {
            AutoSkillConfirm model = ProfileSingleton.GetCurrent().AutoSkillConfirm;
            model.CooldownMs = (int)this.nudCooldown.Value;
            ProfileSingleton.SetConfiguration(model);
        }

        private void chkForeground_CheckedChanged(object sender, EventArgs e)
        {
            AutoSkillConfirm model = ProfileSingleton.GetCurrent().AutoSkillConfirm;
            model.RequireForeground = this.chkForeground.Checked;
            ProfileSingleton.SetConfiguration(model);
        }
    }
}
