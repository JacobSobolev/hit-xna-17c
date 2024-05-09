using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.Screens
{
    public class LevelTransitionScreen : GameScreen
    {
        private const int k_ScreenActiveCounts = 3;
        private const float k_ScreenIntervalTime = 1.0f;
        private float m_Timer = 0;
        private int m_CountNumber = k_ScreenActiveCounts;
        private SpriteLabel m_LevelNumberLabel;
        private SpriteLabel m_CountDownLabel;
        private PlayersManager m_PlayersManager;

        public LevelTransitionScreen(Game i_Game) : base(i_Game)
        {
            m_PlayersManager = Game.Services.GetService(typeof(PlayersManager)) as PlayersManager;
            m_LevelNumberLabel = new SpriteLabel(@"Fonts\Consolas", i_Game);
            m_LevelNumberLabel.Text = string.Format("Level: {0}", m_PlayersManager.Level);
            m_CountDownLabel = new SpriteLabel(@"Fonts\Consolas", i_Game);
            m_CountDownLabel.Text = string.Format("{0}", m_CountNumber);
            Add(m_LevelNumberLabel);
            Add(m_CountDownLabel);
        }

        public override void Initialize()
        {
            base.Initialize();

            m_LevelNumberLabel.Scales = new Vector2(2);
            m_LevelNumberLabel.PositionOrigin = m_LevelNumberLabel.TextCenter;
            m_LevelNumberLabel.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2 - 50);

            m_CountDownLabel.Scales = new Vector2(3);
            m_CountDownLabel.PositionOrigin = m_CountDownLabel.TextCenter;
            m_CountDownLabel.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2 + 80);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            m_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (m_Timer >= k_ScreenIntervalTime)
            {
                m_Timer = 0;
                m_CountNumber--;
                m_CountDownLabel.Text = string.Format("{0}", m_CountNumber);
            }

            if (m_CountNumber == 0)
            {
                ExitScreen();
            }
        }
    }
}
