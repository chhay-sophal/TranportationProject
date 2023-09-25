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
    public partial class PanelModifyUser : Form
    {
        public PanelModifyUser(string userId, string employeeId, string username, string password, string role)
        {
            InitializeComponent();
            // Set the values in the text boxes
            txtUserId.Text = userId;
            txtModifiedUserEmployeeId.Text = employeeId;
            txtModifiedUserUsername.Text = username;
            txtModifiedUserPassword.Text = password;
            cbxModifiedUserRole.Text = role;
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private void btnModifyUserBack_Click(object sender, EventArgs e)
        {
            // Open a PanelEmployees form
            Form form = new PanelUsers();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }

        private void btnModifyUserSave_Click(object sender, EventArgs e)
        {
            // Get the values from the form's controls
            string userId = txtUserId.Text;
            string modifiedEmployeeId = txtModifiedUserEmployeeId.Text;
            string modifiedUsername = txtModifiedUserUsername.Text;
            string modifiedPassword = txtModifiedUserPassword.Text;
            string modifiedRole = cbxModifiedUserRole.SelectedItem.ToString();

            // Validate the input
            if (string.IsNullOrWhiteSpace(modifiedEmployeeId) || string.IsNullOrWhiteSpace(modifiedUsername) ||
                string.IsNullOrWhiteSpace(modifiedPassword) || string.IsNullOrWhiteSpace(modifiedRole))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            // Confirm the update with the user
            DialogResult result = MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Insert the employee data into the database
                ModifyUser(modifiedEmployeeId, modifiedUsername, modifiedPassword, modifiedRole, userId);

                // Display a message indicating successful update
                MessageBox.Show("Record updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open a PanelEmployees form
                Form form = new PanelUsers();
                DashboardForm dashboardForm = FindDashboardForm();
                if (dashboardForm != null)
                {
                    FormLoader.LoadForm(dashboardForm.mainpanel, form);
                }
            }
        }

        private void ModifyUser(string modifiedEmployeeId, string modifiedUsername, string modifiedPassword, string modifiedRole, string userId)
        {
            // Perform the database operation to delete the record
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = "UPDATE Users SET EmployeeID = @EmployeeID, Username = @Username, Password = @Password, Role = @Role WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", modifiedEmployeeId);
                    command.Parameters.AddWithValue("@Username", modifiedUsername);
                    command.Parameters.AddWithValue("@Password", modifiedPassword);
                    command.Parameters.AddWithValue("@Role", modifiedRole);
                    command.Parameters.AddWithValue("@UserID", userId);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
