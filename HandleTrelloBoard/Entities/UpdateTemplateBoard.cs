using HandleTrelloBoard.Entities.Base;
using ReadTextFile.Entities;
using TrelloNet;

namespace HandleTrelloBoard.Entities
{
    public class UpdateTemplateBoard : BaseEntity
    {
        public UpdateTemplateBoard(string key, string token, string idTemplateBoard, Estimate estimate, Trello trello)
        {
            Key = key;
            Token = token;
            IdTemplateBoard = idTemplateBoard
                .Replace("https://trello.com/b/", "");
            Estimate = estimate;
            Trello = trello;
        }

        public string Key { get; private set; }
        public string Token { get; private set; }
        public string IdTemplateBoard { get; private set; }
        public Estimate Estimate { get; private set; }
        public Trello Trello { get; private set; }
        public Board TemplateBoard { get; private set; }
        public List BacklogList { get; private set; }

        public void AddTemplateBoard(Board board)
        {
            if(board == null)
            {
                AddNotification(nameof(TemplateBoard), "Não ocorreu erro ao buscar o quadro da API, porém seu retorno foi vazio");
                return;
            }

            TemplateBoard = board;
            
        }

        public void AddBacklogList(List list) =>
            BacklogList = list;
    }
}
