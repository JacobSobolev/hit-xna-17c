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

namespace SpaceInvaders.Menus
{
    public class ScreenOptionsMenu : SubMenu
    {
        private ScreenOptionsManager m_ScreenOptionsManager;

        public ScreenOptionsMenu(Game i_Game, GameScreen i_Screen)
            : base(i_Game, "Screen Options", i_Screen)
        {
            m_ScreenOptionsManager = this.Game.Services.GetService(typeof(ScreenOptionsManager)) as ScreenOptionsManager;
        }

        public override void Initialize()
        {
            base.Initialize();
            ToggleMenuItem mouseVisabilityItem = new ToggleMenuItem(Game, "Mouse Visability", m_ScreenOptionsManager.IsMouseVisable);
            mouseVisabilityItem.m_ToggleDone += m_ScreenOptionsManager.ToggleMouseVisabilty;
            mouseVisabilityItem.PrevoiusMenu = this;
            AddItem(mouseVisabilityItem);
            ToggleMenuItem fullScreenModeItem = new ToggleMenuItem(Game, "Full Screen Mode", m_ScreenOptionsManager.IsFullScreen);
            fullScreenModeItem.m_ToggleDone += m_ScreenOptionsManager.ToggleFullScreen;
            fullScreenModeItem.PrevoiusMenu = this;
            AddItem(fullScreenModeItem);
            ToggleMenuItem allowWindowResizingItem = new ToggleMenuItem(Game, "Allow Window Resizing", m_ScreenOptionsManager.IsResizeAllowed);
            allowWindowResizingItem.m_ToggleDone += m_ScreenOptionsManager.ToggleAllowWindowResizing;
            fullScreenModeItem.PrevoiusMenu = this;
            AddItem(allowWindowResizingItem);
            ActionMenuItem doneItem = new ActionMenuItem(Game, "Done");
            doneItem.PrevoiusMenu = this;
            doneItem.m_ActionDone += ExitThisMenu;
            AddItem(doneItem);
        }
    }
}
