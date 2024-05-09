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
    public class LivesBarUIComponnet : CompositeDrawableComponent<Sprite>
    {
        private const float k_Width = 100;
        private const float k_Height = 20;
        private const float k_Spacing = 10;
        private string m_AssetName;
        private int m_NumOfSouls;
        private Vector2 m_Position;
        private PlayersManager m_PlayersManager;
        private int m_NumOfSoulsCurrent;

        public int NumOfSouls
        {
            set { m_NumOfSouls = value; }
        }

        public LivesBarUIComponnet(string i_AssetName, Game i_Game, Vector2 i_Position, int i_NumOfSouls)
            : base(i_Game)
        {
            m_AssetName = i_AssetName;
            m_NumOfSouls = i_NumOfSouls;
            m_NumOfSoulsCurrent = m_NumOfSouls;
            m_Position = i_Position;
            for (int i = 0; i < m_NumOfSouls; ++i)
            {
                LifeUISprite lifeUiComponnet = new LifeUISprite(m_AssetName, Game);
                Add(lifeUiComponnet);
            }

            BlendState = BlendState.NonPremultiplied;
            m_PlayersManager = Game.Services.GetService(typeof(PlayersManager)) as PlayersManager;
            m_PlayersManager.MenuExitEvent += MenuExitEventCallback;
        }

        public override void Initialize()
        {
            base.Initialize();
            int i = 0;
            foreach (LifeUISprite lifeUiComponnet in m_Sprites)
            {
                Vector2 positionToDraw = new Vector2(m_Position.X + k_Width - (i + 1) * (lifeUiComponnet.Width + k_Spacing), m_Position.Y);
                lifeUiComponnet.Position = positionToDraw;
                i++;
            }
        }

        public void Player_LifeLost()
        {
            if (m_NumOfSoulsCurrent != 0)
            {
                Sprite lifeToHide = m_Sprites[m_NumOfSoulsCurrent - 1];
                lifeToHide.Enabled = false;
                lifeToHide.Visible = false;
                m_NumOfSoulsCurrent--;
            }
        }

        public void MenuExitEventCallback()
        {
            m_NumOfSoulsCurrent = m_NumOfSouls;
            foreach (LifeUISprite lifeUiComponnet in m_Sprites)
            {
                lifeUiComponnet.Visible = true;
                lifeUiComponnet.Enabled = true;
            }
        }
    }
}
