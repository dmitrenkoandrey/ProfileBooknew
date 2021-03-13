using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
//using ProfileBook.TreeView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
 public   class AddEditProfileViewModel :BindableBase
    {
        string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        INavigationService _navigationService;
        IPageDialogService _pageDialog;
        public string entNickName { get; set; }
        public string entName { get; set; }
        public string entDescription { get; set; }
        public string entRegDate { get; set; }
        public AddEditProfileViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public AddEditProfileViewModel(INavigationService navigationService, IPageDialogService pageDialog)
        {
            _navigationService = navigationService;
            _pageDialog = pageDialog;
        }
        public ICommand SavePersonCommand => new Command(SavePerson);
         
        public AddEditProfileViewModel()
        { 
        }

        private async void SavePerson()
        {
            ApplicationContext db = new ApplicationContext(dbPath);
            entRegDate = DateTime.Now.ToString();
            if (entNickName == null || entName == null || entDescription == null) //проверяем заполнение всех полей
            {
                await _pageDialog.DisplayAlertAsync("Добавление пользователя", "Поля пользователя не введены!", "OK");
                await _navigationService.NavigateAsync("AddEditProfileView");
            }
            else
            {

                try
                {
                    db.Persons.Add(new Person { NickName = entNickName, Name = entName, Description = entDescription, RegDate = entRegDate  });
                    db.SaveChanges();
                    await _pageDialog.DisplayAlertAsync("Регистрация", "Регистрация прошла успешно!", "OK");
                    await _navigationService.NavigateAsync("MainListView");
                }
                catch (Exception ex)
                {
                    await _pageDialog.DisplayAlertAsync("Сообщение об ошибке", ex.Message, "OK");
                }
            }

         }
     }

  }
    

