using NUnit.Framework;

namespace BitCore.Tests
{
    public class BitArray64Tests
    {
        private static readonly int[] ExpectedBits =
        {
            0, 1, 1, 0, 1, 0, 0, 0,
            0, 1, 0, 0, 1, 0, 1, 0,
            0, 0, 1, 0, 1, 0, 1, 0,
            0, 0, 0, 1, 0, 1, 0, 0,
            1, 0, 0, 0, 0, 1, 0, 0,
            1, 0, 0, 0, 0, 0, 1, 0,
            1, 1, 0, 0, 0, 0, 0, 1,
            0, 1, 1, 0, 1, 0, 0, 0,
            0, 1, 0, 0, 1, 0, 1, 0,
            0, 0, 1, 0, 0, 0, 1, 0,
            0, 0, 0, 1, 1, 1, 1, 1,
            0, 1, 1, 0, 1, 0, 0, 0,
            0, 1, 0, 0, 1, 0, 1, 0,
            0, 0, 1, 0, 1, 1, 1, 0,
            0, 0, 0, 1, 0, 0, 1, 0,
            1, 0, 0, 0, 0, 1, 0, 0,
            1, 0, 0, 0, 0, 0, 1, 0,
            1, 0, 0, 0, 0, 0, 0, 1,
            0, 1, 1, 0, 1, 0, 0, 0,
            0, 1, 0, 0, 1, 0, 1, 0,
            0, 0, 1, 0, 1, 0, 1, 0,
            0, 0, 1
        };

        private const int PopCount = 59;
        private static readonly int LoopCount = ExpectedBits.Length;

        [Test]
        public static void Test_SetGetBitAt()
        {
            BitArray64 array = new BitArray64(LoopCount);

            // set all the internal bits
            for (int i = 0; i < LoopCount; i++)
            {
                if (ExpectedBits[i] == 1)
                {
                    array.SetBitAt(i);
                }
            }

            // get all the internal bits
            // set all the internal bits
            for (int i = 0; i < LoopCount; i++)
            {
                int actual = array.GetBitAt(i);
                Assert.AreEqual(ExpectedBits[i], actual, $"BitArray64.GetBitAt({i}): expected {ExpectedBits[i]}, but got {actual}.");
            }
        }

        [Test]
        public static void Test_SetGetBitInvAt()
        {
            BitArray64 array = new BitArray64(LoopCount);

            // set all the internal bits
            for (int i = 0; i < LoopCount; i++)
            {
                if (ExpectedBits[i] == 1)
                {
                    array.SetBitAt(i);
                }
            }

            // get all the internal bits
            for (int i = 0; i < LoopCount; i++)
            {
                int actual = array.GetBitInvAt(i);
                Assert.AreEqual(1 - ExpectedBits[i], actual, $"BitArray64.GetBitInvAt({i}): expected {1 - ExpectedBits[i]}, but got {actual}.");
            }
        }

        [Test]
        public static void Test_ClearBitAt()
        {
            BitArray64 array = new BitArray64(LoopCount);

            // set all the internal bits
            for (int i = 0; i < LoopCount; i++)
            {
                if (ExpectedBits[i] == 1)
                {
                    array.SetBitAt(i);
                }
            }

            // clear the internal bits
            for (int i = 0; i < LoopCount; i++)
            {
                array.ClearBitAt(i);
                Assert.AreEqual(0, array.GetBitAt(i), $"After BitArray64.ClearBitAt({i}), bit at position {i} should be 0.");
            }
        }

        [Test]
        public static void Test_SetBitValueAt()
        {
            BitArray64 array = new BitArray64(LoopCount);

            // set all the internal bits
            for (int i = 0; i < LoopCount; i++)
            {
                array.SetBitValueAt(i, ExpectedBits[i]);
            }

            // get all the internal bits
            for (int i = 0; i < LoopCount; i++)
            {
                int actual = array.GetBitAt(i);
                Assert.AreEqual(ExpectedBits[i], actual, $"BitArray64.GetBitAt({i}): expected {ExpectedBits[i]}, but got {actual}.");
            }
        }

        [Test]
        public static void Test_ToggleBitAt()
        {
            BitArray64 array = new BitArray64(LoopCount);

            // set all the internal bits
            for (int i = 0; i < LoopCount; i++)
            {
                array.SetBitValueAt(i, ExpectedBits[i]);
            }

            // get all the internal bits
            for (int i = 0; i < LoopCount; i++)
            {
                // will invert, 1's will become zero and zero will become 1's
                array.ToggleBitAt(i);
                int actual = array.GetBitAt(i);
                Assert.AreEqual(1 - ExpectedBits[i], actual, $"BitArray64.GetBitAt({i}): expected {1 - ExpectedBits[i]}, but got {actual}.");
            }
        }

        [Test]
        public static void Test_PopCount()
        {
            BitArray64 array = new BitArray64(LoopCount);

            // set all the internal bits
            for (int i = 0; i < LoopCount; i++)
            {
                array.SetBitValueAt(i, ExpectedBits[i]);
            }

            int popCount = array.PopCount();
            Assert.AreEqual(PopCount, popCount, $"BitArray64.PopCount should be {PopCount}, but was {popCount}.");
        }
    }
}