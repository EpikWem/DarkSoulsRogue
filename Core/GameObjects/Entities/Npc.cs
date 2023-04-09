using System.Collections.Generic;
using DarkSoulsRogue.Core.Interfaces;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarkSoulsRogue.Core.GameObjects.Entities;

public class Npc : Mob
{

    private readonly Orientation _baseOrientation;
    private bool _isHostile;
    private readonly List<string> _speaks;
    private int _speakingStage;
    public bool IsSpeaking;

    public Npc(Vector2 positionOnGrid, Texture2D[] textures, Hitbox hitbox, Orientation baseOrientation, List<string> speaks) : base(textures, hitbox)
    {
        PlaceOnGrid(positionOnGrid.X, positionOnGrid.Y, baseOrientation);
        _baseOrientation = baseOrientation;
        _isHostile = false;
        IsSpeaking = false;
        _speaks = speaks;
        _speakingStage = 0;
        Orientation = _baseOrientation;
    }

    public void Interact(Character character)
    {
        if (_isHostile)
            return;
        Speak(character);
    }

    private void Speak(Character character)
    {
        if (!IsSpeaking)
        {
            Orientation = character.Orientation.Opposite();
            ChatBox.Say(_speaks[_speakingStage]);
            IsSpeaking = true;
        }
        else
        {
            if (!Controls.Interact.IsOnePressed)
                return;
            if (_speakingStage >= _speaks.Count-1)
            {
                ChatBox.Clear();
                Orientation = _baseOrientation;
                IsSpeaking = false;
                return;
            }
            _speakingStage++;
            ChatBox.Say(_speaks[_speakingStage]);
        }
    }

}