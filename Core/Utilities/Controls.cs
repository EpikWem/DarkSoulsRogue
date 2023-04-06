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
        MenuBack = new (Keys.A),
        Enter = new (Keys.Enter),
        Debug1 = new (Keys.NumPad1),
        Debug2 = new (Keys.NumPad2),
        Debug3 = new (Keys.NumPad3),
        A = new (Keys.A),
        B = new (Keys.B),
        C = new (Keys.C),
        D = new (Keys.D),
        E = new (Keys.E),
        F = new (Keys.F),
        G = new (Keys.G),
        H = new (Keys.H),
        I = new (Keys.I),
        J = new (Keys.J),
        K = new (Keys.K),
        L = new (Keys.L),
        M = new (Keys.M),
        N = new (Keys.N),
        O = new (Keys.O),
        P = new (Keys.P),
        Q = new (Keys.Q),
        R = new (Keys.R),
        S = new (Keys.S),
        T = new (Keys.T),
        U = new (Keys.U),
        V = new (Keys.V),
        W = new (Keys.W),
        X = new (Keys.X),
        Y = new (Keys.Y),
        Z = new (Keys.Z),
        D6 = new (Keys.D6),
        D8 = new (Keys.D8);
    private static readonly Control[] Array = { KillApp, ToggleFullscreen, Pause, Up, Down, Right, Left, Interact, Run, MenuUp, MenuDown, MenuRight, MenuLeft, MenuBack, Enter, Debug1, Debug2, Debug3, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, D6, D8 };

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