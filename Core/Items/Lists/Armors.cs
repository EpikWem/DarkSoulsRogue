using System.Collections.Generic;

namespace DarkSoulsRogue.Core.Items.Lists;

public static class Armors
{

    public static Armor GetFromIndex(int i) => Values[i];
    public static int GetIndexOf(Armor armor) => Values.IndexOf(armor);

    public static readonly Armor
        Naked = new (""),
        Artorias = new ("Artorias"),
        //BlackIron = new ("Black Iron Armor"),
        Solaire = new ("Solaire");

    private static readonly List<Armor> Values =
    new () {
        Naked,
        Artorias,
        //BlackIron,
        Solaire
    };

}