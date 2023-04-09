using System.Collections.Generic;
using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Statics;

public static class Textures
{

    private const int WallNumber = 1;

    public static Texture2D Void, BgT;
    public static Texture2D[] BonfireT, ChestT, DoorT, LootT;
    public static Texture2D[] LadderBottomT, LadderTopT;
    public static Texture2D[] WallsT;
    public static List<Texture2D[]> ArmorTextures, ShieldTextures;


    public static void Init(ContentManager content)
    {
        Void = LoadTexture("void", content);
        BgT = LoadTexture("bg", content);

        ArmorTextures = new List<Texture2D[]>
        {
            LoadCharacterTextures("link", content),
            LoadCharacterTextures("artorias", content),
            LoadCharacterTextures("solaire", content)
        };
        ShieldTextures = new List<Texture2D[]>
        {
            new [] {Void, Void, Void, Void},
            LoadEquipmentTextures(ETShields, "basic", content),
            LoadEquipmentTextures(ETShields, "grass", content)
        };

        BonfireT = LoadObjectTextures(Bonfire.Name, Bonfire.StateNumber, content);
        ChestT = LoadObjectTextures(Chest.Name, Chest.StateNumber, content);
        DoorT = LoadObjectTextures(Door.Name, Door.StateNumber, content);
        LadderBottomT = new[] { LoadTexture("objects/" + Ladder.Name + "/bottom", content) };
        LadderTopT = new[] { LoadTexture("objects/" + Ladder.Name + "/top", content) };
        LootT = LoadObjectTextures(Loot.Name, Loot.StateNumber, content);
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

    private const string
        ETShields = "shields/";
    private static Texture2D[] LoadEquipmentTextures(string equipmentType, string equipmentName, ContentManager content)
    {
        return new[]
        {
            LoadTexture("equipments/" + equipmentType + equipmentName + "/up", content),
            LoadTexture("equipments/" + equipmentType + equipmentName + "/down", content),
            LoadTexture("equipments/" + equipmentType + equipmentName + "/right", content),
            LoadTexture("equipments/" + equipmentType + equipmentName + "/left", content)
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