using NUnit.Framework;

namespace BitCore.Tests
{

	/// <summary>
	/// Unit tests for verifying the functionality of uint extension methods.
	/// These tests exercise bit-level operations on a 32-bit unsigned integer.
	/// </summary>
	public static class UIntTests
	{
		// Test constants
		private static readonly uint TestValue = 7623190; // 00000000011101000101001000010110 in binary
		private static readonly string TestValueString = "00000000011101000101001000010110";
		private static readonly string TestHex = "745216";
		private const int LoopCount = 32;

		// Expected bits for TestValue (bit positions 0 through 31)
		private static readonly int[] ExpectedBits =
		{
			0, 1, 1, 0, 1, 0, 0, 0,
			0, 1, 0, 0, 1, 0, 1, 0,
			0, 0, 1, 0, 1, 1, 1, 0,
			0, 0, 0, 0, 0, 0, 0, 0
		};

		// Expected bytes when splitting TestValue
		private static readonly byte[] ExpectedBytes = { 127, 246, 0, 187 };

		[Test]
		public static void Test_BitAt()
		{
			for (int i = 0; i < LoopCount; i++)
			{
				uint actual = TestValue.BitAt(i);
				Assert.AreEqual(ExpectedBits[i], actual, $"BitAt({i}): expected {ExpectedBits[i]}, but got {actual}.");
			}
		}

		[Test]
		public static void Test_BitInvAt()
		{
			for (int i = 0; i < LoopCount; i++)
			{
				int expectedInverse = 1 - ExpectedBits[i];
				uint actual = TestValue.BitInvAt(i);
				Assert.AreEqual(expectedInverse, actual, $"BitInvAt({i}): expected {expectedInverse}, but got {actual}.");
			}
		}

		[Test]
		public static void Test_Bool()
		{
			uint positiveValue = 2;
			uint zeroValue = 0;

			Assert.IsTrue(positiveValue.Bool(), "Non-zero value should return true.");
			Assert.IsFalse(zeroValue.Bool(), "Zero value should return false.");
		}

		[Test]
		public static void Test_SetBitAt()
		{
			for (int i = 0; i < LoopCount; i++)
			{
				uint result = TestValue.SetBitAt(i);
				Assert.AreEqual(1, result.BitAt(i), $"After SetBitAt({i}), bit at position {i} should be 1.");
			}
		}

		[Test]
		public static void Test_ClearBitAt()
		{
			for (int i = 0; i < LoopCount; i++)
			{
				uint result = TestValue.ClearBitAt(i);
				Assert.AreEqual(0, result.BitAt(i), $"After UnsetBitAt({i}), bit at position {i} should be 0.");
			}
		}

		[Test]
		public static void Test_SetBitValueAt()
		{
			for (int i = 0; i < LoopCount; i++)
			{
				uint result0 = TestValue.SetBitValueAt(i, 0);
				uint result1 = TestValue.SetBitValueAt(i, 1);
				Assert.AreEqual(0, result0.BitAt(i), $"SetBit({i}, 0) should set bit {i} to 0.");
				Assert.AreEqual(1, result1.BitAt(i), $"SetBit({i}, 1) should set bit {i} to 1.");
			}
		}

		[Test]
		public static void Test_SetUnsetBitValueAt()
		{
			for (int i = 0; i < LoopCount; i++)
			{
				uint resultSetThenUnset = TestValue.SetBitValueAt(i, 0).SetBitValueAt(i, 1);
				uint resultUnsetThenSet = TestValue.SetBitValueAt(i, 1).SetBitValueAt(i, 0);
				Assert.AreEqual(1, resultSetThenUnset.BitAt(i), $"After setting to 0 then 1 at bit {i}, expected 1.");
				Assert.AreEqual(0, resultUnsetThenSet.BitAt(i), $"After setting to 1 then 0 at bit {i}, expected 0.");
			}
		}

		[Test]
		public static void Test_ToggleBitAt()
		{
			uint toggled = TestValue;
			uint expectedToggled = ~TestValue;

			// Toggle each bit once.
			for (int i = 0; i < LoopCount; i++)
			{
				toggled = toggled.ToggleBitAt(i);
			}
			Assert.AreEqual(expectedToggled, toggled, $"After toggling, expected {expectedToggled} but got {toggled}.");

			// Toggle each bit again to revert to original value.
			for (int i = 0; i < LoopCount; i++)
			{
				toggled = toggled.ToggleBitAt(i);
			}
			Assert.AreEqual(TestValue, toggled, $"After toggling twice, expected {TestValue} but got {toggled}.");
		}

		[Test]
		public static void Test_PopCount()
		{
			int popCount = TestValue.PopCount();
			Assert.AreEqual(10, popCount, $"PopCount of {TestValue} should be 10, but was {popCount}.");
		}

		[Test]
		public static void Test_BitString()
		{
			string bitStr = TestValue.BitString();
			Assert.AreEqual(TestValueString, bitStr, $"BitString of {TestValue} should be {TestValueString}, but was {bitStr}.");
		}

		[Test]
		public static void Test_UIntFromBitString()
		{
			uint valueFromString = TestValueString.UIntFromBitString();
			Assert.AreEqual(TestValue, valueFromString, $"UIntFromBitString('{TestValueString}') should yield {TestValue}, but got {valueFromString}.");
		}

		[Test]
		public static void Test_HexString()
		{
			string hexStr = TestValue.HexString();
			Assert.AreEqual(TestHex, hexStr, $"HexString of {TestValue} should be {TestHex}, but was {hexStr}.");
		}

		[Test]
		public static void Test_IsPowerOfTwo()
		{
			uint power1 = 1024;
			uint power2 = 2048;
			uint power3 = 4096;
			uint nonPower1 = 1400;
			uint nonPower2 = 3300;
			uint nonPower3 = 5010;

			Assert.IsTrue(power1.IsPowerOfTwo(), $"{power1} should be a power of two.");
			Assert.IsTrue(power2.IsPowerOfTwo(), $"{power2} should be a power of two.");
			Assert.IsTrue(power3.IsPowerOfTwo(), $"{power3} should be a power of two.");
			Assert.IsFalse(nonPower1.IsPowerOfTwo(), $"{nonPower1} should not be a power of two.");
			Assert.IsFalse(nonPower2.IsPowerOfTwo(), $"{nonPower2} should not be a power of two.");
			Assert.IsFalse(nonPower3.IsPowerOfTwo(), $"{nonPower3} should not be a power of two.");
		}

		[Test]
		public static void Test_ByteTuple()
		{
			var byteTuple = TestValue.UnpackToBytes();
			uint combined = byteTuple.PackToUInt();
			Assert.AreEqual(TestValue, combined, $"Combining bytes should yield {TestValue}, but got {combined}.");
		}

		[Test]
		public static void Test_SByteTuple()
		{
			var sbyteTuple = TestValue.UnpackToSBytes();
			uint combined = sbyteTuple.PackToUInt();
			Assert.AreEqual(TestValue, combined, $"Combining sbytes should yield {TestValue}, but got {combined}.");
		}

		[Test]
		public static void Test_ShortTuple()
		{
			var shortTuple = TestValue.UnpackToShorts();
			uint combined = shortTuple.PackToUInt();
			Assert.AreEqual(TestValue, combined, $"Combining shorts should yield {TestValue}, but got {combined}.");
		}

		[Test]
		public static void Test_UShortTuple()
		{
			var ushortTuple = TestValue.UnpackToUShorts();
			uint combined = ushortTuple.PackToUInt();
			Assert.AreEqual(TestValue, combined, $"Combining ushorts should yield {TestValue}, but got {combined}.");
		}

		[Test]
		public static void Test_GetByte()
		{
			var byteTuple = TestValue.UnpackToBytes();
			byte[] bytes = { byteTuple.Item1, byteTuple.Item2, byteTuple.Item3, byteTuple.Item4 };

			for (int i = 0; i < 4; i++)
			{
				byte expectedByte = TestValue.ByteAt(i);
				Assert.AreEqual(expectedByte, bytes[i], $"ByteAt({i}) should be {expectedByte}, but got {bytes[i]}.");
			}
		}

		[Test]
		public static void Test_SetByteAt()
		{
			var originalTuple = TestValue.UnpackToBytes();
			byte[] originalBytes = { originalTuple.Item1, originalTuple.Item2, originalTuple.Item3, originalTuple.Item4 };

			// There are 4 bytes in a 32-bit uint.
			for (int i = 0; i < 4; i++)
			{
				uint newValue = TestValue.SetByteAt(ExpectedBytes[i], i);
				var newTuple = newValue.UnpackToBytes();
				byte[] newBytes = { newTuple.Item1, newTuple.Item2, newTuple.Item3, newTuple.Item4 };

				Assert.AreEqual(ExpectedBytes[i], newBytes[i], $"After SetByteAt({i}), expected byte {ExpectedBytes[i]}, but got {newBytes[i]}.");

				// Revert to the original byte value.
				newValue = newValue.SetByteAt(originalBytes[i], i);
				newTuple = newValue.UnpackToBytes();
				newBytes = new byte[] { newTuple.Item1, newTuple.Item2, newTuple.Item3, newTuple.Item4 };

				Assert.AreEqual(originalBytes[i], newBytes[i], $"After reverting SetByteAt({i}), expected byte {originalBytes[i]}, but got {newBytes[i]}.");
			}
		}
	}
}