using System.Collections.Generic;
using UnityEngine;


public static class ExtensionMethods 
{
   public static T RemoveLast<T>(this IList<T> list)
   {
      T last = list[list.Count - 1];
      list.RemoveAt(list.Count - 1);
      return last;
   }
}
