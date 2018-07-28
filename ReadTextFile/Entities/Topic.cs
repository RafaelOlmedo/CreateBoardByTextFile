using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile.Entities
{
    public class Topic
    {
        public string Descricao { get; set; }
        public string Label { get; set; }
        public decimal Time { get; set; }
        public string Level { get; set; }

        public List<SubTopic2> Topics { get; set; }

        public Topic()
        {
            //this.Descricao = Descricao;
        }
    }
}
