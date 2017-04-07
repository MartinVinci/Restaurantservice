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
            DoStuff2();
            //DbConfiguration.SetConfiguration(new MySqlEFConfiguration()); 
            //CreateTentativeOrders();
        }

        private void btnTentativeOrders_Click(object sender, EventArgs e)
        {
            //CreateTentativeOrders();
        }

        private void btnCreateLabels_Click(object sender, EventArgs e)
        {

        }

        private void CreateTentativeOrders()
        {
            PdfCreator.CreateTentativeOrder();
        }

        //private DataSet dset;
        //private BilarDS bilarDset = new BilarDS();

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
        private void DoStuff2()
        {
            //string conString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonstren99; database=bradgard_nu;";

            //conn.ConnectionString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonstren99; database=bradgard_nu;";


            var conn = new MySqlConnection();

            conn.ConnectionString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonsten99; database=bradgard_nu;";
            //string cs = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonstren99; database=bradgard_nu;";

            //MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {
                //conn = new MySqlConnection(cs);
                conn.Open();

                string stm = "SELECT * FROM kgportal_orders";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var asdf = rdr.GetInt32(0) + " : "
                        + rdr.GetString(1) + " : "
                        + rdr.GetString(2);

                    MessageBox.Show(rdr.GetInt32(0) + " : "
                        + rdr.GetString(1) + " : "
                        + rdr.GetString(2));
                }

            }
            catch (MySqlException ex)
            {
                var stophere = 2;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }
        }
    }
}

