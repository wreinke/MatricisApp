using Matricis.Models;
using Matricis.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Matricis.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCriteriaPage : ContentPage {

        public NewCriteriaPage() {
            InitializeComponent();
        }

        public void Save_Clicked(object sender, EventArgs e) {
            var x = this.BindingContext as NewCriteriaViewModel;
            x.SaveClickedCommand.Execute(null);
        }
    }
}