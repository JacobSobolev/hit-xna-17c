using Microsoft.Xna.Framework.Input;

namespace C17Ex01
{
    public static class InputComponent
    {
        public static KeyboardState KeyBoardCurrentState { get; private set; }

        public static KeyboardState KeyBoardPreviousState { get; private set; }

        public static MouseState MouseCurrentState { get; private set; }

        public static MouseState MousePreviousState { get; private set; }

        public static void GetCurrentState()
        {
            MouseCurrentState = Mouse.GetState();
            KeyBoardCurrentState = Keyboard.GetState();
        }

        public static void MoveOnFromCurrentState()
        {
            MousePreviousState = MouseCurrentState;
            KeyBoardPreviousState = KeyBoardCurrentState;
        }
    }
}
