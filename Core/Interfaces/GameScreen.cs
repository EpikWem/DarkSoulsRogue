using System.Collections.Generic;
using System.Linq;
using DarkSoulsRogue.Core.GameObjects;
using DarkSoulsRogue.Core.GameObjects.Entities;
using DarkSoulsRogue.Core.GameObjects.InteractiveObjects;
using DarkSoulsRogue.Core.Statics;
using DarkSoulsRogue.Core.System;
using DarkSoulsRogue.Core.Utilities;
using Microsoft.Xna.Framework;

namespace DarkSoulsRogue.Core.Interfaces;

public static class GameScreen
{

    public static int CurrentSaveId;
    private static Background _background;
    public static Character Character;
    private static readonly List<Wall> Walls = new();
    private static Map _currentMap;

    public static void Init()
    {
        CurrentSaveId = 0;
        _background = new Background();
        Character = new Character();
    }

    public static void Update()
    {
        if (Control.Debug1.IsOnePressed())
            Character.Attributes.Increase(Attributes.Attribute.Humanity, 1);
        
        if (Character.IsSpeaking) // if player is speaking to a Npc, skip entities update
        {
            var npc = (Npc)_currentMap.Entities.First(e => e.GetType() == typeof(Npc) && ((Npc)e).IsSpeaking);
            npc.Interact(Character);
            return;
        }
        
        if (Notification.IsActive()) // if a notification is displayed, no other interaction
        {
            Notification.Update();
            return;
        }
        
        if (Bonfire.Menu.IsActive()) // if player is resting to Bonfire, skip entities update
        {
            Bonfire.Update(Character);
            return;
        }
        
        // character position updates
        Character.Move(GetCollisionsList());
        Character.TransitMap(Character.TestOutOfMap());

        // update IngameMenu if displayed
        if (IngameMenu.IsActive())
        {
            IngameMenu.Update();
            return;
        }
        // activate menu if pause is pressed
        if (Control.Pause.IsOnePressed())
            IngameMenu.Activate();

        // interactions with InteractiveObjects and Npc 
        if (Control.Interact.IsOnePressed())
        {
            foreach (var o in _currentMap.Objects.Where(o => o.GetHitbox().Contains(Character.GetLookingPoint())))
                o.Interact();
            foreach (var entity in _currentMap.Entities.Where(e => e.GetType() == typeof(Npc) && e.GetGraphicbox().Contains(Character.GetLookingPoint())))
                ((Npc)entity).Interact(Character);
        }
            
        // shield key test
        Character.ShieldUp = Control.Shield.IsPressed();
    }
    
    public static void Draw()
    {   
        _background.Draw(Main.SpriteBatch);
        if (Main.DrawWalls)
            foreach (var w in Walls)
                w.Draw(Main.SpriteBatch);
        foreach (var o in _currentMap.Objects)
            o.Draw(Main.SpriteBatch);
        foreach (var e in _currentMap.Entities)
            e.Draw(Main.SpriteBatch);
        Character.Draw(Main.SpriteBatch);
        Ath.Draw();
        if (IngameMenu.IsActive())
            IngameMenu.Draw();
        if (Bonfire.Menu.IsActive())
            Bonfire.Menu.Draw();
        if (BigMessage.IsActive())
            BigMessage.Draw();
        if (Notification.IsActive())
            Notification.Draw();
        ChatBox.Draw();
    }
    
    public static void LoadMap(int mapId)
    {
        _currentMap = Map.GetMap(mapId);
        Walls.Clear();
        for (var y = 0; y < _currentMap.Height; y++)
        for (var x = 0; x < _currentMap.Width; x++)
            if (_currentMap.WallsIds[y][x] != 0)
                Walls.Add(new Wall(x, y, Textures.WallsT[_currentMap.WallsIds[y][x]]));
    }

    public static Map CurrentMap() => _currentMap;

    private static List<Rectangle> GetCollisionsList()
    {
        var result = Walls.Select(w => w.GetHitbox()).ToList();
        result.AddRange(_currentMap.Objects
            .Where(
                o => !((o.GetType() == typeof(Door) || o.GetType() == typeof(LockedDoor) || o.GetType() == typeof(OnewayDoor))
                       && o.GetState() == 1))
            .Select(w => w.GetHitbox()).ToList());
        result.AddRange(_currentMap.Entities.Select(e => e.GetHitbox()).ToList());
        return result;
    }
    
}