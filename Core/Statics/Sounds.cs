using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace DarkSoulsRogue.Core.Statics;

public static class Sounds
{

    public static Song
        MTitle;
    public static Song
        SConfirm;

    public static void Init(ContentManager content)
    {
        MTitle = LoadM("title", content);

        SConfirm = LoadS("confirm", content);
    }

    private static Song LoadM(string name, ContentManager content)
        => content.Load<Song>("sounds/musics/" + name);
    private static Song LoadS(string name, ContentManager content)
        => content.Load<Song>("sounds/sfx/" + name);

}