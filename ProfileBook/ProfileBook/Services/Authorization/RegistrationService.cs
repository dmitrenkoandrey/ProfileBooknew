using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProfileBook.Models;
using ProfileBook.Services;
using ProfileBook.TreeView;
using ProfileBook.ViewModels;
using ProfileBook;
using System.Windows.Input;
using Xamarin.Essentials;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Authorization;
using ProfileBook.Views;

namespace ProfileBook.Services.Authorization
{
  public  class RegistrationService : ContentPage
    {
        public Entry entnewLoginName;
        public Entry entnewPassword;
        public Entry entconfPassword;
        public RegistrationService()
        {
        }
        
        string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        private UserLogin newuserLogin;
        private async void btnLogup_Clicked(object sender, EventArgs e)
        {

            ApplicationContext db = new ApplicationContext(dbPath);
            string dblogin;
            string pwd;
            if (entnewLoginName.Text == null || entnewPassword.Text == null || entconfPassword == null) //проверяем заполнение всех полей
            {
                await DisplayAlert("Регистрация", "Login и пароль не введены!", "OK");
                await Navigation.PushAsync(new SignUpView());
            }
            else
            {
                newuserLogin = db.UserLogins.FirstOrDefault(p => p.UserName == entnewLoginName.Text); //проверяем наличие в БД логина
                if (newuserLogin != null)
                {
                    await DisplayAlert("Регистрация", "Login уже зарегистрирован! Введите новое имя!", "OK");
                    await Navigation.PushAsync(new SignUpView());
                }
                else
                {
                    if (entnewPassword.Text != entconfPassword.Text)
                    {
                        await DisplayAlert("Регистрация", "Пароли регистрации и подтверждения должны быть одинаковы!", "OK");
                        await Navigation.PushAsync(new SignUpView());
                    }
                    else
                    {
                        if (entnewPassword.Text.Length <= 4)
                        {
                            await DisplayAlert("Регистрация", "Длина пароля регистрации должна быть больше 4!", "OK");
                            await Navigation.PushAsync(new SignUpView());
                        }
                        else
                        {
                            try
                            {
                                db.UserLogins.Add(new UserLogin { UserName = entnewLoginName.Text, Password = entnewPassword.Text });
                                db.SaveChanges();
                                await DisplayAlert("Регистрация", "Регистрация прошла успешно!", "ОK");
                                await Navigation.PushAsync(new SignInView());
                            }
                            catch (Exception ex)
                            {
                                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
                            }
                        }
                    }

                }
            }

        }
    }
}
