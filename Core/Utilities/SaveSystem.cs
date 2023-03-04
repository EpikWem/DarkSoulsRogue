using System;
using System.Xml;
using DarkSoulsRogue.Core.GameObjects;

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
    
    public static void Load(Character character)
    {
        XmlNode node;

        node = _asset["character"];
        character.Life = GetInt(node["life"]);
        character.Souls = GetInt(node["souls"]);
        character.IsHuman = GetBool(node["isHuman"]);

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
        
    }
    
    public static void Save(Character character)
    {
        var doc = new XmlDocument();
        doc.Load(SaveFilePath);
        XmlNode asset = doc["XnaContent"]["Asset"];
        XmlNode node;

        node = asset["character"];
        node["life"].InnerText = character.Life.ToString();
        node["souls"].InnerText = character.Souls.ToString();
        node["isHuman"].InnerText = character.IsHuman.ToString();

        node = asset["character"]["attributes"];
        for (var i = 0; i < Attributes.NumAttributes; i++)
            node.ChildNodes[i].InnerText = character.Attributes.GetAll()[i].ToString();
        
        node = asset["character"]["triggers"];
        for (var i = 0; i < Triggers.NumTriggers; i++)
            node.ChildNodes[i].InnerText = character.Triggers.GetAll()[i].ToString();
        
        doc.Save(SaveFilePath);
    }

    private static int GetInt(XmlNode node) => int.Parse(node.InnerText);
    private static bool GetBool(XmlNode node) => bool.Parse(node.InnerText);
    private static string GetString(XmlNode node) => node.InnerText;

}