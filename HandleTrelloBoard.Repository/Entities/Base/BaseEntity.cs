using Flunt.Notifications;

namespace HandleTrelloBoard.Repository.Entities.Base
{
    public abstract class BaseEntity : Notifiable
    {
        public BaseEntity(string key, string token)
        {
            Key = key;
            Token = token;
        }

        public string Key { get; private set; }
        public string Token { get; private set; }
    }
}
