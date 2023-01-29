using Microsoft.Xna.Framework.Input;

namespace DarkSoulsRogue;

public abstract class Controls
{
    public class Control
    {
        public readonly Keys KeyCode;
        public bool IsPressed;

        public Control(Keys keyCode)
        {
            KeyCode = keyCode;
            IsPressed = false;
        }
    }
    
    public static readonly Control
        KillApp = new Control(Keys.F10),
        ToggleFullscreen = new Control(Keys.F11),
        Pause = new Control(Keys.Escape),
        Up = new Control(Keys.Up),
        Down = new Control(Keys.Down),
        Right = new Control(Keys.Right),
        Left = new Control(Keys.Left),
        Interact = new Control(Keys.E),
        Run = new Control(Keys.Space);
    
    public static readonly Control[] Array = { KillApp, ToggleFullscreen, Pause, Up, Down, Right, Left, Interact, Run };

    public static void UpdateKeyListener()
    {
        foreach (Control c in Array) {
            c.IsPressed = Keyboard.GetState().IsKeyDown(c.KeyCode);
        }
    }

}