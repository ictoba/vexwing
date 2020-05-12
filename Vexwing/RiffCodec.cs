namespace Vexwing
{
    public class RiffCodec
    {
        public string Signature { get; set; }
        public uint Filesize { get; set; }
        public string FormatHeaderType { get; set; }
        public string FormatChunkMarker { get; set; }
        public uint FormatHeaderSize { get; set; }
        public IRiffFormatHeader FormatHeader { get; set; }
        public string DataChunkMarker { get; set; }
        public uint DataSize { get; set; }
        public byte[] Data { get; set; }
    }
}
