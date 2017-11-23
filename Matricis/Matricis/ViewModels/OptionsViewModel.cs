using Matricis.Helpers;
using Matricis.Models;
using Matricis.Views;
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

        public Command AddOptionClickedCommand { get; set; }
        public Evaluation CurrentEvaluation { get; private set; }

        public OptionsViewModel() {

            Title = "Browse";
            AddOptionClickedCommand = new Command(async () => await AddOptionClickedAsync());
            InitMessaging();

        }
        
        private void InitMessaging() {

            SubscribeAddOptionM();
            SubscribeAddCriteriaM();
            SubscribeAddEvaluationM();

        }

        private async Task AddOptionClickedAsync() {

            var page = Application.Current.MainPage as TabbedPage;
            await page.Children[1].Navigation.PushAsync(new NewOptionPage());
        }

        private void SubscribeAddOptionM() {
            MessagingCenter.Subscribe<NewOptionViewModel, Option>(this, "AddOptionM", (sender, args) => {

                //Add Criterias to new Option
                if (CurrentEvaluation.Criterias != null) {
                    args.Criterias = CurrentEvaluation.Criterias;
                }

                //Add new Option to Optionlist
                Options.Add(args);

                // Update CurrentEvaluation object
                CurrentEvaluation.Options = new List<Option>(Options.ToList());

                try {
                    SqLiteConnection.UpdateWithChildren(CurrentEvaluation);
                } catch (Exception e) {
                    throw new NotImplementedException();
                }
            });
        }

        private void SubscribeAddCriteriaM() {
            MessagingCenter.Subscribe<NewCriteriaViewModel, Criteria>(this, "AddCriteriaM", (sender, args) => {

                Criterias.Add(args);

                //neues Criteria für jede Option in CurrentEvaluation hinzufügen
                foreach (var _option in CurrentEvaluation.Options)
                    if (_option.Criterias != null) {
                        _option.Criterias.Add(args);
                    } else {
                        _option.Criterias = new ObservableRangeCollection<Criteria>().ToList();
                        _option.Criterias.Add(args);
                    }

                // Current Evaluation update
                try {
                    SqLiteConnection.UpdateWithChildren(CurrentEvaluation);
                } catch (Exception e) {
                    throw new NotImplementedException();
                }
            });
        }

        private void SubscribeAddEvaluationM() {
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
    }
}
