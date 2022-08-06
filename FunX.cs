using System;
using System.Collections.Generic;
using System.Linq;

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
                return default(TResult);

            var result = func(val);

            if (result == null && alarm != null)
                alarm(val);

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
                func(val);

        }

        /// <summary>
        /// Execute function on each not-null collection item 
        /// and return the collection.
        /// Invoke callback every time when item is null.
        /// </summary>
        /// <typeparam name="T">Type of items.</typeparam>
        /// <param name="values">Collection of items.</param>
        /// <param name="func">Function to apply to items.</param>
        /// <returns>The same collection.</returns>
        public static IEnumerable<T> FeachWeakly<T, A>(this IEnumerable<T> values, Action<T> func, Func<A> alarm)
        {
            foreach (var val in values)
            {
                if (val != null)
                    func(val);

                else if (alarm != null)
                    alarm();
            }

            return values;
        }

        /// <summary>
        /// Execute function on each collection item 
        /// if all are not null and return the collection.
        /// Invoke callback when at least one item is null.
        /// </summary>
        /// <typeparam name="T">Type of items.</typeparam>
        /// <param name="values">Collection of items.</param>
        /// <param name="func">Function to apply to items.</param>
        /// <returns>The same collection.</returns>
        public static IEnumerable<T> FeachStrictly<T, A>(this IEnumerable<T> values, Action<T> func, Func<A> alarm)
        {
            if (values.Any(v => v == null) && alarm != null)
            {
                alarm();

                return values;
            }
            foreach (var val in values)
                func(val);

            return values;
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

        /// <summary>
        /// Try to execute function on argument.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="val">Object.</param>
        /// <param name="tryAction">Function to try.</param>
        /// <param name="catchAction">Function to catch exception.</param>
        public static void Try<T>(this T val, Action<T> tryAction, Action<Exception> catchAction)
        {
            try
            {
                tryAction(val);
            }
            catch (Exception ex)
            {
                catchAction(ex);
            }
        }

        /// <summary>
        /// Try to execute function on argument.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="val">Object.</param>
        /// <param name="tryAction">Function to try.</param>
        /// <param name="catchAction">Function to catch exception.</param>
        /// <param name="finalAction">Unconditionl function.</param>
        public static void Try<T>(this T val, Action<T> tryAction, Action<Exception> catchAction, Action<T> finalAction)
        {
            try
            {
                tryAction(val);
            }
            catch (Exception ex)
            {
                catchAction(ex);
            }
            finally
            {
                finalAction(val);
            }
        }
    }
}