using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avito
{
    class ValueInt : IValue
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("params")]
        public List<Param> Params { get; set; }
        [JsonIgnore]
        public int intvalue { get; set; }

        public bool ShouldSerializeId()
        {
            return Id != 0;
        }        

    }
}
