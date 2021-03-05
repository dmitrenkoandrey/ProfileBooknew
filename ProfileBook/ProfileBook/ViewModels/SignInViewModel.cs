using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
//using Prism.Navigation.Xaml;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : BindableBase,INavigatedAware
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        public string entLoginName { get; set; }
        public string entPassword { get; set; }
        private UserLogin userLogin;
        string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        public SignInViewModel()
        { }
            INavigationService _navigationService;
        IPageDialogService _pageDialog;
            public SignInViewModel(INavigationService navigationService)
            {
            _navigationService = navigationService;
            }
        
        public SignInViewModel(INavigationService navigationService, IPageDialogService pageDialog)
        {
            _navigationService = navigationService;
            _pageDialog = pageDialog;
        }

        
        //public ICommand IncrementButtonTapCommand => new Command(OnIncrementButtonTap);
        public ICommand btnLoginCommand => new Command(btnLogin);
        public ICommand ClickCommand => new Command<string>((url) =>
        {
            //Navigation.PushAsync(new SignUpView());
            _navigationService.NavigateAsync("SignUpView");
        });
        //private int _count;
        //public int Count
        //{
        //    get => _count;
        //    set => SetProperty(ref _count, value);   
        //}

        //private void OnIncrementButtonTap()
        //{
        //    Count++;
        //}
        private async void btnLogin()
        {
            ApplicationContext db = new ApplicationContext(dbPath);
            string dblogin;
            string pwd;
            userLogin = db.UserLogins.FirstOrDefault(p => p.UserName == entLoginName);
            //pwd = userLogin.Password;
            if (entLoginName == null || entPassword == null)
            {
              await  _pageDialog.DisplayAlertAsync("Login", "Login и пароль не введены!", "OK");
                //await DisplayAlert("Login", "Login и пароль не введены!", "OK");
                await _navigationService.NavigateAsync("SignInView");
                //await Navigation.PushAsync(new SignInView());
                //NavigationPage.NavigateAsync
            }
            else
            {
                userLogin = db.UserLogins.FirstOrDefault(p => p.UserName == entLoginName);
                if (userLogin != null)
                {
                    dblogin = userLogin.UserName;
                    pwd = userLogin.Password;
                    if ((entLoginName != dblogin) || (entPassword != pwd))
                    {
                        await _pageDialog.DisplayAlertAsync("Login", "Login и пароль ошибочны!", "OK");
                        //await DisplayAlert("Login", "Login и пароль ошибочны!", "OK");
                        await _navigationService.NavigateAsync("SignInView");
                    }
                    else
                    {
                        
                        await _navigationService.NavigateAsync("MainListView");
                        //await Navigation.PushAsync(new MainListView());
                    }
                }
                else
                {
                    await _pageDialog.DisplayAlertAsync("Login", "Login и пароль ошибочны!", "OK");
                    //await DisplayAlert("Login", "Login и пароль ошибочны!", "OK");
                    // await Navigation.PushAsync(new SignInView());
                    await _navigationService.NavigateAsync("SignInView");
                }
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }
    }
}
