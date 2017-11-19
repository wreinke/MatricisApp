using Matricis.Helpers;
using Matricis.Models;
using Matricis.Views;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matricis.ViewModels {
    public class OptionsViewModel : BaseViewModel {
        private Option selectedItem;
        private ObservableRangeCollection<Option> options;
        private ObservableRangeCollection<Criteria> criterias;

        public ObservableRangeCollection<Option> Options
        {

            get
            {
                return options;
            }

            set
            {
                SetProperty(ref options, value);
            }
        }

        public ObservableRangeCollection<Criteria> Criterias
        {
            get
            {
                return criterias;
            }
            set
            {
                SetProperty(ref criterias, value);
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

        //public Command LoadCriteriasCommand { get; set; }
        public Command AddOptionClickedCommand { get; set; }
        public Evaluation CurrentEvaluation { get; private set; }

        public OptionsViewModel() {
            Title = "Browse";
            LoadOptions();
            //LoadCriteriasCommand = new Command(() => LoadOptions());
            AddOptionClickedCommand = new Command(async () => await AddOptionClickedAsync());

            MessagingCenter.Subscribe<NewOptionViewModel, Option>(this, "AddOptionM", (sender, args) => {

                //Add Criterias to new Option
                if (CurrentEvaluation.Criterias != null) {
                    args.Criterias = CurrentEvaluation.Criterias;
                }

                //Add new Option to Optionlist
                Options.Add(args);

                // Update CurrentEvaluation object
                CurrentEvaluation.Options = new List<Option>(Options.ToList());
                //   SqLiteConnection.UpdateWithChildren(args);
                SqLiteConnection.UpdateWithChildren(CurrentEvaluation);
            });

            MessagingCenter.Subscribe<NewCriteriaViewModel, Criteria>(this, "AddCriteriaM", (sender, args) => {

                foreach (var _option in CurrentEvaluation.Options)
                    if (_option.Criterias != null) {
                        _option.Criterias.Add(args);
                    } else {
                        _option.Criterias = new ObservableRangeCollection<Criteria>().ToList();
                        _option.Criterias.Add(args);
                    }

                CurrentEvaluation.Options = new List<Option>(Options.ToList());
                SqLiteConnection.UpdateWithChildren(CurrentEvaluation);
                Criterias = new ObservableRangeCollection<Criteria>(CurrentEvaluation.Criterias);
            });

            MessagingCenter.Subscribe<EvaluationsViewModel, Evaluation>(this, "EvaluationSelectedM", (sender, args) => {
                CurrentEvaluation = args;

                if (CurrentEvaluation.Options != null) {
                    Options = new ObservableRangeCollection<Option>(CurrentEvaluation.Options);
                } else {
                    Options = new ObservableRangeCollection<Option>();
                }

                if (CurrentEvaluation.Criterias != null) {
                    Criterias = new ObservableRangeCollection<Criteria>(CurrentEvaluation.Criterias);
                } else {
                    Criterias = new ObservableRangeCollection<Criteria>();
                }
                                
            });
        }

        private async Task AddOptionClickedAsync() {

            var page = Application.Current.MainPage as TabbedPage;
            await page.Children[1].Navigation.PushAsync(new NewOptionPage());
        }

        private void LoadOptions() {
            if (IsBusy)
                return;

            IsBusy = true;

            try {
                Options = new ObservableRangeCollection<Option>(SqLiteConnection.GetWithChildren<Evaluation>(CurrentEvaluation.Id).Options.ToList());

            } catch (Exception ex) {
                if (ex.Message == "no such table: Option") {
                    SqLiteConnection.CreateTable<Option>();
                    Options = new ObservableRangeCollection<Option>(SqLiteConnection.GetWithChildren<Evaluation>(CurrentEvaluation.Id).Options.ToList());
                }
            } finally {
                IsBusy = false;
            }
        }
    }
}
