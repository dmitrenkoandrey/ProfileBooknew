using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProfileBook.Models;
using ProfileBook.Services;
using ProfileBook.TreeView;
using ProfileBook.ViewModels;
using ProfileBook.Services.Repository;
using ProfileBook.Views;
using System.Windows.Input;
using static Android.App.Notification.MessagingStyle;
using Acr.UserDialogs;

using Android;

namespace ProfileBook.Views
{
    [DesignTimeVisible(false)]
    public partial class MainListView : ContentPage
    {
        Person person1 = new Person();
        string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        public MainListView()
        {

            InitializeComponent();
            //this.BindingContext = new PersonsListViewModel();
            logoutItem.IconImageSource = ImageSource.FromFile("logout.png");
            settingsItem.IconImageSource = ImageSource.FromFile("settings.png");

        }



        protected override void OnAppearing()
        {
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                personsList.ItemsSource = db.Persons.ToList();
               
            }
            base.OnAppearing();
        }
        // обработка нажатия элемента в списке
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Person selectedPerson = (Person)e.SelectedItem;
            AddEditProfileView personPage = new AddEditProfileView();
            personPage.BindingContext = selectedPerson;
            person1 = selectedPerson;

            await Navigation.PushAsync(personPage);
        }
        
        async void OnEditProfileItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditProfileView());
        }

        void OnUserInfoItemClicked(object sender, EventArgs e)
        {

            //await Navigation.PushAsync(new UserInfoPage(null));
        }

        private async void EditPerson(object sender, EventArgs e)
        {

            var editres = await DisplayAlert("Редактировать", "Редактировать запись?", "Да", "Нет");
            if (editres)
            {
                var person = person1;
                AddEditProfileView personPage = new AddEditProfileView();
                personPage.BindingContext = person;
                await Navigation.PushAsync(personPage);
            }
        }

        private async void DeletePerson(object sender, EventArgs e)
        {
            var delres = await DisplayAlert("Удалить", "Удалить запись?", "Да", "Нет");
            if (delres)
            {
                var person = person1;

                try
                {
                    using (ApplicationContext db = new ApplicationContext(dbPath))
                    {
                        if(db.Persons != null)
                        {
                            db.Persons.Remove(person);
                            db.SaveChanges();
                        }
                    }
                    await Navigation.PushAsync(new MainListView());
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");

                }
            }
        }
    }
}

