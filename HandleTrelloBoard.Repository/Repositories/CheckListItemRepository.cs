using HandleTrelloBoard.Repository.Constants;
using HandleTrelloBoard.Repository.Entities;
using HandleTrelloBoard.Repository.Entities.Handlers;
using HandleTrelloBoard.Repository.Entities.Responses;
using HandleTrelloBoard.Repository.Repositories.Base;
using System.Net.Http;

namespace HandleTrelloBoard.Repository.Repositories
{
    public class CheckListItemRepository : BaseRepository
    {
        public CreateCheckListItemResponse AddCheckListItem(CreateCheckListItem createCheckListItem)
        {
            HttpResponseMessage response = null;
            CreateCheckListItemResponse createCheckListItemCardResponse = null;

            CreateCheckLisItemtHandler createCheckListItemHandler = new CreateCheckLisItemtHandler
                (
                    createCheckListItem.Key,
                    createCheckListItem.Token,
                    createCheckListItem.Name
                 );

            response = Post(IntegrationTypes.CheckList, createCheckListItemHandler, createCheckListItem.IdCheckList, "checkItems");

            if (!response.IsSuccessStatusCode)
            {
                createCheckListItem.AddNotification("CreateCheckListItem", $"Ocorreu erro ao criar o item do CkeckList no checkList de id {createCheckListItem.IdCheckList}. Retorno API: ({response.StatusCode}) {response.Content.ReadAsStringAsync().Result}");
                return createCheckListItemCardResponse;
            }

            createCheckListItemCardResponse = response.Content.ReadAsAsync<CreateCheckListItemResponse>().Result;
            
            return createCheckListItemCardResponse;
        }
    }
}
