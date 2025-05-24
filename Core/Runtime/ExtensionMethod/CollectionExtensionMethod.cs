using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JellyFramework.ExtensionMethod
{
    public static class CollectionExtensionMethod
    {
        public static void Iterate<T>(this T[] arr, Action<T> action)
        {
            for (int i = 0; i < arr.Length; i++)
                action?.Invoke(arr[i]);
        }

        public static void Iterate<T>(this T[] arr, Action<int, T> action)
        {
            for (int i = 0; i < arr.Length; i++)
                action?.Invoke(i, arr[i]);
        }

        public static void Iterate<T>(this List<T> lst, Action<T> action)
        {
            for (int i = 0; i < lst.Count; i++)
                action?.Invoke(lst[i]);
        }

        public static void Iterate<T>(this List<T> lst, Action<int, T> action)
        {
            for (int i = 0; i < lst.Count; i++)
                action?.Invoke(i, lst[i]);
        }

        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T ChooseRandom<T>(this T[] arr)
        {
            int index = Random.Range(0, arr.Length);
            return arr[index];
        }

        public static T ChooseRandom<T>(this List<T> list)
        {
            int index = Random.Range(0, list.Count);
            return list[index];
        }

        public static KeyValuePair<TKey, TValue> ChooseRandom<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            int index = Random.Range(0, dict.Count);
            return dict.ElementAt(index);
        }

        public static T TakeRandom<T>(this List<T> lst)
        {
            int index = Random.Range(0, lst.Count);
            T element = lst[index];
            lst.RemoveAt(index);
            return element;
        }

        public static KeyValuePair<TKey, TValue> TakeRandom<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            int index = Random.Range(0, dict.Count);
            KeyValuePair<TKey, TValue> element = dict.ElementAt(index);
            dict.Remove(element.Key);
            return element;
        }

        public static T TakeFirst<T>(this List<T> lst)
        {
            T element = lst[0];
            lst.RemoveAt(0);
            return element;
        }

        public static T TakeLast<T>(this List<T> lst)
        {
            T element = lst[lst.Count - 1];
            lst.RemoveAt(lst.Count - 1);
            return element;
        }


        public static bool TryAdd<T>(this List<T> lst, T element)
        {
            bool canAdd = lst.Contains(element);
            if (!canAdd)
                lst.Add(element);
            return canAdd;
        }

        public static void LogInfo<T>(this T[] arr, string label)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{label}: {arr.Length}");
            for (int i = 0; i < arr.Length; i++)
                sb.AppendLine($"- {i}: {arr[i]}");
            Debug.Log(sb);
        }


        public static void LogInfo<T>(this List<T> lst, string label)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{label}: {lst.Count}");
            for (int i = 0; i < lst.Count; i++)
                sb.AppendLine($"- {i}: {lst[i]}");
            Debug.Log(sb);
        }

        public static void LogInfo<TKey, TValue>(this Dictionary<TKey, TValue> dict, string label)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{label}: {dict.Count}");
            foreach (KeyValuePair<TKey, TValue> pair in dict)
                sb.AppendLine($"- {pair.Key}: {pair.Value}");
            Debug.Log(sb);
        }


        public static List<T> TryGetRange<T>(this List<T> lst, int index, int count)
        {
            if (lst.Count == 0)
                return new List<T>();
            int newIndex = Mathf.Clamp(index, 0, lst.Count - 1);
            int newCount = Mathf.Clamp(count, 0, lst.Count - newIndex);
            return lst.GetRange(newIndex, newCount);
        }

        //public static void LogInfo<T>(this List<ElementAmount<T>> lst)
        //{
        //    Debug.Log($"Total: {lst.Count}");
        //    for (int i = 0; i < lst.Count; i++)
        //    {
        //        ElementAmount<T> elementAmount = lst[i];
        //        Debug.Log($"Element {elementAmount.element} - Amount {elementAmount.amount}:");
        //    }
        //}

        //public static void CountElement<T>(this List<ElementAmount<T>> lst, T element)
        //{
        //    CountElement(lst, element, x => x.element.Equals(element));
        //}

        //public static void CountElement<T>(this List<ElementAmount<T>> lst, T element, Predicate<ElementAmount<T>> match)
        //{
        //    ElementAmount<T> elementAmount = lst.Find(match);
        //    if (elementAmount == null)
        //        lst.Add(new ElementAmount<T>(element, 1));
        //    else
        //        elementAmount.amount++;
        //}

        //public static void CollectElement<T1, T2>(this List<ElementGroup<T1, T2>> lst, T2 element, int capacity, Func<T2, T1> getData)
        //{
        //    T1 data = getData(element);
        //    ElementGroup<T1, T2> elementGroup = lst.Find((x) => x.key.Equals(data));
        //    if (elementGroup == null)
        //        lst.Add(new ElementGroup<T1, T2>(data, element, capacity));
        //    else
        //        elementGroup.TryAdd(element);
        //}
    }
}
