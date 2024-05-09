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
    public class LivesBarUIComponnet : RegisteredComponent
    {
        private const int k_DefaultNumOfSouls = 3;
        private const float k_Width = 100;
        private const float k_Height = 20;
        private const float k_Spacing = 20;
        private string m_AssetName;
        private int m_NumOfSouls;

        public int NumOfSouls
        {
            set { m_NumOfSouls = value; }
        }

        private List<Sprite> m_Souls;
        private Vector2 m_Position;

        public LivesBarUIComponnet(string i_AssetName, Game i_Game, Vector2 i_Position)
        : this(i_AssetName, i_Game, i_Position, k_DefaultNumOfSouls)
        {
        }

        public LivesBarUIComponnet(string i_AssetName, Game i_Game, Vector2 i_Position, int i_NumOfSouls)
            : base(i_Game)
        {
            m_AssetName = i_AssetName;
            m_NumOfSouls = i_NumOfSouls;
            m_Position = i_Position;
        }

        public override void Initialize()
        {
            base.Initialize();
            m_Souls = new List<Sprite>();
            for (int i = 0; i < m_NumOfSouls; ++i)
            {
                LifeUISprite lifeUiComponnet = new LifeUISprite(m_AssetName, Game);
                Vector2 positionToDraw = new Vector2(m_Position.X + k_Width - (i + 1) * (lifeUiComponnet.Width + k_Spacing), m_Position.Y);
                lifeUiComponnet.StartPos = positionToDraw;
                m_Souls.Add(lifeUiComponnet);
            }
        }

        public void Player_LifeLost()
        {
            if (m_Souls.Count != 0)
            {
                Sprite soulToRemove = m_Souls[m_Souls.Count - 1];
                soulToRemove.Dispose();
                m_Souls.RemoveAt(m_Souls.Count - 1);
            }
        }
    }
}
