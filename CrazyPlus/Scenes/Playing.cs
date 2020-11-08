using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyPlus.Scenes
{
    public class Playing : Scene
    {
        public Playing(Engine engine) : base("playing", engine)
        {
            BackgroundMusic = new Audio(Engine.Ressources("bk_music.wav"));
        }

        Audio BackgroundMusic { get; set; }
        Grid Grid { get; set; }
        public Score Score { get; set; }

        public override bool OnCreate()
        {
            Grid = new Grid(this.Engine);
            BackgroundMusic.SetVolume(5);
            BackgroundMusic.Play(true);
            return base.OnCreate();
        }

        public override bool OnUpdate(double ElapsedTime)
        {
            ClickedKeys(ElapsedTime);
            Drawing();
            if (!Grid.GameOn())
                Engine.Drawer.String("Game Over! Click Enter", "Consolas", 12, Color.White, new PointF(100,100));
            return base.OnUpdate(ElapsedTime);
        }

        public override bool OnDestroy()
        {
            BackgroundMusic.StopPlaying();
            return base.OnDestroy();
        }

        private float FrameTimer;

        public void ClickedKeys(double ElapsedTime)
        {
            FrameTimer += (float)ElapsedTime;
            if (FrameTimer >= 0.1f)
            {
                FrameTimer -= 0.1f;
                if (Grid.GameOn())
                {
                    if (Engine.KeyClicked(System.Windows.Forms.Keys.Down))
                        Grid.MoveDown();

                    if (Engine.KeyClicked(System.Windows.Forms.Keys.Up))
                        Grid.MoveUp();

                    if (Engine.KeyClicked(System.Windows.Forms.Keys.Left))
                        Grid.MoveLeft();

                    if (Engine.KeyClicked(System.Windows.Forms.Keys.Right))
                        Grid.MoveRight();
                }

                if (Engine.KeyClicked(System.Windows.Forms.Keys.Space))
                {
                    if (BackgroundMusic.IsBeingPlayed)
                        BackgroundMusic.StopPlaying();
                    else
                        BackgroundMusic.Play(true);
                }

                if (!Grid.GameOn())
                {
                    if (Engine.KeyClicked(System.Windows.Forms.Keys.Enter))
                    {
                        Score = new Score(this.Engine, Grid.Score);
                        Engine.RegisterScene(Score);
                        Engine.GoToScene(Score);
                    }
                }
            }

            if (Engine.KeyClicked(System.Windows.Forms.Keys.Escape))
            {
                Menu menu = new Menu(Engine, this);
                Engine.GoToScene(menu);
            }
        }

        public void Drawing()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if(Grid.Tiles[x,y] == null)
                    {
                        Engine.Drawer.Rectangle(x * 100, y * 100, 100, 100, Color.Gray, true);
                        Engine.Drawer.Rectangle(x * 100, y * 100, 100, 100, Color.DarkGray, false);
                    }
                    else
                    {
                        SizeF size = new SizeF(20, 20);
                        int color = Grid.Tiles[x, y].Value;
                        if(Grid.Tiles[x, y].Value > 255)
                            color = 255;
                        Engine.Drawer.Rectangle(x * 100, y * 100, 100, 100, Color.FromArgb(221, 54, color), true);
                        if (Grid.Tiles[x, y] != null)
                        {
                            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                            {
                                size = graphics.MeasureString(Grid.Tiles[x, y].Value.ToString(), new Font("Consolas", 18, FontStyle.Regular, GraphicsUnit.Point));
                            }

                            Engine.Drawer.String(Grid.Tiles[x, y].Value.ToString(), "Consolas", 18, Color.White, new PointF((x * 100) + (size.Width / 2), (y * 100) + size.Height));
                        }
                    }
                }
            }
        }
    }
}
