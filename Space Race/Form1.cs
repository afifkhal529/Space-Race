using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Space_Race
{
    public partial class Form1 : Form
    {
        Rectangle hero = new Rectangle(250, 580, 10, 20);
        Rectangle hero2 = new Rectangle(350, 580, 10, 20);
        Rectangle divider = new Rectangle(300, 580, 8, 100);
        int herospeed = 8;

        Rectangle obstacleFinish = new Rectangle(0, 4, 600, 8);
        List<Rectangle> asteroids = new List<Rectangle>();
        List<Rectangle> asteroids2 = new List<Rectangle>();
        List<Rectangle> asteroids3 = new List<Rectangle>();
        List<Rectangle> asteroids4 = new List<Rectangle>();
        List<Rectangle> asteroids5 = new List<Rectangle>();
        List<Rectangle> asteroids6 = new List<Rectangle>();

        int p1Score = 0;
        int p2Score = 0;

        int time = 0;

        bool upArrow = false;
        bool downArrow = false;
        bool wButton = false;
        bool sButton = false;

        SolidBrush whiteBrush = new SolidBrush(Color.Honeydew);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush cyanBrush = new SolidBrush(Color.Cyan);
        SolidBrush greenBrush = new SolidBrush(Color.Lime);
        SolidBrush pinkBrush = new SolidBrush(Color.DeepPink);
        SolidBrush purpleBrush = new SolidBrush(Color.Purple);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);

        SoundPlayer gong = new SoundPlayer(Properties.Resources.metalGong);
        SoundPlayer beep = new SoundPlayer(Properties.Resources.beep);


        string gameState = "waiting";
        public Form1()
        {
            InitializeComponent();

            asteroids.Add(new Rectangle(0, 100, 20, 5));
            asteroids2.Add(new Rectangle(0, 280, 20, 5));
            asteroids3.Add(new Rectangle(0, 250, 25, 5));
            asteroids4.Add(new Rectangle(0, 150, 30, 5));
            asteroids5.Add(new Rectangle(0, 200, 20, 5));
            asteroids6.Add(new Rectangle(0, 50, 40, 5));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    downArrow = true;
                    break;
                case Keys.Up:
                    upArrow = true;
                    break;
                case Keys.W:
                    wButton = true;
                    break;
                case Keys.S:
                    sButton = true;
                    break;
                case Keys.Space:
                    if (gameState == "waiting" || gameState == "over" || gameState == "finish")

                    {
                        GameInitialize();
                    }
                    break;
                case Keys.Escape:
                    if (gameState == "waiting" || gameState == "finish")

                    {
                        Application.Exit();
                    }

                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    downArrow = false;
                    break;
                case Keys.Up:
                    upArrow = false;
                    break;
                case Keys.W:
                    wButton = false;
                    break;
                case Keys.S:
                    sButton = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move player 1
            if (wButton == true && hero.Y > 0)
            {
                hero.Y -= herospeed;
            }

            if (sButton == true && hero.Y < this.Height - hero.Height)
            {
                hero.Y += herospeed;
            }
            //move player 2
            if (upArrow == true && hero2.Y > 0)
            {
                hero2.Y -= herospeed;
            }

            if (downArrow == true && hero2.Y < this.Height - hero2.Height)
            {
                hero2.Y += herospeed;
            }

            // move  asteroids across
            for (int i = 0; i < asteroids.Count; i++)
            {
                int x = asteroids[i].X + 6;
                asteroids[i] = new Rectangle(x, asteroids[i].Y, asteroids[i].Width, asteroids[i].Height);
            }
            
            for (int i = 0; i < asteroids2.Count; i++)
            {
                int x = asteroids2[i].X + 8;
                asteroids2[i] = new Rectangle(x, asteroids2[i].Y, asteroids2[i].Width, asteroids2[i].Height);
            }

            for (int i = 0; i < asteroids3.Count; i++)
            {
                int x = asteroids3[i].X + 12;
                asteroids3[i] = new Rectangle(x, asteroids3[i].Y, asteroids3[i].Width, asteroids3[i].Height);
            }

            for (int i = 0; i < asteroids4.Count; i++)
            {
                int x = asteroids4[i].X + 7;
                asteroids4[i] = new Rectangle(x, asteroids4[i].Y, asteroids4[i].Width, asteroids4[i].Height);
            }

            for (int i = 0; i < asteroids5.Count; i++)
            {
                int x = asteroids5[i].X + 10;
                asteroids5[i] = new Rectangle(x, asteroids5[i].Y, asteroids5[i].Width, asteroids5[i].Height);
            }

            for (int i = 0; i < asteroids6.Count; i++)
            {
                int x = asteroids6[i].X + 15;
                asteroids6[i] = new Rectangle(x, asteroids6[i].Y, asteroids6[i].Width, asteroids6[i].Height);
            }
            //add new asteroids if it is time
            time++;

            if (time > 16)
            {
                asteroids.Add(new Rectangle(0, 100, 20, 5));
                time = 0;
                asteroids2.Add(new Rectangle(0, 280, 20, 5));
                time = 0;
                asteroids3.Add(new Rectangle(0, 250, 25, 5));
                time = 0;
                asteroids4.Add(new Rectangle(0, 150, 30, 5));
                time = 0;
                asteroids5.Add(new Rectangle(0, 200, 20, 5));
                time = 0;
                asteroids6.Add(new Rectangle(0, 50, 40, 5));
                time = 0;
            }

            //player 1 intersection with asteroids
            for (int i = 0; i < asteroids.Count(); i++)
            {
                if (hero.IntersectsWith(asteroids[i]))
                {
                    beep.Play();
                    hero.Y = this.Height - hero.Height - 40;
                }
            }
            for (int i = 0; i < asteroids2.Count(); i++)
            {
                if (hero.IntersectsWith(asteroids2[i]))
                {
                    beep.Play();
                    hero.Y = this.Height - hero.Height - 40;
                }
            }
            for (int i = 0; i < asteroids3.Count(); i++)
            {
                if (hero.IntersectsWith(asteroids3[i]))
                {
                    beep.Play();
                    hero.Y = this.Height - hero.Height - 40;
                }
            }
            for (int i = 0; i < asteroids4.Count(); i++)
            {
                if (hero.IntersectsWith(asteroids4[i]))
                {
                    beep.Play();
                    hero.Y = this.Height - hero.Height - 40;
                }
            }
            for (int i = 0; i < asteroids5.Count(); i++)
            {
                if (hero.IntersectsWith(asteroids5[i]))
                {
                    beep.Play();
                    hero.Y = this.Height - hero.Height - 40;
                }
            }
            for (int i = 0; i < asteroids6.Count(); i++)
            {
                if (hero.IntersectsWith(asteroids6[i]))
                {
                    beep.Play();
                    hero.Y = this.Height - hero.Height - 40;
                }
            }
            //player 2 intersection with asteroids
            for (int i = 0; i < asteroids.Count(); i++)
            {
                if (hero2.IntersectsWith(asteroids[i]))
                {
                    beep.Play();
                    hero2.Y = this.Height - hero2.Height - 40;
                }
            }
            for (int i = 0; i < asteroids2.Count(); i++)
            {
                if (hero2.IntersectsWith(asteroids2[i]))
                {
                    beep.Play();
                    hero2.Y = this.Height - hero2.Height - 40;
                }
            }
            for (int i = 0; i < asteroids3.Count(); i++)
            {
                if (hero2.IntersectsWith(asteroids3[i]))
                {
                    beep.Play();
                    hero2.Y = this.Height - hero2.Height - 40;
                }
            }
            for (int i = 0; i < asteroids4.Count(); i++)
            {
                if (hero2.IntersectsWith(asteroids4[i]))
                {
                    beep.Play();
                    hero2.Y = this.Height - hero2.Height - 40;
                }
            }
            for (int i = 0; i < asteroids5.Count(); i++)
            {
                if (hero2.IntersectsWith(asteroids5[i]))
                {
                    beep.Play();
                    hero2.Y = this.Height - hero2.Height - 40;
                }
            }
            for (int i = 0; i < asteroids6.Count(); i++)
            {
                if (hero2.IntersectsWith(asteroids6[i]))
                {
                    beep.Play();
                    hero2.Y = this.Height - hero2.Height - 40;
                }
            }

            //heros intersection with "finish line"
            if (hero.IntersectsWith(obstacleFinish))
            {
                p1Score++;

                p1scoreLabel.Text = $"{p1Score}";
                hero.Y = this.Height - hero.Height - 40;
            }

            if (hero2.IntersectsWith(obstacleFinish))
            {
                p2Score++;

                p2scoreLabel.Text = $"{p2Score}";
                hero2.Y = this.Height - hero2.Height - 40;
            }

            //determine the winner by checking to see if players score = 3
            if (p1Score == 3)
            {
                gong.Play();
                gameTimer.Stop();

                gameState = "over";
            }
            else if (p2Score == 3)
            {
                gong.Play();
                gameTimer.Stop();

                gameState = "finish";
            }
            Refresh();
        }

        public void GameInitialize()
        {
            titleLabel.Text = "";
            subtitleLabel.Text = "";
            winLabel.Text = "";

            p1Score = 0;
            p2Score = 0;
            p1scoreLabel.Text = "0";
            p2scoreLabel.Text = "0";

            winLabel.Visible = false;
            gameTimer.Enabled = true;
            gameState = "running";

            asteroids.Clear();
            asteroids2.Clear();
            asteroids3.Clear();
            asteroids4.Clear();
            asteroids5.Clear();
            asteroids6.Clear();

            hero.Y = this.Height - hero.Height - 40;
            hero2.Y = this.Height - hero.Height - 40;
            divider.Y = this.Height - divider.Height - 30;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (gameState == "waiting")
            {
                titleLabel.Text = "SPACE RACE";
                subtitleLabel.Text = "Press Space Bar to Start or Escape to Exit";
            }
            else if (gameState == "running")
            {
                //draw heros 
                e.Graphics.FillRectangle(whiteBrush, hero);
                e.Graphics.FillRectangle(whiteBrush, hero2);

                //draw obstacle finish
                e.Graphics.FillRectangle(whiteBrush, obstacleFinish);
                // draw dividers
                e.Graphics.FillRectangle(whiteBrush, divider);

                //paint asteroids
                for (int i = 0; i < asteroids.Count(); i++)
                {
                    e.Graphics.FillRectangle(pinkBrush, asteroids[i]);
                }

                for (int i = 0; i < asteroids2.Count(); i++)
                {
                    e.Graphics.FillRectangle(redBrush, asteroids2[i]);
                }

                for (int i = 0; i < asteroids3.Count(); i++)
                {
                    e.Graphics.FillRectangle(cyanBrush, asteroids3[i]);
                }

                for (int i = 0; i < asteroids4.Count(); i++)
                {
                    e.Graphics.FillRectangle(greenBrush, asteroids4[i]);
                }

                for (int i = 0; i < asteroids5.Count(); i++)
                {
                    e.Graphics.FillRectangle(purpleBrush, asteroids5[i]);
                }

                for (int i = 0; i < asteroids6.Count(); i++)
                {
                    e.Graphics.FillRectangle(yellowBrush, asteroids6[i]);
                }

            }
            else if (gameState == "over")
            {
                titleLabel.Text = "GAME OVER";
                subtitleLabel.Text += "Press Space Bar to Start or Escape to Exit";
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";
            }
            else if (gameState == "finish")
            {
                titleLabel.Text = "GAME OVER";
                subtitleLabel.Text += "Press Space Bar to Start or Escape to Exit";
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";
            }
            
        }
    }
}
