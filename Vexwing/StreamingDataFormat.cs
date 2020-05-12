using System;

namespace Vexwing
{
    public class StreamingDataFormat
    {
        public uint FormatSize { get; set; }
        public uint Flags { get; set; }
        public uint SampleSize { get; set; }
        public uint Reserved { get; set; }
        public Guid MajorFormat { get; set; }
        public Guid SubFormat { get; set; }
        public Guid Specifier { get; set; }
    }

    public class StreamingAttribute
    {
        public uint Size { get; set; }
        public uint Flags { get; set; }
        public Guid Attribute { get; set; }
    }

    public class StreamingMultipleItem
    {
        public uint Size { get; set; }
        public uint Count { get; set; }
    }

    public class StreamingProperty
    {
        public Guid Set { get; set; }
        public uint Id { get; set; }
        public uint Flags { get; set; }
    }
}
