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
    public partial class ctrlSimpleCalc : UserControl
    {
        public event Action<int> OnCalculationComplete;

        protected virtual void CalculationComplete(int calcResult)
        {
            Action<int> handler = OnCalculationComplete;

            if (handler != null)
            {
                handler(calcResult);
            }

        }
        public ctrlSimpleCalc()
        {
            InitializeComponent();
        }

        public float Result { get { return float.Parse(lblResult.Text); } } 

        public int Val1 { get { return int.Parse(txtVal1.Text); } }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            int result = int.Parse(txtVal1.Text) + int.Parse(txtVal2.Text);
            lblResult.Text = (int.Parse(txtVal1.Text) +int.Parse( txtVal2.Text)).ToString();

            if (OnCalculationComplete != null)
            {
                CalculationComplete(result);
            }
        }
    }
}
