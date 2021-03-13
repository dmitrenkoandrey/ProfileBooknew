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

namespace ProfileBook.Services.Authorization
{
   public class AuthorizationService : ContentPage
    {
      public  AuthorizationService() { }
        string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        public UserLogin userlogin = new UserLogin();
        public Entry entLoginName;
        public Entry entPassword;
        
        public async void btnLogin_ClickedAsync(object sender, EventArgs e)
        {
            {
               // public UserLogin userlogin = new UserLogin();
        ApplicationContext db = new ApplicationContext(dbPath);
                string dblogin;
                string pwd;
              UserLogin  userLogin = db.UserLogins.FirstOrDefault(p => p.UserName == entLoginName.Text);
                //pwd = userLogin.Password;
                if (entLoginName == null || entPassword == null)
                {
                    await DisplayAlert("Login", "Login и пароль не введены!", "OK");

                    await Navigation.PushAsync(new SignInView());
                }
                else
                {
                    userLogin = db.UserLogins.FirstOrDefault(p => p.UserName == entLoginName.Text);
                    if (userLogin != null)
                    {
                        dblogin = userLogin.UserName;
                        pwd = userLogin.Password;
                        if ((entLoginName.Text != dblogin) || (entPassword.Text != pwd))
                        {
                            await DisplayAlert("Login", "Login и пароль ошибочны!", "OK");
                            await Navigation.PushAsync(new SignInView());
                        }
                        else
                        {
                            await Navigation.PushAsync(new MainListView());
                        }
                    }
                    else
                    {
                        await DisplayAlert("Login", "Login и пароль ошибочны!", "OK");
                        await Navigation.PushAsync(new SignInView());
                    }
                }
            }
        }
    }
}
