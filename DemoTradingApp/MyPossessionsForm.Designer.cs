namespace DemoTradingApp
{
    partial class MyPossessionsForm
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
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            flpPossessions = new FlowLayoutPanel();
            kryptonHeader1 = new Krypton.Toolkit.KryptonHeader();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(flpPossessions);
            kryptonPanel1.Controls.Add(kryptonHeader1);
            kryptonPanel1.Dock = DockStyle.Fill;
            kryptonPanel1.Location = new Point(0, 0);
            kryptonPanel1.Margin = new Padding(4, 5, 4, 5);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Size = new Size(1075, 664);
            kryptonPanel1.TabIndex = 0;
            // 
            // flpPossessions
            // 
            flpPossessions.AutoScroll = true;
            flpPossessions.BackColor = Color.Transparent;
            flpPossessions.Dock = DockStyle.Fill;
            flpPossessions.Location = new Point(0, 34);
            flpPossessions.Margin = new Padding(4, 5, 4, 5);
            flpPossessions.Name = "flpPossessions";
            flpPossessions.Padding = new Padding(13, 15, 13, 15);
            flpPossessions.Size = new Size(1075, 630);
            flpPossessions.TabIndex = 1;
            // 
            // kryptonHeader1
            // 
            kryptonHeader1.Dock = DockStyle.Top;
            kryptonHeader1.Location = new Point(0, 0);
            kryptonHeader1.Margin = new Padding(4, 5, 4, 5);
            kryptonHeader1.Name = "kryptonHeader1";
            kryptonHeader1.Size = new Size(1075, 34);
            kryptonHeader1.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            kryptonHeader1.TabIndex = 0;
            kryptonHeader1.Values.Description = "";
            kryptonHeader1.Values.Heading = Properties.Resources.MyPossessions;
            kryptonHeader1.Values.Image = null;
            // 
            // MyPossessionsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1075, 664);
            Controls.Add(kryptonPanel1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MyPossessionsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = Properties.Resources.MyPossessions;
            Load += MyPossessionsForm_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            kryptonPanel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private System.Windows.Forms.FlowLayoutPanel flpPossessions;
    }
}