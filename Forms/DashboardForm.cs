﻿using System;
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

namespace TransportationProject.Forms
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            Form form = new Panels.PanelOverview();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelOverview();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnPassengerBookings_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelPassengerBookings();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnFreightShipments_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelFreightShipments();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnVehicles_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelVehicles();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnRoutes_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelRoutes();
            FormLoader.LoadForm(mainpanel, form);

        }

        private void btnTrips_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelTrips();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelEmployees();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelUsers();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelProfile();
            FormLoader.LoadForm(mainpanel, form);
        }
    }
}