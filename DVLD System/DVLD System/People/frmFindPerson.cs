using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System
{
    public partial class frmFindPerson : Form
    {

        public delegate void DataBackHandler(int PersonID);

        public event DataBackHandler DataBack;

        public frmFindPerson()
        {
            InitializeComponent();
        }

        private void ctrlPersonCardWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            int PersonId = ctrlPersonCardWithFilter1.PersonID;

            if (PersonId > 0)
            {
                DataBack?.Invoke(PersonId);
            }
            
            this.Close();
        }
    }
}
