using Newtonsoft.Json;

namespace HandleTrelloBoard.Repository.Entities.Responses
{
    public class CreateCheckListItemResponse
    {
        [JsonProperty("idChecklist")]
        public string IdCheckList { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
