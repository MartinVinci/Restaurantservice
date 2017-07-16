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

        public static string PICKUP_MOBILIA = "mobilia";
        public static string PICKUP_JAGERSRO = "jägersro";

        public static string DataBaseVersion = "";
        public Form1()
        {
            InitializeComponent();
            CustomInitialize();
            DevelopmentInitialize(); // TODO remove at release
        }
        private void DevelopmentInitialize()
        {
            //CreateLabels(DateTime.Now.ToShortDateString(), false, PICKUP_JAGERSRO);

            CreateTentativeOrders(DateTime.Now, PICKUP_JAGERSRO);

        }
        private void CustomInitialize()
        {
            rbnTodaysDate.Checked = true;
            dtpDateTimePicker.Enabled = false;
            rbnRealDatabase.Checked = true;
            rbnRealDatabase.Enabled = false;
            rbnTestDataBase.Enabled = false;
            lblVersion.Text = "Version: 1.4 - 17-06-08";

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
            string pickupRest = "";
            if (rbnPrelPickupMobilia.Checked == true)
            {
                pickupRest = PICKUP_MOBILIA;
            }
            else if (rbnPrelPickupJägersro.Checked == true)
            {
                pickupRest = PICKUP_JAGERSRO;
            }

            if (pickupRest == "")
            {
                MessageBox.Show("Du har inte valt någon restaurang.");
            }
            else
            {
                if (rbnTodaysDate.Checked == true)
                {
                    CreateTentativeOrders(DateTime.Now, pickupRest);
                }
                else if (rbnPickDate.Checked == true)
                {
                    CreateTentativeOrders(dtpDateTimePicker.Value, pickupRest);
                }
            }
        }

        private void btnCreateLabels_Click(object sender, EventArgs e)
        {
            string pickupRest = "";
            if (rbnPickupMobilia.Checked == true)
            {
                pickupRest = PICKUP_MOBILIA;
            }
            else if (rbnPickupJagersro.Checked == true)
            {
                pickupRest = PICKUP_JAGERSRO;
            }

            if (pickupRest == "")
            {
                MessageBox.Show("Du har inte valt någon restaurang.");
            }
            else
            {
                // Today
                CreateLabels(DateTime.Now.ToShortDateString(), false, pickupRest);

                // Tomorrow
                DateTime nextDeliveryDate = DateTime.Now;
                nextDeliveryDate = nextDeliveryDate.AddDays(1);
                CreateLabels(nextDeliveryDate.ToShortDateString(), true, pickupRest);
            }

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
            //CreateInvoiceCsvFile();
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
        private void CreateInvoiceCsvFile()
        {
            List<InvoiceDataRow> invoiceDataRows = DataAccess.GetInvoiceData();

            bool success = TextFileCreator.CreateInvoices(invoiceDataRows);


        }

        private void CreateTentativeOrders(DateTime deliveryDate, string pickupRest)
        {
            string dateAsString = deliveryDate.ToShortDateString();
            List<Order> allOrdersFromDB = DataAccess.GetTodaysOrders(dateAsString, pickupRest);
            if (allOrdersFromDB.Count == 0)
            {
                MessageBox.Show("Inga beställningar hittade");
            }
            else
            {
                List<string> addresses = (from o in allOrdersFromDB
                                          orderby o.Addr
                                          select o.Addr).Distinct().ToList();

                List<TentativeOrderList> listOfTentativeOrderList = new List<TentativeOrderList>();
                foreach (var addr in addresses)
                {
                    List<Order> ordersForAddr = (from o in allOrdersFromDB
                                                 where o.Addr == addr
                                                 select o).ToList();

                    List<TentativeOrder> tentativeOrders = GetTentativeOrders(ordersForAddr);

                    int totalDishCount = GetTotalDishCount(tentativeOrders);
                    TentativeOrderList tentList = new TentativeOrderList(addr, tentativeOrders, totalDishCount);

                    listOfTentativeOrderList.Add(tentList);
                }

                // Add total tentative orders to print after everything else
                var allTentativeOrders = GetTentativeOrders(allOrdersFromDB);
                int allTotalDishCount = GetTotalDishCount(allTentativeOrders);
                TentativeOrderList tentListTotal = new TentativeOrderList("- - Totalt - - ", allTentativeOrders, allTotalDishCount);
                listOfTentativeOrderList.Add(tentListTotal);

                PdfCreator.CreateTentativeOrders(listOfTentativeOrderList, deliveryDate, pickupRest);
                MessageBox.Show("Preliminära beställningar skapade!");
            }
        }
        private int GetTotalDishCount(List<TentativeOrder> tentativeOrders)
        {
            int returnInt = 0;

            foreach (var tent in tentativeOrders)
            {
                if (tent.IsCountableDish)
                {
                    returnInt += tent.Quantity;
                }
            }

            return returnInt;
        }
        private List<TentativeOrder> GetTentativeOrders(List<Order> orders)
        {
            List<string> uniqueDishes = (from o in orders
                                         orderby o.Dish
                                         select o.Dish).Distinct().ToList();

            List<TentativeOrder> tentativeOrders = new List<TentativeOrder>();

            foreach (var dish in uniqueDishes)
            {
                var subOrders = (from o in orders
                                 where o.Dish == dish
                                 select o).ToList();

                int dishCount = GetDishCount(subOrders);
                string infoText = GetInfoText(subOrders);
                bool isCountableDish = GetIsCountableDish(subOrders);

                tentativeOrders.Add(new TentativeOrder(dish, dishCount, infoText, isCountableDish));
            }

            return tentativeOrders;
        }
        private int GetNoRice(List<Order> subOrders)
        {
            return (from hits in subOrders
                    where hits.NoRice == true
                    select hits).Count();
        }

        private int GetNoGluten(List<Order> subOrders)
        {
            return (from hits in subOrders
                    where hits.NoGluten == true
                    select hits).Count();
        }

        private string GetInfoText(List<Order> subOrders)
        {
            string returnText = "";

            int noRice = GetNoRice(subOrders);
            int noGluten = GetNoGluten(subOrders);

            if (noRice > 0 || noGluten > 0)
            {
                returnText += ", varav ";
            }

            if (noRice > 0)
            {
                returnText += string.Format("{0} utan ris och ", noRice);
            }

            if (noGluten > 0)
            {
                returnText += string.Format("{0} utan gluten ", noGluten);
            }

            return returnText;
        }

        private int GetDishCount(List<Order> subOrders)
        {
            return subOrders.Count();
        }

        private bool GetIsCountableDish(List<Order> subOrders)
        {
            if (subOrders[0].ProductGroup == 1 || subOrders[0].ProductGroup == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateLabels(string date, bool tomorrow, string pickupRest)
        {
            List<Order> orders = DataAccess.GetTodaysOrders(date, pickupRest);

            if (orders.Count == 0)
            {
                if (tomorrow == false)
                {
                    MessageBox.Show("Inga beställningar hittade");
                }
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

