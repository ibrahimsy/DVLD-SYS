using DVLD_Bussiness;
using DVLD_System.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Licenses
{
    public partial class frmShowLicensesHistory : Form
    {
        int _PersonID = -1;
       
        public frmShowLicensesHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmShowLicensesHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlPersonCardWithFilter1.EnableFilter = false;
                ctrlPersonCardWithFilter1.LoadInfo(_PersonID);
                ctrlDriverLicensesHistory1.LoadInfo(_PersonID);
            }
            else
            {
                ctrlPersonCardWithFilter1.EnableFilter = true;
                ctrlPersonCardWithFilter1.FilterFocus();
               
            }
            
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
