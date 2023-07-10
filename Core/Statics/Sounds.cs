using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace DarkSoulsRogue.Core.Statics;

public static class Sounds
{

    public static Song
        MTitle;
    public static SoundEffect
        SConfirm, SDeath, SEstus, SFog, SIllusoryWall, SItem, SLit, SSoul1, SSoul2, SVictory;

    private static Song _currentSong;
    public enum Chanel { Music, Effects, Ambience, Feet, Voice }
    private static float[] _levels;

    public static void Init(ContentManager content)
    {
        MediaPlayer.IsRepeating = true;
        _currentSong = null;
        _levels = new[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
        
        MTitle = LoadM("title", content);

        SConfirm = LoadS("confirm", content);
        SDeath = LoadS("death", content);
        SEstus = LoadS("estus", content);
        SFog = LoadS("fog", content);
        SIllusoryWall = LoadS("illusorywall", content);
        SItem = LoadS("item", content);
        SLit = LoadS("lit", content);
        SSoul1 = LoadS("soulsuck1", content);
        SSoul2 = LoadS("soulsuck2", content);
        SVictory = LoadS("victory", content);
    }

    private static Song LoadM(string name, ContentManager content)
        => content.Load<Song>("sounds/musics/" + name);
    private static SoundEffect LoadS(string name, ContentManager content)
        => content.Load<SoundEffect>("sounds/sfx/" + name);

    public static void Play(Song song)
    {
        if (song == _currentSong)
            return;
        MediaPlayer.Play(song);
        _currentSong = song;
    }
    public static void Play(SoundEffect sfx)
        => sfx.CreateInstance().Play();
    
    public static float[] GetLevels()
        => _levels;   
    public static void SetLevels(float[] levels)
    {
        _levels = levels;
        SoundEffect.MasterVolume = _levels[(int)Chanel.Effects];
    }

}