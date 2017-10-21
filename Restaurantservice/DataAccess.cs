using MySql.Data.MySqlClient;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurantservice
{
    public class DataAccess
    {
        private static MySqlConnection GetSqlConnection()
        {
            var conn = new MySqlConnection();

            string dataBaseVersion = Form1.DataBaseVersion.ToString();

            if (dataBaseVersion == Form1.DATABASELIVE)
            {
                // Kniv och gaffel
                string password = ConfigurationManager.AppSettings["PasswordProd"];
                string server = ConfigurationManager.AppSettings["ServerProd"];
                string userid = ConfigurationManager.AppSettings["UserIdProd"];
                string database = ConfigurationManager.AppSettings["DatabaseProd"];

                conn.ConnectionString = string.Format("server={0}; userid={1}; password={2}; database={3};",
                    server, userid, password, database);
            }
            else if (dataBaseVersion == Form1.DATABASETEST)
            {
                string password = ConfigurationManager.AppSettings["PasswordTest"];
                string server = ConfigurationManager.AppSettings["ServerTest"];
                string userid = ConfigurationManager.AppSettings["UserIdTest"];
                string database = ConfigurationManager.AppSettings["DatabaseTest"];

                // Brädgård
                conn.ConnectionString = string.Format("server={0}; userid={1}; password={2}; database={3};",
                    server, userid, password, database);
            }
            return conn;
        }

        public static List<Order> GetTodaysOrders(string deliveryDate, string pickupRestaurant)
        {
            // use this during development if you want to set at specific date.
            // deliveryDate = "2017-04-30";

            MySqlConnection conn = GetSqlConnection();

            MySqlDataReader rdr = null;

            List<Order> orders = new List<Order>();

            try
            {
                //conn = new MySqlConnection(cs);
                conn.Open();

                string query = "SELECT bru.firstname, bru.lastname, pro.product_name, ord.delivery_date, bru.delivery_street, ord.served_cold, meta1.meta_value, meta2.meta_value, ord.special_packaging, ord.no_rice, ord.gluten_free, pro.product_group, ord.no_lactose, ord.packaging_type " +
                                "FROM kgportal_orders as ord " +
                                "INNER JOIN kgportal_brukare as bru ON ord.customer = bru.id " +
                                "INNER JOIN kgportal_products as pro ON ord.item_id = pro.id " +
                                "INNER JOIN kgportal_usermeta as meta1 ON bru.unit_id = meta1.user_id " +
                                "INNER JOIN kgportal_usermeta as meta2 ON bru.unit_id = meta2.user_id " +
                                "WHERE meta1.meta_key = 'enhet' AND meta2.meta_key = 'restaurant' AND ord.delivery_date = '"
                                + deliveryDate
                                + "'";



                MySqlCommand cmd = new MySqlCommand(query, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string name = rdr.GetString(0) + " " + rdr.GetString(1);
                    string dish = rdr.GetString(2);
                    string date = rdr.GetString(3);
                    string addr = rdr.GetString(6);
                    bool cold = rdr.GetBoolean(5);
                    string pickupRest = rdr.GetString(7);
                    bool specialPackaging = rdr.GetBoolean(8);
                    bool noRice = rdr.GetBoolean(9);
                    bool glutenFree = rdr.GetBoolean(10);
                    int productGroup = rdr.GetInt32(11);
                    bool noLactose = rdr.GetBoolean(12);
                    string packagingType = rdr.GetString(13);

                    date = date.Substring(0, 10);

                    Order order = new Order(name, dish, date, addr, cold, pickupRest, specialPackaging, noRice, glutenFree, productGroup, noLactose, packagingType);

                    if (order.PickupRestaurant.ToLower() == pickupRestaurant)
                    {
                        orders.Add(order);
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
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

            return orders;
        }

        public static List<InvoiceDataRow> GetInvoiceData()
        {
            MySqlConnection conn = GetSqlConnection();

            MySqlDataReader rdr = null;

            List<InvoiceDataRow> invoiceDataRows = new List<InvoiceDataRow>();

            try
            {
                conn.Open();

                string query = "SELECT bru.id, concat(bru.firstname, ' ', bru.lastname) as Namn, count(ord.item_id) as AntalRatter, pro.product_name, pro.price, bru.* "
                        + "FROM `kgportal_orders` as ord "
                        + "INNER JOIN `kgportal_products` as pro "
                        + "ON pro.id = ord.item_id "
                        + "INNER JOIN `kgportal_brukare` as bru "
                        + "ON ord.customer = bru.id "
                        + "WHERE ord.delivery_date >= '2017-04-16' "
                        + "AND ord.delivery_date <= '2017-05-15' "
                        + "GROUP BY ord.item_id, bru.id "
                        + "ORDER BY bru.id ";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string id = rdr.GetString(0);
                    string name = rdr.GetString(1);
                    int amount = rdr.GetInt32(2);
                    string dish = rdr.GetString(3);
                    int price = rdr.GetInt32(4);

                    InvoiceDataRow row = new InvoiceDataRow(id, name, amount, dish, price);

                    invoiceDataRows.Add(row);
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
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

            return invoiceDataRows;
        }

    }
}
