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
    public partial class PanelModifyFreightShipments : Form
    {
        public PanelModifyFreightShipments(string shipmentId, string customerName,
                    string contactNumber, int tripId, decimal weight, string goodsType,
                    string pickupLocation, string deliveryLocation)
        {
            InitializeComponent();

            // Assign values to TextBox controls
            txtShipmentId.Text = shipmentId;
            txtCustomerName.Text = customerName;
            txtContactNumber.Text = contactNumber;
            txtWeight.Text = weight.ToString();
            txtGoodsType.Text = goodsType;
            txtPickupLocation.Text = pickupLocation;
            txtDeliveryLocation.Text = deliveryLocation;

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get the values from the form's controls
            string shipmentId = txtShipmentId.Text;
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
            if (string.IsNullOrWhiteSpace(shipmentId) || string.IsNullOrWhiteSpace(customerName) ||
                string.IsNullOrWhiteSpace(contactNumber) || tripId == 0 || weight == 0 ||
                string.IsNullOrWhiteSpace(goodsType) || string.IsNullOrWhiteSpace(pickupLocation) ||
                string.IsNullOrWhiteSpace(deliveryLocation))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            // Confirm the update with the user
            DialogResult result = MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Perform the database operation to update the freight shipment
                UpdateFreightShipment(shipmentId, customerName, contactNumber, tripId, weight, goodsType, pickupLocation, deliveryLocation);

                // Display a message indicating successful update
                MessageBox.Show("Record updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        }

        private void UpdateFreightShipment(string shipmentId, string customerName, string contactNumber,
    int tripId, decimal weight, string goodsType, string pickupLocation, string deliveryLocation)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = @"UPDATE FreightShipments 
                        SET CustomerName = @CustomerName,
                            ContactNumber = @ContactNumber,
                            TripId = @TripId,
                            Weight = @Weight,
                            GoodsType = @GoodsType,
                            PickupLocation = @PickupLocation,
                            DeliveryLocation = @DeliveryLocation
                        WHERE ShipmentId = @ShipmentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerName", customerName);
                    command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    command.Parameters.AddWithValue("@TripId", tripId);
                    command.Parameters.AddWithValue("@Weight", weight);
                    command.Parameters.AddWithValue("@GoodsType", goodsType);
                    command.Parameters.AddWithValue("@PickupLocation", pickupLocation);
                    command.Parameters.AddWithValue("@DeliveryLocation", deliveryLocation);
                    command.Parameters.AddWithValue("@ShipmentId", shipmentId);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
