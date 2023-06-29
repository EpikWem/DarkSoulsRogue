﻿using System.Collections.Generic;
using System.Xml;
using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Items.Equipments;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.System;

public static class SaveSystem
{

    private const string SaveFilePath = Main.ContentPath + @"saves\save.xml";
    public const int SavesCount = 4;

    private static readonly XmlDocument SaveFile = new();
    
    public static void Init()
    {
        SaveFile.Load(SaveFilePath);
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
        character.Orientation = Orientation.FromInt(GetInt(node?["orientation"]));

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
        character.ChangeWeapon(Weapon.GetFromIndex(GetInt(node?["equippedWeapon"])));
        character.ChangeShield(Shield.GetFromIndex(GetInt(node?["equippedShield"])));
        character.ChangeRing(Ring.GetFromIndex(GetInt(node?["equippedRing"])));

        node = root?["objects"];
        for (var i = 0; i < Map.GetObjectsCount(); i++)
            Map.GetObject(i).SetState(GetInt(node?.ChildNodes[i]));
        
        Main.LoadMap(GetInt(root?["map"]));
    }
    
    
    
    /**=============================================================
     *= SAVE METHOD ================================================
     *============================================================*/
    
    public static void Save()
    {
        var character = Main.Character;
        
        SaveFile["XnaContent"]!["Asset"]!["lastSaveId"]!.InnerText = Main.CurrentSaveId.ToString();
        
        XmlNode root = SaveFile["XnaContent"]?["Asset"]?["s" + Main.CurrentSaveId];
        
        root!["map"]!.InnerText = Main.CurrentMap().Id.ToString();

        XmlNode node = root["character"];
        node!["name"]!.InnerText = character.Name;
        node["life"]!.InnerText = character.Life.ToString();
        node["souls"]!.InnerText = character.Souls.ToString();
        node["isHuman"]!.InnerText = character.IsHuman.ToString();
        
        node = root["character"]?["position"];
        node!["x"]!.InnerText = character.GetPosition().X.ToString();
        node["y"]!.InnerText = character.GetPosition().Y.ToString();
        node["orientation"]!.InnerText = character.Orientation.Index.ToString();

        node = root["character"]["attributes"];
        for (var i = 0; i < Attributes.NumAttributes; i++)
            node!.ChildNodes[i]!.InnerText = character.Attributes.GetAll()[i].ToString();
        
        node = root["character"]["triggers"];
        for (var i = 0; i < Triggers.NumTriggers; i++)
            node!.ChildNodes[i]!.InnerText = character.Triggers.GetAll()[i].ToString();
        
        node = root["character"]["inventory"];
        node!["equippedArmor"]!.InnerText = Armor.GetIndexOf(character.Inventory.EquippedArmor).ToString();
        node["equippedWeapon"]!.InnerText = Weapon.GetIndexOf(character.Inventory.EquippedWeapon).ToString();
        node["equippedShield"]!.InnerText = Shield.GetIndexOf(character.Inventory.EquippedShield).ToString();
        node["equippedRing"]!.InnerText = Ring.GetIndexOf(character.Inventory.EquippedRing).ToString();
        
        node = root["objects"];
        for (var i = 0; i < Map.GetObjectsCount(); i++)
            node!.ChildNodes[i]!.InnerText = Map.GetObject(i).GetState().ToString();
        
        SaveFile.Save(SaveFilePath);
    }

    
    
    /**=============================================================
     *= OTHER METHODS ==============================================
     *============================================================*/
    
    public static void CreateNewSave(int newSaveId, string name, List<int> attributes)
    {
        Main.LoadMap(101);
        Main.Character = new Character(name);
        Main.Character.Attributes.Set(attributes);
        Main.Character.PlaceOnGrid(7, 5, Orientation.Up);
        for (var i = 0; i < Map.GetObjectsCount(); i++)
            Map.GetObject(i).SetState(0);
        Ath.Init(Main.Character);
        Main.CurrentSaveId = newSaveId;
    }

    public static Texture2D GetGameIcon(int gameId) =>
        Armor.GetFromIndex(
            GetInt(SaveFile!["XnaContent"]!["Asset"]!["s" + gameId]!["character"]!["inventory"]!["equippedArmor"]))
            .GetWearingTextures()[1];
    public static string GetGameName(int gameId) =>
        GetString(SaveFile!["XnaContent"]!["Asset"]!["s" + gameId]!["character"]!["name"]);
    public static string GetGameLevel(int gameId) =>
        GetString(SaveFile!["XnaContent"]!["Asset"]!["s" + gameId]!["character"]!["attributes"]?.ChildNodes[0]);

    public static int[] GetGameAttributes(int gameId)
    {
        var result = new int[Attributes.NumAttributes];
        for (var i = 0; i < Attributes.NumAttributes; i++)
            result[i] = GetInt(SaveFile["XnaContent"]?["Asset"]?["s" + gameId]?["character"]?["attributes"]?.ChildNodes[i]);
        return result;
    }
        
    
    private static int GetInt(XmlNode node) => int.Parse(node.InnerText);
    private static bool GetBool(XmlNode node) => bool.Parse(node.InnerText);
    private static string GetString(XmlNode node) => node.InnerText;

}