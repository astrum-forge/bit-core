using BitCore;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Unit tests for byte extension methods from BitCore.
/// These tests verify functionality related to the unsigned 8-bit <see cref="byte"/> data type.
/// </summary>
public static class ByteTests
{
	// Test constants
	private static readonly byte TestValue = 158; // 10011110 in binary
	private static readonly string TestValueString = "10011110";
	private static readonly string TestHex = "9E";
	private const int LoopCount = 8;

	// Expected bit sequence (assuming bit index 0 is the least-significant bit)
	private static readonly int[] ExpectedBits = { 0, 1, 1, 1, 1, 0, 0, 1 };

	[Test]
	public static void Test_BitAt()
	{
		for (int i = 0; i < LoopCount; i++)
		{
			Assert.AreEqual(ExpectedBits[i], TestValue.BitAt(i), $"BitAt({i}) should be {ExpectedBits[i]}.");
		}
	}

	[Test]
	public static void Test_BitInvAt()
	{
		for (int i = 0; i < LoopCount; i++)
		{
			// Expecting the inverse of the bit
			int expectedInverse = 1 - ExpectedBits[i];
			Assert.AreEqual(expectedInverse, TestValue.BitInvAt(i), $"BitInvAt({i}) should be {expectedInverse}.");
		}
	}

	[Test]
	public static void Test_Bool()
	{
		byte positiveValue = 2;
		byte zeroValue = 0;

		Assert.IsTrue(positiveValue.Bool(), "Non-zero value should return true.");
		Assert.IsFalse(zeroValue.Bool(), "Zero value should return false.");
	}

	[Test]
	public static void Test_SetBitAt()
	{
		for (int i = 0; i < LoopCount; i++)
		{
			byte result = TestValue.SetBitAt(i);
			Assert.AreEqual(1, result.BitAt(i), $"After setting, bit at position {i} should be 1.");
		}
	}

	[Test]
	public static void Test_UnsetBitAt()
	{
		for (int i = 0; i < LoopCount; i++)
		{
			byte result = TestValue.UnsetBitAt(i);
			Assert.AreEqual(0, result.BitAt(i), $"After unsetting, bit at position {i} should be 0.");
		}
	}

	[Test]
	public static void Test_SetBit()
	{
		for (int i = 0; i < LoopCount; i++)
		{
			byte result0 = TestValue.SetBit(i, 0);
			Assert.AreEqual(0, result0.BitAt(i), $"SetBit({i}, 0) should result in bit {i} being 0.");

			byte result1 = TestValue.SetBit(i, 1);
			Assert.AreEqual(1, result1.BitAt(i), $"SetBit({i}, 1) should result in bit {i} being 1.");
		}
	}

	[Test]
	public static void Test_SetUnsetBit()
	{
		for (int i = 0; i < LoopCount; i++)
		{
			// Set to 0 then to 1
			byte result = TestValue.SetBit(i, 0).SetBit(i, 1);
			Assert.AreEqual(1, result.BitAt(i), $"After setting to 0 then 1, bit {i} should be 1.");

			// Set to 1 then to 0
			result = TestValue.SetBit(i, 1).SetBit(i, 0);
			Assert.AreEqual(0, result.BitAt(i), $"After setting to 1 then 0, bit {i} should be 0.");
		}
	}

	[Test]
	public static void Test_ToggleBitAt()
	{
		byte toggled = TestValue;
		byte expectedToggled = (byte)~TestValue;

		// Toggle each bit once
		for (int i = 0; i < LoopCount; i++)
		{
			toggled = toggled.ToggleBitAt(i);
		}

		Assert.AreEqual(expectedToggled, toggled, $"After toggling each bit, expected {expectedToggled} but got {toggled}.");

		// Toggle each bit again to return to original value
		for (int i = 0; i < LoopCount; i++)
		{
			toggled = toggled.ToggleBitAt(i);
		}

		Assert.AreEqual(TestValue, toggled, $"After toggling bits a second time, expected {TestValue} but got {toggled}.");
	}

	[Test]
	public static void Test_PopCount()
	{
		int popCount = TestValue.PopCount();
		Assert.AreEqual(5, popCount, $"PopCount of {TestValue} should be 5 but was {popCount}.");
	}

	[Test]
	public static void Test_BitString()
	{
		string bitStr = TestValue.BitString();
		Assert.AreEqual(TestValueString, bitStr, $"BitString of {TestValue} should be '{TestValueString}' but was '{bitStr}'.");
	}

	[Test]
	public static void Test_ByteFromBitString()
	{
		byte result = TestValueString.ByteFromBitString();
		Assert.AreEqual(TestValue, result, $"ByteFromBitString('{TestValueString}') should be {TestValue} but was {result}.");
	}

	[Test]
	public static void Test_HexString()
	{
		string hex = TestValue.HexString();
		Assert.AreEqual(TestHex, hex, $"HexString of {TestValue} should be '{TestHex}' but was '{hex}'.");
	}

	[Test]
	public static void Test_IsPowerOfTwo()
	{
		byte power1 = 16;
		byte power2 = 64;
		byte power3 = 2;
		byte notPower1 = 14;
		byte notPower2 = 33;
		byte notPower3 = 5;

		Assert.IsTrue(power1.IsPowerOfTwo(), $"{power1} should be a power of two.");
		Assert.IsTrue(power2.IsPowerOfTwo(), $"{power2} should be a power of two.");
		Assert.IsTrue(power3.IsPowerOfTwo(), $"{power3} should be a power of two.");
		Assert.IsFalse(notPower1.IsPowerOfTwo(), $"{notPower1} should not be a power of two.");
		Assert.IsFalse(notPower2.IsPowerOfTwo(), $"{notPower2} should not be a power of two.");
		Assert.IsFalse(notPower3.IsPowerOfTwo(), $"{notPower3} should not be a power of two.");
	}

	[Test]
	public static void Test_GetByte()
	{
		byte result = TestValue.ByteAt(0);
		Assert.AreEqual(TestValue, result, $"ByteAt(0) should return {TestValue} but returned {result}.");
	}
}
