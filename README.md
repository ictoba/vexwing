# vexwing
Proof of concept for reading and playing RIFF files using Windows Multimedia API

### Usage

```cs
var wavPath = Path.Combine("C:\\some\\path\\to_folder", "my_file.wav");
// instantiate file parser
var codecParser = new RiffCodecParser();
// parse file
var riff = codecParser.Read(wavPath);
// instantiate player
var player = new RiffPlayer();
// play file
await player.PlayWave(riff);
```
