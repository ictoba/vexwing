using System;
using System.Buffers.Binary;
using System.Collections.Generic;

namespace Vexwing
{
    public static class WaveSubFormatTypes
    {
        public static Guid MEDIASUBTYPE_BASE                   { get => Guid.Parse("00000000-0000-0010-8000-00aa00389b71"); }
        public static Guid COMPRESSED_AUDIO_FORMAT_BASE        { get => Guid.Parse("00000000-0cea-0010-8000-00aa00389b71"); }

        public static Guid KSDATAFORMAT_SUBTYPE_IEC61937_MPEG3 { get => Guid.Parse("00000004-0cea-0010-8000-00aa00389b71"); }
        public static Guid KSDATAFORMAT_SPECIFIER_VC_ID        { get => Guid.Parse("ad98d184-aac3-11d0-a41c-00a0c9223196"); }
        public static Guid KSDATAFORMAT_SPECIFIER_WAVEFORMATEX { get => Guid.Parse("05589f81-c356-11ce-bf01-00aa0055595a"); }
        public static Guid KSDATAFORMAT_SPECIFIER_DSOUND       { get => Guid.Parse("518590a2-a184-11d0-8522-00c04fd9baf3"); }

        public static Guid GetMediaSubtype(WaveFormatTags waveFormatTag)
        {
            ushort tag = (ushort)waveFormatTag;
            byte[] mediaTypeBase = MEDIASUBTYPE_BASE.ToByteArray();
            var span = new Span<byte>(mediaTypeBase);
            BinaryPrimitives.WriteUInt16LittleEndian(span.Slice(0, 2), tag);

            return new Guid(span);
        }

        public static Guid GetCompressedAudioSubtype(ConsumerElectronicsAssociationTypes ceaType)
        {
            if (ceaExceptions.Contains(ceaType))
            {
                return Guid.Empty;
            }

            ushort type = (ushort)ceaType;
            byte[] mediaTypeBase = COMPRESSED_AUDIO_FORMAT_BASE.ToByteArray();
            var span = new Span<byte>(mediaTypeBase);
            BinaryPrimitives.WriteUInt16LittleEndian(span.Slice(0, 2), type);

            return new Guid(span);
        }

        private static readonly HashSet<ConsumerElectronicsAssociationTypes> ceaExceptions = new HashSet<ConsumerElectronicsAssociationTypes>
        {
            ConsumerElectronicsAssociationTypes.UNUSED,
            ConsumerElectronicsAssociationTypes.PCM,
            ConsumerElectronicsAssociationTypes.AC3,
            ConsumerElectronicsAssociationTypes.DTS,
            ConsumerElectronicsAssociationTypes.WMA_PRO,
            ConsumerElectronicsAssociationTypes.RESERVED
        };
    }
}
