using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            //Кол-во серверов
            int n = 3;
            //Список всех задач каждого сервера
            List<List<int>> listServers = new List<List<int>>();
            List<int> canals = new List<int>();
            for (int item = 0; item < n; item++)
            {
                listServers.Add(new List<int>());
                canals.Add(0);
            }
            while (true)
            {

                //Строка с задачами
                string itemsTasks = Console.ReadLine();
                //Конец работы
                if (itemsTasks.Trim() == "0")
                    break;
                //Основная работа
                else
                {
                    var arrayTasks = itemsTasks.Split(',');
                    for (int item = 0; item < arrayTasks.Length; item++)
                    {
                        if (int.TryParse(arrayTasks[item], out int tempTask))
                        {
                            int minValue = canals.Min();
                            int indexMinValue = canals.IndexOf(minValue);
                            canals[indexMinValue] += tempTask;
                            listServers[indexMinValue].Add(tempTask);
                        }
                    }
                }
                //Вывод 
                for (int item = 0; item < listServers.Count; item++)
                {
                    Console.Write((item + 1) + ".");
                    for (int k = 0; k < listServers[item].Count; k++)
                    {
                        Console.Write("|");
                        for (int d = 0; d < listServers[item][k]; d++)
                        {
                            Console.Write("_");
                        }
                        Console.Write("|");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
