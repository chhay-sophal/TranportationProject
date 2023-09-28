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
    public partial class PanelAddVehicle : Form
    {
        public PanelAddVehicle()
        {
            InitializeComponent();
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private DispatcherForm FindDispatcherForm()
        {
            return FormUtils.FindParentForm<DispatcherForm>(this);
        }

        private void btnAddVehicleBack_Click(object sender, EventArgs e)
        {
            // Open a PanelVehicles form
            Form form = new PanelVehicles();
            DashboardForm dashboardForm = FindDashboardForm();
            DispatcherForm dispatcherForm = FindDispatcherForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
            if (dispatcherForm != null)
            {
                FormLoader.LoadForm(dispatcherForm.mainpanel, form);
            }
        }

        private void btnAddVehicleAdd_Click(object sender, EventArgs e)
        {
            string vehicleModel = txtVehicleModel.Text;
            string vehicleType = txtVehicleType.Text;
            string capacity = txtVehicleCapacity.Text;
            string licensePlate = txtLicensePlate.Text;

            // Validate the input
            if (string.IsNullOrWhiteSpace(vehicleModel) || string.IsNullOrWhiteSpace(vehicleType) || string.IsNullOrWhiteSpace(capacity) || string.IsNullOrWhiteSpace(licensePlate))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            if (!IsNumeric(capacity))
            {
                MessageBox.Show("Please enter a valid number for capacity.");
                return;
            }

            // Insert the vehicle data into the database
            InsertVehicle(vehicleModel, vehicleType, capacity, licensePlate);

            // Optionally, you can show a success message or perform other actions as needed
            MessageBox.Show("Vehicle added successfully!", "Add Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Open a PanelVehiles form
            Form form = new PanelVehicles();
            DashboardForm dashboardForm = FindDashboardForm();
            DispatcherForm dispatcherForm = FindDispatcherForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
            if (dispatcherForm != null)
            {
                FormLoader.LoadForm(dispatcherForm.mainpanel, form);
            }
        }

        private bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }

        private void InsertVehicle(string vehicleModel, string vehicleType, string capacity, string licensePlate)
        {
            string connectionString = DatabaseHelper.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Vehicles (Model, Type, Capacity, LicensePlate) VALUES (@Model, @Type, @Capacity, @LicensePlate)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Model", vehicleModel);
                    command.Parameters.AddWithValue("@Type", vehicleType);
                    command.Parameters.AddWithValue("@Capacity", capacity);
                    command.Parameters.AddWithValue("@LicensePlate", licensePlate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
