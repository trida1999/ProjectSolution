using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avito
{
    class Param : IParam
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("value")]
        public object SelectedValue { get; set; }
        [JsonProperty("values")]
        public List<ValueInt> Values { get; set; }

        public bool ShouldSerializeValues()
        {
            return Values != null;
        }
    }
}
