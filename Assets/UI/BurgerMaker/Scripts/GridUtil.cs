using System.Collections.Generic;
using UnityEngine;

namespace UI.BurgerMaker.Scripts
{
public class GridUtil
{
    public static int[] RandomSequence(int len)
    {
        int[] arr = new int[len];
        bool[] taken = new bool[len];

        for (int i = 0; i < len; i++) taken[i] = false;

        for (int i = 0; i < len; i++)
        {
            int rand;
            do
            {
                rand = Random.Range(0, len);
            } while (taken[rand]==true);

            taken[rand] = true;
            arr[rand] = i;
        }

        for (int i = 0; i < len; i++)
        {
            //Debug.Log(arr[i]+" ");
        }
        return arr;
    }
    
    
    public static List<T> ShuffleList<T>(List<T> input, int fixLastElement)
    {
        int[] shuffleIdx = RandomSequence(input.Count-fixLastElement);
        List<T> result = new List<T>();
        int i = 0;
        for (i = 0; i < input.Count-fixLastElement; i++)
        {
            result.Add(input[shuffleIdx[i]]);
        }
        for(; i < input.Count; i++)
        {
            result.Add(input[i]);
        }

            return result;
    }
}
}
