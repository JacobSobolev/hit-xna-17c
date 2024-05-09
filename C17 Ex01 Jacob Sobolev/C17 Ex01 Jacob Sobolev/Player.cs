using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C17Ex01
{
    public class Player : Sprite
    {
        private const string k_TexturePath = @"Assets\Ship01_32x32";
        private const float k_Velocity = 125;
        private const int k_InitLives = 3;
        private const int k_ShotDirection = -1;
        private const float k_HitScoreDec = -1300;

        public int Lives { get; private set; }

        public float Score { get; set; }

        public List<Shot> Shots { get; private set; }
        
        public Player(BaseGame i_baseGame)
            : base(i_baseGame, k_TexturePath)
        {
            Lives = k_InitLives;
            Shots = new List<Shot>();
            TintColor = Color.White;
        }

        public override void Initialize()
        {
            base.Initialize();
            InitializePosition();
        }

        protected override void InitializePosition()
        {
            GoToStartingPosition();
        }

        public void GoToStartingPosition()
        {
            float startingPositionX = 0;
            float startingPositionY = GraphicsDevice.Viewport.Height;
            startingPositionX -= Texture.Width / 2;
            startingPositionY -= Texture.Height * 1.5f;
            Position = new Vector2(startingPositionX, startingPositionY);
        }

        private void UpdatePosition(GameTime i_GameTime)
        {
            Vector2 newPosition = new Vector2(Position.X, Position.Y);
            if (InputComponent.KeyBoardCurrentState.IsKeyDown(Keys.Left))
            {
                newPosition.X -= k_Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (InputComponent.KeyBoardCurrentState.IsKeyDown(Keys.Right))
            {
                newPosition.X += k_Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (InputComponent.MouseCurrentState.X >= 0 && InputComponent.MouseCurrentState.X <= GraphicsDevice.Viewport.Width)
            {
                if (InputComponent.MouseCurrentState.X != InputComponent.MousePreviousState.X)
                {
                    newPosition.X = InputComponent.MouseCurrentState.X;
                }
            }

            newPosition.X = MathHelper.Clamp(newPosition.X, 0, this.GraphicsDevice.Viewport.Width - Texture.Width);
            Position = newPosition;
        }

        private void checkShipFire()
        {
            if (Shots.Count < 3)
            {
                if (InputComponent.MousePreviousState != null && InputComponent.MouseCurrentState.LeftButton == ButtonState.Pressed && InputComponent.MousePreviousState.LeftButton == ButtonState.Released)
                {
                    Shots.Add(new Shot((BaseGame)Game, Position, k_ShotDirection, Color.Red));
                }

                if (InputComponent.KeyBoardPreviousState != null && InputComponent.KeyBoardCurrentState.IsKeyDown(Keys.Enter) && InputComponent.KeyBoardPreviousState.IsKeyUp(Keys.Enter))
                {
                    Shots.Add(new Shot((BaseGame)Game, Position, k_ShotDirection, Color.Red));
                }
            }
        }

        private void checkShotOutOfRange()
        {
            for (int i = 0; i < Shots.Count; ++i)
            {
                if (Shots[i].Position.Y < 0)
                {
                    Shots.RemoveAt(i);
                    Game.Components.Remove(Shots[i]);
                    --i;
                }
            }
        }

        internal void TakeHit()
        {
            Lives--;
            Score -= k_HitScoreDec;
            if (Score <= 0)
            {
                Score = 0;
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            UpdatePosition(i_GameTime);
            checkShipFire();
            foreach (Shot shot in Shots)
            {
                shot.Update(i_GameTime);
            }

            checkShotOutOfRange();
            base.Update(i_GameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Shot shot in Shots)
            {
                shot.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}
