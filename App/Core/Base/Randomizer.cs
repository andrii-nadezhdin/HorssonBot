using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace App.Core.Base
{
    internal class Randomizer : IRandomizer
    {
        private Random _random;
        private Random Random => _random = (_random  ?? CreateRandom());

        private static Random CreateRandom()
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);
            return new Random(result);
        }

        public bool CheckIsLucky(int percent)
        {
           return percent >= Constants.HundredPercent || GetRandom(Constants.HundredPercent) <= percent;
        }

        public int GetRandom(int maxValue)
        {
            return Random.Next(maxValue);
        }

        public IList<T> GetRandomFromList<T>(IList<T> list, int count)
        {
            var ramdomedList = new List<T>();
            if (list.Count <= count)
            {
                ramdomedList.AddRange(list);
                return ramdomedList;
            }
            var existingItems = new List<int>();
            while (ramdomedList.Count < count)
            {
                var item = Random.Next(list.Count);
                if (!existingItems.Exists(i => i == item))
                {
                    ramdomedList.Add(list[item]);
                    existingItems.Add(item);
                }
            }
            return ramdomedList;
        }

        public T GetRandomFromList<T>(IList<T> list)
        {
            return list[Random.Next(list.Count)];
        }
    }
}