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
    public partial class PanelAddUsers : Form
    {
        public PanelAddUsers()
        {
            InitializeComponent();
            SetupEmployeeComboBox();
        }

        private void SetupEmployeeComboBox()
        {
            // Assuming you have a ComboBox named cmbEmployeeId
            cbxUserEmployeeId.DisplayMember = "DisplayName"; // DisplayMember will show the concatenated value of ID, first name, and last name
            cbxUserEmployeeId.ValueMember = "EmployeeID"; // ValueMember will store only the EmployeeID
            cbxUserEmployeeId.DataSource = LoadEmployeeData(); // Load the employee data from the database
        }

        private DataTable LoadEmployeeData()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                // Retrieve employee IDs and names that don't exist in the user table
                string sqlQuery = "SELECT e.EmployeeID, CONCAT(e.EmployeeID, ' - ', e.FirstName, ' ', e.LastName) AS DisplayName " +
                                  "FROM Employees e " +
                                  "WHERE NOT EXISTS (SELECT 1 FROM Users u WHERE u.EmployeeID = e.EmployeeID)";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);

                connection.Close();
            }

            return dataTable;
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private void btnAddUserBack_Click(object sender, EventArgs e)
        {
            // Open a PanelEmployees form
            Form form = new PanelUsers();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }

        private void btnAddUserAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the selected employeeId value
                string employeeId = cbxUserEmployeeId.SelectedValue?.ToString();

                // Check if employeeId is null or empty
                if (string.IsNullOrWhiteSpace(employeeId))
                {
                    MessageBox.Show("Please select an employee.", "Missing Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Retrieve other values from input controls
                string username = txtUserUsername.Text;
                string password = txtUserPassword.Text;
                string role = cbxUserRole.Text;

                // Validate the input
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
                {
                    MessageBox.Show("Please fill in all the fields.", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Insert the user data into the database
                InsertUser(employeeId, username, password, role);

                // Optionally, you can show a success message or perform other actions as needed
                MessageBox.Show("User added successfully!", "Add Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open a PanelUsers form
                Form form = new PanelUsers();
                DashboardForm dashboardForm = FindDashboardForm();
                if (dashboardForm != null)
                {
                    FormLoader.LoadForm(dashboardForm.mainpanel, form);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertUser(string employeeId, string username, string password, string role)
        {
            string connectionString = DatabaseHelper.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (EmployeeID, Username, Password, Role) VALUES (@EmployeeID, @Username, @Password, @Role)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Role", role);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
