using System.Collections.Generic;

namespace DarkSoulsRogue.Core.Statics;

public class Orientation
{
    
    public static readonly Orientation
        Up = new(0),
        Down = new(1),
        Right = new(2),
        Left = new(3),
        Null = new(-1);

    public readonly int Index;

    private Orientation(int index) => Index = index;
    
    public static Orientation FromInt(int i) => i switch
    {
        0 => Up,
        1 => Down,
        2 => Right,
        3 => Left,
        _ => Null
    };
    
    public Orientation Opposite() => Index switch
    {
        0 => Down,
        1 => Up,
        2 => Left,
        3 => Right,
        _ => Null
    };

}
