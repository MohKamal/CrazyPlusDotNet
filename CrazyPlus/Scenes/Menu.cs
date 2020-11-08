using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyPlus.Scenes
{
    public class Menu : Scene
    {
        Playing Game { get; set; }
        public Menu(Engine engine, Playing game) : base("Main Menu", engine)
        {
            UpDown = new Audio(engine.Ressources("click_1.wav"));
            Game = game;
        }

        Audio UpDown;
        public SpriteSheet StartBtnOn { get; set; }
        public SpriteSheet StartBtnOff { get; set; }
        public GameObject StartBtn { get; set; }
        public GameObject RestartBtn { get; set; }
        public SpriteSheet RestartBtnOn { get; set; }
        public SpriteSheet RestartBtnOff { get; set; }
        public GameObject BackgroundObj { get; set; }
        public Playing game { get; set; }

        public Sprite Background { get; set; }

        public override bool OnCreate()
        {
            StartBtnOff = new SpriteSheet("start_btn_off", 200, 100, 1, 0, 0, CrazyPlus.Properties.Resources.Start);
            StartBtnOn = new SpriteSheet("start_btn_on", 200, 100, 1, 1, 1, CrazyPlus.Properties.Resources.Start);

            RestartBtnOn = new SpriteSheet("restart_btn_off", 98, 98, 1, 0, 0, CrazyPlus.Properties.Resources.restart);
            RestartBtnOff = new SpriteSheet("restart_btn_on", 98, 98, 1, 1, 1, CrazyPlus.Properties.Resources.restart);

            StartBtn = new GameObject(new System.Drawing.PointF((Engine.ScreenWidth() / 2) - 100, (Engine.ScreenHeight() / 2) - 100), StartBtnOff);
            StartBtn.Animations.RegisterAnimation(StartBtnOff);
            StartBtn.Animations.RegisterAnimation(StartBtnOn);

            RestartBtn = new GameObject(new System.Drawing.PointF((Engine.ScreenWidth() / 2) - 49, (Engine.ScreenHeight() / 2)), RestartBtnOff);
            RestartBtn.Animations.RegisterAnimation(RestartBtnOff);
            RestartBtn.Animations.RegisterAnimation(RestartBtnOn);

            Background = new Sprite(400, 400);
            Background.LoadFromFile(CrazyPlus.Properties.Resources.bk);
            this.Layers[0].RegisterGameObject(StartBtn);
            this.Layers[0].RegisterGameObject(RestartBtn);

            if (Game == null)
                RestartBtn.Hide();

            return base.OnCreate();
        }

        public override bool OnUpdate(double ElapsedTime)
        {
            Engine.Drawer.Sprite(new PointF(0, 0), Background);
            if (Engine.MouseOnTopOf(StartBtn))
                StartBtn.SetAnimation("start_btn_on");
            else
                StartBtn.SetAnimation("start_btn_off");

            if (Engine.MouseOnTopOf(RestartBtn))
                RestartBtn.SetAnimation("restart_btn_on");
            else
                RestartBtn.SetAnimation("restart_btn_off");

            if (Engine.MouseOnTopOf(StartBtn))
            {
                if (Engine.MouseClicked(System.Windows.Forms.MouseButtons.Left))
                {
                    UpDown.Play(false);
                    if (Game == null)
                        game = new Playing(this.Engine);
                    else
                        game = Game;
                    Engine.RegisterScene(game);
                    Engine.GoToScene(game);
                }
            }

            if (Engine.MouseOnTopOf(RestartBtn))
            {
                if (Engine.MouseClicked(System.Windows.Forms.MouseButtons.Left))
                {
                    UpDown.Play(false);
                    game = new Playing(this.Engine);
                    Engine.RegisterScene(game);
                    Engine.GoToScene(game);
                }
            }

            if (Engine.KeyClicked(System.Windows.Forms.Keys.Space))
            {
                UpDown.Play(false);
                StartBtn.SetAnimation("start_btn_on");
                if (Game == null)
                    game = new Playing(this.Engine);
                else
                    game = Game;
                Engine.RegisterScene(game);
                Engine.GoToScene(game);

            }

            Engine.Drawer.String("Use Space bar to Stop music", "Arial", 8, System.Drawing.Color.Black, new PointF(20, Engine.ScreenHeight() - 50));
            Engine.Drawer.String("Use Escape to go back to this menu", "Arial", 8, System.Drawing.Color.Black, new PointF(20, Engine.ScreenHeight() - 40));


            Engine.Drawer.String("Powred by CSharpGame | https://github.com/MohKamal/CsharpGame", "Arial", 8, System.Drawing.Color.Black, new PointF(20, Engine.ScreenHeight() - 20));

            return base.OnUpdate(ElapsedTime);
        }
    }
}
