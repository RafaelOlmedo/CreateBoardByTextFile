using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile.Entities
{
    public class SubTopic2
    {
        public string DescricaoSubTopic2 { get; set; }
        public List<SubTopic3> Topics { get; set; }

        public SubTopic2()
        {

        }

        public SubTopic2(string SubTopic)
        {
            DescricaoSubTopic2 = SubTopic;
        }

    }
}
