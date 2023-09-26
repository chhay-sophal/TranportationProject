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
    public partial class PanelModifyTrip : Form
    {
        public PanelModifyTrip(string tripId, int vehicleId, int driverId, DateTime startTime, DateTime endTime, int routeId)
        {
            InitializeComponent();

            // Assign values to TextBox controls
            txtTripId.Text = tripId;

            // Assign values to ComboBox controls
            cbxVehicleId.SelectedValue = vehicleId;
            cbxDriverId.SelectedValue = driverId;
            cbxRouteId.SelectedValue = routeId;

            // Assign values to DateTimePicker controls
            dtpStartTime.Value = startTime;
            dtpEndTime.Value = endTime;

            Form_Load();
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private void Form_Load()
        {
            // Retrieve the vehicle IDs from the database
            DataTable vehiclesTable = GetVehicleIds();

            // Set the VehicleId ComboBox's DataSource and configure the value member
            cbxVehicleId.DataSource = vehiclesTable;
            cbxVehicleId.DisplayMember = "Type";
            cbxVehicleId.ValueMember = "VehicleId";

            // Retrieve the driver names from the database
            DataTable driversTable = GetDriverNames();

            // Set the ComboBox's DataSource and configure the display and value member
            cbxDriverId.DataSource = driversTable;
            cbxDriverId.DisplayMember = "FullName";
            cbxDriverId.ValueMember = "EmployeeId";

            // Retrieve the route IDs from the database
            DataTable routesTable = GetRouteIds();

            // Set the RouteId ComboBox's DataSource and configure the value member
            cbxRouteId.DataSource = routesTable;
            cbxRouteId.DisplayMember = "Distination";
            cbxRouteId.ValueMember = "RouteId";
        }

        private DataTable GetVehicleIds()
        {
            DataTable vehiclesTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sql = "SELECT VehicleId, CONCAT(VehicleId, ': ', Type) AS Type FROM Vehicles";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(vehiclesTable);
                    }
                }
            }

            return vehiclesTable;
        }

        private DataTable GetDriverNames()
        {
            DataTable driversTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sql = "SELECT EmployeeId, CONCAT(EmployeeID, ': ', FirstName, ' ', LastName) AS FullName FROM Employees WHERE Role = 'Driver'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(driversTable);
                    }
                }
            }

            return driversTable;
        }

        private DataTable GetRouteIds()
        {
            DataTable routesTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sql = "SELECT RouteId, CONCAT(RouteId, ': ', StartLocation, ' - ', EndLocation) AS Distination FROM Routes";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(routesTable);
                    }
                }
            }

            return routesTable;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Open a PanelTrips form
            Form form = new PanelTrips();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Retrieve the modified values from the form's controls
            string tripId = txtTripId.Text;
            int vehicleId = Convert.ToInt32(cbxVehicleId.SelectedValue);
            int driverId = Convert.ToInt32(cbxDriverId.SelectedValue);
            DateTime startTime = dtpStartTime.Value;
            DateTime endTime = dtpEndTime.Value;
            int routeId = Convert.ToInt32(cbxRouteId.SelectedValue);

            // Confirm the update with the user
            DialogResult result = MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Insert the trip data into the database
                ModifyTrip(tripId, vehicleId, driverId, startTime, endTime, routeId);

                // Display a message indicating successful update
                MessageBox.Show("Record updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open a PanelTrips form
                Form form = new PanelTrips();
                DashboardForm dashboardForm = FindDashboardForm();
                if (dashboardForm != null)
                {
                    FormLoader.LoadForm(dashboardForm.mainpanel, form);
                }
            }
        }

        private void ModifyTrip(string tripId, int vehicleId, int driverId, DateTime startTime, DateTime endTime, int routeId)
        {
            // Perform the database operation to delete the record
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                // Define the SQL update command with parameters
                string sql = "UPDATE Trips SET VehicleId = @VehicleId, DriverId = @DriverId, StartTime = @StartTime, EndTime = @EndTime, RouteId = @RouteId WHERE TripId = @TripId";

                // Create a SqlCommand object with the SQL command and connection
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@VehicleId", vehicleId);
                    command.Parameters.AddWithValue("@DriverId", driverId);
                    command.Parameters.AddWithValue("@StartTime", startTime);
                    command.Parameters.AddWithValue("@EndTime", endTime);
                    command.Parameters.AddWithValue("@RouteId", routeId);
                    command.Parameters.AddWithValue("@TripId", tripId);

                    // Execute the SQL command
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
