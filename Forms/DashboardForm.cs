using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransportationProject.Classes;
using TransportationProject.Database;

namespace TransportationProject.Forms
{
    public partial class DashboardForm : Form
    {
        public DashboardForm(string username)
        {
            InitializeComponent();

            loggedInUsername = username;

            // Display welcome message using a MessageBox
            MessageBox.Show("Welcome, " + loggedInUsername + "!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string loggedInUsername;

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            Form form = new Panels.PanelOverview();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelOverview();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnPassengerBookings_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelPassengerBookings();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnFreightShipments_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelFreightShipments();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnVehicles_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelVehicles();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnRoutes_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelRoutes();
            FormLoader.LoadForm(mainpanel, form);

        }

        private void btnTrips_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelTrips();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelEmployees();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelUsers();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelProfile(loggedInUsername);
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            // Ask for confirmation before logging out
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Show the login form
                LoginForm loginForm = new LoginForm();
                loginForm.Show();

                // Close the current form
                Close();
            }
        }
    }
}
