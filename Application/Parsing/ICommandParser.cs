using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Parsing
{
    public interface ICommandParser
    {
        ParsedCommand Parse(string input);
    }
}
