using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using System.Linq;

namespace AE.Utils
{
    public class RandomUtils
    {
        public static Stack<T> CreateLimitedShuffledDeck<T>(IEnumerable<T> values, int lenght)
        {
            var rand = new Random();

            var list = new List<T>(values);
            var stack = new Stack<T>();

            if (list.Count < lenght)
                lenght = list.Count;

            while (lenght > 0)
            {
                // Get the next item at random.
                var index = rand.Next(0, list.Count);
                var item = list[index];

                // Remove the item from the list and push it to the top of the deck.
                list.RemoveAt(index);
                stack.Push(item);

                lenght--;
            }

            return stack;
        }

        public static Stack<T> CreateShuffledDeck<T>(IEnumerable<T> values)
        {
            return CreateLimitedShuffledDeck(values, values.Count());
        }
    }
}
