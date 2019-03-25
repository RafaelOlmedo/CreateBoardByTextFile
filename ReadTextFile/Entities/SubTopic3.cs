using ReadTextFile.Entities.Base;
using System.Collections.Generic;

namespace ReadTextFile.Entities
{
    public class SubTopic3 : BaseEntity
    {
        public string DescricaoSubTopic3 { get; set; }
        public List<SubTopic4> Topics { get; set; }

        public SubTopic3()
        {
            Topics = new List<SubTopic4>();
        }

        public SubTopic3(string SubTopic)
        {
            Topics = new List<SubTopic4>();
            DescricaoSubTopic3 = SubTopic;
        }

    }
}
