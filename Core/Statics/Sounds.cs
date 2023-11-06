using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace DarkSoulsRogue.Core.Statics;

public static class Sounds
{

    public static Music
        MTitle;
    public static Sfx
        SMenuConfirm, SDeath, SEstus, SFog, SIllusoryWall, SItem, SBonfireRest, SMenuBack, SMenuMove, SSoul1, SSoul2, SVictory;

    public class Music { internal Song Sound; internal Music(Song sound) => Sound = sound; }
    public class Sfx { internal SoundEffect Sound; internal Sfx(SoundEffect sound) => Sound = sound; }
    public class Ambient { internal Song Sound; internal Ambient(Song sound) => Sound = sound; }
    public class Feet { internal SoundEffect Sound; internal Feet(SoundEffect sound) => Sound = sound; }

    private static Music _currentSong;
    private static Ambient _currentAmbience;
    public enum Chanel { Music, Sfx, Ambient, Feet }
    private static int[] _levels;

    public static void Init(ContentManager content)
    {
        MediaPlayer.IsRepeating = true;
        _currentSong = null;
        _currentAmbience = null;
        _levels = new[] { 100, 100, 100, 100 };
        
        MTitle = new Music(LoadM("title", content));

        SMenuConfirm = new Sfx(LoadS("confirm", content));
        SDeath = new Sfx(LoadS("death", content));
        SEstus = new Sfx(LoadS("estus", content));
        SFog = new Sfx(LoadS("fog", content));
        SIllusoryWall = new Sfx(LoadS("illusorywall", content));
        SItem = new Sfx(LoadS("item", content));
        SBonfireRest = new Sfx(LoadS("bonfirerest", content));
        SMenuBack = new Sfx(LoadS("menuback", content));
        SMenuMove = new Sfx(LoadS("menumove", content));
        SSoul1 = new Sfx(LoadS("soulsuck1", content));
        SSoul2 = new Sfx(LoadS("soulsuck2", content));
        SVictory = new Sfx(LoadS("victory", content));
    }

    private static Song LoadM(string name, ContentManager content)
        => content.Load<Song>("sounds/musics/" + name);
    private static SoundEffect LoadS(string name, ContentManager content)
        => content.Load<SoundEffect>("sounds/sfx/" + name);

    public static void Play(Music music)
    {
        if (music == _currentSong)
            return;
        MediaPlayer.Play(music.Sound);
        _currentSong = music;
    }
    public static void Play(Ambient ambient)
    {
        if (ambient == _currentAmbience)
            return;
        MediaPlayer.Play(ambient.Sound);
        _currentAmbience = ambient;
    }
    public static void Play(Sfx sfx)
        => sfx.Sound.CreateInstance().Play();
    public static void Play(Feet feet)
        => feet.Sound.CreateInstance().Play();

    public static void StopMusic()
    {
        MediaPlayer.Stop();
        _currentSong = null;
    }

    public static int[] GetLevels()
        => _levels;
    public static int GetLevel(Chanel chanel)
        => _levels[(int)chanel];   
    public static void SetLevels(int[] levels)
    {
        _levels = levels;
        MediaPlayer.Volume = (float)_levels[(int)Chanel.Music] / 500;
        SoundEffect.MasterVolume = (float)_levels[(int)Chanel.Sfx] / 100;
    }

}