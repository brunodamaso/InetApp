using Xamarin.Forms;
using INetApp.Extensions;
using System;

namespace INetApp.Effects
{
    /// <summary>
    /// BAse effect
    /// </summary>
    public abstract class BaseEffect<T> : RoutingEffect where T: VisualElement
    {
        /// <summary>
        /// constructor
        /// </summary>
        protected BaseEffect(string effectId) : base(effectId)
        {
        }

        /// <summary>
        /// Element
        /// </summary>
        public new T Element
        {
            get;
            private set;
        }

        /// <summary>
        /// create efect if not exists
        /// </summary>
        protected static A CreateEffectIfNotExists<A>(VisualElement view) where A: BaseEffect<T>, new()
        {
            var attached = view.GetSafeEffect<A>();

            if (view != null && attached == null)
            {
                attached = new A();
                view.Effects.Add(attached);
                attached.OnAttach(view as T);
            }

            return attached;
        }

        /// <summary>
        /// On Attach
        /// </summary>
        protected virtual void OnAttach(T obj)
        {
            this.Element = obj;
            this.OnAttached();
        }
    }
    public abstract class BaseEntryEffect<T> : BaseEffect<T> where T : VisualElement
    {
        /// <summary>
        /// enum check type text
        /// </summary>
        public enum CheckTypeTextEnum
        {
            /// <summary>
            /// Text
            /// </summary>
            Text,
            /// <summary>
            /// Double
            /// </summary>
            Double,
            /// <summary>
            /// Integer
            /// </summary>
            Integer,
            /// <summary>
            /// TextNumber
            /// </summary>
            TextNumber
        }

        /// <summary>
        /// Filter entry Inject SQl
        /// </summary>
        [Flags]
        public enum FilterInjectSQlEnum
        {
            /// <summary>
            /// None Filter
            /// </summary>
            None = 1,
            /// <summary>
            /// All checks
            /// </summary>
            All = FilterInjectSQlEnum.Comments | FilterInjectSQlEnum.Dashs | FilterInjectSQlEnum.Point | FilterInjectSQlEnum.QuotationMarks | FilterInjectSQlEnum.Semicolon | FilterInjectSQlEnum.StoredProcedure,
            /// <summary>
            /// Check only ;
            /// </summary>
            Semicolon = 2, // ; Delimitador de consultas.
            /// <summary>
            /// Check only "
            /// </summary>
            QuotationMarks = 4, // "	Delimitador de cadenas de datos de caracteres.
            /// <summary>
            /// Check only --
            /// </summary>
            Dashs = 8, // --	Delimitador de cadenas de datos de caracteres.
            /// <summary>
            /// Check only .
            /// </summary>
            Point = 32, // .
            /// <summary>
            /// Check only /* or *\
            /// </summary>
            Comments = 64, // /* ... */	Delimitadores de comentarios. El servidor no evalúa el texto incluido entre /* y */ .
            /// <summary>
            /// Check only xp_
            /// </summary>
            StoredProcedure = 128, //  xp_

        }

        /// <summary>
        /// constructor
        /// </summary>
        protected BaseEntryEffect(string effectId) : base(effectId)
        {
        }
    }
}
