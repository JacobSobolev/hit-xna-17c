using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.Managers;

namespace SpaceInvaders
{
    public delegate void scoreChanged(float i_Score);

    public delegate void LifeLost();

    public abstract class Player : Sprite, ICollidable2D
    {
        public event scoreChanged m_ScoreChanged;

        public event LifeLost m_LifeLost;

        private const float k_Velocity = 160;
        private const int k_InitLives = 3;
        private const int k_ShotDirection = -1;
        private const float k_HitScoreDec = 1300;
        private const int k_MaxNumOfShots = 3;
        private const float k_LoseLifeAnimationDuration = 2.4f;
        private const float k_DieAnimationDuration = 2.4f;
        protected IInputManager m_InputManager = null;
        private bool m_UseMouseAsPositionControl;
        private Gun m_Gun;
        private bool m_Dying;

        public int Lives { get; private set; }

        public float Score { get; set; }

        public Player(string i_AssetName, Game i_Game, bool i_UseMouseAsPositionControl)
            : base(i_AssetName, i_Game)
        {
            Lives = k_InitLives;
            TintColor = Color.White;
            m_UseMouseAsPositionControl = i_UseMouseAsPositionControl;
            m_Dying = false;
        }

        public override void Initialize()
        {
            base.Initialize();
            GoToStartingPosition();
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            m_Gun = new Gun(k_MaxNumOfShots, k_ShotDirection, Color.Red, this, Game);
            initAnimations();
        }

        protected override void InitOrigins()
        {
            RotationOrigin = TextureCenter;
            PositionOrigin = new Vector2(Texture.Width / 2, 0);
        }

        private void initAnimations()
        {
            float blinkLength = 1.0f / 7.0f;
            BlinkAnimator lifeLoseAnimation = new BlinkAnimator("lifeLoseBlinkAnimation", TimeSpan.FromSeconds(blinkLength), TimeSpan.FromSeconds(k_LoseLifeAnimationDuration));
            Animations.Add(lifeLoseAnimation);
            Animations["lifeLoseBlinkAnimation"].Pause();

            FadeOutAnimator dieFadeAnimation = new FadeOutAnimator("dieFadeAnimation", TimeSpan.FromSeconds(k_DieAnimationDuration));
            dieFadeAnimation.ResetAfterFinish = false;
            Animations.Add(dieFadeAnimation);
            Animations["dieFadeAnimation"].Pause();

            RotationAnimator dieRotateAnimation = new RotationAnimator("dieRotateAnimation", 4, TimeSpan.FromSeconds(k_DieAnimationDuration));
            Animations.Add(dieRotateAnimation);
            dieRotateAnimation.ResetAfterFinish = false;
            Animations["dieRotateAnimation"].Pause();

            dieRotateAnimation.Finished += dieAnimationFinished;

            Animations.Enabled = true;
        }

        public void GoToStartingPosition()
        {
            float startingPositionX = Texture.Width / 2;
            float startingPositionY = GraphicsDevice.Viewport.Height - (Texture.Height * 1.5f);
            Position = new Vector2(startingPositionX, startingPositionY);
        }

        protected abstract bool pressingMoveLeftControl();

        protected abstract bool pressingMoveRightControl();

        protected abstract bool pressedFireControl();

        private Vector2 preformMoveLeftKeyboard(GameTime i_GameTime)
        {
            Vector2 newPosition = new Vector2(Position.X, Position.Y);
            newPosition.X -= k_Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            return newPosition;
        }

        private Vector2 preformMoveRightKeyboard(GameTime i_GameTime)
        {
            Vector2 newPosition = new Vector2(Position.X, Position.Y);
            newPosition.X += k_Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            return newPosition;
        }

        private Vector2 preformMoveUsingMouse(GameTime i_GameTime)
        {
            Vector2 newPositionDelta = Vector2.Zero;
            newPositionDelta.X = m_InputManager.MousePositionDelta.X;
            return newPositionDelta;
        }

        private void fireGun()
        {
            if (!m_Dying)
            {
                m_Gun.Fire(Position);
                (this.Game.Services.GetService(typeof(SoundManager)) as SoundManager).PlaySound("PlayerShot");
            }
        }

        private void controlPlayer(GameTime i_GameTime)
        {
            Vector2 newPosition = Position;
            if (pressingMoveLeftControl())
            {
                newPosition = preformMoveLeftKeyboard(i_GameTime);
            }
            else if (pressingMoveRightControl())
            {
                newPosition = preformMoveRightKeyboard(i_GameTime);
            }
            else if (m_UseMouseAsPositionControl)
            {
                newPosition += preformMoveUsingMouse(i_GameTime);
            }

            if (pressedFireControl())
            {
                fireGun();
            }

            newPosition.X = MathHelper.Clamp(newPosition.X, 0, this.GraphicsDevice.Viewport.Width - Texture.Width);
            Position = newPosition;
        }

        private void takeHit()
        {
            Lives--;
            ScorePoints(-k_HitScoreDec);
            OnLifeLost();
            if (Lives <= 0)
            {
                if (!m_Dying)
                {
                    m_Dying = true;
                    Animations["dieFadeAnimation"].Restart();
                    Animations["dieRotateAnimation"].Restart();
                }
            }
            else
            {
                Animations["lifeLoseBlinkAnimation"].Restart();
            }
            (this.Game.Services.GetService(typeof(SoundManager)) as SoundManager).PlaySound("LifeDie");
        }

        private void dieAnimationFinished(object sender, EventArgs e)
        {
            Dispose();
        }

        internal void ScorePoints(float i_ScoreToAdd)
        {
            Score += i_ScoreToAdd;
            if (Score <= 0)
            {
                Score = 0;
            }

            OnScoreChanged();
        }

        protected virtual void OnScoreChanged()
        {
            if (m_ScoreChanged != null)
            {
                m_ScoreChanged.Invoke(Score);
            }
        }

        protected virtual void OnLifeLost()
        {
            if (m_LifeLost != null)
            {
                m_LifeLost.Invoke();
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            controlPlayer(i_GameTime);
            base.Update(i_GameTime);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is Shot && (i_Collidable as Shot).ShotCreator is Enemy)
            {
                if (Lives > 1)
                {
                    GoToStartingPosition();
                }

                takeHit();
            }

            if (i_Collidable is Enemy)
            {
                Lives = 0;
                takeHit();
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            SpriteBatch nonPremultipliedSpriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteBatch = nonPremultipliedSpriteBatch;
        }

        public override void Draw(GameTime gameTime)
        {
            m_SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            base.Draw(gameTime);
            m_SpriteBatch.End();
        }
    }
}
