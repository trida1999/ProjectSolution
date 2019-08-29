using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avito
{
    interface IValue
    {
        int Id { get; set; }
        string Title { get; set; }
        List<Param> Params { get; set; }
        
    }
}

