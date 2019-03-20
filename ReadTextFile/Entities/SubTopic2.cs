using ReadTextFile.Entities.Base;
using System.Collections.Generic;

namespace ReadTextFile.Entities
{
    public class SubTopic2 : BaseEntity
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
