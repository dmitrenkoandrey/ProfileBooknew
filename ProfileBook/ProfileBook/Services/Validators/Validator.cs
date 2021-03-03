using System;
using System.Text;
using ProfileBook.Services;
using Xamarin.Forms;

namespace ProfileBook
{
    public class Validator : ContentPage
    {
        public Validator()
        {
        }

        public async void ValidMessageAsync(object sender, EventArgs e)
        {
            await DisplayAlert("Валидация", "Поля не заполнены!", "ОK");
        }
    }

}