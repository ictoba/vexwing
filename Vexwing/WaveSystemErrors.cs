namespace Vexwing
{
    public enum WaveSystemErrors : uint
    {
        FILENOTFOUND = 33,
        OUTOFMEMORY = 34,
        CANNOTOPEN = 35,
        CANNOTCLOSE = 36,
        CANNOTREAD = 37,
        CANNOTWRITE = 38,
        CANNOTSEEK = 39,
        CANNOTEXPAND = 40,
        CHUNKNOTFOUND = 41,
        UNBUFFERED = 42,
        PATHNOTFOUND = 43,
        ACCESSDENIED = 44,
        SHARINGVIOLATION = 45,
        NETWORKERROR = 46,
        TOOMANYOPENFILES = 47,
        INVALIDFILE = 48
    }
}
