using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransportationProject.Classes;
using TransportationProject.Database;
using TransportationProject.Forms;

namespace TransportationProject.Panels
{
    public partial class PanelModifyRoute : Form
    {
        public PanelModifyRoute(string routeId, string startLocation, string endLocation, string distance, string travelTime)
        {
            InitializeComponent();

            // Set the values in the text boxes
            txtRouteId.Text = routeId;
            txtStartLocation.Text = startLocation;
            txtEndLocation.Text = endLocation;
            txtDistance.Text = distance;
            txtTravelTime.Text = travelTime;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get the values from the form's controls
            string routeId = txtRouteId.Text;
            string startLocation = txtStartLocation.Text;
            string endLocation = txtEndLocation.Text;

            // Validate the input
            decimal distance;
            if (!decimal.TryParse(txtDistance.Text.Replace(" Km", ""), out distance))
            {
                MessageBox.Show("Please enter a valid number for distance.");
                return;
            }

            decimal travelTimeHours;
            if (!decimal.TryParse(txtTravelTime.Text.Replace(" hour", ""), out travelTimeHours))
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

            // Confirm the update with the user
            DialogResult result = MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Insert the employee data into the database
                ModifyRoute(routeId, startLocation, endLocation, distance, travelTime);

                // Display a message indicating successful update
                MessageBox.Show("Record updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open a PanelRoutes form
                Form form = new PanelRoutes();
                DashboardForm dashboardForm = FindDashboardForm();
                if (dashboardForm != null)
                {
                    FormLoader.LoadForm(dashboardForm.mainpanel, form);
                }
            }
        }

        private void ModifyRoute(string routeId, string startLocation, string endLocation, decimal distance, TimeSpan travelTime)
        {
            // Perform the database operation to delete the record
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = "UPDATE Routes SET StartLocation = @StartLocation, EndLocation = @EndLocation, Distance = @Distance, TravelTime = @TravelTime WHERE RouteID = @RouteID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartLocation", startLocation);
                    command.Parameters.AddWithValue("@EndLocation", endLocation);
                    command.Parameters.AddWithValue("@Distance", distance);
                    command.Parameters.AddWithValue("@TravelTime", travelTime);
                    command.Parameters.AddWithValue("@RouteID", routeId);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
