using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransportationProject.Classes
{
    public static class FormUtils
    {
        public static T FindParentForm<T>(Control control) where T : Form
        {
            Control parent = control.Parent;
            while (parent != null)
            {
                if (parent is T form)
                {
                    return form;
                }
                parent = parent.Parent;
            }
            return null;
        }
    }
}
