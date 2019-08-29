using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avito
{
    class error
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        public error(string a)
        {
            Message = a;
        }
    }
}
