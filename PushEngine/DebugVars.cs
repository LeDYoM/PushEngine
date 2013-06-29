using System;
using System.Collections.Generic;
using PushEngine.Draw;

namespace PushEngine
{
    public class DebugVars : PEClient
    {
        private Dictionary<string, string> dVars = new Dictionary<string, string>();
        List<TextLabel> labels = new List<TextLabel>();
        Scene scene = new Scene();

        internal void AddVar(string name_, object obj)
        {
            dVars.Add(name_, obj.ToString());
        }

        internal void AddVar(string name_, ValueType obj)
        {
            dVars.Add(name_, obj.ToString());
        }

        public override void  Render()
        {
            base.Render();
            while (labels.Count < dVars.Count)
            {
                    TextLabel label = scene.GetNewDrawElement<TextLabel>();
                    label.PostInit();
                    label.setTopPosition(-400 + (20 * labels.Count));
                    labels.Add(label);
            }

            int index = 0;
            foreach (KeyValuePair<string, string> kvp in dVars)
            {
                labels[index].Text = kvp.Key + ": " + kvp.Value;
                labels[index].setLeftPosition(0);
            }

            dVars.Clear();

            scene.Render();

        }
    }
}
