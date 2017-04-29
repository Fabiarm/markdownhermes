using System;

namespace MarkDown.Generator.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ConditionValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="nameProperty"></param>
        public static void ThrowExceptionIfNotValid<T>(bool condition, string nameProperty)
            where T : Exception, new()
        {
            if (!condition) return;
            throw (T) Activator.CreateInstance(typeof(T), nameof(nameProperty));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="nameProperty"></param>
        /// <param name="message"></param>
        public static void ThrowExceptionIfNotValid<T>(bool condition, string nameProperty,
            string message) where T : Exception, new()
        {
            if (!condition) return;
            throw (T) Activator.CreateInstance(typeof(T), nameof(nameProperty), message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void ThrowSystemExceptionIfNotValid<T>(bool condition, string message)
            where T : SystemException, new()
        {
            if (!condition) return;
            throw (T) Activator.CreateInstance(typeof(T), message);
        }
    }
}
