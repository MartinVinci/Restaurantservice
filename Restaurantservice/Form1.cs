using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurantservice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateTentativeOrders();
        }

        private void btnTentativeOrders_Click(object sender, EventArgs e)
        {
            CreateTentativeOrders();
        }

        private void btnCreateLabels_Click(object sender, EventArgs e)
        {

        }

        private void CreateTentativeOrders()
        {
            PdfCreator.CreateTentativeOrder();
        }


    }
}
