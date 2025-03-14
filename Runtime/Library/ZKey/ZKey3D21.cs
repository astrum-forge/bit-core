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
    /// Represents a 3D Morton key (Z-order curve) using a 64-bit unsigned integer,
    /// with 21 bits per component (x, y, z), allowing values from 0 to 2,097,151.
    /// Provides efficient spatial hashing for data structures with improved cache locality.
    /// </summary>
    [Serializable]
    public readonly struct ZKey3D21 : IEquatable<ZKey3D21>, IEquatable<ulong>, IEquatable<(uint x, uint y, uint z)>
    {
        private readonly ulong _zKey;

        /// <summary>
        /// Initializes a new instance with a precomputed Morton key.
        /// </summary>
        /// <param name="zKey">The Morton key as a 64-bit unsigned integer.</param>
        public ZKey3D21(ulong zKey) => _zKey = zKey;

        /// <summary>
        /// Initializes a new instance from a positive long integer key.
        /// </summary>
        /// <param name="zKey">The Morton key as a positive long integer.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in debug mode if zKey is negative.</exception>
        public ZKey3D21(long zKey)
        {
#if BITCORE_DEBUG
            if (zKey < 0) throw new ArgumentOutOfRangeException(nameof(zKey), "Morton key must be positive.");
#endif
            _zKey = (ulong)zKey;
        }

        /// <summary>
        /// Initializes a new instance from x, y, z components (uint).
        /// </summary>
        /// <param name="x">X component (0-2,097,151).</param>
        /// <param name="y">Y component (0-2,097,151).</param>
        /// <param name="z">Z component (0-2,097,151).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in debug mode if any component exceeds 2,097,151.</exception>
        public ZKey3D21(uint x, uint y, uint z)
        {
#if BITCORE_DEBUG
            ValidateComponent(x, nameof(x));
            ValidateComponent(y, nameof(y));
            ValidateComponent(z, nameof(z));
#endif
            _zKey = ZKey3D21Util.Encode(x, y, z);
        }

        public ZKey3D21((uint x, uint y, uint z) tuple) : this(tuple.x, tuple.y, tuple.z) { }

        /// <summary>
        /// Initializes a new instance from x, y, z components (int).
        /// </summary>
        /// <param name="x">X component (0-2,097,151).</param>
        /// <param name="y">Y component (0-2,097,151).</param>
        /// <param name="z">Z component (0-2,097,151).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in debug mode if any component is out of range.</exception>
        public ZKey3D21(int x, int y, int z)
        {
#if BITCORE_DEBUG
            ValidateComponent(x, nameof(x));
            ValidateComponent(y, nameof(y));
            ValidateComponent(z, nameof(z));
#endif
            _zKey = ZKey3D21Util.Encode((uint)x, (uint)y, (uint)z);
        }

        public ZKey3D21((int x, int y, int z) tuple) : this(tuple.x, tuple.y, tuple.z) { }

#if BITCORE_DEBUG
        private static void ValidateComponent(int value, string paramName)
        {
            if (value < 0 || value > 2097151)
                throw new ArgumentOutOfRangeException(paramName, $"Component must be between 0-2,097,151, was {value}.");
        }

        private static void ValidateComponent(uint value, string paramName)
        {
            if (value > 2097151)
                throw new ArgumentOutOfRangeException(paramName, $"Component must be between 0-2,097,151, was {value}.");
        }
#endif

        /// <summary>
        /// Gets the raw Morton key value.
        /// </summary>
        public ulong Key => _zKey;

        /// <summary>
        /// Gets the decoded x component.
        /// </summary>
        public uint X => ZKey3D21Util.DecodePart(_zKey);

        /// <summary>
        /// Gets the decoded y component.
        /// </summary>
        public uint Y => ZKey3D21Util.DecodePart(_zKey >> 1);

        /// <summary>
        /// Gets the decoded z component.
        /// </summary>
        public uint Z => ZKey3D21Util.DecodePart(_zKey >> 2);

        /// <summary>
        /// Gets all decoded components as a tuple.
        /// </summary>
        public (uint x, uint y, uint z) Components => ZKey3D21Util.Decode(_zKey);

        // Increment methods
        public ZKey3D21 IncrementX() => new ZKey3D21(ZKey3D21Util.IncX(_zKey));
        public ZKey3D21 IncrementY() => new ZKey3D21(ZKey3D21Util.IncY(_zKey));
        public ZKey3D21 IncrementZ() => new ZKey3D21(ZKey3D21Util.IncZ(_zKey));
        public ZKey3D21 IncrementXY() => IncrementX().IncrementY();
        public ZKey3D21 IncrementXZ() => IncrementX().IncrementZ();
        public ZKey3D21 IncrementYZ() => IncrementY().IncrementZ();
        public ZKey3D21 IncrementXYZ() => IncrementX().IncrementY().IncrementZ();

        // Decrement methods
        public ZKey3D21 DecrementX() => new ZKey3D21(ZKey3D21Util.DecX(_zKey));
        public ZKey3D21 DecrementY() => new ZKey3D21(ZKey3D21Util.DecY(_zKey));
        public ZKey3D21 DecrementZ() => new ZKey3D21(ZKey3D21Util.DecZ(_zKey));
        public ZKey3D21 DecrementXY() => DecrementX().DecrementY();
        public ZKey3D21 DecrementXZ() => DecrementX().DecrementZ();
        public ZKey3D21 DecrementYZ() => DecrementY().DecrementZ();
        public ZKey3D21 DecrementXYZ() => DecrementX().DecrementY().DecrementZ();

        /// <summary>
        /// Applies modulo operation to the raw key.
        /// </summary>
        public ZKey3D21 Modulo(ulong modulo) => new ZKey3D21(_zKey % modulo);

        /// <summary>
        /// Applies bitwise AND mask to the raw key.
        /// </summary>
        public ZKey3D21 Mask(ulong mask) => new ZKey3D21(_zKey & mask);

        // Operator overloads
        public static ZKey3D21 operator +(ZKey3D21 a, ZKey3D21 b)
        {
            ulong sumX = (a._zKey | ZKey3D21Util.YZ_MASK) + (b._zKey & ZKey3D21Util.X_MASK);
            ulong sumY = (a._zKey | ZKey3D21Util.XZ_MASK) + (b._zKey & ZKey3D21Util.Y_MASK);
            ulong sumZ = (a._zKey | ZKey3D21Util.XY_MASK) + (b._zKey & ZKey3D21Util.Z_MASK);

            return new ZKey3D21((sumX & ZKey3D21Util.X_MASK) | (sumY & ZKey3D21Util.Y_MASK) | (sumZ & ZKey3D21Util.Z_MASK));
        }

        public static ZKey3D21 operator -(ZKey3D21 a, ZKey3D21 b)
        {
            ulong diffX = (a._zKey & ZKey3D21Util.X_MASK) - (b._zKey & ZKey3D21Util.X_MASK);
            ulong diffY = (a._zKey & ZKey3D21Util.Y_MASK) - (b._zKey & ZKey3D21Util.Y_MASK);
            ulong diffZ = (a._zKey & ZKey3D21Util.Z_MASK) - (b._zKey & ZKey3D21Util.Z_MASK);

            return new ZKey3D21((diffX & ZKey3D21Util.X_MASK) | (diffY & ZKey3D21Util.Y_MASK) | (diffZ & ZKey3D21Util.Z_MASK));
        }

        public static ZKey3D21 operator *(ZKey3D21 a, ZKey3D21 b)
        {
            var (ax, ay, az) = a.Components;
            var (bx, by, bz) = b.Components;

            return new ZKey3D21(ax * bx, ay * by, az * bz);
        }

        public static ZKey3D21 operator *(ZKey3D21 a, uint scalar)
        {
            var (x, y, z) = a.Components;

            return new ZKey3D21(x * scalar, y * scalar, z * scalar);
        }

        // Equality implementations
        public bool Equals(ZKey3D21 other) => _zKey == other._zKey;
        public bool Equals(ulong other) => _zKey == other;
        public bool Equals((uint x, uint y, uint z) other) => _zKey == ZKey3D21Util.Encode(other.x, other.y, other.z);
        public override bool Equals(object obj) => obj is ZKey3D21 other && Equals(other);
        public override int GetHashCode() => _zKey.GetHashCode();

        // Explicit conversions
        public static explicit operator ulong(ZKey3D21 key) => key._zKey;
        public static explicit operator ZKey3D21(ulong key) => new ZKey3D21(key);
        public static explicit operator ZKey3D21(long key) => new ZKey3D21(key);
        public static explicit operator ZKey3D21((uint, uint, uint) tuple) => new ZKey3D21(tuple);
        public static explicit operator (uint, uint, uint)(ZKey3D21 key) => key.Components;

        /// <summary>
        /// Creates a copy of this Morton key.
        /// </summary>
        public ZKey3D21 Copy() => new ZKey3D21(_zKey);
    }
}