using System.IO;
using System.Threading.Tasks;

namespace Vexwing.SampleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var wavPath = Path.Combine("C:\\some\\path\\to_folder", "my_file.wav");

            var codecParser = new RiffCodecParser();
            var riff = codecParser.Read(wavPath);

            var player = new RiffPlayer();
            await player.PlayWave(riff);
        }
    }
}
