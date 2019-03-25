using Newtonsoft.Json;

namespace HandleTrelloBoard.Repository.Entities.Responses
{
    public class CreateCardResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }
    }
}
