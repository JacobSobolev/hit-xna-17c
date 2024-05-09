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

namespace SpaceInvaders
{
    public class BarrierLine : CompositeDrawableComponent<Sprite>
    {
        private const float k_DistanceFromBotoom = 80;
        private const float k_BarrierWidth = 44;
        private const float k_BarrierHeight = 32;
        private const float k_BarrierSpace = 44;
        private const int k_NumberOfBarriers = 4;
        private const float m_InitVelocity = 60;
        private int m_Direction = 1;
        private float m_VelocityX;
        private Vector2 m_Position;
        private Vector2 m_StartingPosition;
        private PlayersManager m_PlayersManager;

        public Vector2 TopLeftPosition
        {
            get { return m_Position - new Vector2((float)3.5 * k_BarrierWidth, 0); }
        }

        public BarrierLine(Game i_Game)
            : base(i_Game)
        {
            m_PlayersManager = Game.Services.GetService(typeof(PlayersManager)) as PlayersManager;
            m_PlayersManager.MenuExitEvent += resetToLevelStart;
            m_PlayersManager.LevelClearedEvent += resetToLevelStart;
        }

        public override void Initialize()
        {
            resetToLevelStart();
            updateBarriersPos();
            base.Initialize();
        }

        private void resetToLevelStart()
        {
            foreach (Sprite item in m_Sprites)
            {
                item.Enabled = false;
                item.Visible = false;
            }

            m_VelocityX = m_InitVelocity * m_PlayersManager.GetBarrierLineSpeedMultiplayer();
            Clear();
            m_StartingPosition = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height - k_DistanceFromBotoom - k_BarrierHeight);
            m_Position = m_StartingPosition;
            Barrier barrier;
            for (int i = 0; i < k_NumberOfBarriers; ++i)
            {
                int LinePos = i;
                barrier = new Barrier(Game, LinePos);
                Add(barrier);
            }
        }

        private void updateBarriersPos()
        {
            for (int i = 0; i < m_Sprites.Count; ++i)
            {
                Vector2 newPosition = Vector2.Zero;
                newPosition += TopLeftPosition;
                newPosition.X += i * 2 * k_BarrierWidth;
                m_Sprites[i].Position = newPosition;
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            float verticalLeftBound = m_StartingPosition.X - k_BarrierWidth;
            float verticalRightBound = m_StartingPosition.X + k_BarrierWidth;
            m_Position.X += m_VelocityX * m_Direction * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            m_Position.X = MathHelper.Clamp(m_Position.X, verticalLeftBound, verticalRightBound);
            if (m_Position.X == verticalLeftBound || m_Position.X == verticalRightBound)
            {
                m_Direction *= -1;
            }

            updateBarriersPos();
        }
    }
}