using System.Linq;
using INetApp.Services;
using Xamarin.Forms;

namespace INetApp.Extensions
{
    /// <summary>
    /// Visual element extensions.
    /// </summary>
    public static class VisualElementExtensions
    {
        /// <summary>
        /// Gets the top of Page.
        /// </summary>
        /// <returns>The top.</returns>
        /// <param name="view">View.</param>
        /// <param name="isStatusBar">not sum statusBar</param>
        public static double GetTop(this VisualElement view, bool isStatusBar = true)
        {
            var result = 0d;

            if (view != null)
            {
                result = view.Y;

                if (view.Parent.GetType() != typeof(Application))
                {
                    var parent = view.Parent as VisualElement;

                    // Loop through all parents
                    while (parent != null)
                    {
                        // Add in the coordinates of the parent with respect to ITS parent
                        result += parent.Y;

                        // If the parent of this parent isn't the app itself, get the parent's parent.
                        if (parent.Parent is Application)
                            parent = null;
                        else
                            parent = parent.Parent as VisualElement;
                    }
                }
            }

            if (!isStatusBar)
            {
                var devide = DependencyService.Get<IDeviceService>(); 
                result = result - devide.TopSafeArea;
            }

            return result;
        }

        /// <summary>
        /// Creates the effect if not exists.
        /// </summary>
        /// <param name="view">View.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T CreateEffectIfNotExists<T>(this VisualElement view) where T : Effect, new()
        {
            var attached = view.GetSafeEffect<T>();

            if (view != null && attached == null)
            {
                attached = new T();
                view.Effects.Add(attached);
            }

            return attached;
        }

        /// <summary>
        /// Creates the behavior if not exists.
        /// </summary>
        /// <param name="view">View.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void CreateBehaviorIfNotExists<T>(this VisualElement view) where T : Behavior, new()
        {
            if (view != null && !(view.GetSafeBehavior<T>() is T attached))
            {
                attached = new T();
                view.Behaviors.Add(attached);
            }
        }

        /// <summary>
        /// Gets the effect.
        /// </summary>
        /// <returns>The effect.</returns>
        /// <param name="view">View.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetSafeEffect<T>(this VisualElement view) where T : Effect
        {
            return ((Element)view).GetSafeEffect<T>();
        }

        /// <summary>
        /// Gets the effect.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        /// <returns></returns>
        public static T GetSafeEffect<T>(this Element view) where T : Effect
        {
            return view?.Effects?.FirstOrDefault(e => e is T) as T;
        }

        /// <summary>
        /// Gets the behavior.
        /// </summary>
        /// <returns>The behavior.</returns>
        /// <param name="view">View.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetSafeBehavior<T>(this VisualElement view) where T : Behavior
        {
            return view?.Behaviors?.FirstOrDefault(e => e is T) as T;
        }

        /// <summary>
        /// Get Previous Page (ignore Navigation)
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Page GetPreviousPage(this Page page)
        {
            return null; // App.Current?.Navigation?.GetPreviousPage(page) ?? page.Parent as Page;
        }
    }
}
