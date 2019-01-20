using System;
using System.Windows.Forms;
using System.Configuration;
using Restaurantservice.Logic;

namespace Restaurantservice
{
    public partial class Form1 : Form
    {
        public static string DATABASETEST = "BRADGARD_NU";
        public static string DATABASELIVE = "KNIVOCHGAFFEL";

        public static string PICKUP_MOBILIA = "mobilia";
        public static string PICKUP_JAGERSRO = "jägersro";

        public static int LEFT_COLUMN_ADJUST = Int32.Parse(ConfigurationManager.AppSettings["LeftColumnPosition"]);
        public static int RIGHT_COLUMN_ADJUST = Int32.Parse(ConfigurationManager.AppSettings["RigthColumnPosition"]);

        public static string DataBaseVersion = "";

        public Form1()
        {
            InitializeComponent();
            CustomInitialize();
            //DevelopmentInitialize(); // TODO remove at release
        }

        #region Initialize
        private void DevelopmentInitialize()
        {

            //CreateLabels(DateTime.Now.ToShortDateString(), false, PICKUP_JAGERSRO);
            //CreateLabels("2017-12-13", false, PICKUP_JAGERSRO);

            //DataBaseVersion = DATABASETEST;
            //CreateTentativeOrders(DateTime.Now, PICKUP_JAGERSRO);

        }
        private void CustomInitialize()
        {
            rbnTodaysDate.Checked = true;
            dtpDateTimePicker.Enabled = false;
            rbnRealDatabase.Checked = true;
            rbnRealDatabase.Enabled = false;
            rbnTestDataBase.Enabled = false;
            lblVersion.Text = "Version: 2.2 - 19-01-20";

            DataBaseVersion = DATABASELIVE;

            nudLeftColumn.Enabled = false;
            nudLeftColumn.Maximum = 1000;
            nudLeftColumn.Minimum = -1000;
            nudLeftColumn.Value = LEFT_COLUMN_ADJUST;

            nudRightColumn.Enabled = false;
            nudRightColumn.Maximum = 1000;
            nudRightColumn.Minimum = -1000;
            nudRightColumn.Value = RIGHT_COLUMN_ADJUST;
            btnColumnXPosition.Enabled = false;

        }
        #endregion

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
                    BusinessLogic.CreateTentativeOrders(DateTime.Now, pickupRest);
                }
                else if (rbnPickDate.Checked == true)
                {
                    BusinessLogic.CreateTentativeOrders(dtpDateTimePicker.Value, pickupRest);
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
                BusinessLogic.CreateLabels(DateTime.Now.ToShortDateString(), false, pickupRest);

                // Tomorrow
                DateTime nextDeliveryDate = DateTime.Now;
                nextDeliveryDate = nextDeliveryDate.AddDays(1);
                BusinessLogic.CreateLabels(nextDeliveryDate.ToShortDateString(), true, pickupRest);
            }
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

                nudLeftColumn.Enabled = true;
                nudRightColumn.Enabled = true;
                btnColumnXPosition.Enabled = true;
            }
            else
            {
                MessageBox.Show("Fel lösenord");
            }
        }
        private void btnColumnXPosition_Click(object sender, EventArgs e)
        {
            ChangeColumnsAdjustments();
        }
        private void ChangeColumnsAdjustments()
        {
            try
            {
                int leftAdjust = (int)nudLeftColumn.Value;
                int rightAdjust = (int)nudRightColumn.Value;

                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string configFile = System.IO.Path.Combine(appPath, "Restaurantservice.exe.config");

                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = configFile;
                System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                config.AppSettings.Settings["LeftColumnPosition"].Value = leftAdjust.ToString();
                config.AppSettings.Settings["RigthColumnPosition"].Value = rightAdjust.ToString();

                LEFT_COLUMN_ADJUST = leftAdjust;
                RIGHT_COLUMN_ADJUST = rightAdjust;

                config.Save();

                MessageBox.Show("Ändringarna sparade.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Något gick dessvärre fel" + e.ToString());
            }
        }
        private void btnColumnAdjustmentInformation_Click(object sender, EventArgs e)
        {
            string text = "Här kan du ändra positionen i sidled för etiketterna. \n";
            text += "Du flyttar de fem etiketterna som finns på vänstra ";
            text += "sidan av pappret genom att ändra siffran uppåt eller nedåt. ";
            text += "Tänk på att det går att ha negativa tal också, tex -5.\n\n";
            text += "Ökar du siffran kommer etiketterna hamna längre till höger ";
            text += "på pappret och minskar du siffran flyttas etiketterna till vänster. \n\n";
            text += "Du får prova dig fram till vad som blir bra, men höjs talet med 10 ";
            text += "innebär det ca 4 mm.";

            MessageBox.Show(text);
        }
        #endregion

        #region Easter egg
        string _eastereggstring = "";
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckEasterEggString(tabControl1.SelectedIndex.ToString());
        }
        private void CheckEasterEggString(string number)
        {
            _eastereggstring += number;

            // Only 8 characters
            if (_eastereggstring.Length > 4)
            {
                _eastereggstring = _eastereggstring.Substring(1, 4);
            }

            if (_eastereggstring == "1302")
            {
                System.Diagnostics.Process.Start("http://www.hamsterdance.org/hamsterdance/");
            }
        }

        #endregion
    }
}

