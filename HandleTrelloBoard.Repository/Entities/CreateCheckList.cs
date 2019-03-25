using HandleTrelloBoard.Repository.Entities.Base;

namespace HandleTrelloBoard.Repository.Entities
{
    public class CreateCheckList : BaseEntity
    {
        public CreateCheckList(string key, string token, string idCard, string name) :
            base(key, token)
        {
            IdCard = idCard;
            Name = name;
        }

        public string IdCard { get; private set; }
        public string Name { get; private set; }
    }
}
