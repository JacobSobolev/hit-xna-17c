using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Infrastructure.ServiceInterfaces;
using Infrastructure.Managers;
using Infrastructure.ObjectModel;

namespace Dreidels
{
    public class Dreidel : Model3D
    {
        protected const float k_Quarter = 0.25f;
        protected const string k_AssetName = @"Assets\LettersLine";
        private float m_NextAngle;
        private char m_FrontLetter = 'B';

        public char FrontLetter
        {
            get { return m_FrontLetter; }
        }

        private bool m_IsSpinning;

        public bool IsSpinning
        {
            get { return m_IsSpinning; }
        }

        private float m_SpinVelocity;

        public Dreidel(Game i_Game)
            : base(k_AssetName, i_Game)
        { }

        public void StartSpinning(float i_SpinVelocity, int i_NewFrontSide)
        {
            getFrontSide(i_NewFrontSide);
            m_SpinVelocity = i_SpinVelocity;
            m_IsSpinning = true;
        }

        private void getFrontSide(int i_NewFrontSide)
        {
            switch (i_NewFrontSide)
            {
                case 1:
                    m_FrontLetter = 'P';
                    m_NextAngle = MathHelper.PiOver2;
                    break;
                case 2:
                    m_FrontLetter = 'V';
                    m_NextAngle = MathHelper.Pi;
                    break;
                case 3:
                    m_FrontLetter = 'D';
                    m_NextAngle = 3 * MathHelper.PiOver2;
                    break;
                case 4:
                    m_FrontLetter = 'B';
                    m_NextAngle = MathHelper.TwoPi;
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(m_IsSpinning)
            {
                m_Rotation.Y += m_SpinVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                m_Rotation.Y = m_Rotation.Y % MathHelper.TwoPi;
                if (m_SpinVelocity > 0.5)
                {
                    m_SpinVelocity -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    if (m_NextAngle - m_Rotation.Y < 0.1 && m_NextAngle - m_Rotation.Y > 0)
                    {
                        m_Rotation.Y = MathHelper.Clamp(m_Rotation.Y, -1, m_NextAngle);
                        m_IsSpinning = false;
                    }
                }
            }
        }
    }
}
