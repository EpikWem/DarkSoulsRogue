using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace DarkSoulsRogue.Core.Statics;

public static class Sounds
{

    public static Music
        MTitle;
    public static Sfx
        SBonfireRest, SBowShot, SChest, SMenuConfirm, SCrossbowReload, SCrossbowShot, SDeath, SEstus, SFirebomb, SFog,
        SIllusoryWall, SInvasion, SItem, SMenuBack, SMenuMove, SMiracle, SMoss, SNewArea, SParry, SPyromancy, SRollHeavy,
        SRollLight, SRollMedium, SRollNaked, SSorcery, SSoulsGain, SSoulUse, SStart, SVictory, SWarp;

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

        SBonfireRest = new Sfx(LoadS("bonfirerest", content));
        SBowShot = new Sfx(LoadS("bowshot", content));
        SChest = new Sfx(LoadS("chest", content));
        SMenuConfirm = new Sfx(LoadS("confirm", content));
        SCrossbowReload = new Sfx(LoadS("crossbowreload", content));
        SCrossbowShot = new Sfx(LoadS("crossbowshot", content));
        SDeath = new Sfx(LoadS("death", content));
        SEstus = new Sfx(LoadS("estus", content));
        SFirebomb = new Sfx(LoadS("firebomb", content));
        SFog = new Sfx(LoadS("fog", content));
        SIllusoryWall = new Sfx(LoadS("illusorywall", content));
        SItem = new Sfx(LoadS("item", content));
        SMenuBack = new Sfx(LoadS("menuback", content));
        SMenuMove = new Sfx(LoadS("menumove", content));
        SMiracle = new Sfx(LoadS("miracle", content));
        SMoss = new Sfx(LoadS("moss", content));
        SNewArea = new Sfx(LoadS("newarea", content));
        SParry = new Sfx(LoadS("parry", content));
        SPyromancy = new Sfx(LoadS("pyromancy", content));
        SRollHeavy = new Sfx(LoadS("rollheavy", content));
        SRollLight = new Sfx(LoadS("rolllight", content));
        SRollMedium = new Sfx(LoadS("rollmedium", content));
        SRollNaked = new Sfx(LoadS("rollnaked", content));
        SSorcery = new Sfx(LoadS("sorcery", content));
        SSoulsGain = new Sfx(LoadS("soulsgain", content));
        SSoulUse = new Sfx(LoadS("souluse", content));
        SStart = new Sfx(LoadS("start", content));
        SVictory = new Sfx(LoadS("victory", content));
        SWarp = new Sfx(LoadS("warp", content));
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