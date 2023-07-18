namespace DarkSoulsRogue.Core.Interfaces;

public static class MapCreator
{
    
    private const string FileName = "indexes.txt";
    private static readonly Image
    
    public static void Run()
    {
        Console.WriteLine("[Dark Souls Pixel - Map Creator Utility]");
        Console.WriteLine("Name your index array file \"" + FileName + "\"");
        Console.WriteLine("Then press Enter to make an image of it...");
        Console.Read();

        var firstarray = File.ReadAllLines(FileName); // split lines

        var secondarray = Array.Empty<string[]>();
        for (var i = 0; i < firstarray.Length; i++)
            secondarray[i] = firstarray[i].Split(' '); // split rows in each lines

        var array = Array.Empty<int[]>();
        for (var y = 0; y < secondarray.Length; y++)
        for (var x = 0; x < secondarray[0].Length; x++)
            array[x][y] = int.Parse(secondarray[x][y]); // parse int
        
        

    }
    
}