using HandleTrelloBoard.Repository.Entities.Base;

namespace HandleTrelloBoard.Repository.Entities
{
    public class CreateCheckListItem : BaseEntity
    {
        public CreateCheckListItem(string key, string token, string idCheckList, string name) : 
            base(key, token)
        {
            IdCheckList = idCheckList;
            Name = name;
        }

        public string IdCheckList { get; private set; }
        public string Name { get; private set; }
    }
}
