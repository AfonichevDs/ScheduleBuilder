using System;
using System.Collections.Generic;
using System.Text;

namespace GeneSchedule
{
    class Placer
    {
        static Random r = new Random(DateTime.Now.Millisecond);
        public Lection lection;

        int _MinDay;
        int _MaxDay;
        int _MinPlace;
        int _MaxPlace;
        public int MinDay
        {
            get => _MinDay;
            set
            {
                if (value < 1) _MinDay = 1;
                else if (value > 5) _MinDay = 5;
                else _MinDay = value;

                if (value > _MaxDay)
                    _MinDay = _MaxDay;

            }
        }
        public int MaxDay
        {
            get => _MaxDay;
            set
            {
                if (value < 1) _MaxDay = 1;
                else if (value > 5) _MaxDay = 5;
                else _MaxDay = value;

                if (value < _MinDay)
                    _MaxDay = _MinDay;

            }
        }
        public int MinPlace
        {
            get => _MinPlace;
            set
            {
                if (value < 1)
                    _MinPlace = 1;
                else if (value > 5)
                    _MinPlace = 5;
                else _MinPlace = value;

                if (value > _MaxPlace)
                    _MinPlace = _MaxPlace;
            }

        }
        public int MaxPlace
        {
            get => _MaxPlace;
            set
            {
                if (value < 1)
                    _MaxPlace = 1;
                else if (value > 5) 
                    _MaxPlace = 5; 
                else _MaxPlace = value;

                if (value < _MinPlace)
                    _MaxPlace = _MinPlace;
            }
        }


        public Placer MutateOffspring(double chance)
        {
            var result = new Placer(lection);

            result.MinDay += Mutate(chance);
            result.MaxDay += Mutate(chance);
            result.MinPlace += Mutate(chance);
            result.MaxPlace += Mutate(chance);

            return result;
        }

        private int Mutate(double chance)
        {

            if (r.NextDouble() < chance)
            {
                return r.Next(-5, 5);
            }
            else return 0;
        }

        public Placer(Lection lection)
        {
            this.lection = lection;
            MinDay = r.Next(1, 5);
            MaxDay = r.Next(1, 5);
            MinPlace = r.Next(1, 5);
            MaxPlace = r.Next(1, 5);
        }

        public (int day, int place) Place(int extention = 0)
        {
            return (r.Next(MinDay, MaxDay), r.Next(MinPlace, MaxPlace));
        }
    }
}
