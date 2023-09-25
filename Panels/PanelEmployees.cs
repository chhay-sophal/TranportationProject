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
    public partial class PanelEmployees : Form
    {
        public PanelEmployees()
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

                string sqlQuery = "SELECT * FROM Employees";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvEmployees.DataSource = dataTable;

                connection.Close();
            }
        }

        private void btnSearchEmployee_Click(object sender, EventArgs e)
        {
            string employeeId = txtEmployeeId.Text; // Assuming you have a TextBox named txtEmployeeId to input the employee ID

            // Perform the search query using the employeeId and display the result in the DataGridView
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvEmployees.DataSource = dataTable;

                connection.Close();
            }
        }

        private void btnModifyEmployee_Click(object sender, EventArgs e)
        {
            // Check if an employee is selected in the DataGridView
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgvEmployees.SelectedRows[0];

                // Retrieve the employee data from the selected row
                string selectedEmployeeId = selectedRow.Cells["EmployeeId"].Value.ToString();
                string selectedFirstName = selectedRow.Cells["FirstName"].Value.ToString();
                string selectedLastName = selectedRow.Cells["LastName"].Value.ToString();
                string selectedSex = selectedRow.Cells["Sex"].Value.ToString();
                string selectedContactNumber = selectedRow.Cells["ContactNumber"].Value.ToString();
                string selectedRole = selectedRow.Cells["Role"].Value.ToString();

                // Switch to the PanelModifyEmployee panel
                Form form = new PanelModifyEmployee(selectedEmployeeId, selectedFirstName, selectedLastName, selectedSex, selectedContactNumber, selectedRole);
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

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                // Confirm the deletion with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dgvEmployees.SelectedRows[0];

                    // Get the value of the unique identifier for the selected record
                    int selectedRecordId = Convert.ToInt32(selectedRow.Cells["EmployeeID"].Value);

                    // Perform the database operation to delete the record
                    using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM Employees WHERE EmployeeID = @RecordId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@RecordId", selectedRecordId);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    // Remove the selected row from the DataGridView
                    dgvEmployees.Rows.Remove(selectedRow);

                    // Display a message indicating successful deletion
                    MessageBox.Show("Record deleted successfully.", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            // Switch to the PanelAddEmployee panel
            Form form = new PanelAddEmployee();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }

        private DashboardForm FindDashboardForm()
        {
            Control parent = this.Parent;
            while (parent != null)
            {
                if (parent is DashboardForm dashboardForm)
                {
                    return dashboardForm;
                }
                parent = parent.Parent;
            }
            return null;
        }
    }
}
