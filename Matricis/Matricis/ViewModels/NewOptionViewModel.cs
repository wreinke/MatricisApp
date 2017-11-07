using Matricis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matricis.ViewModels {
    public class NewOptionViewModel : BaseViewModel {
        public Option Option { get; set; }

        public Command SaveClickedCommand { get; set; }

        public NewOptionViewModel() {
            Option = new Option();

            SaveClickedCommand = new Command(async () => await SaveClickedAsync());
        }

        private async Task SaveClickedAsync() {
            if (Option != null) {
                try {
                    SqLiteConnection.Insert(Option);
                } catch (Exception e) {
                    if (e.Message == "no such table: Option") {
                        SqLiteConnection.CreateTable<Option>();
                        SqLiteConnection.Insert(Option);
                    }
                } finally {
                    MessagingCenter.Send<NewOptionViewModel,Option>(this, "AddOptionM",Option);
                    var page = Application.Current.MainPage as TabbedPage;
                    await page.Children[1].Navigation.PopAsync();
                }
            }
        }
    }
}