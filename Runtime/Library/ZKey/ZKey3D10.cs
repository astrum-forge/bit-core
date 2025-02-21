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
    public readonly struct ZKey3D10 : IEquatable<ZKey3D10>, IEquatable<uint>, IEquatable<(uint x, uint y, uint z)>
    {
        private readonly uint _zKey;

        /// <summary>
        /// Initializes a new instance with a precomputed Morton key.
        /// </summary>
        /// <param name="zKey">The Morton key as a 32-bit unsigned integer.</param>
        public ZKey3D10(uint zKey) => _zKey = zKey;

        /// <summary>
        /// Initializes a new instance from a positive integer key.
        /// </summary>
        /// <param name="zKey">The Morton key as a positive integer.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in debug mode if zKey is negative.</exception>
        public ZKey3D10(int zKey)
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
        public ZKey3D10(uint x, uint y, uint z)
        {
#if BITCORE_DEBUG
            ValidateComponent(x, nameof(x));
            ValidateComponent(y, nameof(y));
            ValidateComponent(z, nameof(z));
#endif
            _zKey = ZKey3D10Util.Encode(x, y, z);
        }

        // Constructor from uint tuple
        public ZKey3D10((uint x, uint y, uint z) tuple) : this(tuple.x, tuple.y, tuple.z) { }

        /// <summary>
        /// Initializes a new instance from x, y, z components (int).
        /// </summary>
        /// <param name="x">X component (0-1023).</param>
        /// <param name="y">Y component (0-1023).</param>
        /// <param name="z">Z component (0-1023).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in debug mode if any component is out of range.</exception>
        public ZKey3D10(int x, int y, int z)
        {
#if BITCORE_DEBUG
            ValidateComponent(x, nameof(x));
            ValidateComponent(y, nameof(y));
            ValidateComponent(z, nameof(z));
#endif
            _zKey = ZKey3D10Util.Encode((uint)x, (uint)y, (uint)z);
        }

        // Constructor from int tuple
        public ZKey3D10((int x, int y, int z) tuple) : this(tuple.x, tuple.y, tuple.z) { }

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
        public uint X => ZKey3D10Util.DecodePart(_zKey);

        /// <summary>
        /// Gets the decoded y component.
        /// </summary>
        public uint Y => ZKey3D10Util.DecodePart(_zKey >> 1);

        /// <summary>
        /// Gets the decoded z component.
        /// </summary>
        public uint Z => ZKey3D10Util.DecodePart(_zKey >> 2);

        /// <summary>
        /// Gets all decoded components as a tuple.
        /// </summary>
        public (uint x, uint y, uint z) Components => ZKey3D10Util.Decode(_zKey);

        // Increment methods
        public ZKey3D10 IncrementX() => new ZKey3D10(ZKey3D10Util.IncX(_zKey));
        public ZKey3D10 IncrementY() => new ZKey3D10(ZKey3D10Util.IncY(_zKey));
        public ZKey3D10 IncrementZ() => new ZKey3D10(ZKey3D10Util.IncZ(_zKey));
        public ZKey3D10 IncrementXY() => IncrementX().IncrementY();
        public ZKey3D10 IncrementXZ() => IncrementX().IncrementZ();
        public ZKey3D10 IncrementYZ() => IncrementY().IncrementZ();
        public ZKey3D10 IncrementXYZ() => IncrementX().IncrementY().IncrementZ();

        // Decrement methods
        public ZKey3D10 DecrementX() => new ZKey3D10(ZKey3D10Util.DecX(_zKey));
        public ZKey3D10 DecrementY() => new ZKey3D10(ZKey3D10Util.DecY(_zKey));
        public ZKey3D10 DecrementZ() => new ZKey3D10(ZKey3D10Util.DecZ(_zKey));
        public ZKey3D10 DecrementXY() => DecrementX().DecrementY();
        public ZKey3D10 DecrementXZ() => DecrementX().DecrementZ();
        public ZKey3D10 DecrementYZ() => DecrementY().DecrementZ();
        public ZKey3D10 DecrementXYZ() => DecrementX().DecrementY().DecrementZ();

        /// <summary>
        /// Applies modulo operation to the raw key.
        /// </summary>
        public ZKey3D10 Modulo(uint modulo) => new ZKey3D10(_zKey % modulo);

        /// <summary>
        /// Applies bitwise AND mask to the raw key.
        /// </summary>
        public ZKey3D10 Mask(uint mask) => new ZKey3D10(_zKey & mask);

        // Operator overloads
        public static ZKey3D10 operator +(ZKey3D10 a, ZKey3D10 b)
        {
            uint sumX = (a._zKey | ZKey3D10Util.YZ_MASK) + (b._zKey & ZKey3D10Util.X_MASK);
            uint sumY = (a._zKey | ZKey3D10Util.XZ_MASK) + (b._zKey & ZKey3D10Util.Y_MASK);
            uint sumZ = (a._zKey | ZKey3D10Util.XY_MASK) + (b._zKey & ZKey3D10Util.Z_MASK);

            return new ZKey3D10((sumX & ZKey3D10Util.X_MASK) | (sumY & ZKey3D10Util.Y_MASK) | (sumZ & ZKey3D10Util.Z_MASK));
        }

        public static ZKey3D10 operator -(ZKey3D10 a, ZKey3D10 b)
        {
            uint diffX = (a._zKey & ZKey3D10Util.X_MASK) - (b._zKey & ZKey3D10Util.X_MASK);
            uint diffY = (a._zKey & ZKey3D10Util.Y_MASK) - (b._zKey & ZKey3D10Util.Y_MASK);
            uint diffZ = (a._zKey & ZKey3D10Util.Z_MASK) - (b._zKey & ZKey3D10Util.Z_MASK);

            return new ZKey3D10((diffX & ZKey3D10Util.X_MASK) | (diffY & ZKey3D10Util.Y_MASK) | (diffZ & ZKey3D10Util.Z_MASK));
        }

        public static ZKey3D10 operator *(ZKey3D10 a, ZKey3D10 b)
        {
            var (ax, ay, az) = a.Components;
            var (bx, by, bz) = b.Components;

            return new ZKey3D10(ax * bx, ay * by, az * bz);
        }

        public static ZKey3D10 operator *(ZKey3D10 a, uint scalar)
        {
            var (x, y, z) = a.Components;

            return new ZKey3D10(x * scalar, y * scalar, z * scalar);
        }

        // Equality implementations
        public bool Equals(ZKey3D10 other) => _zKey == other._zKey;
        public bool Equals(uint other) => _zKey == other;
        public bool Equals((uint x, uint y, uint z) other) => _zKey == ZKey3D10Util.Encode(other.x, other.y, other.z);
        public override bool Equals(object obj) => obj is ZKey3D10 other && Equals(other);
        public override int GetHashCode() => _zKey.GetHashCode();

        // Explicit conversions
        public static explicit operator uint(ZKey3D10 key) => key._zKey;
        public static explicit operator ZKey3D10(uint key) => new ZKey3D10(key);
        public static explicit operator ZKey3D10(int key) => new ZKey3D10(key);
        public static explicit operator ZKey3D10((uint, uint, uint) tuple) => new ZKey3D10(tuple);
        public static explicit operator (uint, uint, uint)(ZKey3D10 key) => key.Components;

        /// <summary>
        /// Creates a copy of this Morton key.
        /// </summary>
        public ZKey3D10 Copy() => new ZKey3D10(_zKey);
    }
}