using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using SpaceInvaders.Menus;
using Infrastructure.ObjectModel;
using SpaceInvaders.Actors;
using SpaceInvaders.CompositeActors;
using SpaceInvaders.GameCompositeActors;

namespace SpaceInvaders.Screens
{
    public class PlayScreen : GameScreen
    {
        private PauseScreen m_PauseScreen;
        private PlayersManager m_PlayersManager;
        private ScreensMananger m_ScreenManager;
        private GameOverScreen m_GameOverScreen;
        private SpaceInvaderSoundsManager m_SoundManager;

        public PlayScreen(Game i_Game) : base(i_Game)
        {
            Add(new BackgroundComposite(i_Game));
            Add(new PlayersComposite(i_Game, this));
            Add(new EnemiesComposite(i_Game, this));
            Add(new SpaceInvadersUI(i_Game));
            Add(new BarrierLine(i_Game));
            m_PauseScreen = new PauseScreen(i_Game);
            m_GameOverScreen = new GameOverScreen(i_Game);
            m_PlayersManager = Game.Services.GetService(typeof(PlayersManager)) as PlayersManager;
            m_PlayersManager.Player1LivesChanged += playerLifeChangedCallback;
            m_PlayersManager.Player2LivesChanged += playerLifeChangedCallback;
            m_PlayersManager.LevelClearedEvent += levelClearedCallback;
            m_ScreenManager = Game.Services.GetService(typeof(ScreensMananger)) as ScreensMananger;
            m_SoundManager = i_Game.Services.GetService(typeof(SpaceInvaderSoundsManager)) as SpaceInvaderSoundsManager;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.P))
            {
                ScreensManager.SetCurrentScreen(m_PauseScreen);
            }
            else if (InputManager.KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }
        }

        private void playerLifeChangedCallback()
        {
            if (!m_PlayersManager.CoOpMode)
            {
                if (m_PlayersManager.Player1Lives <= 0)
                {
                    executeGameOver();
                }
            }
            else if (m_PlayersManager.Player1Lives <= 0 && m_PlayersManager.Player2Lives <= 0)
            {
                executeGameOver();
            }
        }

        private void executeGameOver()
        {
            m_SoundManager.PlaySound("GameOver");
            ScreensManager.SetCurrentScreen(m_GameOverScreen);
        }

        private void levelClearedCallback()
        {
            ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game));
        }
    }
}
