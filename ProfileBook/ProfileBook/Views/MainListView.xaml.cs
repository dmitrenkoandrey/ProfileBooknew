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
namespace ProfileBook.Views
{
    [DesignTimeVisible(false)]
    public partial class MainListView : ContentPage
    {
        //private string regDate = DateTime.Now.ToString();
        Person person1 = new Person();
        string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        public MainListView()
        {

            InitializeComponent();
            //this.BindingContext = new PersonsListViewModel();
            logoutItem.IconImageSource = ImageSource.FromFile("logout.png");
            settingsItem.IconImageSource = ImageSource.FromFile("settings.png");
           // BindingContext = this;
           //BindingContext = new PersonsListViewModel() { Navigation = this.Navigation };
            //IsBusy = false;

        }



        protected override void OnAppearing()
        {
            //dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                personsList.ItemsSource = db.Persons.ToList();
            }
            base.OnAppearing();
        }
        // обработка нажатия элемента в списке
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Person selectedPerson = (Person)e.SelectedItem;
            AddEditProfileView personPage = new AddEditProfileView();
            personPage.BindingContext = selectedPerson;
            person1 = selectedPerson;
 
            await Navigation.PushAsync(personPage);
        }
        // обработка нажатия кнопки добавления
        private async void CreatePerson(object sender, EventArgs e)
        {

            Person person = new Person();
            AddEditProfileView personPage = new AddEditProfileView();
            personPage.BindingContext = person;
            person.RegDate = DateTime.Now.ToString();
            await Navigation.PushAsync(personPage);
        }
        async void OnLogoutItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignInView());
        }

        async void OnSettingsItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsView());
        }
        async void OnEditProfileItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditProfileView());
        }

        void OnUserInfoItemClicked(object sender, EventArgs e)
        {

            //await Navigation.PushAsync(new UserInfoPage(null));
        }
        async void OnExitItemClicked(object sender, EventArgs e)
        {
            var diaRes = await DisplayAlert("Exit", "Are you sure you want to close the application?", "Yes", "Cancel");
            if (diaRes) System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
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
                //person = (Person)BindingContext;
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {
                    db.Persons.Remove(person);
                    db.SaveChanges();
                }
                await Navigation.PushAsync(new MainListView());
            }
        }
    }
}
