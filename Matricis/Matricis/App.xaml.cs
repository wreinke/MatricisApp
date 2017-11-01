using Matricis.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinForms.SQLite;

namespace Matricis {
    public partial class App : Application {
        public App() {
            InitializeComponent();


            // MainPage = new SQLiteSamplePage().GetSampleContentPage();

            SetMainPage();

        }

        public static void SetMainPage() {
            Current.MainPage = new TabbedPage {
                Children =
                {
                    new NavigationPage(new CriteriasPage())
                    {
                        Title = "Browse",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
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
