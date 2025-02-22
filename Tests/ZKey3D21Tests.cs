using NUnit.Framework;

namespace BitCore.Tests
{
    /// <summary>
    /// Unit tests for verifying the functionality of the ZKey3D21 struct.
    /// These tests ensure that encoding, decoding, and arithmetic operations on Morton keys work as expected.
    /// </summary>
    public static class ZKey3D21Tests
    {
        [Test]
        public static void Test_ZKeyEncodeZero()
        {
            var test = (0u, 0u, 0u);
            ZKey3D21 zkey = new ZKey3D21(test);
            Assert.AreEqual(0ul, zkey.Key, $"Expected {test} to encode to key 0, but got {zkey.Key}.");
        }

        [Test]
        public static void Test_ZKeyEncodeDecode()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        var decode = zkey.Components;
                        Assert.AreEqual(test, decode, $"Expected {test} to decode to {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeIncX()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x + 1, y, z);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        ZKey3D21 testKey = zkey.IncrementX();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after IncrementX on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeIncXY()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x + 1, y + 1, z);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        ZKey3D21 testKey = zkey.IncrementXY();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after IncrementXY on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeIncXZ()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x + 1, y, z + 1);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        ZKey3D21 testKey = zkey.IncrementXZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after IncrementXZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeIncYZ()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x, y + 1, z + 1);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        ZKey3D21 testKey = zkey.IncrementYZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after IncrementYZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeIncXYZ()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x + 1, y + 1, z + 1);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        ZKey3D21 testKey = zkey.IncrementXYZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after IncrementXYZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecX()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x - 1, y, z);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        ZKey3D21 testKey = zkey.DecrementX();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecrementX on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecY()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x, y - 1, z);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        ZKey3D21 testKey = zkey.DecrementY();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecrementY on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecZ()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x, y, z - 1);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        ZKey3D21 testKey = zkey.DecrementZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecrementZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecXY()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x - 1, y - 1, z);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        ZKey3D21 testKey = zkey.DecrementXY();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecrementXY on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecXZ()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x - 1, y, z - 1);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        ZKey3D21 testKey = zkey.DecrementXZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecrementXZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecYZ()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x, y - 1, z - 1);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        ZKey3D21 testKey = zkey.DecrementYZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecrementYZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyEncodeDecodeDecXYZ()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var test = (x, y, z);
                        var expected = (x - 1, y - 1, z - 1);
                        ZKey3D21 zkey = new ZKey3D21(test);
                        ZKey3D21 testKey = zkey.DecrementXYZ();
                        var decode = testKey.Components;
                        Assert.AreEqual(expected, decode, $"Expected {expected} after DecrementXYZ on {test}, but got {decode}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeyAdd()
        {
            for (uint x = 400; x < 405; x++)
            {
                for (uint y = 300; y < 305; y++)
                {
                    for (uint z = 200; z < 205; z++)
                    {
                        var a = (z, y, z);
                        var b = (x, z, y);
                        ZKey3D21 keyA = new ZKey3D21(a);
                        ZKey3D21 keyB = new ZKey3D21(b);
                        ZKey3D21 sum = keyA + keyB;
                        var expected = (a.Item1 + b.Item1, a.Item2 + b.Item2, a.Item3 + b.Item3);
                        Assert.AreEqual(expected, sum.Components, $"Expected {expected} for addition, but got {sum.Components}.");
                    }
                }
            }
        }

        [Test]
        public static void Test_ZKeySub()
        {
            for (uint x = 400; x < 402; x++)
            {
                for (uint y = 800; y < 802; y++)
                {
                    for (uint z = 1200; z < 1204; z++)
                    {
                        var a = (z, y, z);
                        var b = (x, x, y);
                        ZKey3D21 keyA = new ZKey3D21(a);
                        ZKey3D21 keyB = new ZKey3D21(b);
                        ZKey3D21 diff = keyA - keyB;
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
                    for (uint z = 12; z < 14; z++)
                    {
                        var a = (z, y, z);
                        var b = (x, x, y);
                        ZKey3D21 keyA = new ZKey3D21(a);
                        ZKey3D21 keyB = new ZKey3D21(b);
                        ZKey3D21 product = keyA * keyB;
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
                    for (uint z = 12; z < 14; z++)
                    {
                        var a = (z, y, z);
                        ZKey3D21 keyA = new ZKey3D21(a);
                        uint scalar = 5;
                        ZKey3D21 product = keyA * scalar;
                        var expected = (a.Item1 * scalar, a.Item2 * scalar, a.Item3 * scalar);
                        Assert.AreEqual(expected, product.Components, $"Expected {expected} for scalar multiplication, but got {product.Components}.");
                    }
                }
            }
        }
    }
}