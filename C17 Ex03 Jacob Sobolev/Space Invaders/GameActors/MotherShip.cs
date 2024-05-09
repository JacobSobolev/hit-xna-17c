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
    public class MotherShip : Sprite, ICollidable2D
    {
        public event EventHandler<EventArgs> Disposed;

        private const float k_MinArrivalTime = 2.0f;
        private const float k_MaxArrivalTIme = 5.0f;
        private const float k_Velocity = 90;
        private const float k_RewardInit = 550;
        private const string k_AssetName = @"Assets\MotherShip_32x120";
        private const int k_MaxRandomArriavelTime = 10;
        private const float k_DieAnimationDuration = 2.4f;
        private float m_TimeOfArrival;
        private bool m_Arrived;
        private float m_ArrivalTimer;
        private bool m_Dying;
        private PlayersManager m_PlayersManager;

        public float Reward { get; private set; }

        public MotherShip(Game i_Game)
            : base(k_AssetName, i_Game)
        {
            m_Arrived = false;
            Reward = k_RewardInit;
            TintColor = Color.Red;
            m_Dying = false;
            setTimeOfArrival();
            m_PlayersManager = Game.Services.GetService(typeof(PlayersManager)) as PlayersManager;
            m_PlayersManager.LevelClearedEvent += resetToLevelStart;
            m_PlayersManager.MenuExitEvent += resetToLevelStart;
        }

        private void resetToLevelStart()
        {
            m_Dying = false;
            initStartPosition();
            setTimeOfArrival();
        }

        private void setTimeOfArrival()
        {
            Random random = new Random();
            m_TimeOfArrival = k_MinArrivalTime + (float)random.NextDouble() * k_MaxArrivalTIme; 
        }

        public override void Initialize()
        {
            base.Initialize();
            initStartPosition();
            initAnimations();
        }

        private void initAnimations()
        {
            float blinkLength = 1.0f / 4.0f;
            BlinkAnimator lifeLoseAnimation = new BlinkAnimator("dieBlinkAnimation", TimeSpan.FromSeconds(blinkLength), TimeSpan.FromSeconds(k_DieAnimationDuration));
            Animations.Add(lifeLoseAnimation);
            Animations["dieBlinkAnimation"].Pause();

            FadeOutAnimator dieFadeAnimation = new FadeOutAnimator("dieFadeAnimation", TimeSpan.FromSeconds(k_DieAnimationDuration));
            Animations.Add(dieFadeAnimation);
            Animations["dieFadeAnimation"].Pause();

            ShrinkAnimator dieShrinkAnimation = new ShrinkAnimator("dieShrinkAnimation", TimeSpan.FromSeconds(k_DieAnimationDuration));
            Animations.Add(dieShrinkAnimation);
            Animations["dieShrinkAnimation"].Pause();

            dieShrinkAnimation.Finished += dieAnimationFinished;

            Animations.Enabled = true;
        }

        protected override void InitOrigins()
        {
            RotationOrigin = TextureCenter;
        }

        private void initStartPosition()
        {
            Position = new Vector2(-Texture.Width, Texture.Height / 2);
        }

        internal void Dissappear()
        {
            m_Arrived = false;
            m_ArrivalTimer = 0;
            m_Dying = false;
            initStartPosition();
            setTimeOfArrival();
        }

        public override void Update(GameTime i_GameTime)
        {
            float timePassed = (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            m_ArrivalTimer += timePassed;
            if (m_Arrived == false)
            {
                if (m_ArrivalTimer >= m_TimeOfArrival)
                {
                    m_Arrived = true;
                }
            }
            else
            {
                Vector2 newPosition = new Vector2(Position.X, Position.Y);
                newPosition.X += k_Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
                Position = newPosition;
                if (Position.X > (float)GraphicsDevice.Viewport.Width)
                {
                    Dissappear();
                }
            }

            base.Update(i_GameTime);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is Shot)
            {
                Sprite shooter = (i_Collidable as Shot).ShotCreator;
                if ((i_Collidable as Shot).ShotCreator is Player)
                {
                    if (!m_Dying)
                    {
                        m_Dying = true;
                        ((Player)shooter).ScorePoints(this.Reward);
                        (this.Game.Services.GetService(typeof(SpaceInvaderSoundsManager)) as SpaceInvaderSoundsManager).PlaySound("MotherShipKill");
                        Animations["dieBlinkAnimation"].Restart();
                        Animations["dieFadeAnimation"].Restart();
                        Animations["dieShrinkAnimation"].Restart();
                    }
                }
            }
        }

        private void dieAnimationFinished(object sender, EventArgs e)
        {
            Dissappear();
        }
    }
}
