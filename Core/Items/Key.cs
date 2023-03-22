namespace DarkSoulsRogue.Core.Items;

public class Key : Item
{

    public static readonly Key
        AsylumCell = new("Key of the Cell"),
        AsylumFloor = new ("Undead Asylum Floor Key"),
        AsylumStage1 = new ("Undead Asylum S1 Key"),
        AsylumStage2 = new ("Undead Asylum S2 Key"),
        AsylumGuardian = new ("Guardian Key");

    public Key(string name) : base(name, Categories.Key)
    {
        
    }
    
}