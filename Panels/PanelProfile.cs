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
    public partial class PanelProfile : Form
    {
        public PanelProfile(string loggedInUsername)
        {
            InitializeComponent();
            LoadData(loggedInUsername);
        }

        private void LoadData(string loggedInUsername)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Users JOIN Employees ON Users.EmployeeId = Employees.EmployeeId WHERE Users.Username = @Username";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Username", loggedInUsername); // Replace loggedInUserId with the actual logged-in user's ID
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    txtUserId.Text = reader["UserId"].ToString();
                    txtEmployeeId.Text = reader["EmployeeId"].ToString();
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtLastName.Text = reader["LastName"].ToString();
                    txtContactNumber.Text = reader["ContactNumber"].ToString();
                    txtUsername.Text = reader["Username"].ToString();
                    txtRole.Text = reader["Role"].ToString();
                }

                reader.Close();
                connection.Close();
            }
        }
    }
}
