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
    public class Enemy : Sprite
    {
        private Vector2 m_StartPosition;

        public float Reward { get; private set; }

        public Vector2 MatrixPos { get; private set; }

        public Enemy(BaseGame i_BaseGame, Color i_Color, float i_Reward, string i_TexturePath, Vector2 i_MatrixPos, Vector2 i_StartPosition)
            : base(i_BaseGame, i_TexturePath)
        {
            TintColor = i_Color;
            Reward = i_Reward;
            MatrixPos = i_MatrixPos;
            m_StartPosition = i_StartPosition;
        }

        public override void Initialize()
        {
            LoadContent();
            InitializePosition();
            base.Initialize();
        }

        protected override void InitializePosition()
        {
            Position = m_StartPosition;
        }
    }
}
