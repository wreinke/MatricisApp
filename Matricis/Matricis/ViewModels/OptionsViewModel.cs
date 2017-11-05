using Matricis.Helpers;
using Matricis.Models;
using Matricis.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matricis.ViewModels
{
    public class OptionsViewModel : BaseViewModel
    {
        private Option selectedItem;
        private ObservableRangeCollection<Option> options;

        public ObservableRangeCollection<Option> Options {

            get
            {
                return options;
            }

            set
            {
                SetProperty(ref options, value);
            }
        }

        public Option SelectedItem
        {
            get
            {

                return this.selectedItem;

            }
            set
            {

                this.selectedItem = value;
                Application.Current.MainPage.Navigation.PushAsync(new OptionDetailPage());
                MessagingCenter.Send<OptionsViewModel, Option>(this, "OptionSelectedM", SelectedItem);

            }
        }

        public Command LoadCriteriasCommand { get; set; }
        public Command AddItemClickedCommand { get; set; }

        public OptionsViewModel() {
            Title = "Browse";
            LoadOptions();
            LoadCriteriasCommand = new Command(() => LoadOptions());
            AddItemClickedCommand = new Command(async () => await AddItemClickedAsync());

            MessagingCenter.Subscribe<NewOptionViewModel>(this, "AddOptionM", (sender) => {
                LoadOptions();
            });
        }

        private async Task AddItemClickedAsync() {

            var page = Application.Current.MainPage as TabbedPage;
            await page.Children.First().Navigation.PushAsync(new NewOptionPage());
        }

        private void LoadOptions() {
            if (IsBusy)
                return;

            IsBusy = true;

            try 
            {

                Options = new ObservableRangeCollection<Option>(SqLiteConnection.Table<Option>().ToList());

            }
            catch (Exception ex) 
            {
                if (ex.Message == "no such table: Option")
                {
                    SqLiteConnection.CreateTable<Option>();
                    Options = new ObservableRangeCollection<Option>(SqLiteConnection.Table<Option>().ToList());
                }
            } 
            finally 
            {
                IsBusy = false;
            }
        }
    }
}
