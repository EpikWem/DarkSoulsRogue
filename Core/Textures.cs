using System.Collections.Generic;
using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Items.Lists;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core;

public class Textures
{

    private const int WallNumber = 1;

    private ContentManager _content;

    public Texture2D BgT;
    public Texture2D[]
        BonfireT, DoorT, WallsT;
    private readonly List<Texture2D[]> _armorTextures;
        

    public Textures(ContentManager content)
    {
        _content = content;
        
        BgT = LoadTexture("bg");
        
        _armorTextures = new List<Texture2D[]>
        {
            LoadCharacterTextures("link"),
            LoadCharacterTextures("artorias"),
            //LoadCharacterTextures("black_iron"),
            LoadCharacterTextures("solaire")
        };
        
        BonfireT = LoadObjectTextures(Bonfire.Name, Bonfire.StateNumber);
        DoorT = LoadObjectTextures(Door.Name, Door.StateNumber);
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

    private Texture2D[] LoadObjectTextures(string name, int stateNumber)
    {
        Texture2D[] result = new Texture2D[stateNumber];
        for (int i = 0; i < stateNumber; i++)
            result[i] = LoadTexture("objects/" + name + "/" + i);
        return result;
    }

    private Texture2D[] LoadWallTextures()
    {
        Texture2D[] result = new Texture2D[WallNumber+1];
        for (int i = 1; i < WallNumber+1; i++)
            result[i] = LoadTexture("walls/" + i);
        return result;
    }

    public Texture2D[] GetArmorTextures(Armor armor) => _armorTextures[Armors.GetIndexOf(armor)];

}