using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile.Entities
{
    public class SubTopic3
    {
        public string DescricaoSubTopic3 { get; set; }
        public List<SubTopic4> Topics { get; set; }

        public SubTopic3()
        {

        }

        public SubTopic3(string SubTopic)
        {
            DescricaoSubTopic3 = SubTopic;
        }

    }
}
