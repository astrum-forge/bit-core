namespace BitCore
{
    internal static class ZKey10Util
    {
        public static readonly uint X_MASK = 0x09249249; // Example mask for x component
        public static readonly uint Y_MASK = 0x12492492; // Example mask for y component
        public static readonly uint Z_MASK = 0x24924924; // Example mask for z component

        public static readonly uint XY_MASK = X_MASK | Y_MASK;
        public static readonly uint XZ_MASK = X_MASK | Z_MASK;
        public static readonly uint YZ_MASK = Y_MASK | Z_MASK;

        public static uint Encode(uint x, uint y, uint z)
        {
            uint cx = EncodePart(x);
            uint cy = EncodePart(y);
            uint cz = EncodePart(z);
            return (cz << 2) | (cy << 1) | cx;
        }

        public static (uint x, uint y, uint z) Decode(uint zKey)
        {
            uint cx = DecodePart(zKey >> 0);
            uint cy = DecodePart(zKey >> 1);
            uint cz = DecodePart(zKey >> 2);
            return (cx, cy, cz);
        }

        public static uint EncodePart(uint n)
        {
            uint n0 = n & 0x000003ff;
            uint n1 = (n0 ^ (n0 << 16)) & 0xff0000ff;
            uint n2 = (n1 ^ (n1 << 8)) & 0x0300f00f;
            uint n3 = (n2 ^ (n2 << 4)) & 0x030c30c3;
            uint n4 = (n3 ^ (n3 << 2)) & 0x09249249;
            return n4;
        }

        public static uint DecodePart(uint n)
        {
            uint n0 = n & 0x09249249;
            uint n1 = (n0 ^ (n0 >> 2)) & 0x030c30c3;
            uint n2 = (n1 ^ (n1 >> 4)) & 0x0300f00f;
            uint n3 = (n2 ^ (n2 >> 8)) & 0xff0000ff;
            uint n4 = (n3 ^ (n3 >> 16)) & 0x000003ff;
            return n4;
        }

        public static uint IncX(uint zKey)
        {
            uint sum = (zKey | YZ_MASK) + 1;
            return (sum & X_MASK) | (zKey & YZ_MASK);
        }

        public static uint IncY(uint zKey)
        {
            uint sum = (zKey | XZ_MASK) + 2;
            return (sum & Y_MASK) | (zKey & XZ_MASK);
        }

        public static uint IncZ(uint zKey)
        {
            uint sum = (zKey | XY_MASK) + 1;
            return (sum & Z_MASK) | (zKey & XY_MASK);
        }

        public static uint DecX(uint zKey)
        {
            uint diff = (zKey & X_MASK) - 1;
            return (diff & X_MASK) | (zKey & YZ_MASK);
        }

        public static uint DecY(uint zKey)
        {
            uint diff = (zKey & Y_MASK) - 2;
            return (diff & Y_MASK) | (zKey & XZ_MASK);
        }

        public static uint DecZ(uint zKey)
        {
            uint diff = (zKey & Z_MASK) - 1;
            return (diff & Z_MASK) | (zKey & XY_MASK);
        }
    }
}