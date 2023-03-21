namespace DarkSoulsRogue.Core.Items;

public class Key : Item
{

    public static readonly Key
        AsylumFloor = new ("Undead Asylum Floor Key"),
        AsylumStage1 = new ("Undead Asylum Floor Key"),
        AsylumStage2 = new ("Undead Asylum Floor Key"),
        AsylumGuardian = new ("Guardian Key");

    public Key(string name) : base(name, Categories.Key)
    {
        
    }
    
}