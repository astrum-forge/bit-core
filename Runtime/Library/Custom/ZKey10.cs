#if UNITY_EDITOR
#define BITCORE_DEBUG
#endif

#if NET_4_6 && !BITCORE_DISABLE_INLINE
#define BITCORE_METHOD_INLINE
#endif

using System;

namespace BitCore
{
    internal class ZKey10Util
    {
        public static readonly uint X_MASK = 0x9249249;
        public static readonly uint Y_MASK = 0x12492492;
        public static readonly uint Z_MASK = 0x24924924;

        public static readonly uint XY_MASK = X_MASK | Y_MASK;
        public static readonly uint XZ_MASK = X_MASK | Z_MASK;
        public static readonly uint YZ_MASK = Y_MASK | Z_MASK;
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static uint Encode(uint x, uint y, uint z)
        {
            var cx = EncodePart(x);
            var cy = EncodePart(y);
            var cz = EncodePart(z);

            return (cz << 2) + (cy << 1) + cx;
        }

#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static (uint x, uint y, uint z) Decode(uint zKey)
        {
            uint cx = DecodePart(zKey >> 0);
            uint cy = DecodePart(zKey >> 1);
            uint cz = DecodePart(zKey >> 2);

            return (x: cx, y: cy, z: cz);
        }

#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static uint EncodePart(uint n)
        {
            uint n0 = n & 0x000003ff;

            uint n1 = (n0 ^ (n0 << 16)) & 0xff0000ff;
            uint n2 = (n1 ^ (n1 << 8)) & 0x0300f00f;
            uint n3 = (n2 ^ (n2 << 4)) & 0x030c30c3;
            uint n4 = (n3 ^ (n3 << 2)) & 0x09249249;

            return n4;
        }
#if BITCORE_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static uint DecodePart(uint n)
        {
            uint n0 = n & 0x09249249;

            uint n1 = (n0 ^ (n0 >> 2)) & 0x030c30c3;
            uint n2 = (n1 ^ (n1 >> 4)) & 0x0300f00f;
            uint n3 = (n2 ^ (n2 >> 8)) & 0xff0000ff;
            uint n4 = (n3 ^ (n3 >> 16)) & 0x000003ff;

            return n4;
        }

        /**
        * Given a 3 component Meton Key, increment the X component by 1
        * unit and return the value. This is much more efficient than
        * encoding/decoding for LUT operations.
        */
#if BITSTACK_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static uint IncX(uint zKey)
        {
            uint sum = (zKey | YZ_MASK) + 1;
            return (sum & X_MASK) | (zKey & YZ_MASK);
        }

        /**
         * Given a 3 component Meton Key, increment the Y component by 1
         * unit and return the value. This is much more efficient than
         * encoding/decoding for LUT operations.
         */
#if BITSTACK_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static uint IncY(uint zKey)
        {
            uint sum = (zKey | XZ_MASK) + 2;
            return (sum & Y_MASK) | (zKey & XZ_MASK);
        }

        /**
         * Given a 3 component Meton Key, increment the Z component by 1
         * unit and return the value. This is much more efficient than
         * encoding/decoding for LUT operations.
         */
#if BITSTACK_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static uint IncZ(uint zKey)
        {
            uint sum = (zKey | XY_MASK) + 1;
            return (sum & Z_MASK) | (zKey & XY_MASK);
        }

        /**
         * Given a 3 component Meton Key, decrement the X component by 1
         * unit and return the value. This is much more efficient than
         * encoding/decoding for LUT operations.
         */
#if BITSTACK_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static uint DecX(uint zKey)
        {
            uint diff = (zKey & X_MASK) - 1;
            return (diff & X_MASK) | (zKey & YZ_MASK);
        }

        /**
         * Given a 3 component Meton Key, decrement the Y component by 1
         * unit and return the value. This is much more efficient than
         * encoding/decoding for LUT operations.
         */
#if BITSTACK_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static uint DecY(uint zKey)
        {
            uint diff = (zKey & Y_MASK) - 2;
            return (diff & Y_MASK) | (zKey & XZ_MASK);
        }

        /**
         * Given a 3 component Meton Key, decrement the Z component by 1
         * unit and return the value. This is much more efficient than
         * encoding/decoding for LUT operations.
         */
#if BITSTACK_METHOD_INLINE
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static uint DecZ(uint zKey)
        {
            uint diff = (zKey & Z_MASK) - 1;
            return (diff & Z_MASK) | (zKey & XY_MASK);
        }
    }

    /**
     * Simplifies the usage of Morton Key types. This struct
     * only holds a single uint32 value which represents the morton key.
     * Each component holds 10 bits of information so 2^10 = 1024 maximum values per component
     * This key contains 3 components, x, y and z
     *
     * Morton Keys can be useful for spatial hashing, or data structures with good
     * cache locality.
     *
     * NOTICE ABOUT PERFORMANCE
     * 
     * UNITY_EDITOR or DEBUG flags ensure that common errors are caught. These
     * flags are removed in production mode so don't rely on try/catch methods.
     * If performing benchmarks, ensure that the flags are not taken into account.
     * The flags ensure that common problems are caught in code and taken care of.
     */
    public struct ZKey10 : IEquatable<ZKey10>, IEquatable<uint>, IEquatable<(uint x, uint y, uint z)>
    {
        private readonly uint zKey;

        public ZKey10(uint zKey)
        {
            this.zKey = zKey;
        }

        public ZKey10(int zKey)
        {
#if BITCORE_DEBUG
            if (zKey < 0)
            {
                BitDebug.Throw("ZKey10(int) - morton key must be positive");
            }
#endif

            this.zKey = (uint)zKey;
        }

        public ZKey10(uint x, uint y, uint z)
        {
#if BITCORE_DEBUG
            if (x > 1024 || x < 0)
            {
                BitDebug.Throw("ZKey10(uint, uint, uint) - morton key x component must be between 0-1023 (10 bits), was " + x);
            }

            if (y > 1024 || y < 0)
            {
                BitDebug.Throw("ZKey10(uint, uint, uint) - morton key y component must be between 0-1023 (10 bits), was " + y);
            }

            if (z > 1024 || z < 0)
            {
                BitDebug.Throw("ZKey10(uint, uint, uint) - morton key z component must be between 0-1023 (10 bits), was " + z);
            }
#endif
            zKey = ZKey10Util.Encode(x, y, z);
        }

        public ZKey10(int x, int y, int z)
        {
#if BITCORE_DEBUG
            if (x > 1024 || x < 0)
            {
                BitDebug.Throw("ZKey10(int, int, int) - morton key x component must be between 0-1023 (10 bits), was " + x);
            }

            if (y > 1024 || y < 0)
            {
                BitDebug.Throw("ZKey10(int, int, int) - morton key y component must be between 0-1023 (10 bits), was " + y);
            }

            if (z > 1024 || z < 0)
            {
                BitDebug.Throw("ZKey10(int, int, int) - morton key z component must be between 0-1023 (10 bits), was " + z);
            }
#endif
            zKey = ZKey10Util.Encode((uint)x, (uint)y, (uint)z);
        }

        public uint key
        {
            get
            {
                return zKey;
            }
        }

        public uint x
        {
            get
            {
                return ZKey10Util.DecodePart(zKey);
            }
        }

        public uint y
        {
            get
            {
                return ZKey10Util.DecodePart(zKey >> 1);
            }
        }

        public uint z
        {
            get
            {
                return ZKey10Util.DecodePart(zKey >> 2);
            }
        }

        public (uint x, uint y, uint z) RawValue
        {
            get
            {
                return ZKey10Util.Decode(key);
            }
        }

        public ZKey10 IncX()
        {
            return new ZKey10(ZKey10Util.IncX(zKey));
        }

        public ZKey10 IncY()
        {
            return new ZKey10(ZKey10Util.IncY(zKey));
        }

        public ZKey10 IncZ()
        {
            return new ZKey10(ZKey10Util.IncZ(zKey));
        }

        public ZKey10 IncXY()
        {
            uint key = zKey;

            key = ZKey10Util.IncX(key);
            key = ZKey10Util.IncY(key);

            return new ZKey10(key);
        }

        public ZKey10 IncXZ()
        {
            uint key = zKey;

            key = ZKey10Util.IncX(key);
            key = ZKey10Util.IncZ(key);

            return new ZKey10(key);
        }

        public ZKey10 IncYZ()
        {
            uint key = zKey;

            key = ZKey10Util.IncY(key);
            key = ZKey10Util.IncZ(key);

            return new ZKey10(key);
        }

        public ZKey10 IncXYZ()
        {
            uint key = zKey;

            key = ZKey10Util.IncX(key);
            key = ZKey10Util.IncY(key);
            key = ZKey10Util.IncZ(key);

            return new ZKey10(key);
        }

        public ZKey10 DecX()
        {
            return new ZKey10(ZKey10Util.DecX(zKey));
        }

        public ZKey10 DecY()
        {
            return new ZKey10(ZKey10Util.DecY(zKey));
        }

        public ZKey10 DecZ()
        {
            return new ZKey10(ZKey10Util.DecZ(zKey));
        }

        public ZKey10 DecXY()
        {
            uint key = zKey;

            key = ZKey10Util.DecX(key);
            key = ZKey10Util.DecY(key);

            return new ZKey10(key);
        }

        public ZKey10 DecXZ()
        {
            uint key = zKey;

            key = ZKey10Util.DecX(key);
            key = ZKey10Util.DecZ(key);

            return new ZKey10(key);
        }

        public ZKey10 DecYZ()
        {
            uint key = zKey;

            key = ZKey10Util.DecY(key);
            key = ZKey10Util.DecZ(key);

            return new ZKey10(key);
        }

        public ZKey10 DecXYZ()
        {
            uint key = zKey;

            key = ZKey10Util.DecX(key);
            key = ZKey10Util.DecY(key);
            key = ZKey10Util.DecZ(key);

            return new ZKey10(key);
        }

        public ZKey10 Mod(uint modulo)
        {
            return new ZKey10(zKey % modulo);
        }

        public ZKey10 Mask(uint mask)
        {
            return new ZKey10(zKey & mask);
        }

        /**
         * Overrides - ZKey10(1,2,3) + ZKey10(4,5,6) = ZKey10(5,7,9)
         */
        public static ZKey10 operator +(ZKey10 x, ZKey10 y)
        {
            uint sum_x = (x.zKey | ZKey10Util.YZ_MASK) + (y.zKey & ZKey10Util.X_MASK);
            uint sum_y = (x.zKey | ZKey10Util.XZ_MASK) + (y.zKey & ZKey10Util.Y_MASK);
            uint sum_z = (x.zKey | ZKey10Util.XY_MASK) + (y.zKey & ZKey10Util.Z_MASK);

            return new ZKey10((sum_x & ZKey10Util.X_MASK) | (sum_y & ZKey10Util.Y_MASK) | (sum_z & ZKey10Util.Z_MASK));
        }

        /**
         * Overrides - ZKey10(1,2,3) * ZKey10(4,5,6) = ZKey10(4,10,18)
         */
        public static ZKey10 operator *(ZKey10 x, ZKey10 y)
        {
            // TO-DO, these needs to be replaced with a more efficient method
            var vx = x.RawValue;
            var vy = y.RawValue;

            return new ZKey10((uint)(vx.x * vy.x), (uint)(vx.y * vy.y), (uint)(vx.z * vy.z));
        }

        /**
         * Overrides - ZKey10(1,2,3) * 4 = ZKey10(4,8,12)
         */
        public static ZKey10 operator *(ZKey10 x, uint val)
        {
            // TO-DO, these needs to be replaced with a more efficient method
            var vx = x.RawValue;

            return new ZKey10((uint)(vx.x * val), (uint)(vx.y * val), (uint)(vx.z * val));
        }

        /**
         * Overrides - ZKey10(4,5,6) - ZKey10(1,2,3) = ZKey10(3,3,3)
         */
        public static ZKey10 operator -(ZKey10 x, ZKey10 y)
        {
            uint sum_x = (x.zKey & ZKey10Util.X_MASK) - (y.zKey & ZKey10Util.X_MASK);
            uint sum_y = (x.zKey & ZKey10Util.Y_MASK) - (y.zKey & ZKey10Util.Y_MASK);
            uint sum_z = (x.zKey & ZKey10Util.Z_MASK) - (y.zKey & ZKey10Util.Z_MASK);

            return new ZKey10((sum_x & ZKey10Util.X_MASK) | (sum_y & ZKey10Util.Y_MASK) | (sum_z & ZKey10Util.Z_MASK));
        }

        public bool Equals(ZKey10 other)
        {
            return zKey == other.zKey;
        }

        public bool Equals(uint other)
        {
            return zKey == other;
        }

        public bool Equals((uint x, uint y, uint z) other)
        {
            return zKey == ZKey10Util.Encode(other.x, other.y, other.z);
        }

        public ZKey10 Copy()
        {
            return new ZKey10(key);
        }
    }
}