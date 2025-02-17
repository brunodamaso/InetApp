﻿using System;

namespace INetApp.NFC
{
    /// <summary>
    /// Cross NFC
    /// </summary>
    public static partial class CrossNFC
    {
        static Lazy<INFC> implementation = new Lazy<INFC>(() => CreateNFC(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets if the plugin is supported on the current platform.
        /// </summary>
        public static bool IsSupported => implementation.Value == null ? false : true;

		/// <summary>
        /// Legacy Mode (Supporting Mifare Classic on iOS)
        /// </summary>
		static bool _legacy = false;

		public static bool Legacy 
		{
			get
			{
				return _legacy;
			}

			set 
			{
				_legacy = value;

				implementation = new Lazy<INFC>(() => CreateNFC(), System.Threading.LazyThreadSafetyMode.PublicationOnly);
			}
		}
		
        /// <summary>
        /// Current plugin implementation to use
        /// </summary>
        public static INFC Current
        {
            get
            {
                INFC ret = implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        static INFC CreateNFC()
        {
#if NETSTANDARD1_0 || NETSTANDARD2_0
            return null;
#elif WINDOWS_UWP
            return null;
#elif XAMARIN_IOS
			if (NFCUtils.IsWritingSupported() && !Legacy)
				return new NFCImplementation();
			return new NFCImplementation_Before_iOS13();
#else
#pragma warning disable IDE0022 // Use expression body for methods
            return new NFCImplementation();
#pragma warning restore IDE0022 // Use expression body for methods
#endif
		}

		internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");

    }
}
