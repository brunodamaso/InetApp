using System;

namespace INetApp.Extensions
{
    /// <summary>
    /// Double extensions.
    /// </summary>
    public static class RealNumbersExtensions
    {
        /// <summary>
        /// Is the zero.
        /// </summary>
        /// <returns><c>true</c>, if zero was ised, <c>false</c> otherwise.</returns>
        /// <param name="val">Value.</param>
        public static bool IsZero(this double val) => IsEqual(val, 0);

        /// <summary>
        /// Is the zero.
        /// </summary>
        /// <returns><c>true</c>, if zero was ised, <c>false</c> otherwise.</returns>
        /// <param name="val">Value.</param>
        public static bool IsZero(this float val) => IsEqual(val, 0);

        /// <summary>
        /// Is the equal to value int
        /// </summary>
        /// <returns><c>true</c>, if equal was ised, <c>false</c> otherwise.</returns>
        /// <param name="val">Value.</param>
        /// <param name="value">Value.</param>
        public static bool IsEqual(this double val, int value) => ((int)val) == value;

        /// <summary>
        /// Is the equal to value int
        /// </summary>
        /// <returns><c>true</c>, if equal was ised, <c>false</c> otherwise.</returns>
        /// <param name="val">Value.</param>
        /// <param name="value">Value.</param>
        public static bool IsEqual(this float val, int value) => ((int)val) == value;

        /// <summary>
        /// Get number of decimals.
        /// </summary> 
        /// <returns>number of decimals</returns>
        public static int GetNumberOfDecimals(this double val)
        {
            return GetDecimals(val);
        }

        /// <summary>
        /// Truncate value for x decimals
        /// </summary>
        /// <param name="val"></param>
        /// <param name="maxDecimals">max number of decimal to return</param>
        /// <returns></returns>
        public static double Truncate(this double val, int maxDecimals)
        {
            var result = val;

            if (val.GetNumberOfDecimals() > maxDecimals)
            { 
                var ten = 1;

                for (var i = 0; i < maxDecimals; i++)
                    ten *= 10;

                result = Math.Truncate(ten * val) / ten;
            }

            return result;
        }

        #region Private

        private static int GetDecimals(double d, int i = 0)
        {
            var multiplied = (decimal)(d * Math.Pow(10, i));

            if (Math.Round(multiplied) == multiplied)
                return i;

            return GetDecimals(d, i + 1);
        }

        #endregion
    }
}
