using HandleTrelloBoard.Repository.Entities.Base;
using ReadTextFile.Entities;

namespace HandleTrelloBoard.Repository.Entities
{
    public class CreateCard : BaseEntity
    {
        public CreateCard(string key, string token, string backlogListId, Topic topic) : 
            base(key, token)
        {
            BacklogListId = backlogListId;
            Topic = topic;
        }     
        
        public string BacklogListId { get; private set; }
        public Topic Topic { get; private set; }
    }
}
