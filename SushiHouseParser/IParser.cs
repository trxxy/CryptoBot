using System;
using System.Collections.Generic;
using System.Text;

namespace SushiHouseParser
{
    public interface IParser
    {
        List<ParsedSushi> ParseCatalog();
    }
}