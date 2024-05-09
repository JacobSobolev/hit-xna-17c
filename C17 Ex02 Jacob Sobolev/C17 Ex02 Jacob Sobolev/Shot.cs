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
    public class Shot : Sprite, ICollidable2D
    {
        public event Action<Shot> ShotDoneUsing;

        private const string k_AssetName = @"Assets\Bullet";
        private const float k_StartPositionXOffset = 14;
        private const float k_Speed = 125;

        public Sprite ShotCreator { get; private set; }

        public Shot(Vector2 i_StartingPosition, int i_Direction, Color i_Color, Sprite i_ShotCreator, Game i_Game)
            : base(k_AssetName, i_Game)
        {
            Position = new Vector2(i_StartingPosition.X + k_StartPositionXOffset, i_StartingPosition.Y);
            m_TintColor = i_Color;
            Velocity = new Vector2(0, k_Speed * i_Direction);
            ShotCreator = i_ShotCreator;
        }

        public override void Update(GameTime i_GameTime)
        {
            if (Enabled)
            {
                base.Update(i_GameTime);
                if (Position.Y < 0 || Position.Y > Game.GraphicsDevice.Viewport.Height)
                {
                    if (ShotDoneUsing != null && Enabled)
                    {
                        ShotDoneUsing(this);
                    }
                }
            }
        }

        public void HitWall()
        {
            ShotDoneUsing(this);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (Enabled)
            {
                if (((i_Collidable is Enemy || i_Collidable is MotherShip) && ShotCreator is Player) || (i_Collidable is Player && ShotCreator is Enemy))
                {
                    if (ShotDoneUsing != null && Enabled)
                    {
                        ShotDoneUsing(this);
                    }
                }

                if(ShotCreator is Player && i_Collidable is Shot && (i_Collidable as Shot).ShotCreator is Enemy)
                {
                    if (ShotDoneUsing != null && Enabled)
                    {
                        ShotDoneUsing(this);
                    }
                }

                if (ShotCreator is Enemy && i_Collidable is Shot && (i_Collidable as Shot).ShotCreator is Player)
                {
                    Random random = new Random();
                    float randFloat = (float)random.NextDouble();
                    if (randFloat <= 0.5f && ShotDoneUsing != null && Enabled)
                    {
                        ShotDoneUsing(this);
                    }
                }
            }
        }
    }
}
