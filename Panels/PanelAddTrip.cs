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
    public partial class PanelAddTrip : Form
    {
        public PanelAddTrip()
        {
            InitializeComponent();
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
            // Open a PanelEmployees form
            Form form = new PanelEmployees();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Retrieve the values from the form's controls
            int vehicleId = Convert.ToInt32(cbxVehicleId.SelectedValue);
            int driverId = Convert.ToInt32(cbxDriverId.SelectedValue);
            DateTime startTime = dtpStartTime.Value;
            DateTime endTime = dtpEndTime.Value;
            int routeId = Convert.ToInt32(cbxRouteId.SelectedValue);

            // Validate that all fields are filled
            if (vehicleId == 0 || driverId == 0 || startTime == DateTime.MinValue || endTime == DateTime.MinValue || routeId == 0)
            {
                MessageBox.Show("Please fill in all fields.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the event handler if validation fails
            }

            // Insert the new trip into the database
            InsertTrip(vehicleId, driverId, startTime, endTime, routeId);

            // Optionally, you can show a success message or perform other actions as needed
            MessageBox.Show("Trip added successfully!", "Add Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Open a PanelTrips form
            Form form = new PanelTrips();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }

        private void InsertTrip(int vehicleId, int driverId, DateTime startTime, DateTime endTime, int routeId)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Trips (VehicleId, DriverId, StartTime, EndTime, RouteId) VALUES (@VehicleId, @DriverId, @StartTime, @EndTime, @RouteId)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@VehicleId", vehicleId);
                    command.Parameters.AddWithValue("@DriverId", driverId);
                    command.Parameters.AddWithValue("@StartTime", startTime);
                    command.Parameters.AddWithValue("@EndTime", endTime);
                    command.Parameters.AddWithValue("@RouteId", routeId);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
