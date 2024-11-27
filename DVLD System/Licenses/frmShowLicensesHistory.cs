using System;
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

        public frmShowLicensesHistory()
        {
            InitializeComponent();
        }

        private void frmShowLicensesHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlPersonCardWithFilter1.EnableFilter = false;
                ctrlPersonCardWithFilter1.LoadInfo(_PersonID);
                ctrlDriverLicensesHistory1.LoadInfoByPersonID(_PersonID);
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

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            int SelectedPerson = obj;
            if (SelectedPerson != -1)
            {
                ctrlDriverLicensesHistory1.LoadInfoByPersonID(SelectedPerson);
            }
            else
                ctrlDriverLicensesHistory1.Clear();
        }
    }
}
