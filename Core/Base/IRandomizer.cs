using System.Collections.Generic;

namespace Core.Base
{
    public interface IRandomizer
    {
        int GetRandom(int maxValue);
        List<T> GetRandomFromList<T>(List<T> list, int count);
        T GetRandomFromList<T>(List<T> list);
        bool CheckIsLucky(int percent);
    }
}
