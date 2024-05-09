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
    public class Label : LoadableDrawableComponent
    {
        private float m_Width;

        public float Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }

        private float m_Height;

        public float Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }

        private SpriteBatch m_SpriteBatch;
        private SpriteFont m_SpriteFont;

        public SpriteFont SpriteFont
        {
            get { return m_SpriteFont; }
            set { m_SpriteFont = value; }
        }

        private string m_Messege;

        public string Messege
        {
            get { return m_Messege; }
            set { m_Messege = value; }
        }

        private Vector2 m_Position = Vector2.Zero;

        public Vector2 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        protected Color m_TintColor = Color.White;

        public Color TintColor
        {
            get { return m_TintColor; }
            set { m_TintColor = value; }
        }

        public float Opacity
        {
            get { return (float)m_TintColor.A / (float)byte.MaxValue; }
            set { m_TintColor.A = (byte)(value * (float)byte.MaxValue); }
        }

        public Label(string i_Asset_Name, Game i_Game)
            : base(i_Asset_Name, i_Game, int.MaxValue)
        { }

        protected override void InitBounds()
        {
            m_Width = Game.GraphicsDevice.Viewport.Width;
            m_Height = Game.GraphicsDevice.Viewport.Height;
        }

        public override void Initialize()
        {
            base.Initialize();
            if (m_SpriteBatch == null)
            {
                m_SpriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

                if (m_SpriteBatch == null)
                {
                    m_SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
                }
            }
        }

        protected override void LoadContent()
        {
            m_SpriteFont = Game.Content.Load<SpriteFont>(m_AssetName);
            base.LoadContent();
        }

        private bool m_UseSharedBatch = false;

        public override void Draw(GameTime gameTime)
        {
            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.Begin();
            }

            m_SpriteBatch.DrawString(m_SpriteFont, m_Messege, m_Position, m_TintColor);

            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.End();
            }

            base.Draw(gameTime);
        }

        protected override void DrawBoundingBox()
        {
        }
    }
}
