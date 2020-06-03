using System;
using System.Collections.Generic;
using System.Text;

namespace GeneSchedule
{
    class Schedule
    {
        public Dictionary<(int day, int place), Lection> lections = new Dictionary<(int, int), Lection>();
    }
}
