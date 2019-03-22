using Newtonsoft.Json;

namespace HandleTrelloBoard.Repository.Entities.Base
{
    public abstract class BaseEntity
    {
        public BaseEntity(string key, string token)
        {
            Key = key;
            Token = token;
        }

        [JsonProperty("key")]
        public string Key { get; private set; }

        [JsonProperty("token")]
        public string Token { get; private set; }
    }
}
