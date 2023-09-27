using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransportationProject.Classes;

namespace TransportationProject.Forms
{
    public partial class ReceptionistForm : Form
    {
        public ReceptionistForm(string username)
        {
            InitializeComponent();

            loggedInUsername = username;

            // Display welcome message using a MessageBox
            MessageBox.Show("Welcome, " + loggedInUsername + "!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string loggedInUsername;

        private void ReceptionistForm_Load(object sender, EventArgs e)
        {
            Form form = new Panels.PanelPassengerBookings();
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

        private void btnTrips_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelTrips();
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
