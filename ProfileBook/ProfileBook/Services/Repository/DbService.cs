using ProfileBook;
using ProfileBook.Services.Repository;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProfileBook.Models;
using ProfileBook.Views;

namespace ProfileBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class DbService : ContentPage
    {
        string dbPath;

        public DbService()
        {
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        }
        private void SavePerson(object sender, EventArgs e)
        {
            var person = (Person)BindingContext;
            if (!String.IsNullOrEmpty(person.Name))
            {
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {
                    if (person.Id == 0)
                        db.Persons.Add(person);
                    else
                    {
                        db.Persons.Update(person);
                    }
                    db.SaveChanges();
                }
            }
            this.Navigation.PopAsync();
        }
        private void DeletePerson(object sender, EventArgs e)
        {
            var person = (Person)BindingContext;
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                db.Persons.Remove(person);
                db.SaveChanges();
                DisplayAlert("Удаление", "Удалено", "OK");
            }
            this.Navigation.PopAsync();
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
               
                   //var person = person1;
                AddEditProfileView personPage = new AddEditProfileView();
               // personPage.BindingContext = person;
                await Navigation.PushAsync(personPage);
            }
        }

        
        }
}