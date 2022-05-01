using AnyCAD.Demo.Coordinate;
using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyCAD.Demo
{


    public partial class MultiCoordinateSystemForm : Form
    {
        RenderControl mRenderCtrl;
        MyPart mPart = null;
        MyPlane mPlane = null;
        uint mSelectedObjectId = 0;
        MyObject mCurrentPart = null;
        GAx2 mCurrentAxis = null;

        public MultiCoordinateSystemForm()
        {
            InitializeComponent();

            mRenderCtrl = new RenderControl(this.splitContainer1.Panel2);

            comboBox1.Items.Add("世界坐标系");
            comboBox1.Items.Add("相对坐标系");
            comboBox1.SelectedIndex = 0;

            ShowProperty();
        }

        bool bUpdateUI = false;
        void ShowProperty()
        {
            mCurrentPart = null;
            if (mSelectedObjectId == 0)
            {
                this.splitContainer1.Panel1.Enabled = false;
                return;
            }
            this.splitContainer1.Panel1.Enabled = true;

         
            if (mSelectedObjectId == mPart.Id)
            {
                mCurrentPart = mPart;
                // 显示世界坐标系
                if(comboBox1.SelectedIndex == 0)
                {
                    mCurrentAxis = GP.XOY();
                }
                else // 显示相对于Plane坐标系
                {
                    mCurrentAxis = mPlane.WorldCoordinate;
                    
                }
            }
            else 
            {
                mCurrentPart = mPlane;

                // 显示世界坐标系
                if (comboBox1.SelectedIndex == 0)
                {
                    mCurrentAxis = GP.XOY();
                }
                else // 显示相对于Part坐标系
                {
                    mCurrentAxis = mPart.WorldCoordinate;
                }

            }

            mCurrentPart.UpdateViewModel(mCurrentAxis);

            bUpdateUI = true;
            this.upDownX.Value = (long)mCurrentPart.X;
            this.upDownY.Value = (long)mCurrentPart.Y;
            this.upDownZ.Value = (long)mCurrentPart.Z;
            this.upDownA.Value = (long)mCurrentPart.A;
            this.upDownB.Value = (long)mCurrentPart.B;
            this.upDownC.Value = (long)mCurrentPart.C;
            bUpdateUI = false;
        }

        private void MultiCoordinateSystemForm_Load(object sender, EventArgs e)
        {
            mRenderCtrl.GetContext().GetSceneManager().GetCoodinateGrid().SetVisible(true);
            mPart = new MyPart();
            mPlane = new MyPlane();

            mPart.Show(mRenderCtrl);
            mPlane.Show(mRenderCtrl);

            mRenderCtrl.GetViewer().SetCoordinateWidget(EnumViewCoordinateType.Axis);

            mRenderCtrl.SetSelectCallback((PickedResult ret) =>
            {
                if(ret.IsEmpty())
                {
                    mSelectedObjectId = 0;
                    ShowProperty();
                }
                else
                {
                    mSelectedObjectId = ret.GetItem().GetNodeId();
                    ShowProperty();
                }
            });

        }

        private void MultiCoordinateSystemForm_ChangeCS(object sender, EventArgs e)
        {
            ShowProperty();
        }

        private void upDownX_ValueChanged(object sender, EventArgs e)
        {
            if (mCurrentPart == null || bUpdateUI)
                return;

            mCurrentPart.X = (double)this.upDownX.Value;
            mCurrentPart.Y = (double)this.upDownY.Value;
            mCurrentPart.Z = (double)this.upDownZ.Value;
            mCurrentPart.A = (double)this.upDownA.Value;
            mCurrentPart.B = (double)this.upDownB.Value;
            mCurrentPart.C = (double)this.upDownC.Value;

            mCurrentPart.UpdateModel(mCurrentAxis);

            mRenderCtrl.RequestDraw(EnumUpdateFlags.Scene);
        }   
    }       
}           
            