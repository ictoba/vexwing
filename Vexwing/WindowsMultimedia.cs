using System;
using System.Runtime.InteropServices;
using Vexwing.WaveModels;

namespace Vexwing
{
    public static class WindowsMultimedia
    {
        [DllImport("Winmm.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern uint waveOutOpen(ref IntPtr phwo, uint uDeviceID, ref WAVEFORMAT pwfx, int dwCallback, int dwInstance, int fdwOpen);

        [DllImport("Winmm.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern uint waveOutClose(IntPtr hwo);

        [DllImport("Winmm.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern uint waveOutPrepareHeader(IntPtr hwo, ref WAVEHDR pwh, int cbwh);

        [DllImport("Winmm.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern uint waveOutWrite(IntPtr hwo, ref WAVEHDR pwh, int cbwh);
    }
}
