using Matricis.Helpers;
using Matricis.Models;
using Matricis.Views;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matricis.ViewModels {
    public class CriteriasViewModel : BaseViewModel {

        Criteria selectedItem;

        public Evaluation CurrentEvaluation { get; set; }

        private ObservableRangeCollection<Criteria> criterias;
        private ObservableCollection<Option> options;

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

        public Criteria SelectedItem
        {
            get
            {

                return this.selectedItem;

            }
            set
            {

                this.selectedItem = value;
                var page = Application.Current.MainPage as TabbedPage;
                page.Children[2].Navigation.PushAsync(new OptionDetailPage());
                MessagingCenter.Send<CriteriasViewModel, Criteria>(this, "CriteriaSelectedM", SelectedItem);

            }
        }

        public ObservableCollection<Option> Options
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

        public Command LoadCriteriasCommand { get; set; }
        public Command AddItemClickedCommand { get; set; }

        public CriteriasViewModel() {

            //Options = new ObservableCollection<Option>() {
            //    new Option() {
            //        Title="123"
            //    }
            //};

            // For testing
            //var x = SqLiteConnection.DropTable<Criteria>();
            //x = SqLiteConnection.DropTable<Option>();
            //x = SqLiteConnection.DropTable<Evaluation>();

            //SqLiteConnection.CreateTable<Criteria>();
            //SqLiteConnection.CreateTable<Option>();
            //SqLiteConnection.CreateTable<Evaluation>();

            Title = "Browse";
            AddItemClickedCommand = new Command(async () => await AddItemClickedAsync());

            MessagingCenter.Subscribe<NewOptionViewModel, Option>(this, "AddOptionM", (sender, args) => {

                foreach (var _criteria in CurrentEvaluation.Criterias)
                    if (_criteria.Options != null) {
                        _criteria.Options.Add(args);
                    } else {
                        _criteria.Options = new ObservableRangeCollection<Option>().ToList();
                        _criteria.Options.Add(args);
                    }
                CurrentEvaluation.Criterias = new List<Criteria>(Criterias.ToList());
                SqLiteConnection.UpdateWithChildren(CurrentEvaluation);
                Options = new ObservableCollection<Option>(CurrentEvaluation.Options);
                //Criterias = new ObservableRangeCollection<Criteria>(SqLiteConnection.GetAllWithChildren<Criteria>().Where(c => c.EvaluationID == CurrentEvaluation.Id));
            });

            MessagingCenter.Subscribe<NewCriteriaViewModel, Criteria>(this, "AddCriteriaM", (sender, args) => {

                //Add Options to new Criteria
                args.Options = CurrentEvaluation.Options;

                //Add new Criteria to Collection
                if (Criterias != null) {
                    Criterias.Add(args);
                } else {
                    Criterias = new ObservableRangeCollection<Criteria>();
                    Criterias.Add(args);
                }
                CurrentEvaluation.Criterias = new List<Criteria>(Criterias.ToList());

                //Update full Evaluation
                SqLiteConnection.InsertOrReplaceWithChildren(args);

                //SqLiteConnection.InsertOrReplaceWithChildren(new CriteriaOption() {
                //    Criterias = CurrentEvaluation.Criterias.ToList(),
                //    Options = CurrentEvaluation.Options.ToList()
                //}
                //);

                // SqLiteConnection.UpdateWithChildren(args);
                SqLiteConnection.UpdateWithChildren(CurrentEvaluation);
            });

            MessagingCenter.Subscribe<EvaluationsViewModel, Evaluation>(this, "EvaluationSelectedM", (sender, args) => {
                CurrentEvaluation = args;


                if (CurrentEvaluation.Criterias != null) {
                    Criterias = new ObservableRangeCollection<Criteria>(CurrentEvaluation.Criterias);
                } else {
                    Criterias = new ObservableRangeCollection<Criteria>();
                }

                if (CurrentEvaluation.Options != null) {
                    Options = new ObservableRangeCollection<Option>(CurrentEvaluation.Options);
                } else {
                    Options = new ObservableRangeCollection<Option>();
                }
            });

        }

        private async Task AddItemClickedAsync() {
            var page = Application.Current.MainPage as TabbedPage;
            await page.Children[2].Navigation.PushAsync(new NewCriteriaPage());
        }
    }
}

