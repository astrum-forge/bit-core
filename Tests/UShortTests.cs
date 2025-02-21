using BitCore;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Unit tests for verifying the functionality of ushort extension methods.
/// These tests ensure that bitwise operations and conversions on a 16‑bit unsigned integer work as expected.
/// </summary>
public static class UShortTests
{
    // Test constants
    private static readonly ushort TestValue = 47964; // Binary: 1011101101011100
    private static readonly string TestValueString = "1011101101011100";
    private static readonly string TestHex = "BB5C";
    private const int LoopCount = 16;

    // Expected bit sequence (bit positions 0 (LSB) to 15 (MSB))
    private static readonly int[] ExpectedBits = { 0, 0, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1 };

    // Expected bytes when splitting TestValue (MSB first)
    private static readonly byte[] ExpectedBytes = { 127, 246 };

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
        ushort posValue = 2;
        ushort zeroValue = 0;
        Assert.IsTrue(posValue.Bool(), $"Expected {posValue}.Bool() to be true.");
        Assert.IsFalse(zeroValue.Bool(), $"Expected {zeroValue}.Bool() to be false.");
    }

    [Test]
    public static void Test_SetBitAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            ushort result = TestValue.SetBitAt(i);
            Assert.AreEqual(1, result.BitAt(i),
                $"After SetBitAt({i}), expected bit to be 1.");
        }
    }

    [Test]
    public static void Test_ClearBitAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            ushort result = TestValue.ClearBitAt(i);
            Assert.AreEqual(0, result.BitAt(i),
                $"After UnsetBitAt({i}), expected bit to be 0.");
        }
    }

    [Test]
    public static void Test_SetBitValueAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            ushort result0 = TestValue.SetBitValueAt(i, 0);
            ushort result1 = TestValue.SetBitValueAt(i, 1);
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
            ushort result1 = TestValue.SetBitValueAt(i, 0).SetBitValueAt(i, 1);
            ushort result0 = TestValue.SetBitValueAt(i, 1).SetBitValueAt(i, 0);
            Assert.AreEqual(1, result1.BitAt(i),
                $"After setting 0 then 1 at bit {i}, expected bit to be 1.");
            Assert.AreEqual(0, result0.BitAt(i),
                $"After setting 1 then 0 at bit {i}, expected bit to be 0.");
        }
    }

    [Test]
    public static void Test_ToggleBitAt()
    {
        ushort toggled = TestValue;
        ushort expectedToggled = (ushort)~TestValue;

        for (int i = 0; i < LoopCount; i++)
        {
            toggled = toggled.ToggleBitAt(i);
        }
        Assert.AreEqual(expectedToggled, toggled,
            $"After toggling all bits once, expected {expectedToggled}, but got {toggled}.");

        // Toggle again to revert to original value
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
        Assert.AreEqual(10, popCount,
            $"Expected pop count of {TestValue} to be 10, but got {popCount}.");
    }

    [Test]
    public static void Test_BitString()
    {
        string bitStr = TestValue.BitString();
        Assert.AreEqual(TestValueString, bitStr,
            $"Expected BitString of {TestValue} to be '{TestValueString}', but got '{bitStr}'.");
    }

    [Test]
    public static void Test_UShortFromBitString()
    {
        ushort result = TestValueString.UShortFromBitString();
        Assert.AreEqual(TestValue, result,
            $"Expected UShortFromBitString('{TestValueString}') to yield {TestValue}, but got {result}.");
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
        ushort pow1 = 128;
        ushort pow2 = 256;
        ushort pow3 = 512;
        ushort nonPow1 = 140;
        ushort nonPow2 = 330;
        ushort nonPow3 = 501;

        Assert.IsTrue(pow1.IsPowerOfTwo(), $"{pow1} should be a power of two.");
        Assert.IsTrue(pow2.IsPowerOfTwo(), $"{pow2} should be a power of two.");
        Assert.IsTrue(pow3.IsPowerOfTwo(), $"{pow3} should be a power of two.");
        Assert.IsFalse(nonPow1.IsPowerOfTwo(), $"{nonPow1} should not be a power of two.");
        Assert.IsFalse(nonPow2.IsPowerOfTwo(), $"{nonPow2} should not be a power of two.");
        Assert.IsFalse(nonPow3.IsPowerOfTwo(), $"{nonPow3} should not be a power of two.");
    }

    [Test]
    public static void Test_ByteTuple()
    {
        var tuple = TestValue.SplitIntoByte();
        ushort result = tuple.CombineToUShort();
        Assert.AreEqual(TestValue, result,
            $"Expected combined ushort from byte tuple to be {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_SByteTuple()
    {
        var tuple = TestValue.SplitIntoSByte();
        ushort result = tuple.CombineToUShort();
        Assert.AreEqual(TestValue, result,
            $"Expected combined ushort from sbyte tuple to be {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_GetByte()
    {
        var tuple = TestValue.SplitIntoByte();
        byte[] expectedBytes = { tuple.Item1, tuple.Item2 };
        for (int i = 0; i < 2; i++)
        {
            byte expected = TestValue.ByteAt(i);
            Assert.AreEqual(expected, expectedBytes[i],
                $"Expected byte at position {i} to be {expected}, but got {expectedBytes[i]}.");
        }
    }

    [Test]
    public static void Test_SetByteAt()
    {
        var originalTuple = TestValue.SplitIntoByte();
        byte[] originalBytes = { originalTuple.Item1, originalTuple.Item2 };

        for (int i = 0; i < (LoopCount / 8); i++)
        {
            ushort newValue = TestValue.SetByteAt(ExpectedBytes[i], i);
            var newTuple = newValue.SplitIntoByte();
            byte[] newBytes = { newTuple.Item1, newTuple.Item2 };

            Assert.AreEqual(ExpectedBytes[i], newBytes[i],
                $"After setting, expected byte at position {i} to be {ExpectedBytes[i]}, but got {newBytes[i]}.");

            // Revert to the original value and verify
            newValue = newValue.SetByteAt(originalBytes[i], i);
            newTuple = newValue.SplitIntoByte();
            newBytes = new byte[] { newTuple.Item1, newTuple.Item2 };

            Assert.AreEqual(originalBytes[i], newBytes[i],
                $"After reverting, expected byte at position {i} to be {originalBytes[i]}, but got {newBytes[i]}.");
        }
    }
}