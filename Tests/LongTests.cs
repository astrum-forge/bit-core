using BitCore;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Unit tests for verifying the functionality of long extension methods.
/// These tests ensure that bitwise operations and conversions on 64‑bit signed integers behave as expected.
/// </summary>
public static class LongTests
{
    // Test constants
    private static readonly long TestValue = -396761530871789; // Binary: 1111111111111110100101110010010111000001111001000101000000010011
    private static readonly string TestValueString = "1111111111111110100101110010010111000001111001000101000000010011";
    private static readonly string TestHex = "FFFE9725C1E45013";
    private const int LoopCount = 64;

    // Expected bit sequence (bit positions 0 to 63, where index 0 is LSB)
    private static readonly int[] ExpectedBits = {
        1, 1, 0, 0, 1, 0, 0, 0,
        0, 0, 0, 0, 1, 0, 1, 0,
        0, 0, 1, 0, 0, 1, 1, 1,
        1, 0, 0, 0, 0, 0, 1, 1,
        1, 0, 1, 0, 0, 1, 0, 0,
        1, 1, 1, 0, 1, 0, 0, 1,
        0, 1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 1, 1, 1, 1, 1
    };

    // Expected bytes when splitting TestValue (ordered from most significant to least)
    private static readonly byte[] ExpectedBytes = { 127, 246, 0, 187, 192, 5, 18, 57 };

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
        long posValue = 2;
        long zeroValue = 0;
        long negValue = -2;

        Assert.IsTrue(posValue.Bool(), $"Expected {posValue}.Bool() to be true.");
        Assert.IsFalse(zeroValue.Bool(), $"Expected {zeroValue}.Bool() to be false.");
        Assert.IsFalse(negValue.Bool(), $"Expected {negValue}.Bool() to be false.");
    }

    [Test]
    public static void Test_SetBitAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            long result = TestValue.SetBitAt(i);
            Assert.AreEqual(1, result.BitAt(i),
                $"After SetBitAt({i}), bit should be 1.");
        }
    }

    [Test]
    public static void Test_UnsetBitAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            long result = TestValue.UnsetBitAt(i);
            Assert.AreEqual(0, result.BitAt(i),
                $"After UnsetBitAt({i}), bit should be 0.");
        }
    }

    [Test]
    public static void Test_SetBit()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            long result0 = TestValue.SetBit(i, 0);
            long result1 = TestValue.SetBit(i, 1);
            Assert.AreEqual(0, result0.BitAt(i),
                $"SetBit({i}, 0): expected bit {i} to be 0.");
            Assert.AreEqual(1, result1.BitAt(i),
                $"SetBit({i}, 1): expected bit {i} to be 1.");
        }
    }

    [Test]
    public static void Test_SetUnsetBit()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            long result1 = TestValue.SetBit(i, 0).SetBit(i, 1);
            long result0 = TestValue.SetBit(i, 1).SetBit(i, 0);
            Assert.AreEqual(1, result1.BitAt(i),
                $"After setting 0 then 1 at bit {i}, expected bit to be 1.");
            Assert.AreEqual(0, result0.BitAt(i),
                $"After setting 1 then 0 at bit {i}, expected bit to be 0.");
        }
    }

    [Test]
    public static void Test_ToggleBitAt()
    {
        long toggled = TestValue;
        long expectedToggled = ~TestValue;

        // Toggle every bit once.
        for (int i = 0; i < LoopCount; i++)
        {
            toggled = toggled.ToggleBitAt(i);
        }
        Assert.AreEqual(expectedToggled, toggled,
            $"After toggling all bits once, expected {expectedToggled} but got {toggled}.");

        // Toggle again to revert to original.
        for (int i = 0; i < LoopCount; i++)
        {
            toggled = toggled.ToggleBitAt(i);
        }
        Assert.AreEqual(TestValue, toggled,
            $"After toggling all bits twice, expected {TestValue} but got {toggled}.");
    }

    [Test]
    public static void Test_PopCount()
    {
        int popCount = TestValue.PopCount();
        Assert.AreEqual(17, popCount,
            $"Expected pop count of {TestValue} to be 17, but got {popCount}.");
    }

    [Test]
    public static void Test_BitString()
    {
        string bitStr = TestValue.BitString();
        Assert.AreEqual(TestValueString, bitStr,
            $"Expected BitString of {TestValue} to be '{TestValueString}', but got '{bitStr}'.");
    }

    [Test]
    public static void Test_LongFromBitString()
    {
        long result = TestValueString.LongFromBitString();
        Assert.AreEqual(TestValue, result,
            $"Expected LongFromBitString('{TestValueString}') to yield {TestValue}, but got {result}.");
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
        long pow1 = 1024;
        long pow2 = 2048;
        long pow3 = 4096;
        long nonPow1 = 1400;
        long nonPow2 = 3300;
        long nonPow3 = 5010;

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
        long result = tuple.CombineToLong();
        Assert.AreEqual(TestValue, result,
            $"Expected combined long from byte tuple to be {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_SByteTuple()
    {
        var tuple = TestValue.SplitIntoSByte();
        long result = tuple.CombineToLong();
        Assert.AreEqual(TestValue, result,
            $"Expected combined long from sbyte tuple to be {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_ShortTuple()
    {
        var tuple = TestValue.SplitIntoShort();
        long result = tuple.CombineToLong();
        Assert.AreEqual(TestValue, result,
            $"Expected combined long from short tuple to be {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_UShortTuple()
    {
        var tuple = TestValue.SplitIntoUShort();
        long result = tuple.CombineToLong();
        Assert.AreEqual(TestValue, result,
            $"Expected combined long from ushort tuple to be {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_IntTuple()
    {
        var tuple = TestValue.SplitIntoInt();
        long result = tuple.CombineToLong();
        Assert.AreEqual(TestValue, result,
            $"Expected combined long from int tuple to be {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_UIntTuple()
    {
        var tuple = TestValue.SplitIntoUInt();
        long result = tuple.CombineToLong();
        Assert.AreEqual(TestValue, result,
            $"Expected combined long from uint tuple to be {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_GetByte()
    {
        // Note: SplitIntoByte() returns a nested tuple of two 4-byte tuples.
        var tuple = TestValue.SplitIntoByte();
        byte[] expectedBytes = {
            tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4,
            tuple.Item5, tuple.Item6, tuple.Item7, tuple.Item8
        };

        for (int i = 0; i < 8; i++)
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
        byte[] originalBytes = {
            originalTuple.Item1, originalTuple.Item2, originalTuple.Item3, originalTuple.Item4,
            originalTuple.Item5, originalTuple.Item6, originalTuple.Item7, originalTuple.Item8
        };

        for (int i = 0; i < (LoopCount / 8); i++)
        {
            long newValue = TestValue.SetByteAt(ExpectedBytes[i], i);
            var newTuple = newValue.SplitIntoByte();
            byte[] newBytes = {
                newTuple.Item1, newTuple.Item2, newTuple.Item3, newTuple.Item4,
                newTuple.Item5, newTuple.Item6, newTuple.Item7, newTuple.Item8
            };
            Assert.AreEqual(ExpectedBytes[i], newBytes[i],
                $"After setting, expected byte at position {i} to be {ExpectedBytes[i]}, but got {newBytes[i]}.");

            // Revert to original and check.
            newValue = newValue.SetByteAt(originalBytes[i], i);
            newTuple = newValue.SplitIntoByte();
            newBytes = new byte[] {
                newTuple.Item1, newTuple.Item2, newTuple.Item3, newTuple.Item4,
                newTuple.Item5, newTuple.Item6, newTuple.Item7, newTuple.Item8
            };
            Assert.AreEqual(originalBytes[i], newBytes[i],
                $"After reverting, expected byte at position {i} to be {originalBytes[i]}, but got {newBytes[i]}.");
        }
    }
}