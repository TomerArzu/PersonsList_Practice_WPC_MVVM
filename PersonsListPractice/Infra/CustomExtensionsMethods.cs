using System;
using System.Collections.Generic;

namespace PersonsListPractice.Infra
{
    public static class CustomExtensionsMethods
    {
        /// <summary>
        /// This is extention method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> list, Predicate<T> predicate)
        {
            if (list == null)
                throw new ArgumentNullException("The IEnumerable list you have provided is null");
            if (predicate == null)
                throw new ArgumentNullException("The predicate function you have provided is null");
            foreach (T curItem in list)
            {
                if (predicate.Invoke(curItem))
                {
                    yield return curItem;
                    //You use a yield return statement to return each element one at a time.
                }
            }
        }
    }
}