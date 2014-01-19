using System;
using System.Collections.Generic;
using PushEngine.Draw;
using OpenTK.Graphics;
using PushEngine.Input;
using PushEngine.Events;

namespace PushEngine.Demos.Zooper
{
    internal class Zooper : Client
    {
        internal Zooper()
            : base()
        {
        }

        public override void Start()
        {
            base.Start();

            sceneDirector.GetNewSceneAndPush<GameScene>("gamePlay" );

        }
        public override string Name()
        {
            return "Blocker";
        }
    }
}
