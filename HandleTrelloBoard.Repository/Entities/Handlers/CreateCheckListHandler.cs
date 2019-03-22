using HandleTrelloBoard.Repository.Entities.Base;
using Newtonsoft.Json;

namespace HandleTrelloBoard.Repository.Entities.Handlers
{
    public class CreateCheckListHandler : BaseHandler
    {
        public CreateCheckListHandler(string key, string token, string idCard, string name) : 
            base(key, token)
        {
            IdCard = idCard;
            Name = name;
        }

        [JsonProperty("idCard")]
        public string IdCard { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }
    }
}
