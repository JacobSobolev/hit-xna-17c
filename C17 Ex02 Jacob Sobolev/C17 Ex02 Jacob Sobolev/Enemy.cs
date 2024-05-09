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
using Infrastructure.Managers;

namespace SpaceInvaders
{
    public class Enemy : Sprite, ICollidable2D
    {
        private const string k_AssetName = @"Assets\Enemies192x32";
        private const float k_CellFrameTime = 0.5f;
        private const float k_DieAnimationDuration = 1.6f;
        private const int k_ShotDirection = 1;
        private const int k_MaxNumOfShots = 1;
        private Vector2 m_StartPosition;
        private Vector2 m_FirePos;
        private Gun m_Gun;
        private EnemyType m_EnemyType;
        private int m_StartFrame;
        private EventHandler m_CellFinished;

        public float Reward { get; private set; }
        
        public Enemy(Game i_Game, Color i_Color, float i_Reward, Vector2 i_StartPosition, EnemyType i_EnemyType, int i_StartFrame, EventHandler i_CellFinished)
            : base(k_AssetName, i_Game)
        {
            TintColor = i_Color;
            Reward = i_Reward;
            m_StartPosition = i_StartPosition;
            Position = i_StartPosition;
            m_EnemyType = i_EnemyType;
            m_StartFrame = i_StartFrame;
            m_CellFinished = i_CellFinished;
        }

        public override void Initialize()
        {
            base.Initialize();
            Position = m_StartPosition;
            m_Gun = new Gun(k_MaxNumOfShots, k_ShotDirection, Color.Blue, this, Game);
            m_FirePos = new Vector2(Position.X + Width / 2, Position.Y);
            initAnimations();
        }

        private void initAnimations()
        {
            int startCellFrame = 0;
            int endCellFrame = 0;
            int currentCellFrame = 0;
            if (m_EnemyType == EnemyType.Top)
            {
                startCellFrame = 0;
                endCellFrame = 1;
            }
            else if (m_EnemyType == EnemyType.Center)
            {
                startCellFrame = 2;
                endCellFrame = 3;
            }
            else
            {
                startCellFrame = 4;
                endCellFrame = 5;
            }

            currentCellFrame = startCellFrame + m_StartFrame;
            CellAnimator celAnimation = new CellAnimator(TimeSpan.FromSeconds(k_CellFrameTime), startCellFrame, endCellFrame, currentCellFrame, TimeSpan.FromSeconds(k_CellFrameTime * 2));
            celAnimation.Finished += m_CellFinished;
            Animations.Add(celAnimation);
            RotationAnimator dieRotateAnimation = new RotationAnimator("dieRotateAnimation", 6, TimeSpan.FromSeconds(k_DieAnimationDuration));
            Animations.Add(dieRotateAnimation);
            dieRotateAnimation.ResetAfterFinish = false;
            dieRotateAnimation.Finished += OnDisposed;
            Animations["dieRotateAnimation"].Pause();
            ShrinkAnimator dieShrinkAnimation = new ShrinkAnimator("dieShrinkAnimation", TimeSpan.FromSeconds(k_DieAnimationDuration));
            Animations.Add(dieShrinkAnimation);
            dieShrinkAnimation.ResetAfterFinish = false;
            Animations["dieShrinkAnimation"].Pause();
            Animations.Enabled = true;
        }

        protected override void InitOrigins()
        {
            base.InitOrigins();

            this.RotationOrigin = this.SourceRectangleCenter;
        }

        protected override void InitBounds()
        {
            m_WidthBeforeScale = Texture.Width / 6;
            m_HeightBeforeScale = Texture.Height;
            m_Position = Vector2.Zero;

            InitSourceRectangle();

            InitOrigins();
        }

        public void Shot()
        {
            m_Gun.Fire(m_FirePos);
            (this.Game.Services.GetService(typeof(SoundManager)) as SoundManager).PlaySound("EnemyShot");

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            m_FirePos = Position;
            m_FirePos.X += Width / 2;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is Shot)
            {
                Sprite shooter = (i_Collidable as Shot).ShotCreator;
                if (shooter is Player)
                {
                    ((Player)shooter).ScorePoints(this.Reward);
                    (this.Game.Services.GetService(typeof(SoundManager)) as SoundManager).PlaySound("EnemyKill");
                    Animations["dieRotateAnimation"].Restart();
                    Animations["dieShrinkAnimation"].Restart();
                }
            }
        }

        public void abc(object sender, EventArgs args)
        {
            Console.WriteLine("finished");
        }

        protected override void OnDisposed(object sender, EventArgs args)
        {
            base.OnDisposed(this, args);
            Enabled = false;
        }
    }
}
