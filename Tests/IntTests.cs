using BitCore;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Unit tests for verifying the functionality of int extension methods.
/// These tests ensure that bitwise operations and conversions behave as expected.
/// </summary>
public static class IntTests
{
    // Test constants
    private static readonly int TestValue = -217623190; // Binary: 11110011000001110101010101101010
    private static readonly string TestValueString = "11110011000001110101010101101010";
    private static readonly string TestHex = "F307556A";
    private const int LoopCount = 32;

    // Expected bit sequence (positions 0 to 31)
    private static readonly int[] ExpectedBits =
    {
         0, 1, 0, 1, 0, 1, 1, 0,
         1, 0, 1, 0, 1, 0, 1, 0,
         1, 1, 1, 0, 0, 0, 0, 0,
         1, 1, 0, 0, 1, 1, 1, 1
    };

    // Expected bytes when splitting TestValue (from MSB to LSB)
    private static readonly byte[] ExpectedBytes = { 127, 246, 0, 187 };

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
        int positive = 2;
        int zero = 0;
        int negative = -2;

        Assert.IsTrue(positive.Bool(), $"Expected {positive}.Bool() to be true.");
        Assert.IsFalse(zero.Bool(), $"Expected {zero}.Bool() to be false.");
        Assert.IsFalse(negative.Bool(), $"Expected {negative}.Bool() to be false.");
    }

    [Test]
    public static void Test_SetBitAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            int result = TestValue.SetBitAt(i);
            Assert.AreEqual(1, result.BitAt(i),
                $"After SetBitAt({i}), bit should be 1.");
        }
    }

    [Test]
    public static void Test_UnsetBitAt()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            int result = TestValue.UnsetBitAt(i);
            Assert.AreEqual(0, result.BitAt(i),
                $"After UnsetBitAt({i}), bit should be 0.");
        }
    }

    [Test]
    public static void Test_SetBit()
    {
        for (int i = 0; i < LoopCount; i++)
        {
            int result0 = TestValue.SetBit(i, 0);
            int result1 = TestValue.SetBit(i, 1);
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
            int result1 = TestValue.SetBit(i, 0).SetBit(i, 1);
            int result0 = TestValue.SetBit(i, 1).SetBit(i, 0);
            Assert.AreEqual(1, result1.BitAt(i),
                $"After setting 0 then 1 at bit {i}, expected bit to be 1.");
            Assert.AreEqual(0, result0.BitAt(i),
                $"After setting 1 then 0 at bit {i}, expected bit to be 0.");
        }
    }

    [Test]
    public static void Test_ToggleBitAt()
    {
        int toggled = TestValue;
        int expectedToggled = ~TestValue;

        // Toggle every bit once
        for (int i = 0; i < LoopCount; i++)
        {
            toggled = toggled.ToggleBitAt(i);
        }
        Assert.AreEqual(expectedToggled, toggled,
            $"After toggling all bits once, expected {expectedToggled} but got {toggled}.");

        // Toggle every bit again to revert to original value
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
    public static void Test_IntFromBitString()
    {
        int result = TestValueString.IntFromBitString();
        Assert.AreEqual(TestValue, result,
            $"Expected IntFromBitString('{TestValueString}') to yield {TestValue}, but got {result}.");
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
        int pow1 = 8192;
        int pow2 = 2048;
        int pow3 = 4096;
        int nonPow1 = 21400;
        int nonPow2 = 3300;
        int nonPow3 = 5010;

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
        int result = tuple.CombineToInt();
        Assert.AreEqual(TestValue, result,
            $"Expected combined int from byte tuple to be {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_SByteTuple()
    {
        var tuple = TestValue.SplitIntoSByte();
        int result = tuple.CombineToInt();
        Assert.AreEqual(TestValue, result,
            $"Expected combined int from sbyte tuple to be {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_ShortTuple()
    {
        var tuple = TestValue.SplitIntoShort();
        int result = tuple.CombineToInt();
        Assert.AreEqual(TestValue, result,
            $"Expected combined int from short tuple to be {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_UShortTuple()
    {
        var tuple = TestValue.SplitIntoUShort();
        int result = tuple.CombineToInt();
        Assert.AreEqual(TestValue, result,
            $"Expected combined int from ushort tuple to be {TestValue}, but got {result}.");
    }

    [Test]
    public static void Test_GetByte()
    {
        var tuple = TestValue.SplitIntoByte();
        byte[] expectedBytes = { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4 };
        for (int i = 0; i < 4; i++)
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
        byte[] originalBytes = { originalTuple.Item1, originalTuple.Item2, originalTuple.Item3, originalTuple.Item4 };

        for (int i = 0; i < 4; i++)
        {
            int newValue = TestValue.SetByteAt(ExpectedBytes[i], i);
            var newTuple = newValue.SplitIntoByte();
            byte[] newBytes = { newTuple.Item1, newTuple.Item2, newTuple.Item3, newTuple.Item4 };

            Assert.AreEqual(ExpectedBytes[i], newBytes[i],
                $"After setting, expected byte at position {i} to be {ExpectedBytes[i]}, but got {newBytes[i]}.");

            // Revert the change and verify original value is restored
            newValue = newValue.SetByteAt(originalBytes[i], i);
            newTuple = newValue.SplitIntoByte();
            newBytes = new byte[] { newTuple.Item1, newTuple.Item2, newTuple.Item3, newTuple.Item4 };

            Assert.AreEqual(originalBytes[i], newBytes[i],
                $"After reverting, expected byte at position {i} to be {originalBytes[i]}, but got {newBytes[i]}.");
        }
    }
}