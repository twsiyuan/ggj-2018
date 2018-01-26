using System;
using System.Collections.Generic;

namespace ListExtension.ElementControl
{
    public static class ElementControl
    {

        /// <summary>
        /// Remove first element.
        /// </summary>
        public static T Dequeue<T>(this List<T> input)
        {
            if(input.Count == 0)
                throw new NullReferenceException("List is empty");

            var element = input[0];
            input.RemoveAt(0);

            return element;
        }


        /// <summary>
        /// Remove last element.
        /// </summary>
        public static T Pop<T>(this List<T> input)
        {
            var lastPos = input.Count - 1;
            if(lastPos == -1)
                throw new NullReferenceException("List is empty");

            var element = input[lastPos];
            input.RemoveAt(lastPos);

            return element;
        }


        /// <summary>
        /// Return first element.
        /// </summary>
        public static T PeekFirst<T>(this List<T> input)
        {
            if(input.Count == 0)
                throw new NullReferenceException("List is empty");

            return input[0];
        }


        /// <summary>
        /// Return last element.
        /// </summary>
        public static T PeekLast<T>(this List<T> input)
        {
            var lastPos = input.Count - 1;
            if(lastPos == -1)
                throw new NullReferenceException("List is empty");

            return input[lastPos];
        }

    }
}