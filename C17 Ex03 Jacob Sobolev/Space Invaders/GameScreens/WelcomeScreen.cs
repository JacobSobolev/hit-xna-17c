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
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using SpaceInvaders.Menus;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ObjectModel;
using SpaceInvaders.Actors;
using SpaceInvaders.CompositeActors;

namespace SpaceInvaders.Screens
{
    public class WelcomeScreen : GameScreen
    {
        private MenusScreen m_MenuScreen;

        public WelcomeScreen(Game i_Game) : base(i_Game)
        {
            Add(new BackgroundComposite(i_Game));
            Add(new InstractionComposite(i_Game, "Wellcome", "Enter", "T", "Esc"));
            m_MenuScreen = new MenusScreen(i_Game);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_MenuScreen.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.Enter))
            {
                ExitScreen();
            }
            else if (InputManager.KeyPressed(Keys.T))
            {
                ScreensManager.SetCurrentScreen(m_MenuScreen);
                ExitScreen();
            }
            else if (InputManager.KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }
        }
    }
}
