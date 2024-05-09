using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.Managers;
using Infrastructure.ServiceInterfaces;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using SpaceInvaders.Menus;
using Infrastructure.ObjectModel;
using SpaceInvaders.Actors;
using SpaceInvaders.CompositeActors;

namespace SpaceInvaders.Screens
{
    public class GameOverScreen : GameScreen
    {
        private SpriteLabel m_Player1ScoreLabel;
        private SpriteLabel m_Player2ScoreLabel;
        private PlayersManager m_PlayersManager;

        public GameOverScreen(Game i_Game) : base(i_Game)
        {
            m_PlayersManager = Game.Services.GetService(typeof(PlayersManager)) as PlayersManager;
            Add(new InstractionComposite(i_Game, "Game Over!", "Home", "T", "Esc"));
            m_Player1ScoreLabel = new SpriteLabel(@"Fonts\Consolas", i_Game);
            m_Player1ScoreLabel.Text = string.Format("Player 1 Score is: {0}", m_PlayersManager.Player1Score);
            m_Player2ScoreLabel = new SpriteLabel(@"Fonts\Consolas", i_Game);
            m_Player2ScoreLabel.Text = string.Format("Player 2 Score is: {0}", m_PlayersManager.Player2Score);
            Add(m_Player1ScoreLabel);
            Add(m_Player2ScoreLabel);
            bool isCoOp = m_PlayersManager.CoOpMode;
            m_Player2ScoreLabel.Visible = isCoOp;
            m_Player2ScoreLabel.Enabled = isCoOp;
            m_PlayersManager.MenuExitEvent += menuExitEventCallback;
            m_PlayersManager.Player1LivesChanged += playerLifeChangedCallback;
            m_PlayersManager.Player2LivesChanged += playerLifeChangedCallback;
        }

        public override void Initialize()
        {
            base.Initialize();
            m_Player1ScoreLabel.PositionOrigin = m_Player1ScoreLabel.TextCenter;
            m_Player1ScoreLabel.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2 - 50);
            m_Player2ScoreLabel.PositionOrigin = m_Player2ScoreLabel.TextCenter;
            m_Player2ScoreLabel.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2 - 20);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputManager.KeyPressed(Keys.Home))
            {
                m_PlayersManager.ApplyMenuOptionsToObjects();
                ExitScreen();
            }
            else if (InputManager.KeyPressed(Keys.T))
            {
                MenusScreen menusScreen = new MenusScreen(Game);
                ScreensManager.SetCurrentScreen(menusScreen);
                ExitScreen();
            }
            else if (InputManager.KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }
        }

        private void playerLifeChangedCallback()
        {
            if (m_PlayersManager.Player1Lives == 0)
            {
                m_Player1ScoreLabel.Text = string.Format("Player 1 Score is: {0}", m_PlayersManager.Player1Score);
            }

            if (m_PlayersManager.Player2Lives == 0)
            {
                m_Player2ScoreLabel.Text = string.Format("Player 2 Score is: {0}", m_PlayersManager.Player2Score);
            }
        }

        private void menuExitEventCallback()
        {
            bool isCoOp = m_PlayersManager.CoOpMode;
            m_Player2ScoreLabel.Visible = isCoOp;
            m_Player2ScoreLabel.Enabled = isCoOp;
        }
    }
}