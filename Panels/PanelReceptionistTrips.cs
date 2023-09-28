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

namespace TransportationProject.Panels
{
    public partial class PanelReceptionistTrips : Form
    {
        public PanelReceptionistTrips()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Trips";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvTrips.DataSource = dataTable;

                connection.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchBox.Text.Trim(); // Assuming you have a TextBox named txtEmployeeId to input the employee ID

            // Perform the search query using the employeeId and display the result in the DataGridView
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Trips WHERE " +
                    "TripID LIKE @SearchText OR " +
                    "VehicleID LIKE @SearchText OR " +
                    "DriverID LIKE @SearchText OR " +
                    "StartTime LIKE @SearchText OR " +
                    "EndTime LIKE @SearchText OR " +
                    "RouteID LIKE @SearchText";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dgvTrips.DataSource = dataTable;

                connection.Close();
            }
        }
    }
}
