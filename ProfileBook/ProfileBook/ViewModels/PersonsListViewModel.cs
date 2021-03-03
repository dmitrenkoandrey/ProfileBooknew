using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using ProfileBook.Views;
using ProfileBook.TreeView;
using System.Threading.Tasks;
using System;
using System.IO;
using Xamarin.Essentials;
using ProfileBook.Services;
using ProfileBook.Models;

namespace ProfileBook.ViewModels
{
    
    public class PersonsListViewModel : INotifyPropertyChanged
    {
        public Image Img { protected set; get; }
        //public Validator valid = new Validator();
        public ObservableCollection<PersonViewModel> Persons { get; set; }
        public static bool IsBusy1 { set; get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public AddEditProfileView addedit = new AddEditProfileView();
        public ICommand CreatePersonCommand { protected set; get; }
        public ICommand DeletePersonCommand { protected set; get; }
        public ICommand SavePersonCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        public ICommand TakePhotoCommand { protected set; get; }
        public ICommand GetPhotoCommand { protected set; get; }
        public ICommand EditPersonCommand { protected set; get; }
        PersonViewModel selectedPerson;
        public INavigation Navigation { get; set; }

        public PersonsListViewModel()
        {
            Persons = new ObservableCollection<PersonViewModel>();
            CreatePersonCommand = new Command(CreatePerson);
            EditPersonCommand = new Command(EditPerson);
            DeletePersonCommand = new Command(DeletePerson);
            SavePersonCommand = new Command(SavePerson);
            TakePhotoCommand = new Command(TakePhotoAsync);
            GetPhotoCommand = new Command(GetPhotoAsync);
            BackCommand = new Command(Back);
            Img = new Image();
            //IsBusy1 = false;
        }

        public PersonViewModel SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                if (selectedPerson != value)
                {
                    PersonViewModel tempPerson = value;
                    selectedPerson = null;
                    OnPropertyChanged("SelectedPerson");
                    Navigation.PushAsync(new AddEditProfileView(tempPerson));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void CreatePerson()
        {
            Person person = new Person();
            AddEditProfileView personPage = new AddEditProfileView();
            personPage.BindingContext = person;
            //Navigation.PushAsync(new AddEditProfileView(new PersonViewModel()));//{ ListViewModel = this }));
        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private async void SavePerson(object personObject) //отключен
        {  
            PersonViewModel  person = personObject as PersonViewModel;
            if (person != null && person.IsValid && !Persons.Contains(person))//проверка валидации данных
            {
               // valid.ValidMessageAsync;  //"Валидация, Поля  заполнены!"
                Persons.Add(person);
            }
            else
             {
            
            Back();
          
            }

           //Back();
          await  Navigation.PopAsync();
            
        }

        private async void EditPerson(object personObject)
        {
            PersonViewModel person = personObject as PersonViewModel;
            //if (person != null)
            {
                // Persons.Remove(person);
                await Navigation.PushAsync(new AddEditProfileView());
            }
            //Back();
        }

        private void DeletePerson(object personObject)
        {
            PersonViewModel person = personObject as PersonViewModel;
            if (person != null)
            {
                Persons.Remove(person);
            }
            Back();
        }

      public async void TakePhotoAsync()
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
                //await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
    public async void GetPhotoAsync()
        {
            try
            {
                // выбираем фото
                var photo = await MediaPicker.PickPhotoAsync();
                // загружаем в ImageView
                IsBusy1 = true;
                Img.Source = ImageSource.FromFile(photo.FullPath);
                //IsBusy1 = false;
                AddEditProfileView.Imagesource = photo.FullPath;
             await Navigation.PushAsync(new AddEditProfileView(new PersonViewModel() { ListViewModel = this }));

            }

            catch (Exception ex)
            {
                //await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
    }
}