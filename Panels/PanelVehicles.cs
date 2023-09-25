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
    public partial class PanelVehicles : Form
    {
        public PanelVehicles()
        {
            InitializeComponent();
            LoadData();
        }

        private void PanelVehicles_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT VehicleID, Model, Type, Capacity, LicensePlate FROM Vehicles";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvVehicles.DataSource = dataTable;

                connection.Close();
            }
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private void btnSearchVehicle_Click(object sender, EventArgs e)
        {
            string searchText = txtVehiclesSearchBox.Text.Trim(); // Assuming you have a TextBox named txtEmployeeId to input the employee ID

            // Perform the search query using the employeeId and display the result in the DataGridView
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Vehicles WHERE " +
                    "VehicleID LIKE @SearchText OR " +
                    "Model LIKE @SearchText OR " +
                    "Type LIKE @SearchText OR " +
                    "Capacity LIKE @SearchText OR " +
                    "LicensePlate LIKE @SearchText";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvVehicles.DataSource = dataTable;

                connection.Close();
            }
        }

        private void btnModifyVehicle_Click(object sender, EventArgs e)
        {
            // Check if an employee is selected in the DataGridView
            if (dgvVehicles.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgvVehicles.SelectedRows[0];

                // Retrieve the employee data from the selected row
                string selectedVehicleId = selectedRow.Cells["VehicleID"].Value.ToString();
                string selectedModel = selectedRow.Cells["Model"].Value.ToString();
                string selectedType = selectedRow.Cells["Type"].Value.ToString();
                string selectedCapacity = selectedRow.Cells["Capacity"].Value.ToString();
                string selectedLicensePlate = selectedRow.Cells["LicensePlate"].Value.ToString();

                // Switch to the PanelModifyEmployee panel
                Form form = new PanelModifyVehicle(selectedVehicleId, selectedModel, selectedType, selectedCapacity, selectedLicensePlate);
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

        private void btnDeleteVehicle_Click(object sender, EventArgs e)
        {
            if (dgvVehicles.SelectedRows.Count > 0)
            {
                // Confirm the deletion with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dgvVehicles.SelectedRows[0];

                    // Get the value of the unique identifier for the selected record
                    int selectedRecordId = Convert.ToInt32(selectedRow.Cells["VehicleID"].Value);

                    // Perform the database operation to delete the record
                    using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM Vehicles WHERE VehicleID = @RecordId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@RecordId", selectedRecordId);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Remove the selected row from the DataGridView
                    dgvVehicles.Rows.Remove(selectedRow);

                    // Display a message indicating successful deletion
                    MessageBox.Show("Record deleted successfully.", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            // Switch to the PanelAddEmployee panel
            Form form = new PanelAddVehicle();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }
    }
}
