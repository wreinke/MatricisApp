using Matricis.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Matricis.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewOptionPage : ContentPage
	{
		public NewOptionPage ()
		{
			InitializeComponent ();
		}

        public void Save_Clicked(object sender, EventArgs e) {
            var x = this.BindingContext as NewOptionViewModel;
            x.SaveClickedCommand.Execute(null);
        }

    }
}