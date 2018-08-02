using ReadTextFile.Entities.Base;
using System.Collections.Generic;

namespace ReadTextFile.Entities
{
    public class Estimate : BaseEntity
    {
        public string TaskName { get; set; }
        public int TaskNumber { get; set; }
        public int TotalHoursDevelopment { get; private set; }
        public int TotalHoursTest { get; private set; }

        public List<Topic> Topics { get; set; }



    }
}
