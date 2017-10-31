using Matricis.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Matricis.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCriteriaPage : ContentPage {

        public Criteria Criteria { get; set; }

        public NewCriteriaPage() {
            InitializeComponent();

            Criteria = new Criteria {
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e) {
            MessagingCenter.Send(this, "AddCriteria", Criteria);
            await Navigation.PopToRootAsync();
        }
    }
}