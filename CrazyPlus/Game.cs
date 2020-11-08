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
    /// <summary>
    /// Main game class
    /// </summary>
    public class Game : Engine
    {
        /// <summary>
        /// Game score
        /// </summary>
        public int Score { get; set; }
        public Game(PictureBox drawingArea) : base(drawingArea)
        {
            CalculeFPS = false;
            DisplayFPS = false;
            Score = 0;
        }

        /// <summary>
        /// First scene to be displayed
        /// </summary>
        public Scenes.Menu menu { get; set; }
        /// <summary>
        /// Init the objects
        /// </summary>
        /// <returns></returns>
        public override bool OnCreate()
        {
            menu = new Scenes.Menu(this, null);
            RegisterScene(menu);
            GoToScene(menu);
            return base.OnCreate();
        }

        /// <summary>
        /// No need to this, because every scene has its logic
        /// </summary>
        /// <param name="ElapsedTime"></param>
        /// <returns></returns>
        public override bool OnUpdate(double ElapsedTime)
        {
            return base.OnUpdate(ElapsedTime);
        }
    }
}
