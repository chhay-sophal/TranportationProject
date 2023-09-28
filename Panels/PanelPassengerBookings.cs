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
    public partial class PanelPassengerBookings : Form
    {
        public PanelPassengerBookings()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM PassengerBookings";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvPassengerBookings.DataSource = dataTable;

                connection.Close();
            }
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private ReceptionistForm FindReceptionistForm()
        {
            return FormUtils.FindParentForm<ReceptionistForm>(this);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchBox.Text.Trim();

            // Perform the search query using the searchText and display the result in the DataGridView
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM PassengerBookings WHERE " +
                    "BookingID LIKE @SearchText OR " +
                    "CustomerName LIKE @SearchText OR " +
                    "ContactNumber LIKE @SearchText OR " +
                    "TripID LIKE @SearchText OR " +
                    "SeatNumber LIKE @SearchText OR " +
                    "Fare LIKE @SearchText";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvPassengerBookings.DataSource = dataTable;

                connection.Close();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            // Check if a booking is selected in the DataGridView
            if (dgvPassengerBookings.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgvPassengerBookings.SelectedRows[0];

                // Retrieve the booking data from the selected row
                string selectedBookingId = selectedRow.Cells["BookingID"].Value.ToString();
                string selectedCustomerName = selectedRow.Cells["CustomerName"].Value.ToString();
                string selectedContactNumber = selectedRow.Cells["ContactNumber"].Value.ToString();
                int selectedTripId = Convert.ToInt32(selectedRow.Cells["TripID"].Value);
                int selectedSeatNumber = Convert.ToInt32(selectedRow.Cells["SeatNumber"].Value);
                decimal selectedFare = Convert.ToDecimal(selectedRow.Cells["Fare"].Value);

                // Switch to the PanelModifyPassengerBooking panel
                Form form = new PanelModifyPassengerBooking(selectedBookingId, selectedCustomerName,
                    selectedContactNumber, selectedTripId, selectedSeatNumber, selectedFare);
                DashboardForm dashboardForm = FindDashboardForm();
                ReceptionistForm receptionistForm = FindReceptionistForm();
                if (dashboardForm != null)
                {
                    FormLoader.LoadForm(dashboardForm.mainpanel, form);
                }
                if (receptionistForm != null)
                {
                    FormLoader.LoadForm(receptionistForm.mainpanel, form);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to modify.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPassengerBookings.SelectedRows.Count > 0)
            {
                // Confirm the deletion with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dgvPassengerBookings.SelectedRows[0];

                    // Get the value of the unique identifier for the selected record
                    int selectedBookingId = Convert.ToInt32(selectedRow.Cells["BookingID"].Value);

                    // Perform the database operation to delete the record
                    using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM PassengerBookings WHERE BookingID = @BookingId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@BookingId", selectedBookingId);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Remove the selected row from the DataGridView
                    dgvPassengerBookings.Rows.Remove(selectedRow);

                    // Display a message indicating successful deletion
                    MessageBox.Show("Record deleted successfully.", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Pleaseselect a row to delete.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Switch to the PanelAddPassengerBooking panel
            Form form = new PanelAddPassengerBooking();
            DashboardForm dashboardForm = FindDashboardForm();
            ReceptionistForm receptionistForm = FindReceptionistForm();

            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
            if (receptionistForm != null)
            {
                FormLoader.LoadForm(receptionistForm.mainpanel, form);
            }
        }
    }
}
