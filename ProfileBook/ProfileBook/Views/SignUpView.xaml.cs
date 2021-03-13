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
    public partial class SignUpView : ContentPage
    {
        string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        private UserLogin newuserLogin;
        public SignUpView()
        {
            
            newuserLogin = new UserLogin() { UserName = "" };
            InitializeComponent();
        }
  
    }
}