using Microsoft.Xna.Framework.Input;

namespace DarkSoulsRogue.Core.Utilities;

public abstract class Controls
{
    
    public class Control
    {
        public readonly Keys KeyCode;
        public bool IsPressed;
        public bool IsOnePressed;

        public Control(Keys keyCode)
        {
            KeyCode = keyCode;
            IsPressed = false;
            IsOnePressed = false;
        }
    }

    public static readonly Control
        KillApp = new(Keys.F10),
        ToggleFullscreen = new(Keys.F11),
        Pause = new(Keys.Escape),
        Up = new(Keys.Z),
        Down = new(Keys.S),
        Right = new(Keys.D),
        Left = new(Keys.Q),
        Interact = new(Keys.E),
        Run = new(Keys.Space),
        MenuUp = new(Keys.Up),
        MenuDown = new(Keys.Down),
        MenuRight = new(Keys.Right),
        MenuLeft = new(Keys.Left),
        MenuBack = new Control(Keys.A),
        Debug1 = new (Keys.NumPad1),
        Debug2 = new (Keys.NumPad2),
        Debug3 = new (Keys.NumPad3);
    private static readonly Control[] Array = { KillApp, ToggleFullscreen, Pause, Up, Down, Right, Left, Interact, Run, MenuUp, MenuDown, MenuRight, MenuLeft, MenuBack, Debug1, Debug2, Debug3 };

    public static void UpdateKeyListener()
    {
        foreach (Control c in Array)
        {
            if (IsPressed(c.KeyCode))
            {
                c.IsOnePressed = !c.IsPressed;
                c.IsPressed = true;
            }
            else // key is down
            {
                c.IsPressed = false;
                c.IsOnePressed = false;
            }
        }
    }

    public static bool TestForMovementKey()
    {
        return Up.IsPressed || Down.IsPressed || Right.IsPressed || Left.IsPressed;
    }

    public static bool IsPressed(Keys key) => Keyboard.GetState().IsKeyDown(key);

}