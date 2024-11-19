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
        clsUser _user;
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public int UserID
        {
            get
            {
                return _user != null ? _user.UserID : -1;
            }
        }

        public void LoadUserInfo(int UserID)
        {
            _user = clsUser.Find(UserID);
            if (_user == null)
            {
                MessageBox.Show($"No User With User ID {UserID} is found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                _FillUserInfo();
        }


        void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_user.PersonID);

            lblUserId.Text = _user.UserID.ToString();

            lblUserName.Text = _user.UserName.ToString();

            lblIsActive.Text = _user.IsActive ? "Active" : "DisActive";
        }

    }
}
