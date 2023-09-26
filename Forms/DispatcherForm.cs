using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransportationProject.Classes;

namespace TransportationProject.Forms
{
    public partial class DispatcherForm : Form
    {
        public DispatcherForm()
        {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            Form form = new Panels.PanelOverview();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnTrips_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelTrips();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            Form form = new Panels.PanelProfile();
            FormLoader.LoadForm(mainpanel, form);
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {

        }
    }
}
