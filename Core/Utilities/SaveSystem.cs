using System.Xml;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.Items;

namespace DarkSoulsRogue.Core.Utilities;

public static class SaveSystem
{

    private const string SavesFilePath = @"C:\Users\Lucas\Documents\2_DEVELOP\CS\DarkSoulsRogue\Content\saves\";
    //private const string SavesFilePath = @"C:\Users\lucas\Documents\$_DIVERS\Code\CS\DarkSoulsRogue\Content\saves\";



    /**=============================================================
     *= LOAD METHOD ================================================
     *============================================================*/

    // RETURNS current map id
    public static int Load()
    {
        var saveFile = new XmlDocument();
        saveFile.Load(GetFullFilePath());
        var asset = saveFile["XnaContent"]?["Asset"];
        var character = Main.Character;

        XmlNode node = asset["character"];
        character.Name = GetString(node["name"]);
        character.Life = GetInt(node["life"]);
        character.Souls = GetInt(node["souls"]);
        character.IsHuman = GetBool(node["isHuman"]);

        node = asset["character"]["position"];
        character.SetPosition(GetInt(node["x"]), GetInt(node["y"]));
        character.Orientation = Character.OrientationFromId(GetInt(node["orientation"]));

        node = asset["character"]["attributes"];
        var attributes = new int[Attributes.NumAttributes];
        for (var i = 0; i < Attributes.NumAttributes; i++)
            attributes[i] = GetInt(node.ChildNodes[i]);
        character.Attributes.Set(attributes);
        
        node = asset["character"]["triggers"];
        var triggers = new bool[Triggers.NumTriggers];
        for (var i = 0; i < Triggers.NumTriggers; i++)
            triggers[i] = GetBool(node.ChildNodes[i]);
        character.Triggers.Set(triggers);

        node = asset["character"]["inventory"];
        character.ChangeArmor(Armor.GetFromIndex(GetInt(node["equippedArmor"])));
        character.ChangeRing(Ring.GetFromIndex(GetInt(node["equippedRing"])));
        character.ChangeWeapon(Weapon.GetFromIndex(GetInt(node["equippedWeapon"])));

        node = asset["objects"];
        for (var i = 0; i < node.ChildNodes.Count; i++)
            Maps.GetObjectFromId(i).SetState(GetInt(node.ChildNodes[i]));
        
        return GetInt(asset["map"]);
    }
    
    
    
    /**=============================================================
     *= SAVE METHOD ================================================
     *============================================================*/
    
    public static void Save()
    {
        var character = Main.Character;
        var doc = new XmlDocument();
        doc.Load(GetFullFilePath());
        XmlNode asset = doc["XnaContent"]["Asset"];

        asset["map"].InnerText = Main.CurrentMap.Id.ToString();

        XmlNode node = asset["character"];
        node["name"].InnerText = character.Name;
        node["life"].InnerText = character.Life.ToString();
        node["souls"].InnerText = character.Souls.ToString();
        node["isHuman"].InnerText = character.IsHuman.ToString();
        
        node = asset["character"]["position"];
        node["x"].InnerText = character.GetPosition().X.ToString();
        node["y"].InnerText = character.GetPosition().Y.ToString();
        node["orientation"].InnerText = Character.OrientationId(character.Orientation).ToString();

        node = asset["character"]["attributes"];
        for (var i = 0; i < Attributes.NumAttributes; i++)
            node.ChildNodes[i].InnerText = character.Attributes.GetAll()[i].ToString();
        
        node = asset["character"]["triggers"];
        for (var i = 0; i < Triggers.NumTriggers; i++)
            node.ChildNodes[i].InnerText = character.Triggers.GetAll()[i].ToString();
        
        node = asset["character"]["inventory"];
        node["equippedArmor"].InnerText = Armor.GetIndexOf(character.Inventory.EquippedArmor).ToString();
        node["equippedRing"].InnerText = Ring.GetIndexOf(character.Inventory.EquippedRing).ToString();
        node["equippedWeapon"].InnerText = Weapon.GetIndexOf(character.Inventory.EquippedWeapon).ToString();
        
        node = asset["objects"];
        for (var i = 0; i < node.ChildNodes.Count; i++)
            node.ChildNodes[i].InnerText = Maps.GetObjectFromId(i).GetState().ToString();
        
        doc.Save(GetFullFilePath());
    }

    private static string GetFullFilePath() => SavesFilePath + "save" + Main.SaveId + ".xml";

    private static int GetInt(XmlNode node) => int.Parse(node.InnerText);
    private static bool GetBool(XmlNode node) => bool.Parse(node.InnerText);
    private static string GetString(XmlNode node) => node.InnerText;

}