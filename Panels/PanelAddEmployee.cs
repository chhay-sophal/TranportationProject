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
    public partial class PanelAddEmployee : Form
    {
        public PanelAddEmployee()
        {
            InitializeComponent();
        }

        private void btnAddEmployeeBack_Click(object sender, EventArgs e)
        {
            // Open a PanelEmployees form
            Form form = new PanelEmployees();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }

        private void btnAddEmployeeAdd_Click(object sender, EventArgs e)
        {
            string firstName = txtEmployeeFirstName.Text;
            string lastName = txtEmployeeLastName.Text;
            string sex = cbxEmployeeSex.SelectedItem?.ToString();
            string contactNumber = txtEmployeeContactNumber.Text;
            string role = cbxEmployeeRole.SelectedItem?.ToString();

            // Validate the input
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(sex) ||
                string.IsNullOrWhiteSpace(contactNumber) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            if (!IsTextValid(firstName) || !IsTextValid(lastName))
            {
                MessageBox.Show("First name and last name should contain only letters.");
                return;
            }

            if (!IsContactNumberValid(contactNumber))
            {
                MessageBox.Show("Contact number should be a numeric value with 9 or 10 digits.");
                return;
            }

            // Insert the employee data into the database
            InsertEmployee(firstName, lastName, sex, contactNumber, role);

            // Optionally, you can show a success message or perform other actions as needed
            MessageBox.Show("Employee added successfully!", "Add Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Open a PanelEmployees form
            Form form = new PanelEmployees();
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

        private void InsertEmployee(string firstName, string lastName, string sex, string contactNumber, string role)
        {
            string connectionString = DatabaseHelper.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Employees (FirstName, LastName, Sex, ContactNumber, Role) " +
                               "VALUES (@FirstName, @LastName, @Sex, @ContactNumber, @Role)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Sex", sex);
                    command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    command.Parameters.AddWithValue("@Role", role);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
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
