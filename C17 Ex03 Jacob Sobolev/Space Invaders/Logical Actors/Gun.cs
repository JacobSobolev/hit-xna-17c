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
using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders
{
    public class Gun
    {
        private readonly Vector2 m_ShotInitPos;
        private int m_MaxNumOfShots;
        private List<Shot> m_Magazine;
        private Shot m_NextActiveShot;

        public Gun(int i_MaxNumOfShots, int i_ShotDirection, Color i_ShotColor, Sprite i_GunHolder, Game i_Game, GameScreen i_ParentScreen)
        {
            m_ShotInitPos = new Vector2(-200);
            m_MaxNumOfShots = i_MaxNumOfShots;
            m_Magazine = new List<Shot>();
            for (int i = 0; i < m_MaxNumOfShots; i++)
            {
                Shot shotToAdd = new Shot(m_ShotInitPos, i_ShotDirection, i_ShotColor, i_GunHolder, i_Game);
                shotToAdd.ShotDoneUsing += ShotDoneUsingCallback;
                shotToAdd.Enabled = false;
                shotToAdd.Visible = false;
                m_Magazine.Add(shotToAdd);
                i_ParentScreen.Add(shotToAdd);
            }
        }

        public void Fire(Vector2 i_FirePosition)
        {
            checkNextActiveShot();
            
            if (m_NextActiveShot != null)
            {
                m_NextActiveShot.Position = i_FirePosition;
                m_NextActiveShot.Enabled = true;
                m_NextActiveShot.Visible = true;
            }
        }

        private void checkNextActiveShot()
        {
            m_NextActiveShot = null;
            foreach (Shot shot in m_Magazine)
            {
                if (shot.Enabled == false)
                {
                    m_NextActiveShot = shot;
                    break;
                }
            }
        }

        public bool CanFire()
        {
            checkNextActiveShot();
            return m_NextActiveShot != null;
        }

        private void ShotDoneUsingCallback(Shot i_Shot)
        {
            i_Shot.Position = m_ShotInitPos;
            i_Shot.Enabled = false;
            i_Shot.Visible = false;
        }
    }
}
