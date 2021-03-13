using System;
using ProfileBook.Droid;
using System.IO;
using Xamarin.Forms;
using ProfileBook.Services.Repository;

[assembly: Dependency(typeof(AndroidDbPath))]
namespace ProfileBook.Droid
{
    public class AndroidDbPath : IPath
    {
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
        }
    }
}