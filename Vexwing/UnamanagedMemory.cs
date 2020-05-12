using System;
using System.Runtime.InteropServices;

namespace Vexwing
{
    public class UnamanagedMemory : IDisposable
    {
        private bool _disposed = false;
        private IntPtr _pointer;

        public IntPtr Pointer
        {
            get
            {
                if (_disposed)
                {
                    throw new Exception($"Tried to acces the internal pointer of an disposed {nameof(UnamanagedMemory)} object");
                }
                return _pointer;
            }
        }

        public UnamanagedMemory(int size)
        {
            _pointer = Marshal.AllocHGlobal(size);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Marshal.FreeHGlobal(_pointer);
            }

            _disposed = true;
        }
    }
}
