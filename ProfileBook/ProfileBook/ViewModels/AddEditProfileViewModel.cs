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
        // string dbPath;
        // public Image Img { set; get; }

        //// private PersonViewModel personViewModel;
        // public Image image1;

        //public MainListView mainList = new MainListView();
        //public static string Imagesource { set; get; }
        //ToolbarItem saveItem { get; set; }
        //ToolbarItem takePhotoItem { get; set; }
        //ToolbarItem getPhotoItem { get; set; }
        public AddEditProfileViewModel()
        { }

        //  Img = new Image();
        //image1.Source = "pic_profile1";
        //lbRegDate.Text = DateTime.Now.ToString();
        // dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        //var PersListView = new PersonsListViewModel();
        //  ProfileService profileService = new ProfileService();
        //if (PersonsListViewModel.IsBusy1 == false)
        //{
        //    image1.Source = "pic_profile1.png";

        //}
        //else
        //{
        //    image1.Source = Imagesource;
        //}

        ////ViewModel = vm;
        ////this.BindingContext = ViewModel;
        //saveItem.IconImageSource = ImageSource.FromFile("save.png");
        //takePhotoItem.IconImageSource = ImageSource.FromFile("ic_camera.png");
        //getPhotoItem.IconImageSource = ImageSource.FromFile("ic_camera1.png");// выбор фото
        //                                                                      // getPhotoItem.Clicked += profileService.GetPhotoAsync;
        // takePhotoItem.Clicked += profileService.TakePhotoAsync;    

        //}
        // public AddEditProfileViewModel(PersonViewModel personViewModel)
        ///{
        // this.personViewModel = personViewModel;
        //  }
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

            //var person = (Person)BindingContext;
            //  //person.RegDate = DateTime.Now.ToString();
            //  if (!String.IsNullOrEmpty(person.NickName))
            //  {
            //      using (ApplicationContext db = new ApplicationContext(dbPath))
            //      {

            //          if (person.Id == 0)
            //              db.Persons.Add(person);
            //          else
            //          {
            //              db.Persons.Update(person);
            //          }
            //          db.SaveChanges();
            //      }
            //  }
            //  await _navigationService.NavigateAsync("MainListView");
        }
    

