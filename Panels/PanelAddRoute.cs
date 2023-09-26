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
using TransportationProject.Forms;

namespace TransportationProject.Panels
{
    public partial class PanelAddRoute : Form
    {
        public PanelAddRoute()
        {
            InitializeComponent();
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Open a PanelRoutes form
            Form form = new PanelRoutes();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string startLocation = txtStartLocation.Text;
            string endLocation = txtEndLocation.Text;

            // Validate the input
            decimal distance;
            if (!decimal.TryParse(txtDistance.Text, out distance))
            {
                MessageBox.Show("Please enter a valid number for distance.");
                return;
            }

            decimal travelTimeHours;
            if (!decimal.TryParse(txtTravelTime.Text, out travelTimeHours))
            {
                MessageBox.Show("Please enter a valid number for travel time.");
                return;
            }

            TimeSpan travelTime = TimeSpan.FromHours((double)travelTimeHours);

            if (string.IsNullOrWhiteSpace(startLocation) || string.IsNullOrWhiteSpace(endLocation) ||
                distance == 0 || travelTime == TimeSpan.Zero)
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            // Insert the vehicle data into the database
            InsertRoute(startLocation, endLocation, distance, travelTime);

            // Optionally, you can show a success message or perform other actions as needed
            MessageBox.Show("Route added successfully!", "Add Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Open a PanelRoutes form
            Form form = new PanelRoutes();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }

        private void InsertRoute(string startLocation, string endLocation, decimal distance, TimeSpan travelTime)
        {
            string connectionString = DatabaseHelper.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Routes (StartLocation, EndLocation, Distance, TravelTime) VALUES (@StartLocation, @EndLocation, @Distance, @TravelTime)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartLocation", startLocation);
                    command.Parameters.AddWithValue("@EndLocation", endLocation);
                    command.Parameters.AddWithValue("@Distance", distance);
                    command.Parameters.AddWithValue("@TravelTime", travelTime);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
