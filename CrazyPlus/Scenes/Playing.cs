using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyPlus.Scenes
{
    /// <summary>
    /// Playing scene is the main Game scene, where the Grid is
    /// </summary>
    public class Playing : Scene
    {
        Audio UpDown { get; set; }
        Audio LeftRight { get; set; }
        Audio BackgroundMusic { get; set; }
        Grid Grid { get; set; }
        // Score Scene
        public Score Score { get; set; }

        /// <summary>
        /// Init the scene and Audio objects
        /// </summary>
        /// <param name="engine"></param>
        public Playing(Engine engine) : base("playing", engine)
        {
            BackgroundMusic = new Audio(Engine.Ressources("bk_music.wav"));
            UpDown = new Audio(engine.Ressources("click_1.wav"));
            LeftRight = new Audio(engine.Ressources("click_2.wav"));
        }

        /// <summary>
        /// Init the Grid
        /// </summary>
        /// <returns></returns>
        public override bool OnCreate()
        {
            Grid = new Grid();
            BackgroundMusic.SetVolume(5);
            return base.OnCreate();
        }

        /// <summary>
        /// Check key inputs, drawing and game stop condition
        /// </summary>
        /// <param name="ElapsedTime"></param>
        /// <returns></returns>
        public override bool OnUpdate(double ElapsedTime)
        {
            ClickedKeys(ElapsedTime);
            Drawing();
            if (!Grid.GameOn())
                Engine.Drawer.String("Game Over! Click Enter", "Consolas", 12, Color.White, new PointF(100,100));
            return base.OnUpdate(ElapsedTime);
        }

        /// <summary>
        /// When the player go to other scene, if music is on, it will be stopped
        /// </summary>
        /// <returns></returns>
        public override bool OnDestroy()
        {
            BackgroundMusic.StopPlaying();
            return base.OnDestroy();
        }

        // Using this to prevent player from fast clicking
        private float FrameTimer;

        /// <summary>
        /// Check if the player click any key
        /// </summary>
        /// <param name="ElapsedTime"></param>
        public void ClickedKeys(double ElapsedTime)
        {
            //Prevent player from fast clicking
            FrameTimer += (float)ElapsedTime;
            if (FrameTimer >= 0.1f)
            {
                FrameTimer -= 0.1f;
                if (Grid.GameOn())
                {
                    if (Engine.KeyClicked(System.Windows.Forms.Keys.Down))
                    {
                        UpDown.Play(false);
                        Grid.MoveDown();
                    }

                    if (Engine.KeyClicked(System.Windows.Forms.Keys.Up))
                    {
                        UpDown.Play(false);
                        Grid.MoveUp();
                    }

                    if (Engine.KeyClicked(System.Windows.Forms.Keys.Left))
                    {
                        LeftRight.Play(false);
                        Grid.MoveLeft();
                    }

                    if (Engine.KeyClicked(System.Windows.Forms.Keys.Right))
                    {
                        LeftRight.Play(false);
                        Grid.MoveRight();
                    }
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
            //Go back to the menu
            if (Engine.KeyClicked(System.Windows.Forms.Keys.Escape))
            {
                Menu menu = new Menu(Engine, this);
                Engine.GoToScene(menu);
            }
        }

        /// <summary>
        /// Loop to draw the Grid
        /// </summary>
        public void Drawing()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    //If the Current tile is empty, draw empty square
                    if(Grid.Tiles[x,y] == null)
                    {
                        Engine.Drawer.Rectangle(x * 100, y * 100, 100, 100, Color.Gray, true);
                        Engine.Drawer.Rectangle(x * 100, y * 100, 100, 100, Color.DarkGray, false);
                    }
                    else //If not, draw the square with a color and a text
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
