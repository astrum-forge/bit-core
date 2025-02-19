#if UNITY_EDITOR
#define BITCORE_DEBUG
#endif

#if NET_4_6 && !BITCORE_DISABLE_INLINE
#define BITCORE_METHOD_INLINE
#endif

using System;

#if BITCORE_METHOD_INLINE
using System.Runtime.CompilerServices;
#endif

#if BITCORE_DEBUG
namespace BitCore {
    /// <summary>
    /// Provides debug functionality that is only enabled in editor and debug builds.
    /// In production builds, all debug code is removed.
    /// </summary>
    public static class BitDebug {
        private const string DebugInfo = "NOTE: Debug messages are enabled only in editor and debug modes. In production builds, all debug code is removed.";

#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        /// <summary>
        /// Throws an InvalidOperationException with the provided message and additional debug info.
        /// </summary>
        /// <param name="message">The custom message to include in the exception.</param>
        public static void Throw(string message) {
            throw new InvalidOperationException($"{message}\n{DebugInfo}");
        }
    }
}
#endif