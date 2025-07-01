using Krypton.Toolkit;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DemoTradingApp
{
    /// <summary>
    /// Form for displaying and managing user's purchased possessions.
    /// </summary>
    public partial class MyPossessionsForm : KryptonForm
    {
        private readonly int _userId;

        /// <summary>
        /// Initializes a new instance of the MyPossessionsForm class.
        /// </summary>
        /// <param name="userId">The ID of the user whose possessions to display</param>
        public MyPossessionsForm(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void MyPossessionsForm_Load(object sender, EventArgs e)
        {
            LoadPossessions();
        }

        private void LoadPossessions()
        {
            while (flpPossessions.Controls.Count > 0)
            {
                flpPossessions.Controls[0].Dispose();
            }
            flpPossessions.Controls.Clear();

            DataTable possessions = DatabaseHelper.GetUserPossessions(_userId);

            foreach (DataRow row in possessions.Rows)
            {
                var itemBox = new KryptonGroupBox
                {
                    Size = new Size(180, 260),
                    Margin = new Padding(10),
                    Values = { Heading = row["ProductName"].ToString() }
                };

                var pic = new PictureBox
                {
                    ImageLocation = row["image_path"].ToString(),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.Gainsboro,
                    Dock = DockStyle.Fill
                };

                var deleteButton = new KryptonButton
                {
                    Text = Properties.Resources.Delete,
                    Dock = DockStyle.Bottom,
                    Tag = row["purchase_id"]
                };
                deleteButton.Click += DeletePossession_Click;

                var infoLabel = new KryptonLabel
                {
                    Text = string.Format(
                Properties.Resources.PossessionInfoFormat, 
                row["Quantity"],                           
                Convert.ToDecimal(row["PurchasePrice"]),
                row["Currency"]                            
            ),
                    Dock = DockStyle.Bottom,
                    StateCommon = { ShortText = { TextH = PaletteRelativeAlign.Center } }
                };

                itemBox.Panel.Controls.Add(pic);
                itemBox.Panel.Controls.Add(infoLabel);
                itemBox.Panel.Controls.Add(deleteButton);

                flpPossessions.Controls.Add(itemBox);
            }
        }

        private void DeletePossession_Click(object? sender, EventArgs e)
        {
            var button = sender as KryptonButton;
            if (button?.Tag == null) return;

            int purchaseId = Convert.ToInt32(button.Tag);

            var confirmation = KryptonMessageBox.Show(
                Properties.Resources.DeleteAssetConfirmation,
                Properties.Resources.DeleteConfirmation,
                KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Warning);

            if (confirmation == DialogResult.No) return;

            try
            {
                bool success = DatabaseHelper.DeletePossession(purchaseId);
                if (success)
                {
                    KryptonMessageBox.Show(Properties.Resources.AssetDeletedSuccess, Properties.Resources.SuccessTitle, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                    LoadPossessions();
                }
                else
                {
                    KryptonMessageBox.Show(Properties.Resources.AssetDeleteError, Properties.Resources.ErrorTitle, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(Properties.Resources.DatabaseErrorWithMessage + ex.Message, Properties.Resources.CriticalError, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }
    }
}