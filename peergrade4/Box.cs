using System;
using System.Collections.Generic;
using System.Text;

namespace peergrade4
{
    /// <summary>
    /// Класс для ящика, имеет два свойства:
    /// Вес и стоимость.
    /// </summary>
    class Box
    {
        public int weight;
        public double cost;

        /// <summary>
        /// Инициализация.
        /// </summary>
        /// <param name="weight">Вес ящика.</param>
        /// <param name="cost">Цена ящика.</param>
        public Box(int weight, double cost)
        {
            this.weight = weight;
            this.cost = cost;
        }
    }
}
