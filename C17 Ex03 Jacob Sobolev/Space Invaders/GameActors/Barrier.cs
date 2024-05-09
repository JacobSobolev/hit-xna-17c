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
using Infrastructure.Managers;

namespace SpaceInvaders
{
    public class Barrier : Sprite, ICollidable2D
    {
        public event EventHandler<EventArgs> Disposed;

        private const string k_AssetName = @"Assets\Barrier_44x32";
        private int m_LinePosition;

        public Barrier(Game i_Game, int i_LinePosition)
            : base(k_AssetName, i_Game)
        {
            m_LinePosition = i_LinePosition;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is Shot )
            {
                if((Game.Services.GetService(typeof(PerPixelCollisionManager)) as PerPixelCollisionManager).CheckPerPixelCollision(this, i_Collidable) == true)
                {
                    resolveShot(i_Collidable as Shot);
                }
            }
            else if (i_Collidable is Enemy)
            {
                Enemy enemy = i_Collidable as Enemy;
                resolveFleetHit(enemy);
            }
        }

        private void resolveShot(Shot i_Shot)
        {
            (this.Game.Services.GetService(typeof(SpaceInvaderSoundsManager)) as SpaceInvaderSoundsManager).PlaySound("BarrierHit");
            Color[] barrierPixels = new Color[Texture.Width * Texture.Height];
            this.Texture.GetData(barrierPixels);
            int shotDirection = 1;
            if (i_Shot.ShotCreator is Player)
            {
                shotDirection = -1;
            }

            Rectangle barrierRec = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);
            Rectangle shotRec = new Rectangle((int)i_Shot.Position.X, (int)(i_Shot.Position.Y + shotDirection * (float)0.45 * i_Shot.Texture.Height), i_Shot.Texture.Width, i_Shot.Texture.Height);
            Rectangle intersectionRectangle = Rectangle.Intersect(barrierRec, shotRec);
            for (int j = intersectionRectangle.Top; j < intersectionRectangle.Bottom; ++j)
            {
                for (int i = intersectionRectangle.Left; i < intersectionRectangle.Right; ++i)
                {
                    Color barrierPixel = barrierPixels[(i - barrierRec.Left) + (j - barrierRec.Top) * (barrierRec.Width)];
                    if (barrierPixel.A != 0)
                    {
                        barrierPixels[(i - barrierRec.Left) + (j - barrierRec.Top) * (barrierRec.Width)] = Color.Transparent;
                    }
                }
            }

            this.Texture = new Texture2D(Game.GraphicsDevice, Texture.Width, Texture.Height);
            Texture.SetData(barrierPixels);
            i_Shot.HitWall();
        }

        private void resolveFleetHit(Enemy i_Enemy)
        {
            Color[] barrierPixels = new Color[Texture.Width * Texture.Height];
            this.Texture.GetData(barrierPixels);
            Rectangle barrierRec = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);
            Rectangle enemyRec = new Rectangle((int)i_Enemy.Position.X, (int)i_Enemy.Position.Y, i_Enemy.Texture.Width, i_Enemy.Texture.Height);
            Rectangle intersectionRectangle = Rectangle.Intersect(barrierRec, enemyRec);
            for (int j = intersectionRectangle.Top; j < intersectionRectangle.Bottom; ++j)
            {
                for (int i = intersectionRectangle.Left; i < intersectionRectangle.Right; ++i)
                {
                    Color barrierPixel = barrierPixels[(i - barrierRec.Left) + (j - barrierRec.Top) * (barrierRec.Width)];
                    if (barrierPixel.A != 0)
                    {
                        barrierPixels[(i - barrierRec.Left) + (j - barrierRec.Top) * (barrierRec.Width)] = Color.Transparent;
                    }
                }
            }

            Texture = new Texture2D(Game.GraphicsDevice, Texture.Width, Texture.Height);
            Texture.SetData(barrierPixels);
        }
    }
}
