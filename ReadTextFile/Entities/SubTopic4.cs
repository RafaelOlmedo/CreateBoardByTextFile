using ReadTextFile.Entities.Base;
using System.Collections.Generic;

namespace ReadTextFile.Entities
{
    public class SubTopic4 : BaseEntity
    {
        public string DescricaoSubTopic4 { get; set; }
        public List<SubTopic5> Topics { get; set; }

        public SubTopic4()
        {
            Topics = new List<SubTopic5>();
        }

        public SubTopic4(string SubTopic)
        {
            Topics = new List<SubTopic5>();
            DescricaoSubTopic4 = SubTopic;
        }

    }
}
