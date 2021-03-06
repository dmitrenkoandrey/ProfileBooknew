﻿using ProfileBook.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using ProfileBook.Services.Repository;
using ProfileBook.Models;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;
using ProfileBook.ViewModels;
using System.IO;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {

        //public static bool IsUserLoggedIn { get { return !string.IsNullOrEmpty(Tocken); }}
        public const string DBFILENAME = "personsapp.db";
        public static PersonRepository database;
        public static PersonRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new PersonRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DBFILENAME));
                }
                return database;
            }
        }
        public App(IPlatformInitializer initializer = null) : base(initializer) 
        {      
        }
        #region ---Overrides---
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView,SignInViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }
        protected override void OnInitialized()
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
                    db.Persons.Add(new Person { NickName = "Andrey", Name = "Andrey Dmitrenko", Description = "Hello, Andrey!", RegDate = "02/24/2021 1:50:23 PM", ProfileImage = "person2" });
                    db.Persons.Add(new Person { NickName = "Sveta", Name = "Svetlana Dmitrenko", Description = "Hello, Sveta!", RegDate = "01/15/2021 9:45:34 AM", ProfileImage = "person1" });
                    db.UserLogins.Add(new UserLogin { UserName = "user", Password = "1234567" });
                    db.SaveChanges();
                }
            }
            //MainPage = new NavigationPage(new SignInView());
            NavigationService.NavigateAsync("NavigationPage/SignInView");
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
        #endregion
    }
}
