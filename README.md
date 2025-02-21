<h3 align="center">
  <img src="Assets/icon.png?raw=true" alt="Astrum Forge Studios Logo" width="400">
</h3>

[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

# BitCore

**BitCore** is a lightweight, high-performance C# library for bit manipulation and tuple conversions, optimized for real-time applications like Unity3D. It offers extension methods for packing and unpacking numeric types (`byte`, `sbyte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`) and utilities for bit operations and binary string parsing.

## Features

- Bit-level operations (e.g., `SetBitAt`, `PopCount`, `BitString`).
- Tuple packing/unpacking (e.g., `PackToInt`, `UnpackToBytes`) in big-endian order.
- Aggressive inlining for .NET 4.6+ builds.
- Unity3D-ready with no external dependencies.

## Installation

### Unity Package Manager (UPM)
1. Open Unity, go to `Window > Package Manager`.
2. Click `+` > `Add package from git URL`.
3. Enter: `https://github.com/astrum-forge/bit-core.git`.
4. Click `Add`.

## Usage

### Bit Manipulation
```csharp
using BitCore;

int value = 0b00001010; // 10
value = value.SetBitAt(2); // 0b00001110 (14)
int bitCount = value.PopCount(); // 3
```

### Tuple Conversions
```csharp
using BitCore;

(byte, byte, byte, byte) bytes = (0x12, 0x34, 0x56, 0x78);
int packed = bytes.PackToInt(); // 0x12345678
var unpacked = packed.UnpackToBytes(); // (0x12, 0x34, 0x56, 0x78)
```

## License
Licensed under the Apache 2.0 License. See the [LICENSE](LICENSE) file for details.

## Contributing
Fork, branch, and submit a Pull Request with your changes. Keep it fast, simple, and well-documented.