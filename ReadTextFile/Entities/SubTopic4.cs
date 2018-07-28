using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile.Entities
{
    public class SubTopic4
    {
        public string DescricaoSubTopic4 { get; set; }
        public List<SubTopic5> Topics { get; set; }

        public SubTopic4()
        {

        }

        public SubTopic4(string SubTopic)
        {
            DescricaoSubTopic4 = SubTopic;
        }

    }
}
