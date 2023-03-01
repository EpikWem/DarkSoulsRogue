using System;
using System.Xml;
using DarkSoulsRogue.Core.GameObjects;

namespace DarkSoulsRogue.Core.Utilities;

public static class SaveSystem
{
    
    private const string SaveFilePath = @"saves/save1.xml";

    public static void Load(Character character)
    {
        XmlDocument file = new ();
        //try
        //{
            file.Load(SaveFilePath);

            var attributes = Array.Empty<int>();
            attributes[0] = int.Parse(file["XML"]["character"]["attributes"]["vitality"].InnerText);

            character.Attributes.Set(attributes);
        //}
        //catch
        //{
            //return;
        //}
    }
    
}