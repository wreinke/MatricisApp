using Matricis.Helpers;
using Matricis.Models;
using Matricis.Views;
using SQLiteNetExtensions.Extensions;
using System;
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
                Application.Current.MainPage.Navigation.PushAsync(new CriteriaDetailPage());
                MessagingCenter.Send<CriteriasViewModel, Criteria>(this, "CriteriaSelectedM", SelectedItem);

            }
        }



        public Command LoadCriteriasCommand { get; set; }
        public Command AddItemClickedCommand { get; set; }

        public CriteriasViewModel() {

            // For testing
            var x = SqLiteConnection.DropTable<Criteria>();
            SqLiteConnection.CreateTable<Criteria>();
            SqLiteConnection.Insert(new Criteria { Title = "asd", Descripion = "asdad", EvaluationId = 999 });
            

            Title = "Browse";
            AddItemClickedCommand = new Command(async () => await AddItemClickedAsync());

            MessagingCenter.Subscribe<NewCriteriaViewModel, Criteria>(this, "AddCriteriaM", (sender,args) => {

                if(Criterias != null) {
                    Criterias.Add(args);
                } else {
                    Criterias = new ObservableRangeCollection<Criteria>();
                    Criterias.Add(args);
                }
                CurrentEvaluation.Criterias = Criterias.ToList();
                SqLiteConnection.InsertWithChildren(CurrentEvaluation);

                var d = SqLiteConnection.Table<Evaluation>().ToList();
            });

            MessagingCenter.Subscribe<EvaluationsViewModel, Evaluation>(this, "EvaluationSelectedM", (sender, args) => {
                CurrentEvaluation = args;
                if (CurrentEvaluation.Criterias != null) {
                    Criterias = new ObservableRangeCollection<Criteria>(CurrentEvaluation.Criterias);
                } 
                else {
                    Criterias = new ObservableRangeCollection<Criteria>();
                }
            });

        }

        private async Task AddItemClickedAsync() {
            var page = Application.Current.MainPage as TabbedPage;
            await page.Children[1].Navigation.PushAsync(new NewCriteriaPage());
        }
        }
    }

