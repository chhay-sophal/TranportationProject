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
    public partial class PanelAddFreightShipment : Form
    {
        public PanelAddFreightShipment()
        {
            InitializeComponent();
            Form_Load();
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private DispatcherForm FindDispatcherForm()
        {
            return FormUtils.FindParentForm<DispatcherForm>(this);
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
            // Open a PanelRoutes form
            Form form = new PanelRoutes();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string contactNumber = txtContactNumber.Text;
            int tripId = (int)cbxTripId.SelectedValue;
            decimal weight;
            if (!decimal.TryParse(txtWeight.Text, out weight))
            {
                MessageBox.Show("Please enter a valid weight.");
                return;
            }
            string goodsType = txtGoodsType.Text;
            string pickupLocation = txtPickupLocation.Text;
            string deliveryLocation = txtDeliveryLocation.Text;

            // Validate the input
            if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(contactNumber) || 
                tripId == 0 || weight == 0 || string.IsNullOrWhiteSpace(goodsType) || 
                string.IsNullOrWhiteSpace(pickupLocation) || string.IsNullOrWhiteSpace(deliveryLocation))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            // Insert the freight shipment data into the database
            InsertFreightShipment(customerName, contactNumber, tripId, weight, goodsType, pickupLocation, deliveryLocation);

            // Optionally, you can show a success message or perform other actions as needed
            MessageBox.Show("Freight shipment added successfully!", "Add Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Open a PanelFreightShipments form
            Form form = new PanelFreightShipments();
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

        private void InsertFreightShipment(string customerName, string contactNumber,
            int tripId, decimal weight, string goodsType, string pickupLocation, string deliveryLocation)
        {
            string connectionString = DatabaseHelper.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO FreightShipments (CustomerName, ContactNumber, TripId, Weight, GoodsType, PickupLocation, DeliveryLocation) " +
                               "VALUES (@CustomerName, @ContactNumber, @TripId, @Weight, @GoodsType, @PickupLocation, @DeliveryLocation)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerName", customerName);
                    command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    command.Parameters.AddWithValue("@TripId", tripId);
                    command.Parameters.AddWithValue("@Weight", weight);
                    command.Parameters.AddWithValue("@GoodsType", goodsType);
                    command.Parameters.AddWithValue("@PickupLocation", pickupLocation);
                    command.Parameters.AddWithValue("@DeliveryLocation", deliveryLocation);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
