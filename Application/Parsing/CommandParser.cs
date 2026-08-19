using System;
using System.Collections.Generic;
using System.Text;
using Application.Models;

namespace Application.Parsing
{
    public sealed class CommandParser : ICommandParser
    {
        public ParsedCommand Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ParsedCommand(string.Empty, Array.Empty<string>());
            }

            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var commandName = parts[0].ToLowerInvariant();
            var arguments = parts.Skip(1).ToArray();

            return new ParsedCommand(commandName, arguments);
        }
    }
}
