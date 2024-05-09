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

namespace SpaceInvaders.Screens
{
    public class MenusScreen : GameScreen
    {
        private PlayersManager m_PlayersManager;

        public MenusScreen(Game i_Game) : base(i_Game)
        {
            MainMenu mainMenu = new MainMenu(Game, this);
            setMenu(mainMenu);
            m_PlayersManager = Game.Services.GetService(typeof(PlayersManager)) as PlayersManager;
        }

        public void setMenu(SubMenu i_MainMenu)
        {
            i_MainMenu.Enabled = true;
            i_MainMenu.Visible = true;
            Add(i_MainMenu);
        }

        public void ExitMenuScreen()
        {
            m_PlayersManager.ApplyMenuOptionsToObjects();
            ExitScreen();
        }
    }
}
