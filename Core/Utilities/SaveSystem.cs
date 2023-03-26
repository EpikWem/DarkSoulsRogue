﻿using System.Xml;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Items;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.Utilities;

public static class SaveSystem
{

    private const string SavesPath = @"C:\Users\Lucas\Documents\2_DEVELOP\CS\DarkSoulsRogue\Content\saves\";
    //private const string SavesPath = @"C:\Users\lucas\Documents\$_DIVERS\Code\CS\DarkSoulsRogue\Content\saves\";
    public const int SavesCount = 4;

    private static readonly XmlDocument SaveFile = new ();
    
    public static void Init()
    {
        SaveFile.Load(GetSaveFilePath());
    }



    /**=============================================================
     *= LOAD METHOD ================================================
     *============================================================*/

    public static void Load() => Load(GetInt(SaveFile["XnaContent"]?["Asset"]?["lastSaveId"]));
    public static void Load(int saveId)
    {
        var character = Main.Character;
        
        Main.CurrentSaveId = saveId;
        
        var root = SaveFile["XnaContent"]?["Asset"]?["s" + Main.CurrentSaveId];
        
        XmlNode node = root?["character"];
        character.Name = GetString(node?["name"]);
        character.Life = GetInt(node?["life"]);
        character.Souls = GetInt(node?["souls"]);
        character.IsHuman = GetBool(node?["isHuman"]);

        node = root?["character"]?["position"];
        character.SetPosition(GetInt(node?["x"]), GetInt(node?["y"]));
        character.Orientation = Character.OrientationFromId(GetInt(node?["orientation"]));

        node = root?["character"]?["attributes"];
        var attributes = new int[Attributes.NumAttributes];
        for (var i = 0; i < Attributes.NumAttributes; i++)
            attributes[i] = GetInt(node?.ChildNodes[i]);
        character.Attributes.Set(attributes);
        
        node = root?["character"]?["triggers"];
        var triggers = new bool[Triggers.NumTriggers];
        for (var i = 0; i < Triggers.NumTriggers; i++)
            triggers[i] = GetBool(node?.ChildNodes[i]);
        character.Triggers.Set(triggers);

        node = root?["character"]?["inventory"];
        character.ChangeArmor(Armor.GetFromIndex(GetInt(node?["equippedArmor"])));
        character.ChangeRing(Ring.GetFromIndex(GetInt(node?["equippedRing"])));
        character.ChangeWeapon(Weapon.GetFromIndex(GetInt(node?["equippedWeapon"])));

        node = root?["objects"];
        for (var i = 0; i < Maps.GetObjectsCount(); i++)
            Maps.GetObjectFromId(i).SetState(GetInt(node?.ChildNodes[i]));
        
        Main.LoadMap(GetInt(root?["map"]));
        TitleMenu.IsActive = false;
    }
    
    
    
    /**=============================================================
     *= SAVE METHOD ================================================
     *============================================================*/
    
    public static void Save()
    {
        var character = Main.Character;
        
        SaveFile["XnaContent"]!["Asset"]!["lastSaveId"]!.InnerText = Main.CurrentSaveId.ToString();
        
        XmlNode root = SaveFile["XnaContent"]?["Asset"]?["s" + Main.CurrentSaveId];
        
        root!["map"]!.InnerText = Main.CurrentMap.Id.ToString();

        XmlNode node = root["character"];
        node!["name"]!.InnerText = character.Name;
        node["life"]!.InnerText = character.Life.ToString();
        node["souls"]!.InnerText = character.Souls.ToString();
        node["isHuman"]!.InnerText = character.IsHuman.ToString();
        
        node = root["character"]?["position"];
        node!["x"]!.InnerText = character.GetPosition().X.ToString();
        node["y"]!.InnerText = character.GetPosition().Y.ToString();
        node["orientation"]!.InnerText = Character.OrientationId(character.Orientation).ToString();

        node = root["character"]["attributes"];
        for (var i = 0; i < Attributes.NumAttributes; i++)
            node!.ChildNodes[i]!.InnerText = character.Attributes.GetAll()[i].ToString();
        
        node = root["character"]["triggers"];
        for (var i = 0; i < Triggers.NumTriggers; i++)
            node!.ChildNodes[i]!.InnerText = character.Triggers.GetAll()[i].ToString();
        
        node = root["character"]["inventory"];
        node!["equippedArmor"]!.InnerText = Armor.GetIndexOf(character.Inventory.EquippedArmor).ToString();
        node["equippedRing"]!.InnerText = Ring.GetIndexOf(character.Inventory.EquippedRing).ToString();
        node["equippedWeapon"]!.InnerText = Weapon.GetIndexOf(character.Inventory.EquippedWeapon).ToString();
        
        node = root["objects"];
        for (var i = 0; i < Maps.GetObjectsCount(); i++)
            node!.ChildNodes[i]!.InnerText = Maps.GetObjectFromId(i).GetState().ToString();
        
        SaveFile.Save(GetSaveFilePath());
    }

    public static void CreateNewSave(int newSaveId)
    {
        Main.LoadMap(101);
        Main.Character = new Character();
        Main.Character.PlaceOnGrid(7, 5, Orientation.Up);
        for (var i = 0; i < Maps.GetObjectsCount(); i++)
            Maps.GetObjectFromId(i).SetState(0);
        Ath.Init(Main.Character);
        Main.CurrentSaveId = newSaveId;
        TitleMenu.IsActive = false;
    }

    public static Texture2D GetGameIcon(int gameId) =>
        Armor.GetFromIndex(
            GetInt(SaveFile!["XnaContent"]!["Asset"]!["s" + gameId]!["character"]!["inventory"]!["equippedArmor"]))
            .GetWearingTextures()[1];
    public static string GetGameName(int gameId) =>
        GetString(SaveFile!["XnaContent"]!["Asset"]!["s" + gameId]!["character"]!["name"]);

    private static string GetSaveFilePath() => SavesPath + "save.xml";
    private static string GetSettingsPath() => SavesPath + "settings.xml";

    private static int GetInt(XmlNode node) => int.Parse(node.InnerText);
    private static bool GetBool(XmlNode node) => bool.Parse(node.InnerText);
    private static string GetString(XmlNode node) => node.InnerText;

}