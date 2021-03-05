using ProfileBook.TreeView;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProfileBook.ViewModels;
using ProfileBook.Services.Repository;
using ProfileBook.Models;
using Xamarin.Essentials;
using System.IO;
using ProfileBook.Services.Profile;

namespace ProfileBook.Views
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditProfileView : ContentPage
    {
        string dbPath;
        public Image Img { set; get; }

        private PersonViewModel personViewModel;
        //public MainListView mainList = new MainListView();
        public static string Imagesource { set; get; }
        //public PersonViewModel ViewModel { get; private set; }
        public AddEditProfileView()
        {
            Img = new Image();
            //image1.Source = "pic_profile1";
            //lbRegDate.Text = DateTime.Now.ToString();
            InitializeComponent();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
            //var PersListView = new PersonsListViewModel();
          //  ProfileService profileService = new ProfileService();
            if (PersonsListViewModel.IsBusy1 == false)
            {
                image1.Source = "pic_profile1.png";

            }
            else
            {
                image1.Source = Imagesource;
            }

            //ViewModel = vm;
            //this.BindingContext = ViewModel;
            saveItem.IconImageSource = ImageSource.FromFile("save.png");
            takePhotoItem.IconImageSource = ImageSource.FromFile("ic_camera.png");
            getPhotoItem.IconImageSource = ImageSource.FromFile("ic_camera1.png");// выбор фото
          // getPhotoItem.Clicked += profileService.GetPhotoAsync;
           // takePhotoItem.Clicked += profileService.TakePhotoAsync;    

        }

        public AddEditProfileView(PersonViewModel personViewModel)
        {
            this.personViewModel = personViewModel;
        }

        private void SavePerson(object sender, EventArgs e)
        {

            var person = (Person)BindingContext;
            //person.RegDate = DateTime.Now.ToString();
            if (!String.IsNullOrEmpty(person.NickName))
            {
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {

                    if (person.Id == 0)
                        db.Persons.Add(person);
                    else
                    {
                        db.Persons.Update(person);
                    }
                    db.SaveChanges();
                }
            }
            this.Navigation.PushAsync(new MainListView());
        }
        public void DeletePerson(object sender, EventArgs e)
        {
            var person = (Person)BindingContext;
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                db.Persons.Remove(person);
                db.SaveChanges();
            }
            this.Navigation.PopAsync();
        }
        public async void GetPhotoAsync(object sender, EventArgs e)
        {
            try
            {
                // выбираем фото
                var photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
                // загружаем в ImageView
                PersonsListViewModel.IsBusy1 = true;
                Img.Source = ImageSource.FromFile(photo.FullPath);
                //IsBusy1 = false;
                AddEditProfileView.Imagesource = photo.FullPath;
                //IsBusy1 = true;
                Navigation.InsertPageBefore(new AddEditProfileView(), before: this);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
        public async void TakePhotoAsync(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                // для примера сохраняем файл в локальном хранилище
                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                // загружаем в ImageView
                Img.Source = ImageSource.FromFile(photo.FullPath);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
    }

}
