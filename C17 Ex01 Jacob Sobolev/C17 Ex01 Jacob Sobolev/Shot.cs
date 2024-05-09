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
    public class Shot : Sprite
    {
        private const string k_TexturePath = @"Assets\Bullet";
        private const float k_StartPositionXOffset = 14;
        private const float k_Speed = 125;
        private Vector2 m_StartPosition;
        private Vector2 m_Velocity;

        public Shot(BaseGame i_Game, Vector2 i_StartingPosition, int i_Direction, Color i_Color)
            : base(i_Game, k_TexturePath)
        {
            m_StartPosition = i_StartingPosition;
            m_StartPosition.X += k_StartPositionXOffset;
            m_Velocity = new Vector2(0, k_Speed * i_Direction);
            TintColor = i_Color;
        }

        public override void Initialize()
        {
            base.Initialize();
            InitializePosition();
        }

        protected override void InitializePosition()
        {
            Position = m_StartPosition;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            Position += m_Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
