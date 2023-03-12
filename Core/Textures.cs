using System.Collections.Generic;
using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Textures
{

    private const int WallNumber = 1;

    public static Texture2D BgT;
    public static Texture2D[] BonfireT, ChestT, DoorT;
    public static Texture2D[] WallsT;
    public static List<Texture2D[]> ArmorTextures;
        

    public Textures(ContentManager content)
    {
        BgT = LoadTexture("bg", content);
        
        ArmorTextures = new List<Texture2D[]>
        {
            LoadCharacterTextures("link", content),
            LoadCharacterTextures("artorias", content),
            //LoadCharacterTextures("black_iron"),
            LoadCharacterTextures("solaire", content)
        };
        
        BonfireT = LoadObjectTextures(Bonfire.Name, Bonfire.StateNumber, content);
        ChestT = LoadObjectTextures(Chest.Name, Chest.StateNumber, content);
        DoorT = LoadObjectTextures(Door.Name, Door.StateNumber, content);
        WallsT = LoadWallsTexture(content);
    }
    
    
    
    /**=============================================================
     *= TEXTURES MANAGEMENT ========================================
     *============================================================*/
    
    private static Texture2D LoadTexture(string fileName, ContentManager content)
    {
        return content.Load<Texture2D>("images/" + fileName);
    }

    private static Texture2D[] LoadCharacterTextures(string characterName, ContentManager content)
    {
        return new[]
        {
            LoadTexture("characters/" + characterName + "/up", content),
            LoadTexture("characters/" + characterName + "/down", content),
            LoadTexture("characters/" + characterName + "/right", content),
            LoadTexture("characters/" + characterName + "/left", content)
        };
    }

    private static Texture2D[] LoadObjectTextures(string name, int stateNumber, ContentManager content)
    {
        var result = new Texture2D[stateNumber];
        for (int i = 0; i < stateNumber; i++)
            result[i] = LoadTexture("objects/" + name + "/" + i, content);
        return result;
    }

    private static Texture2D[] LoadWallsTexture(ContentManager content)
    {
        var result = new Texture2D[WallNumber+1];
        for (int i = 1; i < WallNumber+1; i++)
            result[i] = LoadTexture("walls/" + i, content);
        return result;
    }

}