namespace DarkSoulsRogue.Core.Items;

public class Mineral : Item
{
    
    public static Mineral
        TitaniteShard = new("Titanite Shard"),
        TitaniteShardLarge = new("Large Titanite Shard"),
        TitaniteChunk = new("Titanite Chunk"),
        TitaniteSlab = new("Titanite Slab"),
        
        GreenTitanite = new("Green Titanite Shard"),
        BlueTitanite = new("Blue Titanite Chunk"),
        BlueTitaniteSlab = new("Blue Titanite Slab"),
        RedTitanite = new("Red Titanite Chunk"),
        RedTitaniteSlab = new("Red Titanite Slab"),
        WhiteTitanite = new("White Titanite Chunk"),
        WhiteTitaniteSlab = new("White Titanite Slab"),

        TwinklingTitanite = new("Twinkling Titanite"),
        DragonScale = new("Dragon Scale"),
        DemonTitanite = new("DemonTitanite");
    
    public Mineral(string name) : base(name, Categories.Mineral)
    {
        
    }
    
}