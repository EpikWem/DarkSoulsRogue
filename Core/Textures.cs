using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Textures
{

    private const int WallNumber = 3;

    private ContentManager _content;

    public Texture2D BgT;
    public Texture2D[] CharacterDebugT, CharacterLinkT, BonfireT, WallsT;
        

    public Textures(ContentManager content)
    {
        _content = content;
        BgT = LoadTexture("bg");
        CharacterDebugT = LoadCharacterTextures("debug");
        CharacterLinkT = LoadCharacterTextures("link");
        BonfireT = LoadObjectTextures(Bonfire.Name, Bonfire.StateNumber);
        WallsT = LoadWallTextures();
    }
    
    
    
    /**=============================================================
     *= TEXTURES MANAGEMENT ========================================
     *============================================================*/
    
    private Texture2D LoadTexture(string fileName)
    {
        return _content.Load<Texture2D>("images/" + fileName);
    }

    private Texture2D[] LoadCharacterTextures(string character)
    {
        return new[]
        {
            LoadTexture("characters/" + character + "/up"),
            LoadTexture("characters/" + character + "/down"),
            LoadTexture("characters/" + character + "/right"),
            LoadTexture("characters/" + character + "/left")
        };
    }

    private Texture2D[] LoadObjectTextures(string objectName, int statesNumber)
    {
        Texture2D[] result = new Texture2D[statesNumber];
        for (int i = 0; i < statesNumber; i++)
            result[i] = LoadTexture("objects/" + objectName + "/" + i);
        return result;
    }

    private Texture2D[] LoadWallTextures()
    {
        Texture2D[] result = new Texture2D[WallNumber];
        for (int i = 1; i < WallNumber; i++)
            result[i] = LoadTexture("walls/wall" + i);
        return result;
    }
    
}