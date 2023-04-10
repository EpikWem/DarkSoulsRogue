using System.Xml;
using Microsoft.Xna.Framework.Content;

namespace DarkSoulsRogue.Core.Utilities;

public static class Langs
{
    
    private const string LangsPath = Main.ContentPath + @"langs\";

    public static XmlDocument En = new(), Fr = new();

    public static void Init(ContentManager content)
    {
        En.Load(LangFilePath("en"));
        Fr.Load(LangFilePath("fr"));
    }
    
    //TODO: get text from path

    private static string LangFilePath(string lang) => LangsPath + lang + ".xml";

}