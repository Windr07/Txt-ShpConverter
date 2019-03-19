/*---------------------------------------------------------------- 
// auth： Windragon
// date： 
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using OSGeo.OGR;
using WLib.CoordCoverter.Config;
using WLib.CoordCoverter.Utility;

namespace WLib.CoordCoverter.View
{
    public partial class ShpToTxtSettingsForm : Form
    {
        public Layer PolygonLayer;


        public ShpToTxtSettingsForm(string shpPath)
        {
            InitializeComponent();

            GdalHelper.OpenSource(shpPath, dataSource => InitData(dataSource.GetLayerByIndex(0)));
        }

        public ShpToTxtSettingsForm(Layer polygonLayer)
        {
            InitializeComponent();
            InitData(polygonLayer);
        }


        private void InitData(Layer polygonLayer)
        {
            this.cmbJD.SelectedIndex = 2;
            this.cmbDKBH.SelectedIndex = 0;
            this.cmbDKMC.SelectedIndex = 0;
            this.cmbDKMJ.SelectedIndex = 0;
            this.cmbTFH.SelectedIndex = 0;
            this.cmbTDYT.SelectedIndex = 0;
            this.cmbDLBM.SelectedIndex = 0;

            if (polygonLayer != null)
            {
                this.PolygonLayer = polygonLayer;

                var fieldNames = GdalHelper.GetFieldNames(polygonLayer).ToArray();
                this.cmbDKBH.Items.AddRange(fieldNames);
                this.cmbDKMC.Items.AddRange(fieldNames);
                this.cmbDKMJ.Items.AddRange(fieldNames);
                this.cmbTFH.Items.AddRange(fieldNames);
                this.cmbTDYT.Items.AddRange(fieldNames);
                this.cmbDLBM.Items.AddRange(fieldNames);

                this.cmbJD.SelectedItem = CfgRedlineTxt.AttrFields[8].DefaultValue;
                this.cmbDKBH.SelectedItem = CfgRedlineTxt.BlockFields[2].FieldName;
                this.cmbDKMC.SelectedItem = CfgRedlineTxt.BlockFields[3].FieldName;
                this.cmbDKMJ.SelectedItem = CfgRedlineTxt.BlockFields[1].FieldName;
                this.cmbTFH.SelectedItem = CfgRedlineTxt.BlockFields[5].FieldName;
                this.cmbTDYT.SelectedItem = CfgRedlineTxt.BlockFields[6].FieldName;
                this.cmbDLBM.SelectedItem = CfgRedlineTxt.BlockFields[7].FieldName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var dkbhField = this.cmbDKBH.SelectedItem.ToString();
            var dkmcField = this.cmbDKMC.SelectedItem.ToString();
            var dkmjField = this.cmbDKMJ.SelectedItem.ToString();
            var tfhField = this.cmbTFH.SelectedItem.ToString();
            var tdytField = this.cmbTDYT.SelectedItem.ToString();
            var dlbmField = this.cmbDLBM.SelectedItem.ToString();

            const string STR_NONE = "(无)";
            if (dkmjField != STR_NONE) CfgRedlineTxt.BlockFields[1].FieldName = dkmjField;
            if (dkbhField != STR_NONE) CfgRedlineTxt.BlockFields[2].FieldName = dkbhField;
            if (dkmcField != STR_NONE) CfgRedlineTxt.BlockFields[3].FieldName = dkmcField;
            if (tfhField != STR_NONE) CfgRedlineTxt.BlockFields[5].FieldName = tfhField;
            if (tdytField != STR_NONE) CfgRedlineTxt.BlockFields[6].FieldName = tdytField;
            if (dlbmField != STR_NONE) CfgRedlineTxt.BlockFields[7].FieldName = dlbmField;

            var jdValue = this.cmbJD.SelectedItem.ToString();
            CfgRedlineTxt.AttrFields[8].DefaultValue = jdValue;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
