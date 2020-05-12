using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace Vexwing
{
    public class RiffCodecParser
    {
        private const string RIFF_SIGNATURE = "RIFF";
        private const string RIFF_TYPE_WAVE = "WAVE";
        private const string MARKER_FMT = "fmt ";
        private const string MARKER_DATA = "data";

        public RiffCodec Read(string filePath)
        {
            using var file = File.OpenRead(filePath);
            var buffer = new byte[1024];
            int read = file.Read(buffer, 0, 1024);
            var readOnlySpan = new ReadOnlySpan<byte>(buffer);
            var riff = new RiffCodec();
            riff.Signature = Encoding.ASCII.GetString(readOnlySpan.Slice(0, 4));
            EnforceCorrectMarker(riff.Signature, RIFF_SIGNATURE, nameof(riff.Signature));

            riff.Filesize = BinaryPrimitives.ReadUInt32LittleEndian(readOnlySpan.Slice(4, 4));
            riff.FormatHeaderType = Encoding.ASCII.GetString(readOnlySpan.Slice(8, 4));
            riff.FormatChunkMarker = Encoding.ASCII.GetString(readOnlySpan.Slice(12, 4));
            EnforceCorrectMarker(riff.FormatChunkMarker, MARKER_FMT, nameof(riff.FormatChunkMarker));

            riff.FormatHeaderSize = BinaryPrimitives.ReadUInt32LittleEndian(readOnlySpan.Slice(16, 4));
            riff.FormatHeader = ReadFormatHeader(readOnlySpan, riff.FormatHeaderType, riff.FormatHeaderSize);
            riff.DataChunkMarker = Encoding.ASCII.GetString(readOnlySpan.Slice(20 + (int)riff.FormatHeaderSize, 4));
            EnforceCorrectMarker(riff.DataChunkMarker, MARKER_DATA, nameof(riff.DataChunkMarker));

            riff.DataSize = BinaryPrimitives.ReadUInt32LittleEndian(readOnlySpan.Slice(24 + (int)riff.FormatHeaderSize, 4));
            riff.Data = new byte[riff.DataSize];
            int currentPos = 28 + (int)riff.FormatHeaderSize;
            Array.Copy(buffer, currentPos, riff.Data, 0, 1024 - currentPos);
            bool finished = false;
            currentPos = 1024 - currentPos;
            do
            {
                read = file.Read(buffer, 0, 1024);
                if (read > 0)
                {
                    if ((currentPos + read) > riff.DataSize)
                    {
                        int amountToCopy = (int)riff.DataSize - currentPos;
                        Array.Copy(buffer, 0, riff.Data, currentPos, amountToCopy);
                        currentPos += amountToCopy;
                        //var ros = new ReadOnlySpan<byte>(buffer);
                        //Console.WriteLine($"Weird: {Encoding.ASCII.GetString(ros.Slice(amountToCopy, read - amountToCopy))}");
                    }
                    else
                    {
                        Array.Copy(buffer, 0, riff.Data, currentPos, read);
                        currentPos += read;
                    }
                }
                else
                {
                    finished = true;
                }
            } while (!finished);

            return riff;
        }

        private static void EnforceCorrectMarker(string expectedMarker, string actualMarker, string markerName)
        {
            if (!string.Equals(expectedMarker, actualMarker))
            {
                throw new ArgumentException($"{markerName} marker is incorrect.");
            }
        }

        private IRiffFormatHeader ReadFormatHeader(ReadOnlySpan<byte> readOnlySpan, string typeHeader, uint headerSize)
        {
            if (typeHeader == RIFF_TYPE_WAVE)
            {
                var header = new WaveFormatHeader
                {
                    FormatTag = BinaryPrimitives.ReadUInt16LittleEndian(readOnlySpan.Slice(20, 2)),
                    ChannelsCount = BinaryPrimitives.ReadUInt16LittleEndian(readOnlySpan.Slice(22, 2)),
                    SampleRate = BinaryPrimitives.ReadUInt32LittleEndian(readOnlySpan.Slice(24, 4)),
                    AvgBytesPerSec = BinaryPrimitives.ReadUInt32LittleEndian(readOnlySpan.Slice(28, 4)),
                    BlockAlign = BinaryPrimitives.ReadUInt16LittleEndian(readOnlySpan.Slice(32, 2)),
                    BitsPerSample = BinaryPrimitives.ReadUInt16LittleEndian(readOnlySpan.Slice(34, 2))
                };
                if (header.FormatTag != (ushort)WaveFormatTags.FORMAT_PCM)
                {
                    header.ExtraInfoSize = BinaryPrimitives.ReadUInt16LittleEndian(readOnlySpan.Slice(36, 2));
                    header.Samples = new WaveSamples
                    {
                        ValidBitsPerSample = BinaryPrimitives.ReadUInt16LittleEndian(readOnlySpan.Slice(38, 2))
                    };
                    header.ChannelMask = BinaryPrimitives.ReadUInt32LittleEndian(readOnlySpan.Slice(40, 4));
                    header.SubFormat = new Guid(readOnlySpan.Slice(44, 16));

                    var mask = (SpeakerPositions)header.ChannelMask;
                    Console.WriteLine($"ChannelMask: {mask}");
                }
                return header;
            }

            return null;
        }
    }
}
