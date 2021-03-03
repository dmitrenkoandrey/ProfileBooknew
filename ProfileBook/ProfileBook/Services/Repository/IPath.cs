using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Repository
{
    public interface IPath
    {
        string GetDatabasePath(string filename);
    }
}

