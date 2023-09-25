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
    public partial class PanelUsers : Form
    {
        public PanelUsers()
        {
            InitializeComponent();
            LoadData();
        }

        private void PanelEmployees_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Users";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvUsers.DataSource = dataTable;

                connection.Close();
            }
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private void btnSearchUser_Click(object sender, EventArgs e)
        {
            string searchText = txtUsersSearchBox.Text.Trim(); // Assuming you have a TextBox named txtEmployeeId to input the employee ID

            // Perform the search query using the employeeId and display the result in the DataGridView
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Users WHERE " +
                    "UserID LIKE @SearchText OR " +
                    "EmployeeID LIKE @SearchText OR " +
                    "Username LIKE @SearchText OR " +
                    "Password LIKE @SearchText OR " +
                    "Role LIKE @SearchText";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvUsers.DataSource = dataTable;

                connection.Close();
            }
        }

        private void btnModifyUser_Click(object sender, EventArgs e)
        {
            // Check if an employee is selected in the DataGridView
            if (dgvUsers.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgvUsers.SelectedRows[0];

                // Retrieve the employee data from the selected row
                string selectedUserId = selectedRow.Cells["UserID"].Value.ToString();
                string selectedUsername = selectedRow.Cells["Username"].Value.ToString();
                string selectedPassword = selectedRow.Cells["Password"].Value.ToString();
                string selectedEmployeeId = selectedRow.Cells["EmployeeID"].Value.ToString();
                string selectedRole = selectedRow.Cells["Role"].Value.ToString();

                // Switch to the PanelModifyEmployee panel
                Form form = new PanelModifyUser(selectedUserId, selectedEmployeeId, selectedUsername, selectedPassword, selectedRole);
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

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                // Confirm the deletion with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dgvUsers.SelectedRows[0];

                    // Get the value of the unique identifier for the selected record
                    int selectedRecordId = Convert.ToInt32(selectedRow.Cells["UserID"].Value);

                    // Perform the database operation to delete the record
                    using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM Users WHERE UserID = @RecordId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@RecordId", selectedRecordId);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Remove the selected row from the DataGridView
                    dgvUsers.Rows.Remove(selectedRow);

                    // Display a message indicating successful deletion
                    MessageBox.Show("Record deleted successfully.", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            // Switch to the PanelAddEmployee panel
            Form form = new PanelAddUsers();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }
    }
}
