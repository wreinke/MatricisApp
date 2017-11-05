using Matricis.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinForms.SQLite;

namespace Matricis {
    public partial class App : Application {

        public Page MyTab { get; set; }

        public App() {
            InitializeComponent();

            MainPage = SetTabPage();
        }

        public static TabbedPage SetTabPage() {
            return new TabbedPage {
                Children =
                {
                    new NavigationPage(new EvaluationsPage())
                    {
                        Title = "Evaluations",
                    },
                    new NavigationPage(new CriteriasPage())
                    {
                        Title = "Criterias",
                    },
                    new NavigationPage(new OptionsPage()) {
                        Title = "Options"
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                    },
                }
            };
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
