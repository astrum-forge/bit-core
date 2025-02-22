using NUnit.Framework;

namespace BitCore.Tests
{
    /// <summary>
    /// Unit tests for verifying the functionality of the ZKey3D10 struct.
    /// These tests ensure that encoding, decoding, and arithmetic operations on Morton keys work as expected.
    /// </summary>
    public static class ZKey3D10Tests
    {
        [Test]
        public static void Test_ZKeyEncodeZero()
        {
            var test = (0u, 0u, 0u);
            ZKey3D10 zkey = new ZKey3D10(test);
            Assert.AreEqual(0u, zkey.Key, $"Expected {test} to encode to key 0, but got {zkey.Key}.");
        }

        [Test]
        public static void Test_ZKeyEncodeDecode()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        var decode = zkey.Components;
                        Assert.AreEqual(test, decode, $"Expected {test} to decode to {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeIncX()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x + 1, y, z);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        ZKey3D10 testKey = zkey.IncrementX();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after IncX on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeIncXY()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x + 1, y + 1, z);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        ZKey3D10 testKey = zkey.IncrementXY();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after IncXY on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeIncXZ()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x + 1, y, z + 1);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        ZKey3D10 testKey = zkey.IncrementXZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after IncXZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeIncYZ()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x, y + 1, z + 1);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        ZKey3D10 testKey = zkey.IncrementYZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after IncYZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeIncXYZ()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x + 1, y + 1, z + 1);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        ZKey3D10 testKey = zkey.IncrementXYZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after IncXYZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecX()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x - 1, y, z);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        ZKey3D10 testKey = zkey.DecrementX();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecX on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecY()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x, y - 1, z);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        ZKey3D10 testKey = zkey.DecrementY();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecY on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecZ()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x, y, z - 1);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        ZKey3D10 testKey = zkey.DecrementZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecXY()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x - 1, y - 1, z);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        ZKey3D10 testKey = zkey.DecrementXY();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecXY on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecXZ()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x - 1, y, z - 1);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        ZKey3D10 testKey = zkey.DecrementXZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecXZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecYZ()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x, y - 1, z - 1);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        ZKey3D10 testKey = zkey.DecrementYZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecYZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecXYZ()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x - 1, y - 1, z - 1);
                        ZKey3D10 zkey = new ZKey3D10(test);
                        ZKey3D10 testKey = zkey.DecrementXYZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecXYZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyAdd()
        {
            for (uint x = 4; x < 9; x++)
            {
                for (uint y = 3; y < 10; y++)
                {
                    for (uint z = 2; z < 11; z++)
                    {
                        var a = (z, y, z);
                        var b = (x, z, y);
                        ZKey3D10 keyA = new ZKey3D10(a);
                        ZKey3D10 keyB = new ZKey3D10(b);
                        ZKey3D10 sum = keyA + keyB;
                        var expected = (a.Item1 + b.Item1, a.Item2 + b.Item2, a.Item3 + b.Item3);
                        Assert.AreEqual(expected, sum.Components, $"Expected {expected} for addition, but got {sum.Components}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeySub()
        {
            for (uint x = 4; x < 6; x++)
            {
                for (uint y = 8; y < 10; y++)
                {
                    for (uint z = 12; z < 16; z++)
                    {
                        var a = (z, y, z);
                        var b = (x, x, y);
                        ZKey3D10 keyA = new ZKey3D10(a);
                        ZKey3D10 keyB = new ZKey3D10(b);
                        ZKey3D10 diff = keyA - keyB;
                        var expected = (a.Item1 - b.Item1, a.Item2 - b.Item2, a.Item3 - b.Item3);
                        Assert.AreEqual(expected, diff.Components, $"Expected {expected} for subtraction, but got {diff.Components}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyMul()
        {
            for (uint x = 4; x < 6; x++)
            {
                for (uint y = 8; y < 10; y++)
                {
                    for (uint z = 12; z < 16; z++)
                    {
                        var a = (z, y, z);
                        var b = (x, x, y);
                        ZKey3D10 keyA = new ZKey3D10(a);
                        ZKey3D10 keyB = new ZKey3D10(b);
                        ZKey3D10 product = keyA * keyB;
                        var expected = (a.Item1 * b.Item1, a.Item2 * b.Item2, a.Item3 * b.Item3);
                        Assert.AreEqual(expected, product.Components, $"Expected {expected} for multiplication, but got {product.Components}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyMulVal()
        {
            for (uint x = 4; x < 6; x++)
            {
                for (uint y = 8; y < 10; y++)
                {
                    for (uint z = 12; z < 16; z++)
                    {
                        var a = (z, y, z);
                        ZKey3D10 keyA = new ZKey3D10(a);
                        uint scalar = 5;
                        ZKey3D10 product = keyA * scalar;
                        var expected = (a.Item1 * scalar, a.Item2 * scalar, a.Item3 * scalar);
                        Assert.AreEqual(expected, product.Components, $"Expected {expected} for scalar multiplication, but got {product.Components}.");
                    }
                }
            }
        }
    }
}