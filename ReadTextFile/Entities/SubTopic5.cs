using ReadTextFile.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile.Entities
{
    public class SubTopic5 : BaseEntity
    {
        public string DescricaoSubTopic5 { get; set; }

        public SubTopic5(string SubTopic)
        {
            DescricaoSubTopic5 = SubTopic;
        }

    }
}
