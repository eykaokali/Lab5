using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Lab5
{
    public partial class fTV : Form
    {
        public TV TheTV;
        private object chbIsSmart;

        public fTV(ref TV t)
        {
            TheTV = t;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            TheTV.Model = tbModel.Text.Trim();
            TheTV.Brand = tbBrand.Text.Trim();
            TheTV.ScreenSize = int.Parse(tbSize.Text.Trim());
            TheTV.Resolution = tbResolution.Text.Trim();
            TheTV.IsSmart = rbYes.Checked;
            TheTV.Color = tbColor.Text.Trim();
            TheTV.Price = float.Parse(tbPrice.Text.Trim());

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void fTV_Load_1(object sender, EventArgs e)
        {
             if (TheTV != null)
            {
                tbModel.Text = TheTV.Model;
                tbBrand.Text = TheTV.Brand;
                tbSize.Text = TheTV.ScreenSize.ToString();
                tbResolution.Text = TheTV.Resolution;
                tbColor.Text = TheTV.Color;
                tbPrice.Text = TheTV.Price.ToString("0.00");
                if (TheTV.IsSmart) {rbYes.Checked = true;}
                else {rbNo.Checked = true;}
            }
        }
    }
}
