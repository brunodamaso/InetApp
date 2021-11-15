using System;
using System.Threading.Tasks;

namespace INetApp.Extensions
{
    /// <summary>
    /// Object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Convert the specified obj and defaultValue.
        /// </summary>
        /// <returns>The convert.</returns>
        /// <param name="obj">Object.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T Convert<T>(this object obj, T defaultValue = default(T))
        {
            if (obj is T val)
                return val;

            return defaultValue;
        }

        /// <summary>
        /// Ises the typed.
        /// </summary>
        /// <returns><c>true</c>, if typed was ised, <c>false</c> otherwise.</returns>
        /// <param name="obj">Object.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static bool IsTyped<T>(this object obj)
        {
            return obj is T;
        }

        /// <summary>
        /// Runs the safe.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="action">Action.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void RunSafe<T>(this T obj, Action<T> action)
        {
            if (obj is T current)
                action?.Invoke(current);
        }

        /// <summary>
        /// Runs the safe async.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="action">Action.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static async Task RunSafeAsync<T>(this T obj, Func<T, Task> action)
        {
            if (obj is T current)
                await action?.Invoke(current);
        }

        /// <summary>
        /// Runs the safe async.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="action">Action.</param>
        /// <param name="default">default value if not exist obj</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        /// <typeparam name="B">Parameter to out</typeparam>
        public static async Task<B> RunSafeAsync<T, B>(this T obj, Func<T, Task<B>> action, B @default = default(B))
        {
            if (obj is T current)
                return await action?.Invoke(current);

            return @default;
        }

        /// <summary>
        /// Return if is same type two objects.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static bool IsSameType(this object val1, object val2)
        {
            if (val1 == null || val2 == null)
                return false;

            return val1.GetType().Equals(val2.GetType());
        }

        /// <summary>
        /// Return true if nullable object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsOfNullableType<T>(this T val)
        {
            var type = typeof(T);
            return Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// Is Numeric
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNumericType(this object val)
        {
            if (val != null)
            {
                var type = val as Type ?? val.GetType();
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Single:
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTyped<T>(this Type type)
        {
            var tType = typeof(T);
            return tType.Equals(type);
        }

        /// <summary>
        /// Get default value type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefault(this Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }
    }
}
