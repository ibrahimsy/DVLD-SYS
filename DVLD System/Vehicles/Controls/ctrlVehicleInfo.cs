using BankBussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Vehicles.Controls
{
    public partial class ctrlVehicleInfo : UserControl
    {
        clsVehichle _VehicleInfo;
        int _VehicleID = -1;

        public int VehicleID
        {
            get
            {
                return _VehicleID;
            }
        }

        clsVehichle clsVehichleInfo
        {
            get
            {
                return _VehicleInfo;
            }
        }
       
        public ctrlVehicleInfo()
        {
            InitializeComponent();
        }

        public void LoadInfoByVehcileID(int VehicleID)
        {
            _VehicleID = VehicleID;
            _VehicleInfo = clsVehichle.Find(VehicleID);
            if (_VehicleInfo == null)
            {
                MessageBox.Show($"No Vehicle Found With ID = [{VehicleID}]");
                _VehicleID = -1;
                ResetVehicleInfo();
                return;
            }

            lblVehichleID.Text = _VehicleInfo.VehichleID.ToString();
            lblChassisNumber.Text = _VehicleInfo.ChassisNumber;
            lblPlateNumber.Text = _VehicleInfo.PlateNumber;
            lblMake.Text = _VehicleInfo.MakeInfo.Make;
            lblModel.Text = _VehicleInfo.ModelInfo.ModelName;
           
            if (_VehicleInfo.SubModelID != -1)
                
                lblSubModel.Text = _VehicleInfo.SubModelInfo.SubModelName;
            else
                lblSubModel.Text = "";

            lblColor.Text = _VehicleInfo.Color; 
            lblOwnerFullName.Text = _VehicleInfo.OwnerInfo.FullName;
            lblType.Text = _VehicleInfo.BodyInfo.BodyName;
            lblYear.Text = _VehicleInfo.Year.ToString();
            lblCreatedBy.Text = _VehicleInfo.UserInfo.UserName;

        }

        public void LoadInfoByChassisNumber(string  ChassisNumber)
        {
            
            _VehicleInfo = clsVehichle.FindByChassisNumber(ChassisNumber);
            if (_VehicleInfo == null)
            {
                MessageBox.Show($"No Vehicle Found With ID = [{_VehicleInfo.VehichleID}]");
                _VehicleID = -1;
                ResetVehicleInfo();
                return;
            }
            _VehicleID = _VehicleInfo.VehichleID;
            lblVehichleID.Text = _VehicleInfo.VehichleID.ToString();
            lblChassisNumber.Text = _VehicleInfo.ChassisNumber;
            lblPlateNumber.Text = _VehicleInfo.PlateNumber;
            lblMake.Text = _VehicleInfo.MakeInfo.Make;
            lblModel.Text = _VehicleInfo.ModelInfo.ModelName;
            lblSubModel.Text = _VehicleInfo.SubModelInfo.SubModelName;
            lblOwnerFullName.Text = _VehicleInfo.OwnerInfo.FullName;
            lblType.Text = _VehicleInfo.BodyInfo.BodyName;
            lblYear.Text = _VehicleInfo.Year.ToString();
            lblCreatedBy.Text = _VehicleInfo.UserInfo.UserName;

        }

        public void LoadInfoByPlateNumber(string PlateNumber)
        {

            _VehicleInfo = clsVehichle.FindByPlateNumber(PlateNumber);
            if (_VehicleInfo == null)
            {
                MessageBox.Show($"No Vehicle Found With ID = [{_VehicleInfo.VehichleID}]");
                _VehicleID = -1;
                ResetVehicleInfo();
                return;
            }
            _VehicleID = _VehicleInfo.VehichleID;
            lblVehichleID.Text = _VehicleInfo.VehichleID.ToString();
            lblChassisNumber.Text = _VehicleInfo.ChassisNumber;
            lblPlateNumber.Text = _VehicleInfo.PlateNumber;
            lblMake.Text = _VehicleInfo.MakeInfo.Make;
            lblModel.Text = _VehicleInfo.ModelInfo.ModelName;
            lblSubModel.Text = _VehicleInfo.SubModelInfo.SubModelName;
            lblOwnerFullName.Text = _VehicleInfo.OwnerInfo.FullName;
            lblType.Text = _VehicleInfo.BodyInfo.BodyName;
            lblYear.Text = _VehicleInfo.Year.ToString();
            lblCreatedBy.Text = _VehicleInfo.UserInfo.UserName;

        }
   
        public void ResetVehicleInfo()
        {
         
            lblVehichleID.Text    = "[? ? ? ? ?]";
            lblChassisNumber.Text = "[? ? ? ? ?]";
            lblPlateNumber.Text   = "[? ? ? ? ?]";
            lblMake.Text          = "[? ? ? ? ?]";
            lblModel.Text         = "[? ? ? ? ?]";
            lblSubModel.Text      = "[? ? ? ? ?]";
            lblOwnerFullName.Text = "[? ? ? ? ?]";
            lblType.Text          = "[? ? ? ? ?]";
            lblYear.Text          = "[? ? ? ? ?]";
            lblCreatedBy.Text     = "[? ? ? ? ?]";
            lblColor.Text         = "[? ? ? ? ?]";
        }
    
    
    }
}
