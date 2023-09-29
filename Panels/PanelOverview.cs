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
    public partial class PanelOverview : Form
    {
        public PanelOverview()
        {
            InitializeComponent();

            // Retrieve the count of users and employees from the database or data source
            int shipmentCount = GetTotalShipmentCount();
            int bookingCount = GetTotalBookingCount();
            int vehicleCount = GetTotalVehicleCount();
            int routeCount = GetTotalRouteCount();
            int tripCount = GetTotalTripCount();
            int employeeCount = GetTotalEmployeeCount();
            int userCount = GetTotalUserCount();

            // Update the labels to display the counts
            lblShipmentCount.Text = shipmentCount.ToString();
            lblBookingCount.Text = bookingCount.ToString();
            lblVehicleCount.Text = vehicleCount.ToString();
            lblRouteCount.Text = routeCount.ToString();
            lblTripCount.Text = tripCount.ToString();
            lblEmployeeCount.Text = employeeCount.ToString();
            lblUserCount.Text = userCount.ToString();
        }

        private int GetTotalShipmentCount()
        {
            int shipmentCount = 0;

            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM FreightShipments";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    shipmentCount = (int)command.ExecuteScalar();
                }

                connection.Close();
            }

            return shipmentCount;
        }

        private int GetTotalBookingCount()
        {
            int bookingCount = 0;

            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM PassengerBookings";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    bookingCount = (int)command.ExecuteScalar();
                }

                connection.Close();
            }

            return bookingCount;
        }

        private int GetTotalVehicleCount()
        {
            int vehicleCount = 0;

            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Vehicles";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    vehicleCount = (int)command.ExecuteScalar();
                }

                connection.Close();
            }

            return vehicleCount;
        }

        private int GetTotalRouteCount()
        {
            int routeCount = 0;

            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Routes";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    routeCount = (int)command.ExecuteScalar();
                }

                connection.Close();
            }

            return routeCount;
        }

        private int GetTotalTripCount()
        {
            int tripCount = 0;

            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Trips";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    tripCount = (int)command.ExecuteScalar();
                }

                connection.Close();
            }

            return tripCount;
        }

        private int GetTotalEmployeeCount()
        {
            int employeeCount = 0;

            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Employees";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    employeeCount = (int)command.ExecuteScalar();
                }

                connection.Close();
            }

            return employeeCount;
        }

        private int GetTotalUserCount()
        {
            int userCount = 0;

            using (SqlConnection connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Users";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    userCount = (int)command.ExecuteScalar();
                }

                connection.Close();
            }

            return userCount;
        }
    }
}
