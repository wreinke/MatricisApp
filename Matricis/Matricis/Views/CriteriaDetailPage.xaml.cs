using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Matricis.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
        public partial class CriteriaDetailPage : ContentPage {
        CriteriaDetailViewModel viewModel;

            // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
            public CriteriaDetailPage() {
                InitializeComponent();
            }

            public CriteriaDetailPage(CriteriaDetailViewModel viewModel) {
                InitializeComponent();

                BindingContext = this.viewModel = viewModel;
            }
        }
    }