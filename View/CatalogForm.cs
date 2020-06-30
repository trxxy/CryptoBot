using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class CatalogForm : Form
    {
        public CatalogForm()
        {
            InitializeComponent();
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            var x = File.ReadAllLines("");
            foreach (var item in x)
            {

            }

        }

        private void lbSushies_SelectedIndexChanged(object sender, EventArgs e) { }

        private void btnProductMinus_Click(object sender, EventArgs e)
        {

        }

        private void btnProductPlus_Click(object sender, EventArgs e)
        {

        }

        private void btnAddToBasket_Click(object sender, EventArgs e)
        {

        }

        private void lbBasket_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnMakeOrder_Click(object sender, EventArgs e)
        {

        }
        private void btnClearBasket_Click(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
