using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
  public class MainListViewModel : BindableBase
    {
    
    public MainListViewModel() { }
        INavigationService _navigationService;
        IPageDialogService _pageDialog;
        Person person1 = new Person();
        public MainListViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public MainListViewModel(INavigationService navigationService, IPageDialogService pageDialog)
        {
            _navigationService = navigationService;
            _pageDialog = pageDialog;
        }
        public ICommand CreatePersonCommand => new Command(CreatePerson);
        public ICommand DeletePersonCommand => new Command(DeletePerson);
        public ICommand LogoutItemCommand => new Command(LogoutItem);      
        public ICommand EditPersonCommand => new Command(EditPerson);
        public ICommand SettingsItemCommand => new Command(SettingsItem);
        string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        
        private async void CreatePerson()
        {
            await _navigationService.NavigateAsync("AddEditProfileView");
        }
        public async void EditPerson()
        {

            var editres = await _pageDialog.DisplayAlertAsync("Редактировать", "Редактировать запись?", "Да", "Нет");
            if (editres)
            {
                var person = person1;
                AddEditProfileView personPage = new AddEditProfileView();
                personPage.BindingContext = person;
                await _navigationService.NavigateAsync("AddEditProfileView");
            }
        }
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Person selectedPerson = (Person)e.SelectedItem;
            AddEditProfileView personPage = new AddEditProfileView();
            personPage.BindingContext = selectedPerson;
            person1 = selectedPerson;

            await _navigationService.NavigateAsync("AddEditProfileView");
        }
        public async void DeletePerson()
        {
            var delres = await _pageDialog.DisplayAlertAsync("Удалить", "Удалить запись?", "Да", "Нет");
            if (delres)
            {
                var person = person1;
                // person = (Person)BindingContext;
                try
                {
                    using (ApplicationContext db = new ApplicationContext(dbPath))
                    {
                        db.Persons.Remove(person);
                        db.SaveChanges();
                    }
                    await _navigationService.NavigateAsync("MainListView");
                }
                catch (Exception ex)
                {
                    await _pageDialog.DisplayAlertAsync("Сообщение об ошибке", ex.Message, "OK");

                }
            }
        }
        async void LogoutItem()
        {
            await _navigationService.NavigateAsync("SignInView");
        }

        async void SettingsItem()
        {
            await _navigationService.NavigateAsync("SettingsView");
        }
    }
}
