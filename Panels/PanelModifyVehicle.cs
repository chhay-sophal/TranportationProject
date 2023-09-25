using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransportationProject.Classes;
using TransportationProject.Database;
using TransportationProject.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TransportationProject.Panels
{
    public partial class PanelModifyVehicle : Form
    {
        public PanelModifyVehicle(string vehicleId, string model, string type, string capacity, string licensePlate)
        {
            InitializeComponent();
            // Set the values in the text boxes
            txtVehicleId.Text = vehicleId;
            txtModifiedVehicleModel.Text = model;
            txtModifiedVehicleType.Text = type;
            txtModifiedVehicleCapacity.Text = capacity;
            txtModifiedVehicleLicensePlate.Text = licensePlate;
        }

        private DashboardForm FindDashboardForm()
        {
            return FormUtils.FindParentForm<DashboardForm>(this);
        }

        private void btnModifyVehicleBack_Click(object sender, EventArgs e)
        {
            // Open a PanelEmployees form
            Form form = new PanelVehicles();
            DashboardForm dashboardForm = FindDashboardForm();
            if (dashboardForm != null)
            {
                FormLoader.LoadForm(dashboardForm.mainpanel, form);
            }
        }

        private void btnModifyVehicleSave_Click(object sender, EventArgs e)
        {
            // Get the values from the form's controls
            string vehicleId = txtVehicleId.Text;
            string modifiedModel = txtModifiedVehicleModel.Text;
            string modifiedType = txtModifiedVehicleType.Text;
            string modifiedCapacity = txtModifiedVehicleCapacity.Text;
            string modifiedLicensePlate = txtModifiedVehicleLicensePlate.Text;

            // Validate the input
            if (string.IsNullOrWhiteSpace(modifiedModel) || string.IsNullOrWhiteSpace(modifiedType) ||
                string.IsNullOrWhiteSpace(modifiedCapacity) || string.IsNullOrWhiteSpace(modifiedLicensePlate))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            if (!IsNumeric(modifiedCapacity))
            {
                MessageBox.Show("Please enter a valid number for capacity.");
                return;
            }

            // Confirm the update with the user
            DialogResult result = MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Insert the employee data into the database
                ModifyVehicle(vehicleId, modifiedModel, modifiedType, modifiedCapacity, modifiedLicensePlate);

                // Display a message indicating successful update
                MessageBox.Show("Record updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open a PanelEmployees form
                Form form = new PanelVehicles();
                DashboardForm dashboardForm = FindDashboardForm();
                if (dashboardForm != null)
                {
                    FormLoader.LoadForm(dashboardForm.mainpanel, form);
                }
            }
        }

        private bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }

        private void ModifyVehicle(string vehicleId, string modifiedModel, string modifiedType, string modifiedCapacity, string modifiedLicensePlate)
        {
            // Perform the database operation to delete the record
            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = "UPDATE Vehicles SET Model = @Model, Type = @Type, Capacity = @Capacity, LicensePlate = @LicensePlate WHERE VehicleID = @VehicleID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Model", modifiedModel);
                    command.Parameters.AddWithValue("@Type", modifiedType);
                    command.Parameters.AddWithValue("@Capacity", modifiedCapacity);
                    command.Parameters.AddWithValue("@LicensePlate", modifiedLicensePlate);
                    command.Parameters.AddWithValue("@VehicleID", vehicleId);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
