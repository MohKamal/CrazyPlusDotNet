using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyPlus.Scenes
{
    public class Score : Scene
    {
        Playing game { get; set; }
        public int ScoreValue { get; set; }
        public Score(Engine engine, int score) : base("score", engine)
        {
            ScoreValue = score;
        }

        public override bool OnUpdate(double ElapsedTime)
        {
            Engine.Drawer.Clear(System.Drawing.Color.LightGoldenrodYellow);
            Engine.Drawer.String("Your Score Is : ", "Arial", 24, System.Drawing.Color.Black, new System.Drawing.PointF((Engine.ScreenWidth() / 2) - 100, (Engine.ScreenHeight() / 2) - 40));
            Engine.Drawer.String($"{ScoreValue}", "Arial", 24, System.Drawing.Color.Black, new System.Drawing.PointF((Engine.ScreenWidth() / 2) - 100, (Engine.ScreenHeight() / 2)));
            if (Engine.KeyClicked(System.Windows.Forms.Keys.Space))
            {
                game = new Playing(this.Engine);
                Engine.RegisterScene(game);
                Engine.GoToScene(game);
            }
            return base.OnUpdate(ElapsedTime);
        }
    }
}
