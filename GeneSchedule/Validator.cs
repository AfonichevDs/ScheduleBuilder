using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneSchedule
{
    static class Validator
    {
        public static double Validate(Schedule schedule)
        {
            var result = 0d;
            for (int i = 1; i < 5; i++)
            {
                //day i lections
                var lections = schedule.lections.Where(l => l.Key.day == i).ToList();

                if (lections.Any())
                    for (int j = 1; j < lections.Count() - 1; j++)
                    {
                        // if has windows
                        if (lections[j].Key.place != lections[j].Key.place + 1)
                        {
                            result -= 5;
                        }
                    }

                //distinct lections must be maximum, except for matesh
                result -= (4 - lections.Select(l => l.Value).Where(l=>!l.name.Equals("Матеша"))
                    .Distinct().Count()) * 5;

                //do should occur later
                foreach (var l in lections.Where(x => x.Value.name.Equals("ДО")))
                {
                    result -= l.Key.place * 5;
                }

                //matesha should only occur on friday
                result -= lections.Where(x => x.Value.name.Equals("Матеша") && x.Key.day != 5).Count() * 100;

                //equal lectrion distribution per day
                if(lections.Any())
                result += lections.GroupBy(l => l.Key.day).Min(g=>g.Count())*10;
            }


            // max lecture placement
            var lectionCount = schedule.lections.Count();
            result -= (23 - lectionCount) * 10;

            return result;
        }
    }
}
