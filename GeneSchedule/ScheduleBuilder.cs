using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneSchedule
{
    class ScheduleBuilder
    {
        public List<Placer> placers = new List<Placer>();
        List<Lection> lections;
        public ScheduleBuilder(ScheduleBuilder parent, double mutation)
        {
            foreach (var placer in parent.placers)
            {
                var newPlacer = placer.MutateOffspring(mutation);
                placers.Add(newPlacer);

            }
        }

        public ScheduleBuilder(List<Lection> lections)
        {
            foreach (var lection in lections)
            {
                for (int i = 0; i < lection.count; i++)
                {
                    placers.Add(new Placer(lection));
                }
                
            }
        }

        public Schedule Build()
        {
            Schedule schedule = new Schedule();
            foreach (var placer in this.placers)
            {

                bool isPlaced = false;
                for (int ext = 0; ext < 5; ext++)
                {
                    if (isPlaced) break;
                    var possiblePlacements = new List<(int day, int place)>();
                    for (int i = Math.Max(placer.MaxDay - ext, 1); i <= Math.Min(placer.MaxDay+1, 5); i++)
                    {
                        for (int j = Math.Max(placer.MinPlace - ext, 1); j <= Math.Min(placer.MaxPlace+1, 5); j++)
                        {
                            possiblePlacements.Add((i, j));
                        }
                    }

                    //randomize order
                    possiblePlacements = possiblePlacements.OrderBy(x => new Guid()).ToList();

                    foreach (var placement in possiblePlacements)
                    {
                        if (!schedule.lections.ContainsKey(placement))
                        {
                            isPlaced = true;
                            schedule.lections.Add(placement, placer.lection);
                            break;
                        }
                    }
                }
            }
            return schedule;
        }
    }
}
