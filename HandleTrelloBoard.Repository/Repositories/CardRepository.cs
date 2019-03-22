using HandleTrelloBoard.Repository.Constants;
using HandleTrelloBoard.Repository.Entities;
using HandleTrelloBoard.Repository.Repositories.Base;
using ReadTextFile.Entities;
using System.Net.Http;
using System.Text;

namespace HandleTrelloBoard.Repository.Repositories
{
    public class CardRepository : BaseRepository
    {
        public void AddCard(string key, string token, string idList, Topic topic)
        {
            HttpResponseMessage response = null;        

            CreateCard createCard = new CreateCard
                (
                    key, 
                    token, 
                    idList, 
                    topic.Description,
                    topic
                 );

            response = Post(IntegrationTypes.Card, createCard);
        }
    }
}
