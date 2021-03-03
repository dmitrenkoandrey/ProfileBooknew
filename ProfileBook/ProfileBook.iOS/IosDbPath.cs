using System;
using Xamarin.Forms;
using System.IO;
using ProfileBook.iOS;
using ProfileBook.Services.Repository;

[assembly: Dependency(typeof(IosDbPath))]
namespace ProfileBook.iOS
{
    public class IosDbPath : IPath
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            // определяем путь к бд
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", sqliteFilename);
        }
    }
}
