using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Vexwing.WaveModels;

namespace Vexwing
{
    public class RiffPlayer
    {
        public async Task PlayWave(RiffCodec riff)
        {
            uint WAVE_MAPPER = uint.MaxValue;
            int CALLBACK_NULL = 0;
            IntPtr handle = IntPtr.Zero;

            var header = riff.FormatHeader as WaveFormatHeader;
            int duration = Convert.ToInt32(header.AvgBytesPerSec / header.SampleRate);

            var waveFormat = new WAVEFORMAT
            {
                wFormatTag = header.FormatTag,
                nChannels = header.ChannelsCount,
                nSamplesPerSec = header.SampleRate,
                nAvgBytesPerSec = header.AvgBytesPerSec,
                nBlockAlign = header.BlockAlign,
                wBitsPerSample = header.BitsPerSample,
                cbSize = header.ExtraInfoSize
            };

            var waveHdr = new WAVEHDR
            {
                dwBufferLength = riff.DataSize,
                dwBytesRecorded = riff.DataSize,
                dwUser = 0,
                dwFlags = 0,
                dwLoops = 0
            };

            using var buffer = new UnamanagedMemory((int)riff.DataSize);
            Marshal.Copy(riff.Data, 0, buffer.Pointer, riff.Data.Length);
            waveHdr.lpData = buffer.Pointer;

            uint res = uint.MaxValue;
            res = WindowsMultimedia.waveOutOpen(ref handle, WAVE_MAPPER, ref waveFormat, 0, 0, CALLBACK_NULL);
            CheckMethodResult(nameof(WindowsMultimedia.waveOutOpen), res);

            res = WindowsMultimedia.waveOutPrepareHeader(handle, ref waveHdr, Marshal.SizeOf(waveHdr));
            CheckMethodResult(nameof(WindowsMultimedia.waveOutPrepareHeader), res);

            res = WindowsMultimedia.waveOutWrite(handle, ref waveHdr, Marshal.SizeOf(waveHdr));
            CheckMethodResult(nameof(WindowsMultimedia.waveOutWrite), res);
            await Task.Delay(duration * 1000);

            res = WindowsMultimedia.waveOutClose(handle);
            CheckMethodResult(nameof(WindowsMultimedia.waveOutClose), res);
        }

        private void CheckMethodResult(string methodName, uint result)
        {
            if (result != (uint)MultimediaSystemErrors.NOERROR)
            {
                if (result > (uint)MultimediaSystemErrors.LASTERROR)
                {
                    var errorName = Enum.GetName(typeof(WaveSystemErrors), result);
                    throw new Exception($"{methodName} failed with error: {errorName}");
                }
                throw new Exception($"{methodName} failed with error code: {result}");
            }
        }
    }
}
