using Matricis.Models;
using Matricis.ViewModels;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Matricis.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TablePage : ContentPage {

        public SfDataGrid SfDataGrid { get; set; }
        
        public TablePage() {

            InitializeComponent();

            MessagingCenter.Subscribe<EvaluationsViewModel, Evaluation>(this, "EvaluationSelectedM", (sender, args) => {
                InitSfDataGrid();
            });
        }

        public void InitSfDataGrid() {

            //SfDataGrid = this.FindByName<SfDataGrid>("dataGrid");
            //TableViewModel tableViewModel = BindingContext as TableViewModel;
            //var x = tableViewModel.CriteriaOptions;
            
            //foreach (Option option in x.Take(0))

            //x.Add

            //x.ToList();

            ////            SfDataGrid.ItemsSource = tableViewModel.CurrentEvaluation.Criterias;
            //SfDataGrid.Collumns. 

            //              preis   unterhalt   
            //      audi   
            //      vw
            //      merci
            //      seat
            //      skoda
        }

        //public List<List<Criteria>>OptionsCriteriaMatrixAsList(Dictionary<Option, List<Criteria>>input){
                        
        //    var result = new List<List<Criteria>>() {

        //    };


        //    return result;
        //}
            
        }

        
    }
