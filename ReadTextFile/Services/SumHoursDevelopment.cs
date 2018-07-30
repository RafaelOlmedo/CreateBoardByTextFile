using ReadTextFile.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile.Services
{
    public class SumHoursDevelopment
    {
        public decimal SumHoursDevelopmentOfTopics(List<Topic> lstTopics)
        {
            decimal dTotalHours = 0.0m;

            foreach (var topic in lstTopics)
            {
                dTotalHours += topic.TimeDevelopment;
            }

            return dTotalHours;
        }
    }
}
