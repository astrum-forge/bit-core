#if UNITY_EDITOR
#define BITCORE_DEBUG
#endif

#if NET_4_6 && !BITCORE_DISABLE_INLINE
#define BITCORE_METHOD_INLINE
#endif

using System;
#if BITCORE_METHOD_INLINE
using System.Runtime.CompilerServices;
#endif

namespace BitCore
{
    /// <summary>
    /// Provides a simplified representation of a 3-component Morton key.
    /// This struct encapsulates a single 32‑bit unsigned integer (zKey) that interleaves 10 bits each for x, y, and z.
    /// Morton keys are useful for spatial hashing and data structures with good cache locality.
    /// <para>
    /// In debug builds, input validation is performed; these checks are omitted in production.
    /// </para>
    /// </summary>
    public readonly struct ZKey10 : IEquatable<ZKey10>, IEquatable<uint>, IEquatable<(uint x, uint y, uint z)>
    {
        private readonly uint zKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZKey10"/> struct with the given Morton key.
        /// </summary>
        /// <param name="zKey">The Morton key as a 32‑bit unsigned integer.</param>
        public ZKey10(uint zKey) => this.zKey = zKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZKey10"/> struct from a positive integer key.
        /// </summary>
        /// <param name="zKey">The Morton key as a positive integer.</param>
        public ZKey10(int zKey)
        {
#if BITCORE_DEBUG
            if (zKey < 0)
            {
                BitDebug.Throw("ZKey10(int) - Morton key must be positive");
            }
#endif
            this.zKey = (uint)zKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZKey10"/> struct from separate x, y, and z components.
        /// Each component must be between 0 and 1023 (10 bits).
        /// </summary>
        /// <param name="x">The x component.</param>
        /// <param name="y">The y component.</param>
        /// <param name="z">The z component.</param>
        public ZKey10(uint x, uint y, uint z)
        {
#if BITCORE_DEBUG
            if (x > 1023)
                BitDebug.Throw($"ZKey10(uint, uint, uint) - x component must be between 0-1023 (10 bits), was {x}");
            if (y > 1023)
                BitDebug.Throw($"ZKey10(uint, uint, uint) - y component must be between 0-1023 (10 bits), was {y}");
            if (z > 1023)
                BitDebug.Throw($"ZKey10(uint, uint, uint) - z component must be between 0-1023 (10 bits), was {z}");
#endif
            zKey = ZKey10Util.Encode(x, y, z);
        }

        public ZKey10((uint x, uint y, uint z) tuple)
        {
#if BITCORE_DEBUG
            if (tuple.x > 1023)
                BitDebug.Throw($"ZKey10(uint, uint, uint) - x component must be between 0-1023 (10 bits), was {tuple.x}");
            if (tuple.y > 1023)
                BitDebug.Throw($"ZKey10(uint, uint, uint) - y component must be between 0-1023 (10 bits), was {tuple.y}");
            if (tuple.z > 1023)
                BitDebug.Throw($"ZKey10(uint, uint, uint) - z component must be between 0-1023 (10 bits), was {tuple.z}");
#endif
            zKey = ZKey10Util.Encode(tuple.x, tuple.y, tuple.z);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZKey10"/> struct from separate x, y, and z components.
        /// Each component must be between 0 and 1023 (10 bits).
        /// </summary>
        /// <param name="x">The x component.</param>
        /// <param name="y">The y component.</param>
        /// <param name="z">The z component.</param>
        public ZKey10(int x, int y, int z)
        {
#if BITCORE_DEBUG
            if (x < 0 || x > 1023)
                BitDebug.Throw($"ZKey10(int, int, int) - x component must be between 0-1023 (10 bits), was {x}");
            if (y < 0 || y > 1023)
                BitDebug.Throw($"ZKey10(int, int, int) - y component must be between 0-1023 (10 bits), was {y}");
            if (z < 0 || z > 1023)
                BitDebug.Throw($"ZKey10(int, int, int) - z component must be between 0-1023 (10 bits), was {z}");
#endif
            zKey = ZKey10Util.Encode((uint)x, (uint)y, (uint)z);
        }

        public ZKey10((int x, int y, int z) tuple)
        {
#if BITCORE_DEBUG
            if (tuple.x < 0 || tuple.x > 1023)
                BitDebug.Throw($"ZKey10(int, int, int) - x component must be between 0-1023 (10 bits), was {tuple.x}");
            if (tuple.y < 0 || tuple.y > 1023)
                BitDebug.Throw($"ZKey10(int, int, int) - y component must be between 0-1023 (10 bits), was {tuple.y}");
            if (tuple.z < 0 || tuple.z > 1023)
                BitDebug.Throw($"ZKey10(int, int, int) - z component must be between 0-1023 (10 bits), was {tuple.z}");
#endif
            zKey = ZKey10Util.Encode((uint)tuple.x, (uint)tuple.y, (uint)tuple.z);
        }

        /// <summary>
        /// Gets the raw Morton key.
        /// </summary>
        public uint key => zKey;

        /// <summary>
        /// Gets the x component of the Morton key.
        /// </summary>
        public uint x => ZKey10Util.DecodePart(zKey);

        /// <summary>
        /// Gets the y component of the Morton key.
        /// </summary>
        public uint y => ZKey10Util.DecodePart(zKey >> 1);

        /// <summary>
        /// Gets the z component of the Morton key.
        /// </summary>
        public uint z => ZKey10Util.DecodePart(zKey >> 2);

        /// <summary>
        /// Gets the raw (x, y, z) components of the Morton key.
        /// </summary>
        public (uint x, uint y, uint z) RawValue => ZKey10Util.Decode(zKey);

        /// <summary>
        /// Increments the x component of the Morton key by one unit.
        /// </summary>
        public ZKey10 IncX() => new ZKey10(ZKey10Util.IncX(zKey));

        /// <summary>
        /// Increments the y component of the Morton key by one unit.
        /// </summary>
        public ZKey10 IncY() => new ZKey10(ZKey10Util.IncY(zKey));

        /// <summary>
        /// Increments the z component of the Morton key by one unit.
        /// </summary>
        public ZKey10 IncZ() => new ZKey10(ZKey10Util.IncZ(zKey));

        /// <summary>
        /// Increments the x and y components of the Morton key by one unit each.
        /// </summary>
        public ZKey10 IncXY()
        {
            uint key = zKey;

            key = ZKey10Util.IncX(key);
            key = ZKey10Util.IncY(key);

            return new ZKey10(key);
        }

        /// <summary>
        /// Increments the x and z components of the Morton key by one unit each.
        /// </summary>
        public ZKey10 IncXZ()
        {
            uint key = zKey;

            key = ZKey10Util.IncX(key);
            key = ZKey10Util.IncZ(key);

            return new ZKey10(key);
        }

        /// <summary>
        /// Increments the y and z components of the Morton key by one unit each.
        /// </summary>
        public ZKey10 IncYZ()
        {
            uint key = zKey;

            key = ZKey10Util.IncY(key);
            key = ZKey10Util.IncZ(key);

            return new ZKey10(key);
        }

        /// <summary>
        /// Increments all three components (x, y, and z) of the Morton key by one unit.
        /// </summary>
        public ZKey10 IncXYZ()
        {
            uint key = zKey;

            key = ZKey10Util.IncX(key);
            key = ZKey10Util.IncY(key);
            key = ZKey10Util.IncZ(key);

            return new ZKey10(key);
        }

        /// <summary>
        /// Decrements the x component of the Morton key by one unit.
        /// </summary>
        public ZKey10 DecX() => new ZKey10(ZKey10Util.DecX(zKey));

        /// <summary>
        /// Decrements the y component of the Morton key by one unit.
        /// </summary>
        public ZKey10 DecY() => new ZKey10(ZKey10Util.DecY(zKey));

        /// <summary>
        /// Decrements the z component of the Morton key by one unit.
        /// </summary>
        public ZKey10 DecZ() => new ZKey10(ZKey10Util.DecZ(zKey));

        /// <summary>
        /// Decrements the x and y components of the Morton key by one unit each.
        /// </summary>
        public ZKey10 DecXY()
        {
            uint key = zKey;

            key = ZKey10Util.DecX(key);
            key = ZKey10Util.DecY(key);

            return new ZKey10(key);
        }

        /// <summary>
        /// Decrements the x and z components of the Morton key by one unit each.
        /// </summary>
        public ZKey10 DecXZ()
        {
            uint key = zKey;

            key = ZKey10Util.DecX(key);
            key = ZKey10Util.DecZ(key);

            return new ZKey10(key);
        }

        /// <summary>
        /// Decrements the y and z components of the Morton key by one unit each.
        /// </summary>
        public ZKey10 DecYZ()
        {
            uint key = zKey;

            key = ZKey10Util.DecY(key);
            key = ZKey10Util.DecZ(key);

            return new ZKey10(key);
        }

        /// <summary>
        /// Decrements all three components (x, y, and z) of the Morton key by one unit.
        /// </summary>
        public ZKey10 DecXYZ()
        {
            uint key = zKey;
            key = ZKey10Util.DecX(key);
            key = ZKey10Util.DecY(key);
            key = ZKey10Util.DecZ(key);
            return new ZKey10(key);
        }

        /// <summary>
        /// Returns a new Morton key with the key value modulo the specified value.
        /// </summary>
        /// <param name="modulo">The modulo value.</param>
        public ZKey10 Mod(uint modulo) => new ZKey10(zKey % modulo);

        /// <summary>
        /// Returns a new Morton key with the key value masked by the specified mask.
        /// </summary>
        /// <param name="mask">The mask to apply.</param>
        public ZKey10 Mask(uint mask) => new ZKey10(zKey & mask);

        /// <summary>
        /// Adds two Morton keys component-wise.
        /// For example, ZKey10(1,2,3) + ZKey10(4,5,6) yields ZKey10(5,7,9).
        /// </summary>
        /// <param name="x">The first Morton key.</param>
        /// <param name="y">The second Morton key.</param>
        /// <returns>The component-wise sum as a new Morton key.</returns>
        public static ZKey10 operator +(ZKey10 x, ZKey10 y)
        {
            uint sumX = (x.zKey | ZKey10Util.YZ_MASK) + (y.zKey & ZKey10Util.X_MASK);
            uint sumY = (x.zKey | ZKey10Util.XZ_MASK) + (y.zKey & ZKey10Util.Y_MASK);
            uint sumZ = (x.zKey | ZKey10Util.XY_MASK) + (y.zKey & ZKey10Util.Z_MASK);

            return new ZKey10((sumX & ZKey10Util.X_MASK) | (sumY & ZKey10Util.Y_MASK) | (sumZ & ZKey10Util.Z_MASK));
        }

        /// <summary>
        /// Multiplies two Morton keys component-wise.
        /// For example, ZKey10(1,2,3) * ZKey10(4,5,6) yields ZKey10(4,10,18).
        /// </summary>
        /// <param name="x">The first Morton key.</param>
        /// <param name="y">The second Morton key.</param>
        /// <returns>The component-wise product as a new Morton key.</returns>
        public static ZKey10 operator *(ZKey10 x, ZKey10 y)
        {
            // TO-DO: Replace with a more efficient method if needed.
            var vx = x.RawValue;
            var vy = y.RawValue;

            return new ZKey10((uint)(vx.x * vy.x), (uint)(vx.y * vy.y), (uint)(vx.z * vy.z));
        }

        /// <summary>
        /// Multiplies each component of the Morton key by a scalar value.
        /// For example, ZKey10(1,2,3) * 4 yields ZKey10(4,8,12).
        /// </summary>
        /// <param name="x">The Morton key.</param>
        /// <param name="val">The scalar multiplier.</param>
        /// <returns>A new Morton key with each component multiplied by the scalar.</returns>
        public static ZKey10 operator *(ZKey10 x, uint val)
        {
            // TO-DO: Replace with a more efficient method if needed.
            var vx = x.RawValue;
            return new ZKey10((uint)(vx.x * val), (uint)(vx.y * val), (uint)(vx.z * val));
        }

        /// <summary>
        /// Subtracts two Morton keys component-wise.
        /// For example, ZKey10(4,5,6) - ZKey10(1,2,3) yields ZKey10(3,3,3).
        /// </summary>
        /// <param name="x">The minuend Morton key.</param>
        /// <param name="y">The subtrahend Morton key.</param>
        /// <returns>The component-wise difference as a new Morton key.</returns>
        public static ZKey10 operator -(ZKey10 x, ZKey10 y)
        {
            uint diffX = (x.zKey & ZKey10Util.X_MASK) - (y.zKey & ZKey10Util.X_MASK);
            uint diffY = (x.zKey & ZKey10Util.Y_MASK) - (y.zKey & ZKey10Util.Y_MASK);
            uint diffZ = (x.zKey & ZKey10Util.Z_MASK) - (y.zKey & ZKey10Util.Z_MASK);

            return new ZKey10((diffX & ZKey10Util.X_MASK) | (diffY & ZKey10Util.Y_MASK) | (diffZ & ZKey10Util.Z_MASK));
        }

        /// <summary>
        /// Determines whether this Morton key is equal to another.
        /// </summary>
        public bool Equals(ZKey10 other) => zKey == other.zKey;

        /// <summary>
        /// Determines whether this Morton key is equal to a given uint value.
        /// </summary>
        public bool Equals(uint other) => zKey == other;

        /// <summary>
        /// Determines whether this Morton key is equal to the given (x, y, z) tuple.
        /// </summary>
        public bool Equals((uint x, uint y, uint z) other) =>
            zKey == ZKey10Util.Encode(other.x, other.y, other.z);

        /// <summary>
        /// Returns a copy of this Morton key.
        /// </summary>
        public ZKey10 Copy() => new ZKey10(key);

        public static explicit operator uint(ZKey10 key)
        {
            return key.zKey;
        }

        public static explicit operator ZKey10(uint key)
        {
            return new ZKey10(key);
        }

        public static explicit operator ZKey10(int key)
        {
            return new ZKey10(key);
        }

        public static explicit operator ZKey10((uint, uint, uint) tuple)
        {
            return new ZKey10(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        public static explicit operator (uint, uint, uint)(ZKey10 key)
        {
            return key.RawValue;
        }
    }
}