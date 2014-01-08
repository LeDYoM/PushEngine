using System;
using OpenTK.Input;
using System.Collections.Generic;

namespace PushEngine.Input
{
    public class Keyboard
    {
        private KeyData[] keyData = new KeyData[(int)Key.LastKey];
        private KeyboardDevice keyboardReference = null;
        private List<KeyData> active = new List<KeyData>();

        internal void setKeyboard(KeyboardDevice keyboardReference_)
        {
            keyboardReference = keyboardReference_;

            for (int i = 0; i < keyData.Length; ++i)
            {
                keyData[i] = new KeyData((Key)i);
            }
        }

        public void ApplyKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            keyData[(int)e.Key].ApplyKeyDown(e);
            active.Add(keyData[(int)e.Key]);
        }

        public void ApplyKeyUp(object sender, KeyboardKeyEventArgs e)
        {
            keyData[(int)e.Key].ApplyKeyUp(e);
        }

        public void ApplyUpdate()
        {
            List<KeyData> temp = new List<KeyData>();

            foreach (KeyData kd in active)
            {
                kd.UpdateIdle();
                if (kd.KState != KeyData.KeyState.NotPressed)
                {
                    temp.Add(kd);
                    PushEngineCore.Instance.clientManager.OnKey(new Events.KeyEventData(kd));
                }
            }
            active = temp;

        }

        internal List<KeyData> ActiveKeys { get { return active; } }
    }
}
