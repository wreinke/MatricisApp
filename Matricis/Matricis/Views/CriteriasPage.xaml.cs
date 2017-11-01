using Matricis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Matricis.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CriteriasPage : ContentPage {
        public CriteriasPage() {
            InitializeComponent();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args) {
            var criteria = args.SelectedItem as Criteria;
            if (criteria == null)
                return;

            await Navigation.PushAsync(new CriteriaDetailPage(new CriteriaDetailViewModel(criteria)));

            // Manually deselect criteria
            ItemsListView.SelectedItem = null;
        }

    }
}