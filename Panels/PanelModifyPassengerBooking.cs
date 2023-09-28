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
    public partial class PanelModifyPassengerBooking : Form
    {
        public PanelModifyPassengerBooking(string bookingId, string customerName,
                    string contactNumber, int tripId, int seatNumber, decimal fare)
        {
            InitializeComponent();

            // Assign values to TextBox controls
            txtBookingId.Text = bookingId;
            txtCustomerName.Text = customerName;
            txtContactNumber.Text = contactNumber;

            // Assign values to NumericUpDown controls
            numSeatNumber.Value = seatNumber;
            numFare.Value = fare;

            // Assign values to ComboBox control
            cbxTripId.SelectedValue = tripId;

            Form_Load();
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private ReceptionistForm FindReceptionistForm()
        {
            return FormUtils.FindParentForm<ReceptionistForm>(this);
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
            ReceptionistForm receptionistForm = FindReceptionistForm();

            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
            if (receptionistForm != null)
            {
                FormLoader.LoadForm(receptionistForm.mainpanel, form);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get the modified values from the form
            int bookingId = int.Parse(txtBookingId.Text);
            string customerName = txtCustomerName.Text;
            string contactNumber = txtContactNumber.Text;
            int tripId = (int)cbxTripId.SelectedValue;
            int seatNumber = (int)numSeatNumber.Value;
            decimal fare = numFare.Value;

            // Update the record in the database
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string sql = "UPDATE PassengerBookings SET CustomerName = @CustomerName, ContactNumber = @ContactNumber, " +
                             "TripID = @TripID, SeatNumber = @SeatNumber, Fare = @Fare WHERE BookingID = @BookingID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CustomerName", customerName);
                    command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    command.Parameters.AddWithValue("@TripID", tripId);
                    command.Parameters.AddWithValue("@SeatNumber", seatNumber);
                    command.Parameters.AddWithValue("@Fare", fare);
                    command.Parameters.AddWithValue("@BookingID", bookingId);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            MessageBox.Show("Passenger booking updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Navigate back to the PanelPassengerBookings form
            btnBack_Click(sender, e);
        }
    }
}
