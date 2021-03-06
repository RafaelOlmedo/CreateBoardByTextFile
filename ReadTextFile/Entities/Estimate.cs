﻿using ReadTextFile.Entities.Base;
using System.Collections.Generic;
using System.Linq;

namespace ReadTextFile.Entities
{
    public class Estimate : BaseEntity
    {
        public string TaskName { get; set; }
        public int TaskNumber { get; set; }
        public decimal TotalHoursDevelopment { get; private set; }
        public decimal TotalHoursTest { get; private set; }

        public List<Topic> Topics { get; set; }

        public void sumHoursDevelopment() =>        
            TotalHoursDevelopment = Topics.Sum(t => t.TimeDevelopment);

        public void sumHoursTest() =>
            TotalHoursTest = Topics.Sum(t => t.TimeTest);


    }
}
