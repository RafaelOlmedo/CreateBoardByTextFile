using HandleTrelloBoard.Repository.Constants;
using HandleTrelloBoard.Repository.Entities;
using HandleTrelloBoard.Repository.Entities.Handlers;
using HandleTrelloBoard.Repository.Entities.Responses;
using HandleTrelloBoard.Repository.Repositories.Base;
using System.Net.Http;

namespace HandleTrelloBoard.Repository.Repositories
{
    public class CheckListRepository : BaseRepository
    {
        public CreateCheckListResponse AddCheckList(CreateCheckList createCheckList)
        {
            HttpResponseMessage response = null;
            CreateCheckListResponse createCheckListResponse = null;

            CreateCheckListHandler createCheckListHandler = new CreateCheckListHandler
                (
                    createCheckList.Key,
                    createCheckList.Token,
                    createCheckList.IdCard,
                    createCheckList.Name
                 );

            response = Post(IntegrationTypes.CheckList, createCheckListHandler);

            if (!response.IsSuccessStatusCode)
            {
                createCheckList.AddNotification("CreateCheckList", $"Ocorreu erro ao criar o CkeckList no cartão de id {createCheckList.IdCard}. Retorno API: ({response.StatusCode}) {response.Content.ReadAsStringAsync().Result}");
                return createCheckListResponse;
            }

            createCheckListResponse = response.Content.ReadAsAsync<CreateCheckListResponse>().Result;

            return createCheckListResponse;
        }
    }
}
