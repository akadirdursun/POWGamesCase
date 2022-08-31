using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeMatch
{
    public static class ExtentionMethods
    {
        public static T LastItem<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }
    }
}