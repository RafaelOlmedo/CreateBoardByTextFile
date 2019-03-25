using ReadTextFile.Entities.Base;

namespace ReadTextFile.Entities.Config
{
    public class CreateBoard : BaseEntity
    {
        public bool Create { get; set; }
        public string Token { get; set; }
        public string Key { get; set; }
        public string IdBoard { get; set; }
    }
}
