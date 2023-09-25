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
    public partial class PanelModifyEmployee : Form
    {

        public PanelModifyEmployee(string employeeId, string firstName, string lastName, string sex, string contactNumber, string role)
        {
            InitializeComponent();
            // Set the values in the text boxes
            txtEmployeeId.Text = employeeId;
            txtModifiedEmployeeFirstName.Text = firstName;
            txtModifiedEmployeeLastName.Text = lastName;
            cbxModifiedEmployeeSex.Text = sex;
            txtModifiedEmployeeContactNumber.Text = contactNumber;
            cbxModifiedEmployeeRole.Text = role;
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

        private void btnModifyEmployeeBack_Click(object sender, EventArgs e)
        {
            // Open a PanelEmployees form
            Form form = new PanelEmployees();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }

        private void btnModifyEmployeeSave_Click(object sender, EventArgs e)
        {
            // Get the values from the form's controls
            string employeeId = txtEmployeeId.Text;
            string modifiedFirstName = txtModifiedEmployeeFirstName.Text;
            string modifiedLastName = txtModifiedEmployeeLastName.Text;
            string modifiedSex = cbxModifiedEmployeeSex.SelectedItem.ToString();
            string modifiedContactNumber = txtModifiedEmployeeContactNumber.Text;
            string modifiedRole = cbxModifiedEmployeeRole.SelectedItem.ToString();

            // Validate the input
            if (string.IsNullOrWhiteSpace(modifiedFirstName) || string.IsNullOrWhiteSpace(modifiedLastName) || string.IsNullOrWhiteSpace(modifiedSex) ||
                string.IsNullOrWhiteSpace(modifiedContactNumber) || string.IsNullOrWhiteSpace(modifiedRole))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            if (!IsTextValid(modifiedFirstName) || !IsTextValid(modifiedLastName))
            {
                MessageBox.Show("First name and last name should contain only letters.");
                return;
            }

            if (!IsContactNumberValid(modifiedContactNumber))
            {
                MessageBox.Show("Contact number should be a numeric value with 9 or 10 digits.");
                return;
            }

            // Confirm the update with the user
            DialogResult result = MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Insert the employee data into the database
                ModifyEmployee(modifiedFirstName, modifiedLastName, modifiedSex, modifiedContactNumber, modifiedRole, employeeId);

                // Display a message indicating successful update
                MessageBox.Show("Record updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open a PanelEmployees form
                Form form = new PanelEmployees();
                DashboardForm dashboardForm = FindDashboardForm();
                if (dashboardForm != null)
                {
                    FormLoader.LoadForm(dashboardForm.mainpanel, form);
                }
            }
        }

        private void ModifyEmployee(string modifiedFirstName, string modifiedLastName, string modifiedSex, string modifiedContactNumber, string modifiedRole, string employeeId)
        {
            // Perform the database operation to delete the record
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Sex = @Sex, ContactNumber = @ContactNumber, Role = @Role WHERE EmployeeID = @EmployeeID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", modifiedFirstName);
                    command.Parameters.AddWithValue("@LastName", modifiedLastName);
                    command.Parameters.AddWithValue("@Sex", modifiedSex);
                    command.Parameters.AddWithValue("@ContactNumber", modifiedContactNumber);
                    command.Parameters.AddWithValue("@Role", modifiedRole);
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private bool IsTextValid(string text)
        {
            // Validate that the text contains only letters
            return !string.IsNullOrEmpty(text) && text.All(char.IsLetter);
        }

        private bool IsContactNumberValid(string number)
        {
            // Remove any non-digit characters from the number
            string cleanedNumber = new string(number.Where(char.IsDigit).ToArray());

            // Validate that the cleaned number has 9 or 10 digits
            return cleanedNumber.Length >= 9 && cleanedNumber.Length <= 10;
        }
    }
}
