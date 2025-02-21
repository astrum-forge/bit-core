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
	/// Provides high-performance extension methods for approximate equality comparisons of floating-point numbers.
	/// These methods mitigate precision issues in real-time applications like Unity3D by using a hybrid absolute and relative error approach.
	/// <para><b>Performance Note:</b> Methods are aggressively inlined in .NET 4.6+ builds for minimal overhead.</para>
	/// </summary>
	public static class FloatExtensions
	{
		// Thresholds for subnormal numbers (smallest normal value per IEEE 754)
		private const float FloatNormal = 1.17549435E-38f; // 2^-126, smallest normal float
		private const double DoubleNormal = 2.2250738585072014E-308; // 2^-1022, smallest normal double

		// Default tolerances (configurable via overloads)
		private const float FloatEpsilon = 1E-6f;  // 0.000001, suitable for Unity
		private const double DoubleEpsilon = 1E-6; // 0.000001, aligned with float for consistency

		/// <summary>
		/// Determines whether two <see cref="float"/> values are approximately equal using a hybrid absolute and relative error comparison.
		/// </summary>
		/// <param name="a">The first float value.</param>
		/// <param name="b">The second float value.</param>
		/// <returns>True if the values are approximately equal within a tolerance of 1e-6; otherwise, false.</returns>
		/// <remarks>
		/// Returns false for NaN values. Uses an absolute threshold near zero and a relative threshold elsewhere.
		/// See overload for custom tolerance.
		/// </remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsRoughlyEqual(this float a, float b) => IsRoughlyEqual(a, b, FloatEpsilon);

		/// <summary>
		/// Determines whether two <see cref="float"/> values are approximately equal using a custom tolerance.
		/// </summary>
		/// <param name="a">The first float value.</param>
		/// <param name="b">The second float value.</param>
		/// <param name="epsilon">The tolerance for equality (e.g., 1e-6).</param>
		/// <returns>True if the values are approximately equal within the specified tolerance; otherwise, false.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if epsilon is negative or zero.</exception>
		/// <remarks>
		/// Returns false for NaN values. Optimized for performance with minimal branching.
		/// </remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsRoughlyEqual(this float a, float b, float epsilon)
		{
#if BITCORE_DEBUG
			if (epsilon <= 0) throw new ArgumentOutOfRangeException(nameof(epsilon), $"Epsilon must be positive, was {epsilon}.");
#endif
#pragma warning disable RECS0018 // Disabled: Exact equality check is intentional for shortcut
			if (a == b) return true; // Exact match, including infinities
#pragma warning restore RECS0018

			if (float.IsNaN(a) || float.IsNaN(b)) return false;

			float diff = Math.Abs(a - b);
			if (diff < FloatNormal) return diff < FloatNormal * epsilon; // Absolute near zero

			float absA = Math.Abs(a);
			float absB = Math.Abs(b);
			return diff <= (absA + absB) * epsilon; // Relative elsewhere, avoids MaxValue cap
		}

		/// <summary>
		/// Determines whether two <see cref="double"/> values are approximately equal using a hybrid absolute and relative error comparison.
		/// </summary>
		/// <param name="a">The first double value.</param>
		/// <param name="b">The second double value.</param>
		/// <returns>True if the values are approximately equal within a tolerance of 1e-6; otherwise, false.</returns>
		/// <remarks>
		/// Returns false for NaN values. Uses an absolute threshold near zero and a relative threshold elsewhere.
		/// See overload for custom tolerance.
		/// </remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsRoughlyEqual(this double a, double b) => IsRoughlyEqual(a, b, DoubleEpsilon);

		/// <summary>
		/// Determines whether two <see cref="double"/> values are approximately equal using a custom tolerance.
		/// </summary>
		/// <param name="a">The first double value.</param>
		/// <param name="b">The second double value.</param>
		/// <param name="epsilon">The tolerance for equality (e.g., 1e-6).</param>
		/// <returns>True if the values are approximately equal within the specified tolerance; otherwise, false.</returns>
		/// <exception cref="ArgumentOutOfRangeException">In debug mode, thrown if epsilon is negative or zero.</exception>
		/// <remarks>
		/// Returns false for NaN values. Optimized for performance with minimal branching.
		/// </remarks>
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static bool IsRoughlyEqual(this double a, double b, double epsilon)
		{
#if BITCORE_DEBUG
			if (epsilon <= 0) throw new ArgumentOutOfRangeException(nameof(epsilon), $"Epsilon must be positive, was {epsilon}.");
#endif
#pragma warning disable RECS0018 // Disabled: Exact equality check is intentional for shortcut
			if (a == b) return true; // Exact match, including infinities
#pragma warning restore RECS0018

			if (double.IsNaN(a) || double.IsNaN(b)) return false;

			double diff = Math.Abs(a - b);
			if (diff < DoubleNormal) return diff < DoubleNormal * epsilon; // Absolute near zero

			double absA = Math.Abs(a);
			double absB = Math.Abs(b);
			return diff <= (absA + absB) * epsilon; // Relative elsewhere, avoids MaxValue cap
		}
	}
}