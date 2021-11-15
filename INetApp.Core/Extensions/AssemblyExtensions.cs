using System;
using System.Reflection;
using INetApp.Services;

namespace INetApp.Extensions
{
    /// <summary>
    /// Assembly extensions.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the assembly.
        /// </summary>
        /// <returns>The assembly.</returns>
        /// <param name="obj">Object.</param>
        public static Assembly GetAssembly(this Type obj)
        {
            return obj.GetTypeInfo().Assembly;
        }

        /// <summary>
        /// Gets the assembly.
        /// </summary>
        /// <returns>The assembly.</returns>
        /// <param name="obj">Object.</param>
        public static Assembly GetAssembly(this object obj)
        {
            return obj.GetType().GetAssembly();
        }

        /// <summary>
        /// Exists the resource.
        /// </summary>
        /// <returns><c>true</c>, if resource was existed, <c>false</c> otherwise.</returns>
        /// <param name="obj">Object.</param>
        /// <param name="uri">URI.</param>
        public static bool ExistResource(this Assembly obj, string uri)
        {
            var result = false;

            if (obj != null)
            {
                var val = obj.GetManifestResourceInfo(uri);
                result = (val?.ResourceLocation != null);
            }

            return result;
        }

        /// <summary>
        /// Gets the assembly from uri.
        /// </summary>
        /// <returns>The assembly.</returns>
        /// <param name="uri">URI.</param>
        public static Assembly GetAssembly(this Uri uri)
        {
            return uri?.AbsolutePath.GetAssembly();
        }

        /// <summary>
        /// Gets the assembly from uri path.
        /// </summary>
        /// <returns>The assembly.</returns>
        /// <param name="uri">URI.</param>
        public static Assembly GetAssembly(this string uri)
        {
            var list = AppDomain.CurrentDomain.GetAssemblies();

            if (!list.IsNullOrNotElements())
                foreach (var item in list)
                {
                    if (item.FullName.StartsWith(uri, StringComparison.InvariantCulture))
                        return item;
                }

            return null;
        }

        /// <summary>
        /// Get content resource in Assembly.
        /// </summary>
        /// <returns>The resource.</returns>
        /// <param name="assembly">Assembly.</param>
        /// <param name="uri">URI.</param>
        public static string GetResource(this Assembly assembly, string uri)
        {
            string result = null;
            var err = "";

            if (assembly != null)
            {
                try
                {
                    using (System.IO.Stream stream = assembly.GetManifestResourceStream(uri))
                    {
                        if (stream != null)
                            using (var reader = new System.IO.StreamReader(stream))
                            {
                                result = reader.ReadToEnd();
                            }
                    }
                }
                catch (Exception ex)
                {
                    err = ex.ToString();
                }
            }

            if (result.IsNullOrEmpty())
                Console.WriteLine($"error to get resource {uri} in assembly {assembly?.GetName()} -> {err}");

            return result;
        }

    }
}
