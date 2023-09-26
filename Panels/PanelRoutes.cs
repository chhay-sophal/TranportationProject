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
    public partial class PanelRoutes : Form
    {
        public PanelRoutes()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT RouteID, StartLocation, EndLocation, CONCAT(Distance, ' Km') AS Distance, CONCAT(DATEPART(HOUR, TravelTime), ' hour') AS TravelTime FROM Routes";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvRoutes.DataSource = dataTable;

                connection.Close();
            }
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private void btnSearchRoutes_Click(object sender, EventArgs e)
        {
            string searchText = txtRoutesSearchBox.Text.Trim(); 

            // Perform the search query using the employeeId and display the result in the DataGridView
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Routes WHERE " +
                    "RouteID LIKE @SearchText OR " +
                    "StartLocation LIKE @SearchText OR " +
                    "EndLocation LIKE @SearchText OR " +
                    "Distance LIKE @SearchText OR " +
                    "TravelTime LIKE @SearchText";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvRoutes.DataSource = dataTable;

                connection.Close();
            }
        }

        private void btnModifyRoute_Click(object sender, EventArgs e)
        {
            // Check if a route is selected in the DataGridView
            if (dgvRoutes.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgvRoutes.SelectedRows[0];

                // Retrieve the route data from the selected row
                string selectedRouteId = selectedRow.Cells["RouteID"].Value.ToString();
                string selectedStartLocation = selectedRow.Cells["StartLocation"].Value.ToString();
                string selectedEndLocation = selectedRow.Cells["EndLocation"].Value.ToString();
                string selectedDistance = selectedRow.Cells["Distance"].Value.ToString();
                string selectedTravelTime = selectedRow.Cells["TravelTime"].Value.ToString();

                // Switch to the PanelModifyRoute panel
                Form form = new PanelModifyRoute(selectedRouteId, selectedStartLocation, selectedEndLocation, selectedDistance, selectedTravelTime);
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

        private void btnDeleteRoute_Click(object sender, EventArgs e)
        {
            if (dgvRoutes.SelectedRows.Count > 0)
            {
                // Confirm the deletion with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dgvRoutes.SelectedRows[0];

                    // Get the value of the unique identifier for the selected record
                    int selectedRecordId = Convert.ToInt32(selectedRow.Cells["RouteID"].Value);

                    // Perform the database operation to delete the record
                    using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM Routes WHERE RouteID = @RecordId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@RecordId", selectedRecordId);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Remove the selected row from the DataGridView
                    dgvRoutes.Rows.Remove(selectedRow);

                    // Display a message indicating successful deletion
                    MessageBox.Show("Record deleted successfully.", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddRoute_Click(object sender, EventArgs e)
        {
            // Switch to the PanelAddRoute panel
            Form form = new PanelAddRoute();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }
    }
}
