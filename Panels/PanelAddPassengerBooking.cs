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
    public partial class PanelAddPassengerBooking : Form
    {
        public PanelAddPassengerBooking()
        {
            InitializeComponent();
            Form_Load();
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private void Form_Load()
        {
            // Retrieve the vehicle IDs from the database
            DataTable tripTable = GetTripIds();

            // Set the TripId ComboBox's DataSource and configure the value member
            cbxTripId.DataSource = tripTable;
            cbxTripId.ValueMember = "TripId";
        }

        private DataTable GetTripIds()
        {
            DataTable tripsTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sql = "SELECT TripId FROM Trips";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(tripsTable);
                    }
                }
            }

            return tripsTable;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Open a PanelPassengerBookings form
            Form form = new PanelPassengerBookings();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Get the values from the form
            string customerName = txtCustomerName.Text;
            string contactNumber = txtContactNumber.Text;
            int tripId = (int)cbxTripId.SelectedValue;
            int seatNumber = (int)numSeatNumber.Value;
            decimal fare = numFare.Value;

            // Insert the new record into the database
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sql = "INSERT INTO PassengerBookings (CustomerName, ContactNumber, TripID, SeatNumber, Fare) " +
                             "VALUES (@CustomerName, @ContactNumber, @TripID, @SeatNumber, @Fare)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CustomerName", customerName);
                    command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    command.Parameters.AddWithValue("@TripID", tripId);
                    command.Parameters.AddWithValue("@SeatNumber", seatNumber);
                    command.Parameters.AddWithValue("@Fare", fare);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            MessageBox.Show("Passenger booking added successfully.", "Add Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Open a PanelPassengerBookings form
            Form form = new PanelPassengerBookings();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }
    }
}
