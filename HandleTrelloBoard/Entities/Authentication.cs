using HandleTrelloBoard.Entities.Base;
using System;
using TrelloNet;

namespace HandleTrelloBoard.Services
{
    public class Authentication : BaseEntity
    {
        public Authentication(string key, string token)
        {
            Key = key;
            Token = token;
        }

        public string Key { get; private set; }
        public string Token { get; private set; }

        public Trello Authenticate()
        {
            if (string.IsNullOrEmpty(Key))
            {
                AddNotification(nameof(Key), "Chave para acesso não informada");
                return new Trello("");
            }


            if (string.IsNullOrEmpty(Token))
            {
                AddNotification(nameof(Token), "Token para acesso não informada");
                return new Trello("");
            }

            Trello trello = new Trello(Key);

            // The user will receive a token, call Authenticate with it (https://trello.com/app-key and click in Token)
            trello.Authorize(Token);

            ValidAuthorization(trello);

            return trello;

        }

        public void ValidAuthorization(Trello trello)
        {
            try
            {
                var myBoards = trello.Boards.ForMe();
            }
            catch (Exception ex)
            {
                if (ex.Message == "invalid key")
                    AddNotification("Key", "Chave informada é inválida");
                else if (ex.Message == "invalid token")
                    AddNotification("Key", "Token informado é inválido");
                else
                    AddNotification("Autorização", "Erro ao validar autorização com base na chave e token");
            }
           
        }
    }
}
