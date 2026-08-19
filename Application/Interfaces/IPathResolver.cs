using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IPathResolver
    {
        string Resolve(string currentDirectory, string inputPath);
    }
}
