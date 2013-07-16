using System;
using System.Collections.Generic;
using PushEngine.Draw;

namespace PushEngine
{
    public class DebugVars : Client
    {
        private Dictionary<string, string> dVars = new Dictionary<string, string>();
        List<TextLabel> labels = new List<TextLabel>();

        internal void AddVar(string name_, object obj)
        {
            dVars.Add(name_, obj.ToString());
        }

        internal void AddVar(string name_, ValueType obj)
        {
            dVars.Add(name_, obj.ToString());
        }

        public override void Start()
        {
            base.Start();
            Scene scene = context.sceneDirector.GetNewAndPush();
        }

        private int a = 387658765;
        public override void Update()
        {
            base.Update();
            AddVar("abc", a);
        }
        public override void  Render()
        {
            base.Render();

            while (labels.Count < dVars.Count)
            {
                TextLabel label = context.sceneDirector.CurrentScene.GetNewDrawElement<TextLabel>();
                label.setContextProperty("index", labels.Count);
                label.OnCreationCompleted += delegate()
                {
                    int pos = (int)label.getAndRemoveContextProperty("index");
                    label.setLeftPosition(context.viewPort.Left);
                    label.setTopPosition(context.viewPort.Top + (20 * pos));
                };
                labels.Add(label);
            }

            int index = 0;
            foreach (KeyValuePair<string, string> kvp in dVars)
            {
                labels[index].Text = kvp.Key + ": " + kvp.Value;
            }

            dVars.Clear();
        }
    }
}
