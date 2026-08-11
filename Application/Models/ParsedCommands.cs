using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public sealed record ParsedCommand(

        string Name,
        IReadOnlyList<string> Arguments
    );
}
