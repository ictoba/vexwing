using System;

namespace Vexwing
{
    public class WaveFormatHeader : IRiffFormatHeader
    {
        public ushort FormatTag { get; set; }
        public ushort ChannelsCount { get; set; }
        public uint SampleRate { get; set; }
        public uint AvgBytesPerSec { get; set; }
        public ushort BlockAlign { get; set; }
        public ushort BitsPerSample { get; set; }
        public ushort ExtraInfoSize { get; set; }

        public WaveSamples Samples { get; set; }
        public uint ChannelMask { get; set; }
        public Guid SubFormat { get; set; }
    }
}
