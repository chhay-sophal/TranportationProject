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
    public partial class PanelTrips : Form
    {
        public PanelTrips()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Trips";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvTrips.DataSource = dataTable;

                connection.Close();
            }
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchBox.Text.Trim(); // Assuming you have a TextBox named txtEmployeeId to input the employee ID

            // Perform the search query using the employeeId and display the result in the DataGridView
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Trips WHERE " +
                    "TripsID LIKE @SearchText OR " +
                    "VehicleID LIKE @SearchText OR " +
                    "Driver LIKE @SearchText OR " +
                    "StartTime LIKE @SearchText OR " +
                    "EndTime LIKE @SearchText OR " +
                    "RouteID LIKE @SearchText";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvTrips.DataSource = dataTable;

                connection.Close();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            // Check if a trip is selected in the DataGridView
            if (dgvTrips.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgvTrips.SelectedRows[0];

                // Retrieve the trip data from the selected row
                string selectedTripID = selectedRow.Cells["TripID"].Value.ToString();
                int selectedVehicleID = Convert.ToInt32(selectedRow.Cells["VehicleID"].Value);
                int selectedDriverID = Convert.ToInt32(selectedRow.Cells["DriverID"].Value);
                DateTime selectedStartTime = Convert.ToDateTime(selectedRow.Cells["StartTime"].Value);
                DateTime selectedEndTime = Convert.ToDateTime(selectedRow.Cells["EndTime"].Value);
                int selectedRouteID = Convert.ToInt32(selectedRow.Cells["RouteID"].Value);

                // Switch to the PanelModifyTrip panel
                Form form = new PanelModifyTrip(selectedTripID, selectedVehicleID, selectedDriverID, selectedStartTime, selectedEndTime, selectedRouteID);
                DashboardForm dashboardForm = FindDashboardForm();
                if (dashboardForm != null)
                {
                    FormLoader.LoadForm(dashboardForm.mainpanel, form);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to modify.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTrips.SelectedRows.Count > 0)
            {
                // Confirm the deletion with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dgvTrips.SelectedRows[0];

                    // Get the value of the unique identifier for the selected record
                    int selectedRecordId = Convert.ToInt32(selectedRow.Cells["TripID"].Value);

                    // Perform the database operation to delete the record
                    using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM Trips WHERE TripID = @RecordId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@RecordId", selectedRecordId);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Remove the selected row from the DataGridView
                    dgvTrips.Rows.Remove(selectedRow);

                    // Display a message indicating successful deletion
                    MessageBox.Show("Record deleted successfully.", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form form = new PanelAddTrip();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }
    }
}
