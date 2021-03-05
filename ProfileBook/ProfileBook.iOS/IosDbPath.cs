using System;
using Xamarin.Forms;
using System.IO;
using ProfileBook.iOSInitializer;
using ProfileBook.Services.Repository;

[assembly: Dependency(typeof(IosDbPath))]
namespace ProfileBook.iOSInitializer
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
