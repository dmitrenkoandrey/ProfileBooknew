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
         
        }

 
    }
}