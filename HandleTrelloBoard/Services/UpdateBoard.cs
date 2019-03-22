using HandleTrelloBoard.Entities;
using HandleTrelloBoard.Repository.Entities;
using HandleTrelloBoard.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using TrelloNet;

namespace HandleTrelloBoard.Services
{
    public class UpdateBoard
    {
        public UpdateTemplateBoard UpdateTemplateBoard(UpdateTemplateBoard updateTemplateBoard)
        {
            #region 1 - Recupera quadro template.

            if (string.IsNullOrEmpty(updateTemplateBoard.IdTemplateBoard))
            {
                updateTemplateBoard.AddNotification("IdBoard", "Id do quadro template não foi informado");
                return updateTemplateBoard;
            }

            GetBoardById(updateTemplateBoard);

            if (updateTemplateBoard.Invalid)
                return updateTemplateBoard;

            #endregion

            #region 2 - Recupera lista 'Backlog' no quadro template.

            GetBacklogList(updateTemplateBoard);

            if (updateTemplateBoard.Invalid)
                return updateTemplateBoard;

            #endregion

            #region 3 - Cria os cartões.

            CardRepository cardRepository = new CardRepository();

            foreach (var topic in updateTemplateBoard.Estimate.Topics)
            {
                var createCard = new CreateCard
                                (
                                    updateTemplateBoard.Key,
                                    updateTemplateBoard.Token,
                                    updateTemplateBoard.BacklogList.Id,
                                    topic
                                );

                var createCardResponse = cardRepository.AddCard(createCard);

                if(createCard.Invalid)
                {
                    updateTemplateBoard.AddNotification(createCard.Notifications.FirstOrDefault().Property, createCard.Notifications.FirstOrDefault().Message);
                    return updateTemplateBoard;
                }

                Card card = new Card()
                {
                    Id = createCardResponse.Id,
                    Desc = createCardResponse.Description,
                    Name = createCardResponse.Name,
                };

                updateTemplateBoard.AddCard(card);

                #region 3.1 - Cria o CheckList "Tópicos a serem desenvolvidos".

                CheckListRepository checkListRepository = new CheckListRepository();

                var createCheckList = new CreateCheckList
                    (
                       updateTemplateBoard.Key,
                       updateTemplateBoard.Token,
                       createCardResponse.Id,
                       "Tópicos que serão desenvolvidos"
                    );

                // Não grava em nunhum lugar essa informação no momento, pois não é necessária. Caso em algum momento seja necessário utilizar essa informação, necessário implementar.
                var createCheckListResponse = checkListRepository.AddCheckList(createCheckList);

                if (createCheckList.Invalid)
                {
                    updateTemplateBoard.AddNotification(createCheckList.Notifications.FirstOrDefault().Property, createCheckList.Notifications.FirstOrDefault().Message);
                    return updateTemplateBoard;
                }

                #endregion

                #region 3.2 - Cria os itens para o CheckList "Tópicos a serem desenvolvidos".

                int checkListItemCount = 0;

                foreach (var topic2 in topic.Topics)
                {
                    checkListItemCount++;

                    CheckListItemRepository checkListItemRepository = new CheckListItemRepository();

                    var createCheckListItem = new CreateCheckListItem
                        (
                           updateTemplateBoard.Key,
                           updateTemplateBoard.Token,
                           createCheckListResponse.Id,
                           $"Ponto {checkListItemCount}"
                        );

                    var checkListItemResponse = checkListItemRepository.AddCheckListItem(createCheckListItem);

                    if (createCheckList.Invalid)
                    {
                        updateTemplateBoard.AddNotification(createCheckList.Notifications.FirstOrDefault().Property, createCheckList.Notifications.FirstOrDefault().Message);
                        return updateTemplateBoard;
                    }
                }
                                
                #endregion
            }

            #endregion


            return updateTemplateBoard;
        }

        private UpdateTemplateBoard GetBoardById(UpdateTemplateBoard updateTemplateBoard)
        {
            try
            {
                var templateBoard = updateTemplateBoard.Trello.Boards.WithId(updateTemplateBoard.IdTemplateBoard);
                updateTemplateBoard.AddTemplateBoard(templateBoard);
            }
            catch (Exception ex)
            {
                if (ex.Message == "invalid id")
                    updateTemplateBoard.AddNotification(nameof(updateTemplateBoard.TemplateBoard), "Quadro template não encontrado. Id informado não encontrou nenhum quadro");
            }

            return updateTemplateBoard;
        }

        private UpdateTemplateBoard GetBacklogList(UpdateTemplateBoard updateTemplateBoard)
        {
            // Recupera as listas do quadro de template.
            IEnumerable<List> templateBoardLists = updateTemplateBoard.Trello.Lists.ForBoard(updateTemplateBoard.TemplateBoard);

            if (templateBoardLists == null)
            {
                updateTemplateBoard.AddNotification(nameof(updateTemplateBoard.BacklogList), $"Erro ao recuperar listas presentes no quadro ({ updateTemplateBoard.TemplateBoard.Name })");
                return updateTemplateBoard;
            }

            var backlogList = templateBoardLists
                .FirstOrDefault(l => l.Name == "Backlog");

            if (backlogList == null)
            {
                updateTemplateBoard.AddNotification(nameof(updateTemplateBoard.BacklogList), $"Não foi encontrada nenhuma lista chamada Backlog no quadro ({ updateTemplateBoard.TemplateBoard.Name })");
                return updateTemplateBoard;
            }

            updateTemplateBoard.AddBacklogList(backlogList);

            return updateTemplateBoard;
        }
    }
}
