using Matricis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matricis.ViewModels {
    public class NewEvaluationViewModel : BaseViewModel {

        public Evaluation Evaluation { get; set; }

        public Command SaveClickedCommand { get; set; }

        public NewEvaluationViewModel() {
            Evaluation = new Evaluation();

            SaveClickedCommand = new Command(async () => await SaveClickedAsync());
        }

        private async Task SaveClickedAsync() {
            if (Evaluation != null) {
                try {
                    SqLiteConnection.Insert(Evaluation);
                } catch (Exception e) {
                    if (e.Message == "no such table: Evaluation") {
                        SqLiteConnection.CreateTable<Evaluation>();
                        SqLiteConnection.Insert(Evaluation);
                    }
                } finally {
                    MessagingCenter.Send<NewEvaluationViewModel>(this, "AddEvaluationM");
                    var page = Application.Current.MainPage as TabbedPage;
                    NavigationPage navPage = page.Children[0] as NavigationPage;
                    await navPage.Navigation.PopAsync();
                }

                //await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }
    }
}
