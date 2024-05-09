using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders
{
    public class LifeUISprite : Sprite
    {
        public Vector2 StartPos { get; set; }

        public LifeUISprite(string i_AssetName, Game i_Game)
            : base(i_AssetName, i_Game)
        {
            StartPos = Vector2.Zero;
        }

        public override void Initialize()
        {
            base.Initialize();
            Scales = new Vector2((float)0.5, (float)0.5);
            Opacity = 0.5f;
            Position = StartPos;
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
