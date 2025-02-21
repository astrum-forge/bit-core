using BitCore;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Unit tests for verifying the functionality of sbyte extension methods.
/// These tests ensure that bitwise operations and conversions on an 8‑bit signed integer work as expected.
/// </summary>
public static class SByteTests
{
    // Test constants
    private static readonly sbyte TestValue = -87; // Binary representation: 10101001
    private static readonly string TestValueString = "10101001";
    private static readonly string TestHex = "A9";
    private const int LoopCount = 8;

    // Expected bit sequence (from LSB (index 0) to MSB (index 7))
    private static readonly int[] ExpectedBits = { 1, 0, 0, 1, 0, 1, 0, 1 };

    [Test]
    public static void Test_BitAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            int actual = TestValue.BitAt(i);
            Assert.AreEqual(ExpectedBits[i], actual,
                $"BitAt({i}): expected {ExpectedBits[i]}, but got {actual}.");
        }
    }

    [Test]
    public static void Test_BitInvAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            int expected = 1 - ExpectedBits[i];
            int actual = TestValue.BitInvAt(i);
            Assert.AreEqual(expected, actual,
                $"BitInvAt({i}): expected {expected}, but got {actual}.");
        }
    }

    [Test]
    public static void Test_Bool()
    {
        sbyte posValue = 2;
        sbyte zeroValue = 0;
        sbyte negValue = -2;

        Assert.IsTrue(posValue.Bool(), $"Expected {posValue}.Bool() to be true.");
        Assert.IsFalse(zeroValue.Bool(), $"Expected {zeroValue}.Bool() to be false.");
        Assert.IsFalse(negValue.Bool(), $"Expected {negValue}.Bool() to be false.");
    }

    [Test]
    public static void Test_SetBitAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            sbyte result = TestValue.SetBitAt(i);
            Assert.AreEqual(1, result.BitAt(i),
                $"After SetBitAt({i}), expected bit to be 1.");
        }
    }

    [Test]
    public static void Test_ClearBitAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            sbyte result = TestValue.ClearBitAt(i);
            Assert.AreEqual(0, result.BitAt(i),
                $"After UnsetBitAt({i}), expected bit to be 0.");
        }
    }

    [Test]
    public static void Test_SetBitValueAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            sbyte result0 = TestValue.SetBitValueAt(i, 0);
            sbyte result1 = TestValue.SetBitValueAt(i, 1);
            Assert.AreEqual(0, result0.BitAt(i),
                $"SetBit({i}, 0): expected bit to be 0.");
            Assert.AreEqual(1, result1.BitAt(i),
                $"SetBit({i}, 1): expected bit to be 1.");
        }
    }

    [Test]
    public static void Test_SetUnsetBitValueAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            sbyte result1 = TestValue.SetBitValueAt(i, 0).SetBitValueAt(i, 1);
            sbyte result0 = TestValue.SetBitValueAt(i, 1).SetBitValueAt(i, 0);
            Assert.AreEqual(1, result1.BitAt(i),
                $"After setting 0 then 1 at bit {i}, expected bit to be 1.");
            Assert.AreEqual(0, result0.BitAt(i),
                $"After setting 1 then 0 at bit {i}, expected bit to be 0.");
        }
    }

    [Test]
    public static void Test_ToggleBitAt()
    {
        sbyte toggled = TestValue;
        sbyte expectedToggled = (sbyte)~TestValue;

        // Toggle every bit once.
        for (int i = 0; i < LoopCount; i++)
        {
            toggled = toggled.ToggleBitAt(i);
        }
        Assert.AreEqual(expectedToggled, toggled,
            $"After toggling all bits once, expected {expectedToggled}, but got {toggled}.");

        // Toggle every bit again to revert to original value.
        for (int i = 0; i < LoopCount; i++)
        {
            toggled = toggled.ToggleBitAt(i);
        }
        Assert.AreEqual(TestValue, toggled,
            $"After toggling all bits twice, expected {TestValue}, but got {toggled}.");
    }

    [Test]
    public static void Test_PopCount()
    {
        int popCount = TestValue.PopCount();
        Assert.AreEqual(4, popCount,
            $"Expected pop count of {TestValue} to be 4, but got {popCount}.");
    }

    [Test]
    public static void Test_BitString()
    {
        string bitStr = TestValue.BitString();
        Assert.AreEqual(TestValueString, bitStr,
            $"Expected BitString of {TestValue} to be '{TestValueString}', but got '{bitStr}'.");
    }

    [Test]
    public static void Test_SByteFromBitString()
    {
        sbyte result = TestValueString.SByteFromBitString();
        Assert.AreEqual(TestValue, result,
            $"Expected SByteFromBitString('{TestValueString}') to yield {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_HexString()
    {
        string hex = TestValue.HexString();
        Assert.AreEqual(TestHex, hex,
            $"Expected HexString of {TestValue} to be '{TestHex}', but got '{hex}'.");
    }

    [Test]
    public static void Test_IsPowerOfTwo()
    {
        sbyte pow1 = 16;
        sbyte pow2 = 64;
        sbyte pow3 = 2;
        sbyte nonPow1 = 14;
        sbyte nonPow2 = 33;
        sbyte nonPow3 = 5;

        Assert.IsTrue(pow1.IsPowerOfTwo(), $"{pow1} should be a power of two.");
        Assert.IsTrue(pow2.IsPowerOfTwo(), $"{pow2} should be a power of two.");
        Assert.IsTrue(pow3.IsPowerOfTwo(), $"{pow3} should be a power of two.");
        Assert.IsFalse(nonPow1.IsPowerOfTwo(), $"{nonPow1} should not be a power of two.");
        Assert.IsFalse(nonPow2.IsPowerOfTwo(), $"{nonPow2} should not be a power of two.");
        Assert.IsFalse(nonPow3.IsPowerOfTwo(), $"{nonPow3} should not be a power of two.");
    }

    [Test]
    public static void Test_GetByte()
    {
        // For sbyte, ByteAt returns the byte representation (only valid for index 0)
        byte expected = (byte)TestValue;
        byte actual = TestValue.ByteAt(0);
        Assert.AreEqual(expected, actual,
            $"Expected ByteAt(0) to return {expected}, but got {actual}.");
    }
}