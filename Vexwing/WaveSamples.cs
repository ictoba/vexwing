using System.Runtime.InteropServices;

namespace Vexwing
{
    [StructLayout(LayoutKind.Explicit)]
    public struct WaveSamples
    {
        [FieldOffset(0)]
        public ushort ValidBitsPerSample;

        [FieldOffset(0)]
        public ushort SamplesPerBlock;

        [FieldOffset(0)]
        public ushort Reserved;
    }
}
