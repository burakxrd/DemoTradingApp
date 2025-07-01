namespace DemoTradingApp
{
    partial class ExpensiveMarketForm
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
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            flpMarketItems = new FlowLayoutPanel();
            pnlCart = new Krypton.Toolkit.KryptonPanel();
            cmbPaymentMethod = new Krypton.Toolkit.KryptonComboBox();
            lblPaymentMethod = new Krypton.Toolkit.KryptonLabel();
            btnConfirmPurchase = new Krypton.Toolkit.KryptonButton();
            lblCartTotal = new Krypton.Toolkit.KryptonLabel();
            flpCartItems = new FlowLayoutPanel();
            kryptonHeader1 = new Krypton.Toolkit.KryptonHeader();
            btnShowCart = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pnlCart).BeginInit();
            pnlCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbPaymentMethod).BeginInit();
            SuspendLayout();
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(flpMarketItems);
            kryptonPanel1.Controls.Add(pnlCart);
            kryptonPanel1.Controls.Add(btnShowCart);
            kryptonPanel1.Dock = DockStyle.Fill;
            kryptonPanel1.Location = new Point(0, 0);
            kryptonPanel1.Margin = new Padding(4, 5, 4, 5);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Padding = new Padding(13, 15, 13, 15);
            kryptonPanel1.Size = new Size(1320, 989);
            kryptonPanel1.TabIndex = 0;
            // 
            // flpMarketItems
            // 
            flpMarketItems.AutoScroll = true;
            flpMarketItems.BackColor = Color.Transparent;
            flpMarketItems.Location = new Point(13, 15);
            flpMarketItems.Margin = new Padding(4, 5, 4, 5);
            flpMarketItems.Name = "flpMarketItems";
            flpMarketItems.Size = new Size(869, 986);
            flpMarketItems.TabIndex = 3;
            // 
            // pnlCart
            // 
            pnlCart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pnlCart.Controls.Add(cmbPaymentMethod);
            pnlCart.Controls.Add(lblPaymentMethod);
            pnlCart.Controls.Add(btnConfirmPurchase);
            pnlCart.Controls.Add(lblCartTotal);
            pnlCart.Controls.Add(flpCartItems);
            pnlCart.Controls.Add(kryptonHeader1);
            pnlCart.Location = new Point(904, 83);
            pnlCart.Margin = new Padding(4, 5, 4, 5);
            pnlCart.Name = "pnlCart";
            pnlCart.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            pnlCart.Size = new Size(400, 890);
            pnlCart.TabIndex = 2;
            pnlCart.Visible = false;
            // 
            // cmbPaymentMethod
            // 
            cmbPaymentMethod.Dock = DockStyle.Bottom;
            cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaymentMethod.DropDownWidth = 288;
            cmbPaymentMethod.IntegralHeight = false;
            cmbPaymentMethod.Location = new Point(0, 724);
            cmbPaymentMethod.Margin = new Padding(4, 5, 4, 5);
            cmbPaymentMethod.Name = "cmbPaymentMethod";
            cmbPaymentMethod.Size = new Size(400, 26);
            cmbPaymentMethod.TabIndex = 5;
            // 
            // lblPaymentMethod
            // 
            lblPaymentMethod.Dock = DockStyle.Bottom;
            lblPaymentMethod.Location = new Point(0, 750);
            lblPaymentMethod.Margin = new Padding(4, 5, 4, 5);
            lblPaymentMethod.Name = "lblPaymentMethod";
            lblPaymentMethod.Size = new Size(400, 24);
            lblPaymentMethod.StateCommon.ShortText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPaymentMethod.TabIndex = 4;
            lblPaymentMethod.Values.Text = "Ödeme Yöntemi:";
            // 
            // btnConfirmPurchase
            // 
            btnConfirmPurchase.Dock = DockStyle.Bottom;
            btnConfirmPurchase.Location = new Point(0, 774);
            btnConfirmPurchase.Margin = new Padding(4, 5, 4, 5);
            btnConfirmPurchase.Name = "btnConfirmPurchase";
            btnConfirmPurchase.Size = new Size(400, 89);
            btnConfirmPurchase.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnConfirmPurchase.TabIndex = 3;
            btnConfirmPurchase.Values.DropDownArrowColor = Color.Empty;
            btnConfirmPurchase.Values.Text = "Satın Almayı Onayla";
            btnConfirmPurchase.Click += btnConfirmPurchase_Click;
            // 
            // lblCartTotal
            // 
            lblCartTotal.Dock = DockStyle.Bottom;
            lblCartTotal.Location = new Point(0, 863);
            lblCartTotal.Margin = new Padding(4, 5, 4, 5);
            lblCartTotal.Name = "lblCartTotal";
            lblCartTotal.Size = new Size(400, 27);
            lblCartTotal.StateCommon.ShortText.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCartTotal.TabIndex = 2;
            lblCartTotal.Values.Text = "Toplam: ";
            // 
            // flpCartItems
            // 
            flpCartItems.AutoScroll = true;
            flpCartItems.BackColor = Color.Transparent;
            flpCartItems.Dock = DockStyle.Fill;
            flpCartItems.FlowDirection = FlowDirection.TopDown;
            flpCartItems.Location = new Point(0, 33);
            flpCartItems.Margin = new Padding(4, 5, 4, 5);
            flpCartItems.Name = "flpCartItems";
            flpCartItems.Padding = new Padding(7, 8, 7, 8);
            flpCartItems.Size = new Size(400, 857);
            flpCartItems.TabIndex = 1;
            flpCartItems.WrapContents = false;
            // 
            // kryptonHeader1
            // 
            kryptonHeader1.Dock = DockStyle.Top;
            kryptonHeader1.Location = new Point(0, 0);
            kryptonHeader1.Margin = new Padding(4, 5, 4, 5);
            kryptonHeader1.Name = "kryptonHeader1";
            kryptonHeader1.Size = new Size(400, 33);
            kryptonHeader1.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            kryptonHeader1.TabIndex = 0;
            kryptonHeader1.Values.Description = "";
            kryptonHeader1.Values.Heading = "Alışveriş Sepeti";
            kryptonHeader1.Values.Image = null;
            // 
            // btnShowCart
            // 
            btnShowCart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnShowCart.Location = new Point(904, 20);
            btnShowCart.Margin = new Padding(4, 5, 4, 5);
            btnShowCart.Name = "btnShowCart";
            btnShowCart.Size = new Size(400, 52);
            btnShowCart.TabIndex = 1;
            btnShowCart.Values.DropDownArrowColor = Color.Empty;
            btnShowCart.Values.Text = "Sepeti Göster";
            btnShowCart.Click += btnShowCart_Click;
            // 
            // ExpensiveMarketForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1320, 989);
            Controls.Add(kryptonPanel1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "ExpensiveMarketForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lüks Pazar";
            Load += ExpensiveMarketForm_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pnlCart).EndInit();
            pnlCart.ResumeLayout(false);
            pnlCart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cmbPaymentMethod).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton btnShowCart;
        private Krypton.Toolkit.KryptonPanel pnlCart;
        private Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private System.Windows.Forms.FlowLayoutPanel flpCartItems;
        private Krypton.Toolkit.KryptonButton btnConfirmPurchase;
        private Krypton.Toolkit.KryptonLabel lblCartTotal;
        private System.Windows.Forms.FlowLayoutPanel flpMarketItems;
        private Krypton.Toolkit.KryptonComboBox cmbPaymentMethod;
        private Krypton.Toolkit.KryptonLabel lblPaymentMethod;
    }
}