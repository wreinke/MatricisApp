using Syncfusion.SfDataGrid.XForms.UWP;

namespace Matricis.UWP {
    public sealed partial class MainPage {
        public MainPage() {
            this.InitializeComponent();
            SfDataGridRenderer.Init();
            LoadApplication(new Matricis.App());
        }
    }
}
