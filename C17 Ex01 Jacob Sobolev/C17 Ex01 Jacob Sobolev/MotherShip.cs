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
    public class MotherShip : Sprite
    {
        private const float k_Velocity = 90;
        private const float k_RewardInit = 550;
        private const string k_TexturePath = @"Assets\MotherShip_32x120";
        private const int k_MaxRandomArriavelTime = 10;
        private float m_TimeOfArrival;
        private bool m_Arrived;
        private float m_ArrivalTimer;

        public float Reward { get; private set; }

        public MotherShip(BaseGame i_BaseGame)
            : base(i_BaseGame, k_TexturePath)
        {
            m_Arrived = false;
            Reward = k_RewardInit;
            TintColor = Color.Red;
            setTimeOfArrival();
        }

        private void setTimeOfArrival()
        {
            Random random = new Random();
            m_TimeOfArrival = (float)random.Next(0, 0) + (float)random.NextDouble();
        }

        public override void Initialize()
        {
            base.Initialize();
            InitializePosition();
        }

        protected override void InitializePosition()
        {
            Position = new Vector2(-Texture.Width, Texture.Height);
        }

        internal void Dissappear()
        {
            m_Arrived = false;
            m_ArrivalTimer = 0;
            InitializePosition();
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
    }
}
