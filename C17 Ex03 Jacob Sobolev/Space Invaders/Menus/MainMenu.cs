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
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ObjectModel;
using SpaceInvaders.Screens;

namespace SpaceInvaders.Menus
{
    public class MainMenu : SubMenu
    {
        private const string k_SoundName = "MenuMove";
        private PlayersManager m_PlayersManager;

        public MainMenu(Game i_Game, GameScreen i_Screen)
            : base(i_Game, "Main Menu", i_Screen)
        {
            m_SoundName = k_SoundName;
            m_PlayersManager = Game.Services.GetService(typeof(PlayersManager)) as PlayersManager;
        }

        public override void Initialize()
        {
            base.Initialize();
            SpaceInvaderSoundsManager soundManager = this.Game.Services.GetService(typeof(SpaceInvaderSoundsManager)) as SpaceInvaderSoundsManager;
            m_FocusMoved += soundManager.PlaySound;
            SoundOptionsMenu soundOptionsMenu = new SoundOptionsMenu(Game, m_GameScreen);
            soundOptionsMenu.PrevoiusMenu = this as SubMenu;
            (soundOptionsMenu as SubMenu).SoundName = k_SoundName;
            soundOptionsMenu.m_FocusMoved += soundManager.PlaySound;
            ScreenOptionsMenu screenOptionsMenu = new ScreenOptionsMenu(Game, m_GameScreen);
            screenOptionsMenu.PrevoiusMenu = this as SubMenu;
            screenOptionsMenu.SoundName = k_SoundName;
            screenOptionsMenu.m_FocusMoved += soundManager.PlaySound;
            ToggleMenuItem numberOfPlayersItem = new ToggleMenuItem(Game, "Players Options", m_PlayersManager.CoOpMode);
            numberOfPlayersItem.m_ToggleDone += m_PlayersManager.ToggleCoOpMode; 
            numberOfPlayersItem.Enabled = true;
            numberOfPlayersItem.TrueText = "Two";
            numberOfPlayersItem.FalseText = "One";
            ActionMenuItem playItem = new ActionMenuItem(Game, "Play");
            playItem.m_ActionDone += (m_GameScreen as MenusScreen).ExitMenuScreen;
            playItem.m_ActionDone += ExitThisMenu;
            playItem.Enabled = true;
            ActionMenuItem quitItem = new ActionMenuItem(Game, "Quit");
            quitItem.Enabled = true;
            quitItem.m_ActionDone += Game.Exit;
            AddItem(soundOptionsMenu);
            AddItem(screenOptionsMenu);
            AddItem(numberOfPlayersItem);
            AddItem(playItem);
            AddItem(quitItem);
        }
    }
}
