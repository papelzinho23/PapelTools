namespace _4RTools.Forms
{
    partial class AutoSkillConfirmForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblIntro = new System.Windows.Forms.Label();
            this.btnCapture = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSampleCount = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblCooldown = new System.Windows.Forms.Label();
            this.nudCooldown = new System.Windows.Forms.NumericUpDown();
            this.lblCooldownMs = new System.Windows.Forms.Label();
            this.chkForeground = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudCooldown)).BeginInit();
            this.SuspendLayout();
            //
            // lblIntro
            //
            this.lblIntro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblIntro.Location = new System.Drawing.Point(10, 8);
            this.lblIntro.Name = "lblIntro";
            this.lblIntro.Size = new System.Drawing.Size(500, 40);
            this.lblIntro.TabIndex = 0;
            this.lblIntro.Text = "Aperte a magia no jogo (o cursor vira o ícone de mira) e clique em \"Capturar\" antes " +
                "dele voltar ao normal. Repita 2-3x pra pegar variações do ícone.";
            //
            // btnCapture
            //
            this.btnCapture.Location = new System.Drawing.Point(10, 52);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(190, 28);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "Capturar cursor de mira";
            this.btnCapture.UseVisualStyleBackColor = true;
            //
            // btnClear
            //
            this.btnClear.Location = new System.Drawing.Point(210, 52);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 28);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Limpar amostras";
            this.btnClear.UseVisualStyleBackColor = true;
            //
            // lblStatus
            //
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblStatus.Location = new System.Drawing.Point(10, 86);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(500, 20);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "";
            //
            // lblSampleCount
            //
            this.lblSampleCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSampleCount.Location = new System.Drawing.Point(10, 110);
            this.lblSampleCount.Name = "lblSampleCount";
            this.lblSampleCount.Size = new System.Drawing.Size(300, 18);
            this.lblSampleCount.TabIndex = 4;
            this.lblSampleCount.Text = "Amostras capturadas: 0";
            //
            // lblCooldown
            //
            this.lblCooldown.AutoSize = true;
            this.lblCooldown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblCooldown.Location = new System.Drawing.Point(10, 148);
            this.lblCooldown.Name = "lblCooldown";
            this.lblCooldown.Text = "Cooldown entre cliques";
            //
            // nudCooldown
            //
            this.nudCooldown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.nudCooldown.Location = new System.Drawing.Point(150, 145);
            this.nudCooldown.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            this.nudCooldown.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudCooldown.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            this.nudCooldown.Name = "nudCooldown";
            this.nudCooldown.Size = new System.Drawing.Size(70, 22);
            this.nudCooldown.TabIndex = 5;
            this.nudCooldown.Value = new decimal(new int[] { 150, 0, 0, 0 });
            //
            // lblCooldownMs
            //
            this.lblCooldownMs.AutoSize = true;
            this.lblCooldownMs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblCooldownMs.Location = new System.Drawing.Point(225, 148);
            this.lblCooldownMs.Name = "lblCooldownMs";
            this.lblCooldownMs.Text = "ms";
            //
            // chkForeground
            //
            this.chkForeground.AutoSize = true;
            this.chkForeground.Checked = true;
            this.chkForeground.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkForeground.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.chkForeground.Location = new System.Drawing.Point(10, 180);
            this.chkForeground.Name = "chkForeground";
            this.chkForeground.Text = "Somente quando o jogo estiver em primeiro plano";
            this.chkForeground.UseVisualStyleBackColor = true;
            //
            // AutoSkillConfirmForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(560, 274);
            this.Controls.Add(this.chkForeground);
            this.Controls.Add(this.lblCooldownMs);
            this.Controls.Add(this.nudCooldown);
            this.Controls.Add(this.lblCooldown);
            this.Controls.Add(this.lblSampleCount);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.lblIntro);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AutoSkillConfirmForm";
            this.Text = "AutoSkillConfirmForm";
            ((System.ComponentModel.ISupportInitialize)(this.nudCooldown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblIntro;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSampleCount;
        private System.Windows.Forms.Label lblCooldown;
        private System.Windows.Forms.NumericUpDown nudCooldown;
        private System.Windows.Forms.Label lblCooldownMs;
        private System.Windows.Forms.CheckBox chkForeground;
    }
}
