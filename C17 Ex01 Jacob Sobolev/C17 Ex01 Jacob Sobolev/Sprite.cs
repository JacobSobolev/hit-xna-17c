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
    public abstract class Sprite : DrawableGameComponent
    {
        protected bool m_IsInitialized = false;
        private string m_TexturePath;

        public Texture2D Texture { get; private set; }

        public Vector2 Position { get; set; }

        public Color TintColor { get; set; }

        public Sprite(BaseGame i_BaseGame, string i_TexturePath)
            : base(i_BaseGame)
        {
            m_TexturePath = i_TexturePath;
        }

        public override void Initialize()
        {
            m_IsInitialized = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Texture = Game.Content.Load<Texture2D>(m_TexturePath);
            base.LoadContent();
        }

        public override void Update(GameTime i_GameTime)
        {
            if (!m_IsInitialized)
            {
                Initialize();
            }

            base.Update(i_GameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ((BaseGame)Game).SpriteBatch.Draw(Texture, Position, TintColor);
            base.Draw(gameTime);
        }

        protected abstract void InitializePosition();
    }
}
