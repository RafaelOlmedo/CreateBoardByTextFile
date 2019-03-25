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
            Topics = new List<SubTopic3>();
        }

        public SubTopic2(string SubTopic)
        {
            Topics = new List<SubTopic3>();
            DescricaoSubTopic2 = SubTopic;
        }

    }
}
