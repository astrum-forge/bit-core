using BitCore;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Unit tests to verify approximate equality comparisons of float (32-bit) and double (64-bit) values.
/// These tests ensure that tolerance-based equality checks work correctly despite precision loss.
/// </summary>
public static class DecimalTests
{
	private static readonly float FloatSmallPositive = float.Epsilon;
	private static readonly float FloatSmallNegative = -float.Epsilon;
	private static readonly float FloatZero = 0.0f;

	private static readonly double DoubleSmallPositive = double.Epsilon;
	private static readonly double DoubleSmallNegative = -double.Epsilon;
	private static readonly double DoubleZero = 0.0;

	[Test]
	public static void Test_FloatEqualTolerance()
	{
		float test = 0.0f;

		Assert.IsTrue(test.IsRoughlyEqual(FloatZero), $"Expected {test} to be equal to {FloatZero}.");
		Assert.IsTrue(test.IsRoughlyEqual(FloatSmallPositive), $"Expected {test} to be equal to {FloatSmallPositive}.");
		Assert.IsTrue(test.IsRoughlyEqual(FloatSmallNegative), $"Expected {test} to be equal to {FloatSmallNegative}.");
	}

	[Test]
	public static void Test_FloatUnequalTolerance()
	{
		float test = 0.000001f;

		Assert.IsFalse(test.IsRoughlyEqual(FloatZero), $"Expected {test} not to be equal to {FloatZero}.");
		Assert.IsFalse(test.IsRoughlyEqual(FloatSmallPositive), $"Expected {test} not to be equal to {FloatSmallPositive}.");
		Assert.IsFalse(test.IsRoughlyEqual(FloatSmallNegative), $"Expected {test} not to be equal to {FloatSmallNegative}.");
	}

	[Test]
	public static void Test_DoubleEqualTolerance()
	{
		double test = 0.0;

		Assert.IsTrue(test.IsRoughlyEqual(DoubleZero), $"Expected {test} to be equal to {DoubleZero}.");
		Assert.IsTrue(test.IsRoughlyEqual(DoubleSmallPositive), $"Expected {test} to be equal to {DoubleSmallPositive}.");
		Assert.IsTrue(test.IsRoughlyEqual(DoubleSmallNegative), $"Expected {test} to be equal to {DoubleSmallNegative}.");
	}

	[Test]
	public static void Test_DoubleUnequalTolerance()
	{
		double test = 0.0000001;

		Assert.IsFalse(test.IsRoughlyEqual(DoubleZero), $"Expected {test} not to be equal to {DoubleZero}.");
		Assert.IsFalse(test.IsRoughlyEqual(DoubleSmallPositive), $"Expected {test} not to be equal to {DoubleSmallPositive}.");
		Assert.IsFalse(test.IsRoughlyEqual(DoubleSmallNegative), $"Expected {test} not to be equal to {DoubleSmallNegative}.");
	}
}
