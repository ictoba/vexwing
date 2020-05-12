namespace Vexwing
{
    public enum ConsumerElectronicsAssociationTypes : byte
    {
        // https://docs.microsoft.com/en-us/windows-hardware/drivers/audio/subformat-guids-for-compressed-audio-formats

        UNUSED = 0x00,
        PCM = 0x01, // IEC 60958 PCM
        AC3 = 0x02, // AC-3
        MPEG1 = 0x03, // MPEG-1 (Layer1 & 2)
        MPEG3 = 0x04, // MPEG-3 (Layer 3)
        MPEG2 = 0x05, // MPEG-2 (Multichanel)
        AAC = 0x06, // Advanced audio coding* (MPEG-2/4 AAC in ADTS)
        DTS = 0x07, // Digital Theater Sound (DTS)
        ATRAC = 0x08, // Adaptive Transform Acoustic Coding (ATRAC)
        ONE_BIT_AUDIO = 0x08, // One-bit audio
        DDS = 0x0a, // Dolby Digital Plus
        DTSHD = 0x0b, // DTS-HD (24-bit, 95KHz)
        MAT_MLP = 0x0c, // MAT(MLP)- Meridian Lossless Packing (Dolby Digital True HD - 24-bit 196KHz/up to 18M bps, 8 channels)
        DST = 0x0d, // Direct Stream Transport (DST)
        WMA_PRO = 0x0e, // Windows Media Audio (WMA) Pro
        RESERVED = 0x0f,
    }
}
