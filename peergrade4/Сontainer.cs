using System;
using System.Collections.Generic;
using System.Text;

namespace peergrade4
{
    class Сontainer
    {
        /// <summary>
        /// Класс контейнера.
        /// Свойства класса, хранят основные данные о контейнере.
        /// </summary>
        public int id;
        public int weight;
        public List<Box> boxs = new List<Box>();
        /// <summary>
        /// Метод инициализирующий контейнер через консоль.
        /// </summary>
        /// <param name="id">Идентификатор контейнера.</param>
        /// <param name="n">Количество ящиков в контейнере.</param>
        public Сontainer(int id, int n)
        {
            Random rand = new Random();
            this.id = id;
            this.weight = rand.Next(50,1000);
            
            int sumWeight = 0;
            for(int i = 0; i < n; i++)
            {
                while (true)
                {
                    Console.WriteLine($"Вместимость контейнера:{this.weight}, занято:{sumWeight}");
                    Console.WriteLine("Введите массу и цену ящика");
                    var input = Console.ReadLine().Split(' ');
                    if (input.Length == 2)
                    {
                        if (!uint.TryParse(input[0], out uint weight))
                        {
                            Console.WriteLine("Введенная масса не корректна, для подолжения нажмите кнопку");
                            Console.ReadKey();
                            continue;
                        }
                        if (!double.TryParse(input[1], out double cost) || cost < 0.0)
                        {
                            Console.WriteLine("Введенная цена не корректна, для продолжения нажмите кнопку");
                            Console.ReadKey();
                            continue;
                        }
                        if(sumWeight + weight > this.weight)
                        {
                            Console.WriteLine("Ящик не помещается в контейнер(сумарный вес ящиков больше допустимого), для продолжения нажмите кнопку");
                            Console.ReadKey();
                            continue;
                        }
                        boxs.Add(new Box((int)weight, cost));
                        sumWeight += (int)weight;
                    }
                    else
                    {
                        Console.WriteLine("Введено непонятно что, для продолжения нажмите кнопку");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    break;
                }

            }
        }
        /// <summary>
        /// Метод инициализирующий контейнер через Файл.
        /// </summary>
        /// <param name="id">Идентификатор контейнера.</param>
        /// <param name="input">List с количеством ящиков в контейнере, и информацией о них.</param>
        public Сontainer(int id,ref List<double> input)
        {
            Random rand = new Random();
            this.id = id;
            this.weight = rand.Next(50, 1000);
            int sumWeight = 0;
            var j = 1;
            for (int i = 0; i < input[0]; i++)
            {
                boxs.Add(new Box((int)input[j], input[j+1]));
                sumWeight += (int)input[j];
            }
        }
        /// <summary>
        /// Метод для посчета стоимости контейнера.
        /// </summary>
        /// <returns>Возвращает суммарную стоимость контейнера.</returns>
        public double GetPrice()
        {
            double ans = 0;
            for(int i = 0; i < boxs.Count; i++)
            {
                ans += boxs[i].cost;
            }
            return ans;
        }
        /// <summary>
        /// Перегрузка метода ToString.
        /// </summary>
        /// <returns>Cтроку с идентификатором стоимостью и весом.</returns>
        public override string ToString()
        {
            return ("Id:" + id + "; Weight:" + weight + "; Price:" + GetPrice());
        }
    }
}
