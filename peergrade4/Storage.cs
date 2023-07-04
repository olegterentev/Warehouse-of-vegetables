using System;
using System.Collections.Generic;
using System.Text;

namespace peergrade4
{
    class Storage
    {
        public List<Сontainer> сontainers = new List<Сontainer>();
        public int capacity;
        public double storagePrice;
        double money = 0;

        public Storage(int capacity, double storagePrice)
        {
            this.capacity = capacity;
            this.storagePrice = storagePrice;
        }

        public void Add(int n)
        {
            Random rand = new Random();
            if (сontainers.Count == 0) сontainers.Add(new Сontainer(1, (int)n));
            else
            {
                сontainers.Add(new Сontainer(сontainers[сontainers.Count - 1].id + 1, (int)n));
            }
            if (rand.Next(0, 500) / 100.0 * сontainers[сontainers.Count - 1].GetPrice() < storagePrice)
            {
                сontainers.RemoveAt(сontainers.Count - 1);
                Console.WriteLine("Контейнер не добавлен на склад тк его стоимость меньше аренды.");
                return;
            }
            else if (сontainers.Count > capacity)
            {
                Console.WriteLine("Места на складе нет, мы продали старый контейнер.");
                money += сontainers[0].GetPrice();
                сontainers.RemoveAt(0);
                return;
            }
            Console.WriteLine(сontainers[сontainers.Count - 1]);
            Console.WriteLine("Контейнер успешно добавлен. Для продолжения нажмите любую кнопку.");
            Console.ReadKey();
        }
        public void Add(ref List<double> input)
        {
            Random rand = new Random();
            if (сontainers.Count == 0) сontainers.Add(new Сontainer(1, ref input));
            else
            {
                сontainers.Add(new Сontainer(сontainers[сontainers.Count - 1].id + 1, ref input));
            }
            if (rand.Next(0, 500) / 100.0 * сontainers[сontainers.Count - 1].GetPrice() < storagePrice)
            {
                сontainers.RemoveAt(сontainers.Count - 1);
                Console.WriteLine("Контейнер не добавлен на склад тк его стоимость меньше аренды.");
                return;
            }
            else if (сontainers.Count > capacity)
            {
                Console.WriteLine("Места на складе нет, мы продали старый контейнер.");
                money += сontainers[0].GetPrice();
                сontainers.RemoveAt(0);
                return;
            }
            Console.WriteLine(сontainers[сontainers.Count - 1]);
            Console.WriteLine("Контейнер успешно добавлен. Для продолжения нажмите любую кнопку.");
            Console.ReadKey();
        }

        public bool GetId(string id)
        {
            if (!int.TryParse(id, out int inputId)) return false;
            foreach(var i in сontainers)
            {
                if (i.id == inputId) return true;
            }
            return false;
        }
        public void Pop(string id)
        {
            var inputId = int.Parse(id);
            for(int i = 0; i < сontainers.Count;i++)
            {
                if (сontainers[i].id == inputId)
                {
                    сontainers.RemoveAt(i);
                    break;
                }
            }
            return;
        }
    }
}
