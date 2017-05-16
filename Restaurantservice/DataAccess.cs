﻿using MySql.Data.MySqlClient;
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
        public static List<Order> GetTodaysOrders(string deliveryDate, string pickupRestaurant)
        {
            //// Use this during development if you want to set at specific date.
            //if (deliveryDate == "0")
            //{
            //    deliveryDate = "2017-04-15";
            //}

            var conn = new MySqlConnection();

            string dataBaseVersion = Form1.DataBaseVersion.ToString();

            if (dataBaseVersion == Form1.DATABASELIVE)
            {
                // Kniv och gaffel
                conn.ConnectionString = "server=mysql201.loopia.se; userid=admin@k159216; password=JeokDeeVCz; database=knivochgaffel_se;";
            }
            else if (dataBaseVersion == Form1.DATABASETEST)
            {
                // Brädgård_nu
                conn.ConnectionString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonsten99; database=bradgard_nu;";
            }


            MySqlDataReader rdr = null;

            List<Order> orders = new List<Order>();

            try
            {
                //conn = new MySqlConnection(cs);
                conn.Open();


                //deliveryDate = "2017-04-15";
                //string query = "SELECT bru.firstname, bru.lastname, pro.product_name, ord.delivery_date, bru.delivery_street, ord.served_cold "
                //    + "FROM kgportal_orders as ord "
                //    + "INNER JOIN kgportal_brukare as bru ON ord.customer = bru.id "
                //    + "INNER JOIN kgportal_products as pro ON ord.item_id = pro.id "
                //    + "WHERE ord.delivery_date = '"
                //    + deliveryDate
                //    + "'";

                string query = "SELECT bru.firstname, bru.lastname, pro.product_name, ord.delivery_date, bru.delivery_street, ord.served_cold, meta1.meta_value, meta2.meta_value " +
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

                    date = date.Substring(0, 10);
                    Order order = new Order(name, dish, date, addr, cold, pickupRest);

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
        //private void DoStuff2()
        //{
        //    //string conString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonstren99; database=bradgard_nu;";

        //    //conn.ConnectionString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonstren99; database=bradgard_nu;";


        //    var conn = new MySqlConnection();

        //    conn.ConnectionString = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonsten99; database=bradgard_nu;";
        //    //string cs = "server=mysql507.loopia.se; userid=wp_admin@b113010; password=mediakonstren99; database=bradgard_nu;";

        //    //MySqlConnection conn = null;
        //    MySqlDataReader rdr = null;

        //    try
        //    {
        //        //conn = new MySqlConnection(cs);
        //        conn.Open();

        //        string stm = "SELECT * FROM kgportal_orders";
        //        MySqlCommand cmd = new MySqlCommand(stm, conn);
        //        rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            var asdf = rdr.GetInt32(0) + " : "
        //                + rdr.GetString(1) + " : "
        //                + rdr.GetString(2);

        //        }

        //    }
        //    catch (MySqlException ex)
        //    {

        //    }
        //    finally
        //    {
        //        if (rdr != null)
        //        {
        //            rdr.Close();
        //        }

        //        if (conn != null)
        //        {
        //            conn.Close();
        //        }

        //    }
        //}



    }
}
