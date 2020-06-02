using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisualBIChatBot.Common.Models
{
    public class EntityLuis
    {
        public List<DatetimeEntity> datetime { get; set; }

        [JsonProperty("$instance")]
        public Instance Instance { get; set; }
    }

    public class DatetimeEntity
    {
        public List<string> timex { get; set; }
        public string type { get; set; }
    }
    public class Instance
    {
        public List<Gestion> Gestion { get; set; }
    }
    public class Gestion
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string ModelType { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public List<string> RecognitionSources { get; set; }
    }
}
