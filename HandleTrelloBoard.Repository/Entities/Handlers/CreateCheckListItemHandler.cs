using HandleTrelloBoard.Repository.Entities.Base;
using Newtonsoft.Json;

namespace HandleTrelloBoard.Repository.Entities.Handlers
{
    public class CreateCheckLisItemtHandler : BaseHandler
    {
        public CreateCheckLisItemtHandler(string key, string token, string name) :
            base(key, token)
        {
            Name = name;
        }

        [JsonProperty("name")]
        public string Name { get; private set; }
    }
}
