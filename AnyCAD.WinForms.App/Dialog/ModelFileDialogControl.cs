using AnyCAD.Forms;
using FileDialogExtenders;

namespace AnyCAD.WinForms.App.Dialog
{
    public partial class ModelFileDialogControl : FileDialogControlBase
    {
        RenderControl _Viewer;
        public ModelFileDialogControl()
        {
            InitializeComponent();
            _Viewer = new RenderControl(panel3d);
        }
 
        private void ModelFileDialogControl_Load(object sender, EventArgs e)
        {
            _Viewer.EnableAnimation(true);
        }

    }
}
