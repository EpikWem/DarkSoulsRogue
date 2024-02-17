using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace DarkSoulsRogue.Core.Statics;

public static class Sounds
{

    public static Music
        MTitle;
    public static Sfx
        SBonfireRest, SBowShot, SChest, SCrossbowReload, SCrossbowShot, SEstus, SFirebomb, SFog,
        SIllusoryWall, SInvasion, SItem, SMiracle, SMoss, SParry, SPyromancy, SRollHeavy,
        SRollLight, SRollMedium, SRollNaked, SSorcery, SSoulsGain, SSoulUse, SWarp;
    public static Interface
        IMenuConfirm, IDeath, IMenuBack, IMenuMove, INewArea, IStart, IVictory;
    
    public class Music { internal Song Sound; internal Music(Song sound) => Sound = sound; }
    public class Sfx { internal SoundEffect Sound; internal Sfx(SoundEffect sound) => Sound = sound; }
    public class Ambient { internal Song Sound; internal Ambient(Song sound) => Sound = sound; }
    public class Interface { internal SoundEffect Sound; internal Interface(SoundEffect sound) => Sound = sound; }

    private static Music _currentSong;
    private static Ambient _currentAmbience;
    public enum Chanel { Master, Music, Sfx, Ambient, Interface }
    private static int[] _levels;

    public static void Init(ContentManager content)
    {
        MediaPlayer.IsRepeating = true;
        _currentSong = null;
        _currentAmbience = null;
        _levels = new[] { 100, 100, 100, 100, 100 };
        
        MTitle = new Music(LoadM("title", content));

        SBonfireRest = new Sfx(LoadS("bonfirerest", content));
        SBowShot = new Sfx(LoadS("bowshot", content));
        SChest = new Sfx(LoadS("chest", content));
        SCrossbowReload = new Sfx(LoadS("crossbowreload", content));
        SCrossbowShot = new Sfx(LoadS("crossbowshot", content));
        IDeath = new Interface(LoadS("death", content));
        SEstus = new Sfx(LoadS("estus", content));
        SFirebomb = new Sfx(LoadS("firebomb", content));
        SFog = new Sfx(LoadS("fog", content));
        SIllusoryWall = new Sfx(LoadS("illusorywall", content));
        SItem = new Sfx(LoadS("item", content));
        SMiracle = new Sfx(LoadS("miracle", content));
        SMoss = new Sfx(LoadS("moss", content));
        SParry = new Sfx(LoadS("parry", content));
        SPyromancy = new Sfx(LoadS("pyromancy", content));
        SRollHeavy = new Sfx(LoadS("rollheavy", content));
        SRollLight = new Sfx(LoadS("rolllight", content));
        SRollMedium = new Sfx(LoadS("rollmedium", content));
        SRollNaked = new Sfx(LoadS("rollnaked", content));
        SSorcery = new Sfx(LoadS("sorcery", content));
        SSoulsGain = new Sfx(LoadS("soulsgain", content));
        SSoulUse = new Sfx(LoadS("souluse", content));
        SWarp = new Sfx(LoadS("warp", content));
        
        IMenuConfirm = new Interface(LoadS("confirm", content));
        IMenuBack = new Interface(LoadS("menuback", content));
        IMenuMove = new Interface(LoadS("menumove", content));
        INewArea = new Interface(LoadS("newarea", content));
        IStart = new Interface(LoadS("start", content));
        IVictory = new Interface(LoadS("victory", content));
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
    public static void Play(Interface sound)
        => sound.Sound.CreateInstance().Play();

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
        MediaPlayer.Volume = _levels[(int)Chanel.Music]/100f * _levels[(int)Chanel.Master]/100f /8f;
        SoundEffect.MasterVolume = _levels[(int)Chanel.Sfx]/100f * _levels[(int)Chanel.Master]/100f /8f;
    }

}