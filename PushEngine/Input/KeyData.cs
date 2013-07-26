using System;
using OpenTK;
using OpenTK.Input;


namespace PushEngine.Input
{
    public class KeyData
    {
        public enum KeyState
        {
            NotPressed = 0,
            Pressed = 1,
            Pressing = 2,
            Released = 3
        }

        internal Key KeyId;
        public KeyState keyState { get; private set; }

        internal KeyData(Key keyValue)
        {
            KeyId = keyValue;
            keyState = KeyState.NotPressed;
        }

        internal void ApplyKeyDown(KeyboardKeyEventArgs e)
        {
            keyState = KeyState.Pressed;
        }

        internal void ApplyKeyUp(KeyboardKeyEventArgs e)
        {
            keyState = KeyState.Released;
        }

        internal void UpdateIdle()
        {
            switch (keyState)
            {
                case KeyState.NotPressed:
                    // Should not happen
                    break;
                case KeyState.Pressed:
                    keyState = KeyState.Pressing;
                    break;
                case KeyState.Pressing:
                    // Maintain the state
                    break;
                case KeyState.Released:
                    keyState = KeyState.NotPressed;
                    break;
                default:
                    break;
            }
        }

    }
}
