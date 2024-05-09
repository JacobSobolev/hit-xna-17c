using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders
{
    public class LifeUISprite : Sprite
    {
        public LifeUISprite(string i_AssetName, Game i_Game)
            : base(i_AssetName, i_Game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Scales = new Vector2((float)0.5, (float)0.5);
            Opacity = 0.5f;
            Position = Vector2.Zero;
        }
    }
}
