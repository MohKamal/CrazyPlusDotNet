using CrazyPlus.Scenes;
using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrazyPlus
{
    public class Game : Engine
    {
        public int Score { get; set; }
        public Game(PictureBox drawingArea) : base(drawingArea)
        {
            CalculeFPS = false;
            DisplayFPS = false;
            Score = 0;
        }

        public Scenes.Menu menu { get; set; }
        public override bool OnCreate()
        {
            menu = new Scenes.Menu(this, null);
            RegisterScene(menu);
            GoToScene(menu);
            return base.OnCreate();
        }

        public override bool OnUpdate(double ElapsedTime)
        {
            return base.OnUpdate(ElapsedTime);
        }
    }
}
