using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
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
        private async void CreatePerson()
        {

            //Person person = new Person();
            //AddEditProfileView personPage = new AddEditProfileView();
            //personPage.BindingContext = person;
            //person.RegDate = DateTime.Now.ToString();
            await _navigationService.NavigateAsync("AddEditProfileView");
        }
    }
}
