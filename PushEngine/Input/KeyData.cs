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

        public readonly Key KeyId;
        public KeyState KState { get; private set; }

        internal KeyData(Key keyValue)
        {
            KeyId = keyValue;
            KState = KeyState.NotPressed;
        }

        internal void ApplyKeyDown(KeyboardKeyEventArgs e)
        {
            KState = KeyState.Pressed;
        }

        internal void ApplyKeyUp(KeyboardKeyEventArgs e)
        {
            KState = KeyState.Released;
        }

        internal void UpdateIdle()
        {
            switch (KState)
            {
                case KeyState.NotPressed:
                    // Should not happen
                    break;
                case KeyState.Pressed:
                    KState = KeyState.Pressing;
                    break;
                case KeyState.Pressing:
                    // Maintain the state
                    break;
                case KeyState.Released:
                    KState = KeyState.NotPressed;
                    break;
                default:
                    break;
            }
        }

    }
}
