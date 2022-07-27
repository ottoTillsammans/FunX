using System;
using System.Collections.Generic;

namespace FunX
{
    public static class Methods
    {
        /// <summary>
        /// Execute function and return result.
        /// </summary>
        /// <typeparam name="T">Type of function argument.</typeparam>
        /// <typeparam name="TResult">Type of function result.</typeparam>
        /// <param name="val">Argument.</param>
        /// <param name="func">Function.</param>
        /// <returns>Func executing result.</returns>
        public static TResult Apply<T, TResult>(this T val, Func<T, TResult> func) => func(val);

        /// <summary>
        /// Execute function without returning result.
        /// </summary>
        /// <typeparam name="T">Type of function argument.</typeparam>
        /// <param name="val">Argument.</param>
        /// <param name="func">Function.</param>
        public static void Apply<T>(this T val, Action<T> func) => func(val);

        /// <summary>
        /// Execute function if argument is not null and return result or null.
        /// Invoke callback if executing result is null.
        /// </summary>
        /// <typeparam name="T">Type of function argument.</typeparam>
        /// <typeparam name="TResult">Type of function result.</typeparam>
        /// <param name="val">Argument.</param>
        /// <param name="func">Function.</param>
        /// <param name="alarm">Failure callback.</param>
        /// <returns>Func executing result or null.</returns>
        public static TResult ApplySafe<T, TResult>(this T val, Func<T, TResult> func, Action<T> alarm)
        {
            if (val == null)
            {
                return default(TResult);
            }
            var result = func(val);

            if (result == null && alarm != null)
            {
                alarm(val);
            }
            return result;
        }

        /// <summary>
        /// Execute function if argument is not null.
        /// </summary>
        /// <typeparam name="T">Type of function argument.</typeparam>
        /// <param name="val">Argument.</param>
        /// <param name="func">Function.</param>
        public static void ApplySafe<T>(this T val, Action<T> func)
        {
            if (val != null)
            {
                func(val);
            }
        }

        /// <summary>
        /// Execute function on each collection item and return the collection.
        /// </summary>
        /// <typeparam name="T">Type of items.</typeparam>
        /// <param name="values">Collection of items.</param>
        /// <param name="func">Function to apply to items.</param>
        /// <returns>The same collection.</returns>
        public static IEnumerable<T> Feach<T>(this IEnumerable<T> values, Action<T> func)
        {
            foreach (var val in values) func(val);

            return values;
        }

        /// <summary>
        /// Execute function on each collection item.
        /// </summary>
        /// <typeparam name="T">Type of items.</typeparam>
        /// <param name="values">Collection of items.</param>
        /// <param name="func">Function to apply to items.</param>
        public static void FeachEnd<T>(this IEnumerable<T> values, Action<T> func)
        {
            foreach (var val in values) func(val);
        }

        /// <summary>
        /// Return this if not null, otherwise new.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="val">Object.</param>
        /// <returns>This object or new one.</returns>
        public static T ThisOrNew<T>(this T val) where T : new() => val != null ? val : new T();

        /// <summary>
        /// Return this if not null, otherwise execute function, returning the same type.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="val">Object.</param>
        /// <param name="func">Function.</param>
        /// <returns>This object or function result.</returns>
        public static T ThisOrExecResult<T>(this T val, Func<T> func) => val != null ? val : func();

        /// <summary>
        /// Curry two-argument function.
        /// </summary>
        public static Func<T1, Func<T2, TResult>> Curry<T1, T2, TResult>(this Func<T1, T2, TResult> funс)
        {
            return (x) => (y) => funс(x, y);
        }

        /// <summary>
        /// Curry three-argument function.
        /// </summary>
        public static Func<T1, Func<T2, Func<T3, TResult>>> Curry<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> funс)
        {
            return (x) => (y) => (z) => funс(x, y, z);
        }

        /// <summary>
        /// Curry four-argument function.
        /// </summary>
        public static Func<T1, Func<T2, Func<T3, Func<T4, TResult>>>> Curry<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> funс)
        {
            return (w) => (x) => (y) => (z) => funс(w, x, y, z);
        }

        /// <summary>
        /// Curry five-argument function.
        /// </summary>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> Curry<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> funс)
        {
            return (v) => (w) => (x) => (y) => (z) => funс(v, w, x, y, z);
        }
    }
}