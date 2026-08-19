using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.FileSystem
{
    public sealed class FileSystemService : IFileSystemService
    {
        public IReadOnlyList<string> ListEntries(string path)
        {
            var directories = Directory
                .GetDirectories(path)
                .Select(directory => $"[DIR] {Path.GetFileName(directory)}");

            var files = Directory
                .GetFiles(path)
                .Select(file => $"[FILE] {Path.GetFileName(file)}");
            return directories
                .Concat(files)
                .OrderBy(entry => entry)
                .ToList();
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
        
        public void CreateFile(string path)
        {
            if(File.Exists(path))
            {
                return;
            }
            using var stream = File.Create(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
