using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
 public   class SignUpViewModel : BindableBase
    {
        
        private UserLogin newuserLogin;
        string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        public string entnewLoginName { get; set; }
        public string entnewPassword { get; set; }
        public string entconfPassword { get; set; }
        public SignUpViewModel()
        {
        }
        INavigationService _navigationService;
        IPageDialogService _pageDialog;
        public SignUpViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public SignUpViewModel(INavigationService navigationService, IPageDialogService pageDialog)
        {
            _navigationService = navigationService;
            _pageDialog = pageDialog;
        }
        public ICommand btnLogupCommand => new Command(btnLogup);
        private async void btnLogup()
        {

            ApplicationContext db = new ApplicationContext(dbPath);
            string dblogin;
            string pwd;
            if (entnewLoginName == null || entnewPassword == null || entconfPassword == null) //проверяем заполнение всех полей
            {
                //await DisplayAlert("Регистрация", "Login и пароль не введены!", "OK");
                //await Navigation.PushAsync(new SignUpView());
                await _pageDialog.DisplayAlertAsync("Регистрация", "Login и пароль не введены!", "OK");
                //await DisplayAlert("Login", "Login и пароль ошибочны!", "OK");
                await _navigationService.NavigateAsync("SignUpView");
            }
            else
            {
                newuserLogin = db.UserLogins.FirstOrDefault(p => p.UserName == entnewLoginName
                ); //проверяем наличие в БД логина
                if (newuserLogin != null)
                {
                    //await DisplayAlert("Регистрация", "Login уже зарегистрирован! Введите новое имя!", "OK");
                    //await Navigation.PushAsync(new SignUpView());
                    await _pageDialog.DisplayAlertAsync("Регистрация", "Login уже зарегистрирован! Введите новое имя!!", "OK");
                    await _navigationService.NavigateAsync("SignUpView");
                }
                else
                {
                    if (entnewPassword != entconfPassword)
                    {
                        //await DisplayAlert("Регистрация", "Пароли регистрации и подтверждения должны быть одинаковы!", "OK");
                        //await Navigation.PushAsync(new SignUpView());
                        await _pageDialog.DisplayAlertAsync("Регистрация", "Пароли регистрации и подтверждения должны быть одинаковы!", "OK");
                        await _navigationService.NavigateAsync("SignUpView");
                    }
                    else
                    {
                        if (entnewPassword.Length <= 4)
                        {
                            //await DisplayAlert("Регистрация", "Длина пароля регистрации должна быть больше 4!", "OK");
                            //await Navigation.PushAsync(new SignUpView());
                            await _pageDialog.DisplayAlertAsync("Регистрация", "Длина пароля регистрации должна быть больше 4!", "OK");
                            await _navigationService.NavigateAsync("SignUpView");

                        }
                        else
                        {
                            try
                            {
                                db.UserLogins.Add(new UserLogin { UserName = entnewLoginName, Password = entnewPassword });
                                db.SaveChanges();
                                //await DisplayAlert("Регистрация", "Регистрация прошла успешно!", "ОK");
                                //await Navigation.PushAsync(new SignInView());
                                await _pageDialog.DisplayAlertAsync("Регистрация", "Регистрация прошла успешно!", "OK");
                                await _navigationService.NavigateAsync("SignInView");
                            }
                            catch (Exception ex)
                            {
                                // await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
                                await _pageDialog.DisplayAlertAsync("Сообщение об ошибке", ex.Message, "OK");
                            }
                        }
                    }

                }
            }

        }
    }
}
