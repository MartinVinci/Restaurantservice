using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.Entity;
using MySql.Data.Entity;
using System.Data.SqlClient;

namespace Restaurantservice
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            CustomInitialize();
            //TODO Remove this line
            //CreateTentativeOrders(DateTime.Now);
            //CreateLabels("2017-04-15");
        }
        private void CustomInitialize()
        {
            rbnTodaysDate.Checked = true;
            dtpDateTimePicker.Enabled = false;
            
        }
        private void rbnPickDate_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    dtpDateTimePicker.Enabled = true;
                }
            }
        }
        private void rbnTodaysDate_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    dtpDateTimePicker.Enabled = false;
                }
            }
        }

        private void btnTentativeOrders_Click(object sender, EventArgs e)
        {
            if (rbnTodaysDate.Checked == true)
            {
                CreateTentativeOrders(DateTime.Now);
            }
            else if (rbnPickDate.Checked == true)
            {
                CreateTentativeOrders(dtpDateTimePicker.Value);
            }
        }

        private void btnCreateLabels_Click(object sender, EventArgs e)
        {
            CreateLabels(DateTime.Now.ToShortDateString());
        }

        private void CreateTentativeOrders(DateTime deliveryDate)
        {
            // TODO Skapa string

            string dateAsString = deliveryDate.ToShortDateString();
            List<Order> orders = DataAccess.GetTodaysOrders(dateAsString);

            List<TentativeOrder> tentativeOrders = GetTentativeOrders(orders);

            PdfCreator.CreateTentativeOrders(tentativeOrders, deliveryDate);
        }
        private List<TentativeOrder> GetTentativeOrders(List<Order> orders)
        {
            List<string> uniqueDishes = (from o in orders
                                         orderby o.Dish
                                         select o.Dish).Distinct().ToList();

            List<TentativeOrder> tentativeOrders = new List<TentativeOrder>();

            foreach (var item in uniqueDishes)
            {
                var count = (from o in orders
                             where o.Dish == item
                             select o).Count();

                tentativeOrders.Add(new TentativeOrder(item, count));
            }

            return tentativeOrders;
        }
        private void CreateLabels(string date)
        {
            List<Order> orders = DataAccess.GetTodaysOrders(date);

            PdfCreator.CreateLabels(orders, date);
        }


        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        #region Garbage
        private void DoStuff()
        {
            var conn = new MySqlConnection();
            string conString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonsten99; database=bradgard_nu;";

            conn.ConnectionString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonstren99; database=bradgard_nu;";
            //bilarDset.Clear();

            //DataSet dset = new DataSet("Hej");


            conn.Open();
            //SqlDataAdapter myAdapter = new SqlDataAdapter("SELECT * FROM testtabell", conn);
            //myAdapter.Fill(dset, "Bilar");
            conn.Close();

            //dataGridView1.DataSource = null;
            //this.dataGridView1.DataSource = bilarDset.Tables["Bilar"];



            //cn.ConnectionString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonstren99; database=bradgard_nu;";
            //cn.Open();


            // using (var db = new MyContext)


            MessageBox.Show("Yes!!!");

        }

        #endregion

    }
}

