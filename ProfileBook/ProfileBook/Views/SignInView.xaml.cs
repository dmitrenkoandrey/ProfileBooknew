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

namespace ProfileBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInView : ContentPage
    {
        private UserLogin userLogin;
        string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        public SignInView()
        {

            userLogin = new UserLogin() { UserName = "" };
            InitializeComponent();
            BindingContext = this;
            AuthorizationService authorization = new AuthorizationService();
            Binding binding = new Binding() {Source = entLoginName, Path = "Text"};
           // authorization.entLoginName.SetBinding(authorization.entLoginName, binding);


            //  entLoginName.Text = "user";
            // entPassword.Text = "1234567";
            // BindableObject.BindingContextProperty  authorization.entLoginName.SetBinding(Entry.TextProperty, new Binding { Source = entLoginName, Path = "Text" });
            //authorization.entLoginName.SetBinding(authorization.entLoginName, entLoginName); 
            // entPassword.SetBinding(Entry.TextProperty, new Binding { Source = userLogin, Path = "Password" });

        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            ApplicationContext db = new ApplicationContext(dbPath);
            string dblogin;
            string pwd;
            userLogin = db.UserLogins.FirstOrDefault(p => p.UserName == entLoginName.Text);
            //pwd = userLogin.Password;
            if (entLoginName.Text == null || entPassword.Text == null)
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
        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Navigation.PushAsync(new SignUpView());
        });
    }
}