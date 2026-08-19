using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IFileSystemService
    {
        IReadOnlyList<string> ListEntries(string path);
        void CreateDirectory(string path);
        void CreateFile(string path);
        bool DirectoryExists(string path);
    }
}
