namespace _4RTools.Forms
{
    partial class SkillTimerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();

            this.txtAutoRefreshDelay = new System.Windows.Forms.NumericUpDown();
            this.txtAutoRefreshDelay2 = new System.Windows.Forms.NumericUpDown();
            this.txtAutoRefreshDelay3 = new System.Windows.Forms.NumericUpDown();
            this.txtAutoRefreshDelay4 = new System.Windows.Forms.NumericUpDown();
            this.txtAutoRefreshDelay5 = new System.Windows.Forms.NumericUpDown();

            this.txtSkillTimerKey = new System.Windows.Forms.TextBox();
            this.txtSkillTimerKey2 = new System.Windows.Forms.TextBox();
            this.txtSkillTimerKey3 = new System.Windows.Forms.TextBox();
            this.txtSkillTimerKey4 = new System.Windows.Forms.TextBox();
            this.txtSkillTimerKey5 = new System.Windows.Forms.TextBox();

            this.lblDelay1 = new System.Windows.Forms.Label();
            this.lblDelay2 = new System.Windows.Forms.Label();
            this.lblDelay3 = new System.Windows.Forms.Label();
            this.lblDelay4 = new System.Windows.Forms.Label();
            this.lblDelay5 = new System.Windows.Forms.Label();

            this.lblKey1 = new System.Windows.Forms.Label();
            this.lblKey2 = new System.Windows.Forms.Label();
            this.lblKey3 = new System.Windows.Forms.Label();
            this.lblKey4 = new System.Windows.Forms.Label();
            this.lblKey5 = new System.Windows.Forms.Label();

            this.lblSec1 = new System.Windows.Forms.Label();
            this.lblSec2 = new System.Windows.Forms.Label();
            this.lblSec3 = new System.Windows.Forms.Label();
            this.lblSec4 = new System.Windows.Forms.Label();
            this.lblSec5 = new System.Windows.Forms.Label();

            this.lblHint = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.txtAutoRefreshDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAutoRefreshDelay2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAutoRefreshDelay3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAutoRefreshDelay4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAutoRefreshDelay5)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();

            // stacked one below another
            this.BuildSlot(this.groupBox1, 2,   "Skill timer 1", this.lblDelay1, this.txtAutoRefreshDelay,  this.lblSec1, this.lblKey1, this.txtSkillTimerKey);
            this.BuildSlot(this.groupBox2, 47,  "Skill timer 2", this.lblDelay2, this.txtAutoRefreshDelay2, this.lblSec2, this.lblKey2, this.txtSkillTimerKey2);
            this.BuildSlot(this.groupBox3, 92,  "Skill timer 3", this.lblDelay3, this.txtAutoRefreshDelay3, this.lblSec3, this.lblKey3, this.txtSkillTimerKey3);
            this.BuildSlot(this.groupBox4, 137, "Skill timer 4", this.lblDelay4, this.txtAutoRefreshDelay4, this.lblSec4, this.lblKey4, this.txtSkillTimerKey4);
            this.BuildSlot(this.groupBox5, 182, "Skill timer 5", this.lblDelay5, this.txtAutoRefreshDelay5, this.lblSec5, this.lblKey5, this.txtSkillTimerKey5);

            //
            // lblHint
            //
            this.lblHint.AutoSize = true;
            this.lblHint.ForeColor = System.Drawing.Color.DimGray;
            this.lblHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.lblHint.Location = new System.Drawing.Point(9, 230);
            this.lblHint.MaximumSize = new System.Drawing.Size(300, 0);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(10, 13);
            this.lblHint.TabIndex = 50;
            this.lblHint.Text = "Delay em segundos - aceita frações (use as setas ou digite; ex.: 0,5 / 0,3). Clique no campo Key e pressione a tecla.";
            //
            // SkillTimerForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(320, 274);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SkillTimerForm";
            this.Text = "SkillTimerForm";

            ((System.ComponentModel.ISupportInitialize)(this.txtAutoRefreshDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAutoRefreshDelay2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAutoRefreshDelay3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAutoRefreshDelay4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAutoRefreshDelay5)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BuildSlot(System.Windows.Forms.GroupBox box, int y, string title,
            System.Windows.Forms.Label lblDelay, System.Windows.Forms.NumericUpDown delay,
            System.Windows.Forms.Label lblSec, System.Windows.Forms.Label lblKey,
            System.Windows.Forms.TextBox key)
        {
            box.Location = new System.Drawing.Point(8, y);
            box.Size = new System.Drawing.Size(300, 44);
            box.TabStop = false;
            box.Text = title;

            lblDelay.AutoSize = true;
            lblDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            lblDelay.Location = new System.Drawing.Point(8, 19);
            lblDelay.Text = "Delay";

            delay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            delay.Location = new System.Drawing.Point(46, 15);
            delay.Size = new System.Drawing.Size(52, 22);
            delay.DecimalPlaces = 1;
            delay.Increment = new decimal(new int[] { 1, 0, 0, 65536 });   // 0.1
            delay.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            delay.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
            delay.Value = new decimal(new int[] { 1, 0, 0, 0 });

            lblSec.AutoSize = true;
            lblSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            lblSec.Location = new System.Drawing.Point(101, 19);
            lblSec.Text = "sec";

            lblKey.AutoSize = true;
            lblKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            lblKey.Location = new System.Drawing.Point(148, 19);
            lblKey.Text = "Key";

            key.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            key.Location = new System.Drawing.Point(180, 15);
            key.Size = new System.Drawing.Size(110, 22);

            box.Controls.Add(lblDelay);
            box.Controls.Add(delay);
            box.Controls.Add(lblSec);
            box.Controls.Add(lblKey);
            box.Controls.Add(key);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;

        private System.Windows.Forms.NumericUpDown txtAutoRefreshDelay;
        private System.Windows.Forms.NumericUpDown txtAutoRefreshDelay2;
        private System.Windows.Forms.NumericUpDown txtAutoRefreshDelay3;
        private System.Windows.Forms.NumericUpDown txtAutoRefreshDelay4;
        private System.Windows.Forms.NumericUpDown txtAutoRefreshDelay5;

        private System.Windows.Forms.TextBox txtSkillTimerKey;
        private System.Windows.Forms.TextBox txtSkillTimerKey2;
        private System.Windows.Forms.TextBox txtSkillTimerKey3;
        private System.Windows.Forms.TextBox txtSkillTimerKey4;
        private System.Windows.Forms.TextBox txtSkillTimerKey5;

        private System.Windows.Forms.Label lblDelay1;
        private System.Windows.Forms.Label lblDelay2;
        private System.Windows.Forms.Label lblDelay3;
        private System.Windows.Forms.Label lblDelay4;
        private System.Windows.Forms.Label lblDelay5;

        private System.Windows.Forms.Label lblKey1;
        private System.Windows.Forms.Label lblKey2;
        private System.Windows.Forms.Label lblKey3;
        private System.Windows.Forms.Label lblKey4;
        private System.Windows.Forms.Label lblKey5;

        private System.Windows.Forms.Label lblSec1;
        private System.Windows.Forms.Label lblSec2;
        private System.Windows.Forms.Label lblSec3;
        private System.Windows.Forms.Label lblSec4;
        private System.Windows.Forms.Label lblSec5;

        private System.Windows.Forms.Label lblHint;
    }
}
