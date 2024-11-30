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

namespace DVLD_System.WrittenTest
{
    public partial class frmWrittenTest : Form
    {
        int _AppointmentID = -1;
        clsTestType.enTestType _TestType = clsTestType.enTestType.enWrittinTest;
        public frmWrittenTest(int AppointmentID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
        }

        private void frmWrittenTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestType = _TestType;
            ctrlScheduledTest1.LoadInfo(_AppointmentID);
        }
    }
}
