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

        private void btnSearchUser_Click(object sender, EventArgs e)
        {

        }

        private void btnModifyUser_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {

        }
    }
}
