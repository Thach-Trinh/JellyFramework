using System;
using System.Collections.Generic;
using System.Collections;

namespace JellyFramework
{
    public static class ListPool
    {
        private static readonly Dictionary<Type, Stack<IList>> pools = new();

        public static List<T> Get<T>()
        {
            var type = typeof(T);
            if (pools.TryGetValue(type, out Stack<IList> stack) && stack.Count > 0)
                return stack.Pop() as List<T>;
            return new List<T>();
        }

        public static void Return<T>(List<T> list)
        {
            list.Clear();
            Type type = typeof(T);
            if (!pools.TryGetValue(type, out Stack<IList> stack))
            {
                stack = new Stack<IList>();
                pools[type] = stack;
            }
            stack.Push(list);
        }
    }
}
