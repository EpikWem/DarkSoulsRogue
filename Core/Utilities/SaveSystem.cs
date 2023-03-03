using System;
using System.Xml;
using DarkSoulsRogue.Core.GameObjects;

namespace DarkSoulsRogue.Core.Utilities;

public static class SaveSystem
{

    private const string SaveFilePath = @"C:\Users\Lucas\Documents\2_DEVELOP\CS\DarkSoulsRogue\Content\saves\save0.xml";
    private static XmlDocument _saveFile;
    private static XmlNode _node;

    public static void Init()
    {
        _saveFile = new XmlDocument();
        _saveFile.Load(SaveFilePath);
        _node = _saveFile["XnaContent"]["Asset"];
        
    }
    
    public static void Load(Character character)
    {
        character.Life = int.Parse(_node["Life"].InnerText);
        character.Souls = int.Parse(_node["Souls"].InnerText);
        
        var attributes = new int[Attributes.NumAttributes];
        for (var i = 0; i < Attributes.NumAttributes; i++)
            attributes[i] = int.Parse(_node["Attributes"].ChildNodes[i].InnerText);

        character.Attributes.Set(attributes);
    }
    
}