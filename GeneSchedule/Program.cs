using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace GeneSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Number of iterations: ");
            var generations = int.Parse(Console.ReadLine());

            Console.WriteLine("Number of children: ");
            var instanceCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Mutation chance: ");
            var mutation = double.Parse(Console.ReadLine());

            List<Lection> lections = new List<Lection> {
                new Lection{ name="Матеша", count=6 },
                new Lection{ name="Дифуры", count=3 },
                new Lection{ name="Алгебра", count=4 },
                new Lection{ name="Физкульт", count=2 },
                new Lection{ name="ДО", count=7 },
                new Lection{ name="Шевченкознавство", count=1 },
            };

            List<ScheduleBuilder> currentInstances = new List<ScheduleBuilder>();
            for (int i = 0; i < instanceCount; i++)
            {
                currentInstances.Add(new ScheduleBuilder(lections));
            }
            var previousBestBatchRes = -99999d;
            var previousBestBatch = currentInstances;

            var bestInstance = currentInstances.FirstOrDefault();
            var bestInstanceResult = Validator.Validate(bestInstance.Build());

            for (int i = 0; i < generations; i++)
            {
                currentInstances = currentInstances.OrderByDescending(instance => Validator.Validate(instance.Build())).ToList();
                var bestInstances = currentInstances.Take(3).ToList();
                var newBestRes = Validator.Validate(bestInstances.FirstOrDefault().Build());


                previousBestBatchRes = newBestRes;
                currentInstances = currentInstances.Take(instanceCount * instanceCount).ToList();
                previousBestBatch = currentInstances.OrderByDescending(instance => Validator.Validate(instance.Build()))
                    .Take(instanceCount * instanceCount).ToList();
                foreach (var instance in bestInstances)
                {
                    for (int instanceIndex = 0; instanceIndex < instanceCount; instanceIndex++)
                    {
                        currentInstances.Add(new ScheduleBuilder(instance, mutation));
                    }
                }
                foreach (var p in previousBestBatch)
                {
                    currentInstances.Add(new ScheduleBuilder(p, mutation));
                }
                if (bestInstanceResult < newBestRes)
                {
                    bestInstance = bestInstances.FirstOrDefault();
                    bestInstanceResult = newBestRes;
                }

                Console.WriteLine($"Best result of gen {i}: {previousBestBatchRes}");
            }

            //results
            var topInstance = currentInstances.OrderByDescending(instance => Validator.Validate(instance.Build())).FirstOrDefault();
            var schedule = topInstance.Build();
            foreach (var lection in schedule.lections.OrderBy(l => l.Key.day).ThenBy(l => l.Key.place))
            {
                Console.WriteLine($"day {lection.Key.day} lection {lection.Key.place}: {lection.Value.name}");
            }
        }
    }
}
