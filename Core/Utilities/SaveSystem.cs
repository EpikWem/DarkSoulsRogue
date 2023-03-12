using System;
using System.Xml;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.Items;
using DarkSoulsRogue.Core.Items.Lists;

namespace DarkSoulsRogue.Core.Utilities;

public static class SaveSystem
{

    private const string SaveFilePath = @"C:\Users\Lucas\Documents\2_DEVELOP\CS\DarkSoulsRogue\Content\saves\save0.xml";
    private static XmlDocument _saveFile;
    private static XmlNode _asset;

    public static void Init()
    {
        _saveFile = new XmlDocument();
        _saveFile.Load(SaveFilePath);
        _asset = _saveFile["XnaContent"]["Asset"];
    }
    
    // RETURNS current map id
    public static int Load(Character character)
    {
        XmlNode node = _asset["character"];
        character.Life = GetInt(node["life"]);
        character.Souls = GetInt(node["souls"]);
        character.IsHuman = GetBool(node["isHuman"]); 
        
        node = _asset["character"]["position"];
        character.SetPosition(GetInt(node["x"]), GetInt(node["y"]));
        character.Orientation = GetInt(node["orientation"]) switch
        {
            0 => Orientation.Up,
            1 => Orientation.Down,
            2 => Orientation.Right,
            3 => Orientation.Left,
            _ => Orientation.Null
        };

        node = _asset["character"]["attributes"];
        var attributes = new int[Attributes.NumAttributes];
        for (var i = 0; i < Attributes.NumAttributes; i++)
            attributes[i] = GetInt(node.ChildNodes[i]);
        character.Attributes.Set(attributes);
        
        node = _asset["character"]["triggers"];
        var triggers = new bool[Triggers.NumTriggers];
        for (var i = 0; i < Triggers.NumTriggers; i++)
            triggers[i] = GetBool(node.ChildNodes[i]);
        character.Triggers.Set(triggers);

        node = _asset["character"]["inventory"];
        character.ChangeArmor(Armors.GetFromIndex(GetInt(node["equippedArmor"])));
        character.ChangeRing(Rings.GetFromIndex(GetInt(node["equippedRing"])));

        return GetInt(_asset["map"]);
    }
    
    public static void Save(int mapId, Character character)
    {
        var doc = new XmlDocument();
        doc.Load(SaveFilePath);
        XmlNode asset = doc["XnaContent"]["Asset"];

        asset["map"].InnerText = mapId.ToString();

        XmlNode node = asset["character"];
        node["life"].InnerText = character.Life.ToString();
        node["souls"].InnerText = character.Souls.ToString();
        node["isHuman"].InnerText = character.IsHuman.ToString();
        
        node = asset["character"]["position"];
        node["x"].InnerText = character.GetPosition().X.ToString();
        node["y"].InnerText = character.GetPosition().Y.ToString();
        node["orientation"].InnerText = (character.Orientation switch
        {
            Orientation.Up => 0,
            Orientation.Down => 1,
            Orientation.Right => 2,
            Orientation.Left => 3,
            Orientation.Null => 0,
            _ => 0
        }).ToString();

        node = asset["character"]["attributes"];
        for (var i = 0; i < Attributes.NumAttributes; i++)
            node.ChildNodes[i].InnerText = character.Attributes.GetAll()[i].ToString();
        
        node = asset["character"]["triggers"];
        for (var i = 0; i < Triggers.NumTriggers; i++)
            node.ChildNodes[i].InnerText = character.Triggers.GetAll()[i].ToString();
        
        node = asset["character"]["inventory"];
        node["equippedArmor"].InnerText = Armors.GetIndexOf(character.Inventory.EquippedArmor).ToString();
        node["equippedRing"].InnerText = Rings.GetIndexOf(character.Inventory.EquippedRing).ToString();
        
        doc.Save(SaveFilePath);
    }

    private static int GetInt(XmlNode node) => int.Parse(node.InnerText);
    private static bool GetBool(XmlNode node) => bool.Parse(node.InnerText);
    private static string GetString(XmlNode node) => node.InnerText;

}