using Newtonsoft.Json;

namespace HandleTrelloBoard.Repository.Entities.Responses
{
    public class CreateCheckListResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
