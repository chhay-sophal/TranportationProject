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
    public partial class PanelFreightShipments : Form
    {
        public PanelFreightShipments()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM FreightShipments";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvFreightShipments.DataSource = dataTable;

                connection.Close();
            }
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchBox.Text.Trim();

            // Perform the search query using the employeeId and display the result in the DataGridView
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM FreightShipments WHERE " +
                    "ShipmentID LIKE @SearchText OR " +
                    "CustomerName LIKE @SearchText OR " +
                    "ContactNumber LIKE @SearchText OR " +
                    "TripID LIKE @SearchText OR " +
                    "Weight LIKE @SearchText OR " +
                    "GoodsType LIKE @SearchText OR " +
                    "PickupLocation LIKE @SearchText OR " +
                    "DeliveryLocation LIKE @SearchText";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvFreightShipments.DataSource = dataTable;

                connection.Close();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            // Check if a route is selected in the DataGridView
            if (dgvFreightShipments.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgvFreightShipments.SelectedRows[0];

                // Retrieve the route data from the selected row
                string selectedShipmentId = selectedRow.Cells["ShipmentID"].Value.ToString();
                string selectedCustomerName = selectedRow.Cells["CustomerName"].Value.ToString();
                string selectedContactNumber = selectedRow.Cells["ContactNumber"].Value.ToString();
                int selectedTripId = Convert.ToInt32(selectedRow.Cells["TripID"].Value);
                decimal selectedWeight = Convert.ToInt32(selectedRow.Cells["Weight"].Value);
                string selectedGoodsType = selectedRow.Cells["GoodsType"].Value.ToString();
                string selectedPickupLocation = selectedRow.Cells["PickupLocation"].Value.ToString();
                string selectedDeliveryLocation = selectedRow.Cells["DeliveryLocation"].Value.ToString();


                // Switch to the PanelModifyRoute panel
                Form form = new PanelModifyFreightShipments(selectedShipmentId, selectedCustomerName, 
                    selectedContactNumber, selectedTripId, selectedWeight, selectedGoodsType,
                    selectedPickupLocation, selectedDeliveryLocation);
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
            if (dgvFreightShipments.SelectedRows.Count > 0)
            {
                // Confirm the deletion with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dgvFreightShipments.SelectedRows[0];

                    // Get the value of the unique identifier for the selected record
                    int selectedRecordId = Convert.ToInt32(selectedRow.Cells["ShipmentID"].Value);

                    // Perform the database operation to delete the record
                    using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM FreightShipments WHERE ShipmentID = @RecordId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@RecordId", selectedRecordId);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Remove the selected row from the DataGridView
                    dgvFreightShipments.Rows.Remove(selectedRow);

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
            // Switch to the PanelAddRoute panel
            Form form = new PanelAddFreightShipment();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }
    }
}
