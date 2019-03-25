using HandleTrelloBoard.Repository.Constants;
using HandleTrelloBoard.Repository.Entities;
using HandleTrelloBoard.Repository.Entities.Handlers;
using HandleTrelloBoard.Repository.Entities.Responses;
using HandleTrelloBoard.Repository.Repositories.Base;
using ReadTextFile.Entities;
using System.Net.Http;
using System.Text;

namespace HandleTrelloBoard.Repository.Repositories
{
    public class CardRepository : BaseRepository
    {
        public CreateCardResponse AddCard(CreateCard createCard)
        {
            HttpResponseMessage response = null;
            CreateCardResponse createCardResponse = null;

            CreateCardHandler createCardHandler = new CreateCardHandler
                (
                    createCard.Key,
                    createCard.Token,
                    createCard.BacklogListId,
                    createCard.Topic.Description,
                    createCard.Topic
                 );

            response = Post(IntegrationTypes.Card, createCardHandler);

            if(!response.IsSuccessStatusCode)
            {
                createCard.AddNotification("CreateCard", $"Ocorreu erro ao criar o cartão {createCard.Topic.Description}. Retorno API: ({response.StatusCode}) {response.Content.ReadAsStringAsync().Result}");
                return createCardResponse;
            }

            createCardResponse = response.Content.ReadAsAsync<CreateCardResponse>().Result;

            return createCardResponse;
        }
    }
}
