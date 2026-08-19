using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.FileSystem
{
    public sealed class PathResolver : IPathResolver
    {
        public string Resolve(string currentDirectory, string inputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
            {
                return currentDirectory;
            }
            
            var path = Path.IsPathRooted(inputPath)
                ? inputPath
                : Path.Combine(currentDirectory, inputPath);

            return Path.GetFullPath(path);
        }
    }
}
