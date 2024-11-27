using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int personId)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(personId);
            }
        }

        bool _ShowAddPerson = true;

        bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddPerson.Enabled = _ShowAddPerson;
            }
        }


        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        int _PrsonID = -1;
        public int PersonID
        {
            get
            {
                _PrsonID = ctrlPersonCard1.PersonID;
                return _PrsonID;
            }
        }

        public clsPerson SelectedPersonInfo
        {
            get 
            {
                return ctrlPersonCard1.SelectedPersonInfo;
            }
        }

        bool _EnableFilter = true;
        public bool EnableFilter
        {
            get
            {
                return _EnableFilter;
            }
            set
            {
                _EnableFilter = value;
                gpFilterBy.Enabled = _EnableFilter;
            }
        }
       
        void _FillComboBox()
        {
            cbFilterBy.Items.Add("National No");
            cbFilterBy.Items.Add("Person ID");

            cbFilterBy.SelectedIndex = 0;
        }
        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            _FillComboBox();

        }



        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            btnFindPerson.Enabled = !string.IsNullOrWhiteSpace(txtFilterValue.Text); 
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            _FindNow();
        }

        public void LoadInfo(int PersonID)
        {
            txtFilterValue.Text = PersonID.ToString();
            cbFilterBy.Text = "Person ID";
            _FindNow();
        }

        void _FindNow()
        {
            switch(cbFilterBy.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;
                case "National No":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;
            }

            if (OnPersonSelected != null && EnableFilter)
                OnPersonSelected(ctrlPersonCard1.PersonID);

        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo addEditPersonInfo = new frmAddEditPersonInfo();

            addEditPersonInfo.DataBack += _PersonInformatio_DataBack;

            addEditPersonInfo.ShowDialog();
        }

        private void _PersonInformatio_DataBack(int PersonID)
        {
            txtFilterValue.Text = PersonID.ToString();
            cbFilterBy.SelectedIndex = 1;
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnFindPerson.PerformClick();
            }

            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
