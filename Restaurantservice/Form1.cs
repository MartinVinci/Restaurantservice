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
using System.Configuration;


namespace Restaurantservice
{
    public partial class Form1 : Form
    {
        public static string DATABASETEST = "BRADGARD_NU";
        public static string DATABASELIVE = "KNIVOCHGAFFEL";

        public static string DataBaseVersion = "";
        public Form1()
        {
            InitializeComponent();
            CustomInitialize();
        }
        private void CustomInitialize()
        {
            rbnTodaysDate.Checked = true;
            dtpDateTimePicker.Enabled = false;
            rbnRealDatabase.Checked = true;
            rbnRealDatabase.Enabled = false;
            rbnTestDataBase.Enabled = false;
            lblVersion.Text = "Version: 1.0 - 17-05-03";

            DataBaseVersion = DATABASELIVE;

        }

        #region Forms actions
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
        private void rbnTestDataBase_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    DataBaseVersion = DATABASETEST;
                }
            }
        }

        private void rbnRealDatabase_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    DataBaseVersion = DATABASELIVE;
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
            // Today
            CreateLabels(DateTime.Now.ToShortDateString(), false);

            // Tomorrow
            DateTime nextDeliveryDate = DateTime.Now;
            nextDeliveryDate = nextDeliveryDate.AddDays(1);
            CreateLabels(nextDeliveryDate.ToShortDateString(), true);

            #region Three days forward
            //int dayOfWeek = (int)nextDeliveryDate.DayOfWeek;

            //int tomorrow = dayOfWeek++;

            //if (dayOfWeek == 5)
            //{
            //    nextDeliveryDate = nextDeliveryDate.AddDays(3);
            //}
            //else if (tomorrow == 6)
            //{
            //    nextDeliveryDate = nextDeliveryDate.AddDays(2);
            //}
            //else 
            //{
            //    nextDeliveryDate = nextDeliveryDate.AddDays(1);
            //}

            //CreateLabels(nextDeliveryDate.ToShortDateString(), true);
            #endregion
        }
        private void btnCreateInvoices_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ingen funktionalitet utvecklad för denna knapp än.");
        }

        private void btnAdminPassWord_Click(object sender, EventArgs e)
        {
            if (tbxAdminPassWord.Text == "niklas")
            {
                rbnRealDatabase.Enabled = true;
                rbnTestDataBase.Enabled = true;
            }
            else
            {
                MessageBox.Show("Fel lösenord");
            }
        }
        #endregion
        #region Methods
        private void CreateTentativeOrders(DateTime deliveryDate)
        {
            string dateAsString = deliveryDate.ToShortDateString();
            List<Order> orders = DataAccess.GetTodaysOrders(dateAsString);
            if (orders.Count == 0)
            {
                MessageBox.Show("Inga beställningar hittade");
            }
            else
            {
                List<TentativeOrder> tentativeOrders = GetTentativeOrders(orders);

                PdfCreator.CreateTentativeOrders(tentativeOrders, deliveryDate);
                MessageBox.Show("Preliminära beställningar skapade!");
            }

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
        private void CreateLabels(string date, bool tomorrow)
        {
            List<Order> orders = DataAccess.GetTodaysOrders(date);

            if (orders.Count == 0)
            {
                MessageBox.Show("Inga beställningar hittade");
            }
            else
            {
                PdfCreator.CreateLabels(orders, date, tomorrow);

                if (tomorrow == false)
                {
                    MessageBox.Show("Etiketter skapade!");
                }
            }
        }
        #endregion

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

