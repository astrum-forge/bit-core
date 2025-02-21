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
    /// Represents a 3D Morton key (Z-order curve) using a 32-bit unsigned integer,
    /// with 10 bits per component (x, y, z), allowing values from 0 to 1023.
    /// Provides efficient spatial hashing for data structures with improved cache locality.
    /// </summary>
    [Serializable] // Unity serialization support
    public readonly struct ZKey10 : IEquatable<ZKey10>, IEquatable<uint>, IEquatable<(uint x, uint y, uint z)>
    {
        private readonly uint _zKey;

        /// <summary>
        /// Initializes a new instance with a precomputed Morton key.
        /// </summary>
        /// <param name="zKey">The Morton key as a 32-bit unsigned integer.</param>
        public ZKey10(uint zKey) => _zKey = zKey;

        /// <summary>
        /// Initializes a new instance from a positive integer key.
        /// </summary>
        /// <param name="zKey">The Morton key as a positive integer.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in debug mode if zKey is negative.</exception>
        public ZKey10(int zKey)
        {
#if BITCORE_DEBUG
            if (zKey < 0) throw new ArgumentOutOfRangeException(nameof(zKey), "Morton key must be positive.");
#endif
            _zKey = (uint)zKey;
        }

        /// <summary>
        /// Initializes a new instance from x, y, z components (uint).
        /// </summary>
        /// <param name="x">X component (0-1023).</param>
        /// <param name="y">Y component (0-1023).</param>
        /// <param name="z">Z component (0-1023).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in debug mode if any component exceeds 1023.</exception>
        public ZKey10(uint x, uint y, uint z)
        {
#if BITCORE_DEBUG
            ValidateComponent(x, nameof(x));
            ValidateComponent(y, nameof(y));
            ValidateComponent(z, nameof(z));
#endif
            _zKey = ZKey10Util.Encode(x, y, z);
        }

        // Constructor from uint tuple
        public ZKey10((uint x, uint y, uint z) tuple) : this(tuple.x, tuple.y, tuple.z) { }

        /// <summary>
        /// Initializes a new instance from x, y, z components (int).
        /// </summary>
        /// <param name="x">X component (0-1023).</param>
        /// <param name="y">Y component (0-1023).</param>
        /// <param name="z">Z component (0-1023).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in debug mode if any component is out of range.</exception>
        public ZKey10(int x, int y, int z)
        {
#if BITCORE_DEBUG
            ValidateComponent(x, nameof(x));
            ValidateComponent(y, nameof(y));
            ValidateComponent(z, nameof(z));
#endif
            _zKey = ZKey10Util.Encode((uint)x, (uint)y, (uint)z);
        }

        // Constructor from int tuple
        public ZKey10((int x, int y, int z) tuple) : this(tuple.x, tuple.y, tuple.z) { }

#if BITCORE_DEBUG
        private static void ValidateComponent(int value, string paramName)
        {
            if (value < 0 || value > 1023)
                throw new ArgumentOutOfRangeException(paramName, $"Component must be between 0-1023, was {value}.");
        }

        private static void ValidateComponent(uint value, string paramName)
        {
            if (value > 1023)
                throw new ArgumentOutOfRangeException(paramName, $"Component must be between 0-1023, was {value}.");
        }
#endif

        /// <summary>
        /// Gets the raw Morton key value.
        /// </summary>
        public uint Key => _zKey;

        /// <summary>
        /// Gets the decoded x component.
        /// </summary>
        public uint X => ZKey10Util.DecodePart(_zKey);

        /// <summary>
        /// Gets the decoded y component.
        /// </summary>
        public uint Y => ZKey10Util.DecodePart(_zKey >> 1);

        /// <summary>
        /// Gets the decoded z component.
        /// </summary>
        public uint Z => ZKey10Util.DecodePart(_zKey >> 2);

        /// <summary>
        /// Gets all decoded components as a tuple.
        /// </summary>
        public (uint x, uint y, uint z) Components => ZKey10Util.Decode(_zKey);

        // Increment methods
        public ZKey10 IncrementX() => new ZKey10(ZKey10Util.IncX(_zKey));
        public ZKey10 IncrementY() => new ZKey10(ZKey10Util.IncY(_zKey));
        public ZKey10 IncrementZ() => new ZKey10(ZKey10Util.IncZ(_zKey));
        public ZKey10 IncrementXY() => IncrementX().IncrementY();
        public ZKey10 IncrementXZ() => IncrementX().IncrementZ();
        public ZKey10 IncrementYZ() => IncrementY().IncrementZ();
        public ZKey10 IncrementXYZ() => IncrementX().IncrementY().IncrementZ();

        // Decrement methods
        public ZKey10 DecrementX() => new ZKey10(ZKey10Util.DecX(_zKey));
        public ZKey10 DecrementY() => new ZKey10(ZKey10Util.DecY(_zKey));
        public ZKey10 DecrementZ() => new ZKey10(ZKey10Util.DecZ(_zKey));
        public ZKey10 DecrementXY() => DecrementX().DecrementY();
        public ZKey10 DecrementXZ() => DecrementX().DecrementZ();
        public ZKey10 DecrementYZ() => DecrementY().DecrementZ();
        public ZKey10 DecrementXYZ() => DecrementX().DecrementY().DecrementZ();

        /// <summary>
        /// Applies modulo operation to the raw key.
        /// </summary>
        public ZKey10 Modulo(uint modulo) => new ZKey10(_zKey % modulo);

        /// <summary>
        /// Applies bitwise AND mask to the raw key.
        /// </summary>
        public ZKey10 Mask(uint mask) => new ZKey10(_zKey & mask);

        // Operator overloads
        public static ZKey10 operator +(ZKey10 a, ZKey10 b)
        {
            uint sumX = (a._zKey | ZKey10Util.YZ_MASK) + (b._zKey & ZKey10Util.X_MASK);
            uint sumY = (a._zKey | ZKey10Util.XZ_MASK) + (b._zKey & ZKey10Util.Y_MASK);
            uint sumZ = (a._zKey | ZKey10Util.XY_MASK) + (b._zKey & ZKey10Util.Z_MASK);

            return new ZKey10((sumX & ZKey10Util.X_MASK) | (sumY & ZKey10Util.Y_MASK) | (sumZ & ZKey10Util.Z_MASK));
        }

        public static ZKey10 operator -(ZKey10 a, ZKey10 b)
        {
            uint diffX = (a._zKey & ZKey10Util.X_MASK) - (b._zKey & ZKey10Util.X_MASK);
            uint diffY = (a._zKey & ZKey10Util.Y_MASK) - (b._zKey & ZKey10Util.Y_MASK);
            uint diffZ = (a._zKey & ZKey10Util.Z_MASK) - (b._zKey & ZKey10Util.Z_MASK);

            return new ZKey10((diffX & ZKey10Util.X_MASK) | (diffY & ZKey10Util.Y_MASK) | (diffZ & ZKey10Util.Z_MASK));
        }

        public static ZKey10 operator *(ZKey10 a, ZKey10 b)
        {
            var (ax, ay, az) = a.Components;
            var (bx, by, bz) = b.Components;

            return new ZKey10(ax * bx, ay * by, az * bz);
        }

        public static ZKey10 operator *(ZKey10 a, uint scalar)
        {
            var (x, y, z) = a.Components;

            return new ZKey10(x * scalar, y * scalar, z * scalar);
        }

        // Equality implementations
        public bool Equals(ZKey10 other) => _zKey == other._zKey;
        public bool Equals(uint other) => _zKey == other;
        public bool Equals((uint x, uint y, uint z) other) => _zKey == ZKey10Util.Encode(other.x, other.y, other.z);
        public override bool Equals(object obj) => obj is ZKey10 other && Equals(other);
        public override int GetHashCode() => _zKey.GetHashCode();

        // Explicit conversions
        public static explicit operator uint(ZKey10 key) => key._zKey;
        public static explicit operator ZKey10(uint key) => new ZKey10(key);
        public static explicit operator ZKey10(int key) => new ZKey10(key);
        public static explicit operator ZKey10((uint, uint, uint) tuple) => new ZKey10(tuple);
        public static explicit operator (uint, uint, uint)(ZKey10 key) => key.Components;

        /// <summary>
        /// Creates a copy of this Morton key.
        /// </summary>
        public ZKey10 Copy() => new ZKey10(_zKey);
    }
}