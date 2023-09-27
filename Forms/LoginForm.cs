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
using TransportationProject.Database;

namespace TransportationProject.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Check if the entered username and password match the database records
            string role = ValidateUserCredentials(username, password);

            if (role == "Administrator")
            {
                // Admin user, navigate to DashboardForm
                DashboardForm dashboardForm = new DashboardForm(username);
                dashboardForm.Show();
                Hide();
            }
            else if (role == "Dispatcher")
            {
                // Dispatcher user, navigate to DispatcherForm
                DispatcherForm dispatcherForm = new DispatcherForm(username);
                dispatcherForm.Show();
                Hide();
            }
            else if (role == "Receptionist")
            {
                // Receptionist user, navigate to ReceptionistForm
                ReceptionistForm receptionistForm = new ReceptionistForm(username);
                receptionistForm.Show();
                Hide();
            }
            else
            {
                // Invalid username or password
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Clear the password field
                txtPassword.Text = string.Empty;
                txtPassword.Focus();
            }
        }

        private string ValidateUserCredentials(string username, string password)
        {
            string role = "";

            // TODO: Replace with your own logic to validate the user credentials against the database
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sql = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        role = result.ToString();
                    }
                }

                connection.Close();
            }

            return role;
        }
    }
}
