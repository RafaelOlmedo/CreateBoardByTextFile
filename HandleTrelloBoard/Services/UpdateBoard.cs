using HandleTrelloBoard.Entities;
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
            #region 1° - Recupera quadro template.

            if (string.IsNullOrEmpty(updateTemplateBoard.IdTemplateBoard))
            {
                updateTemplateBoard.AddNotification("IdBoard", "Id do quadro template não foi informado");
                return updateTemplateBoard;
            }

            GetBoardById(updateTemplateBoard);

            if (updateTemplateBoard.Invalid)
                return updateTemplateBoard;

            #endregion

            #region 2° - Recupera lista 'Backlog' no quadro template.

            GetBacklogList(updateTemplateBoard);

            if (updateTemplateBoard.Invalid)
                return updateTemplateBoard;

            #endregion

            #region 3° - Cria os cartões.

            CardRepository cardRepository = new CardRepository();

            foreach (var topic in updateTemplateBoard.Estimate.Topics)
            {
                cardRepository.AddCard
                    (
                        updateTemplateBoard.Key,
                        updateTemplateBoard.Token,
                        updateTemplateBoard.BacklogList.Id,
                        topic
                    );
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
                .Where(l => l.Name == "Backlog")
                .FirstOrDefault();

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
