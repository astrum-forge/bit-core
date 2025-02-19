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

namespace BitCore
{
	/// <summary>
	/// Provides utility extension methods for approximate equality tests on floating-point numbers.
	/// These methods help mitigate precision loss in real-time applications.
	/// </summary>
	public static class DecimalExtensions
	{
		// Threshold constants for determining when a floating-point value is considered "normal".
		private const float FloatNormal = (1 << 23) * float.Epsilon;
		private const double DoubleNormal = (1L << 52) * double.Epsilon;

		/// <summary>
		/// Determines whether two <see cref="float"/> values are approximately equal.
		/// Uses both absolute and relative error comparisons.
		/// </summary>
		/// <param name="a">The first float value.</param>
		/// <param name="b">The second float value.</param>
		/// <returns>
		/// <c>true</c> if the values are approximately equal; otherwise, <c>false</c>.
		/// </returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsRoughlyEqual(this float a, float b)
		{
#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
			// Shortcut: if values are exactly equal, including infinities.
			if (a == b)
			{
				return true;
			}

			float diff = Math.Abs(a - b);

			// For values near zero, use an absolute error threshold.
			if (a == 0.0f || b == 0.0f || diff < FloatNormal)
			{
				return diff < (FloatNormal * 0.00001f);
			}

			float absA = Math.Abs(a);
			float absB = Math.Abs(b);

			// Use a relative error threshold.
			return diff < Math.Min((absA + absB), float.MaxValue) * 0.00001f;
#pragma warning restore RECS0018
		}

		/// <summary>
		/// Determines whether two <see cref="double"/> values are approximately equal.
		/// Uses both absolute and relative error comparisons.
		/// </summary>
		/// <param name="a">The first double value.</param>
		/// <param name="b">The second double value.</param>
		/// <returns>
		/// <c>true</c> if the values are approximately equal; otherwise, <c>false</c>.
		/// </returns>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsRoughlyEqual(this double a, double b)
		{
#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
			// Shortcut: if values are exactly equal, including infinities.
			if (a == b)
			{
				return true;
			}

			double diff = Math.Abs(a - b);

			// For values near zero, use an absolute error threshold.
			if (a == 0.0 || b == 0.0 || diff < DoubleNormal)
			{
				return diff < (DoubleNormal * 0.00000001);
			}

			double absA = Math.Abs(a);
			double absB = Math.Abs(b);

			// Use a relative error threshold.
			return diff < Math.Min((absA + absB), double.MaxValue) * 0.00000001;
#pragma warning restore RECS0018
		}
	}
}