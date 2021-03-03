using ProfileBook.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using ProfileBook.Services.Repository;
using ProfileBook.Models;
namespace ProfileBook
{
    public partial class App : Application
    {

        //public static bool IsUserLoggedIn { get { return !string.IsNullOrEmpty(Tocken); }}
        public const string DBFILENAME = "personsapp.db";
        public App()
        {
            InitializeComponent();

            string dbPath = DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME);
            using (var db = new ApplicationContext(dbPath))
            {
                // Создаем бд, если она отсутствует
                //db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                if (db.Persons.Count() == 0)
                {
                    db.Persons.Add(new Person {NickName = "Andrey", Name = "Andrey Dmitrenko", Description = "Hello, Andrey!", RegDate = "02/24/2021 1:50:23 PM" });
                    db.Persons.Add(new Person { NickName = "Sveta", Name = "Svetlana Dmitrenko", Description = "Hello, Sveta!", RegDate = "01/15/2021 9:45:34 AM" });
                    db.UserLogins.Add(new UserLogin { UserName = "user", Password = "1234567" });
                    db.SaveChanges();
                }
            }
            MainPage = new NavigationPage(new SignInView());
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
