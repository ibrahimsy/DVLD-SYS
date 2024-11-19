using DVLD_Bussiness;
using DVLD_System.Properties;
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
    public partial class ctrlUserCard : UserControl
    {
        clsUser _UserInfo;

        int _UserID = -1;
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public int UserID
        {
            get
            {
                return _UserID;
            }
        }

        void _ResetPersonInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();

            lblUserId.Text = "[ ? ? ? ]";
            lblUserName.Text = "[ ? ? ? ]";
            lblIsActive.Text = "[ ? ? ? ]";
        }

        public void LoadUserInfo(int UserID)
        {
            _UserInfo = clsUser.Find(UserID);
            if (_UserInfo == null)
            {
                MessageBox.Show($"No User With User ID {UserID} is found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetPersonInfo();
            }
            else
                _FillUserInfo();
        }


        void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_UserInfo.PersonID);

            _UserID = _UserInfo.UserID;

            lblUserId.Text = _UserInfo.UserID.ToString();

            lblUserName.Text = _UserInfo.UserName.ToString();

            lblIsActive.Text = _UserInfo.IsActive ? "Active" : "InActive";
        }

    }
}
