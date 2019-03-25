using HandleTrelloBoard.Repository.Constants;
using HandleTrelloBoard.Repository.Entities.Base;
using Newtonsoft.Json;
using ReadTextFile.Entities;
using System.Text;

namespace HandleTrelloBoard.Repository.Entities.Handlers
{
    public class CreateCardHandler : BaseHandler
    {
        public CreateCardHandler(string key, string token, string idList, string name, Topic topic) : 
            base(key, token)
        {
            IdList = idList;
            Name = "(Nível X) [D] Xh / [T] Xh: " + name;
            Description = FormatDescription(topic);
        }

        [JsonProperty("idList")]
        public string IdList { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("desc")]
        public string Description { get; private set; }      
        
        private string FormatDescription(Topic topic)
        {
            StringBuilder description = new StringBuilder();

            foreach (var topic2 in topic.Topics)
            {
                FormatCardDescription.ScoreCount++;

                description.AppendLine(FormatCardDescription.GetTopicLevel1() + topic2.DescricaoSubTopic2.Replace("*", "**"));

                foreach (var topic3 in topic2.Topics)
                {
                    description.AppendLine(FormatCardDescription.TopicLevel2 + topic3.DescricaoSubTopic3.Replace("*", "**"));

                    foreach (var topic4 in topic3.Topics)
                    {
                        description.AppendLine(FormatCardDescription.TopicLevel3 + topic4.DescricaoSubTopic4.Replace("*", "**"));

                        foreach (var topic5 in topic4.Topics)
                        {
                            description.AppendLine(FormatCardDescription.TopicLevel4 + topic5.DescricaoSubTopic5.Replace("*", "**"));
                        }
                    }
                }                
            }

            FormatCardDescription.ScoreCount = 0;

            return description.ToString();
        }
    }
}
