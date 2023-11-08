namespace DarkSoulsRogue.Core.Utilities;

public class Triggers
{

    public enum Trigger
    {
        FinishedFirstAsylum, HasRiteOfKindling, HasTeleportationSkill, UnlockedGoldenBarriers, HasArtoriasRing,
        BeatNito, BeatSeath, BeatFourKings, BeatBedOfChaos
    }

    public const int NumTriggers = 9;

    private readonly bool[] _values = new bool[NumTriggers];

    public Triggers()
    {
        for (int i = 0; i < NumTriggers; i++)
            _values[i] = false;
    }

    public bool[] GetAll()
        => _values;
    public bool Get(Trigger trigger)
        => _values[(int)trigger];

    public void Activate(Trigger trigger)
        => _values[(int)trigger] = true;
    
    public void Set(bool[] values)
    {
        for (var i = 0; i < NumTriggers; i++)
            _values[i] = values[i];
    }

}