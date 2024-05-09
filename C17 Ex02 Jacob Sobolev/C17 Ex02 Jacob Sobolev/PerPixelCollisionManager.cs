using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Infrastructure.ServiceInterfaces;
using Infrastructure.ObjectModel;

namespace SpaceInvaders
{
    public class PerPixelCollisionManager : GameService
    {
        public PerPixelCollisionManager(Game i_Game)
            : base(i_Game)
        { }

        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(PerPixelCollisionManager), this);
        }

        public bool CheckPerPixelCollision(ICollidable i_firstCollidable, ICollidable i_SecondCollidable)
        {
            bool isPixelCollided = false;
            Sprite firstSprite = i_firstCollidable as Sprite;
            Sprite secondSprite = i_SecondCollidable as Sprite;
            Color[] firstSpritePixels = new Color[firstSprite.Texture.Width * firstSprite.Texture.Height];
            firstSprite.Texture.GetData(firstSpritePixels);
            Color[] secondSpritePixels = new Color[secondSprite.Texture.Width * secondSprite.Texture.Height];
            secondSprite.Texture.GetData(secondSpritePixels);
            Rectangle firstSpriteRec = new Rectangle((int)firstSprite.Position.X, (int)firstSprite.Position.Y, firstSprite.Texture.Width, firstSprite.Texture.Height);
            Rectangle shotRec = new Rectangle((int)secondSprite.Position.X, (int)secondSprite.Position.Y, secondSprite.Texture.Width, secondSprite.Texture.Height);
            Rectangle intersectionRectangle = Rectangle.Intersect(firstSpriteRec, shotRec);
            int i, j;
            for (j = intersectionRectangle.Top; j < intersectionRectangle.Bottom; ++j)
            {
                for (i = intersectionRectangle.Left; i < intersectionRectangle.Right; ++i)
                {
                    Color barrierPixel = firstSpritePixels[(i - firstSpriteRec.Left) + (j - firstSpriteRec.Top) * (firstSpriteRec.Width)];
                    Color shotPixel = secondSpritePixels[(i - shotRec.Left) + (j - shotRec.Top) * (shotRec.Width)];
                    if (shotPixel.A != 0 && barrierPixel.A != 0)
                    {
                        isPixelCollided = true;
                        break;
                    }
                }

                if (isPixelCollided)
                {
                    break;
                }
            }

            return isPixelCollided;
        }
    }
}
