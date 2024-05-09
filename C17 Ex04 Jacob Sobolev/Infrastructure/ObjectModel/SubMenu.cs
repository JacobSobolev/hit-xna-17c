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

namespace Infrastructure.ObjectModel
{
    public delegate void FocusMoved(string i_MenuMobeSoundName);

    public class SubMenu : MenuItem
    {
        public event FocusMoved m_FocusMoved;
        protected GameScreen m_GameScreen;
        protected string m_SoundName;
        public string SoundName
        {
            set { m_SoundName = value; }
        }

        protected bool m_StartedWorking = false;
        public bool StartedWorking
        {
            get { return m_StartedWorking; }
        }

        private const float k_TitleDistanceFromTop = 50;
        private const float k_ItemsSpacing = 30;
        private List<IMenuItem> m_Menus;
        private SpriteLabel m_TitleLabel;
        private int m_FocusedMenuItemIndex;
        public SubMenu(Game i_Game, string i_Title, GameScreen i_Screen)
            : base(i_Game, i_Title)
        {
            this.Enabled = false;
            this.Visible = false;
            m_TitleLabel = new SpriteLabel(@"Fonts\Consolas", i_Game);
            m_TitleLabel.Text = i_Title;
            m_TitleLabel.Scales = new Vector2(2, 2);
            m_GameScreen = i_Screen;

            Add(m_TitleLabel);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_Menus = new List<IMenuItem>();
            m_TitleLabel.Position = new Vector2((Game.GraphicsDevice.Viewport.Width / 2) - (m_TitleLabel.Width / 2), k_TitleDistanceFromTop);
        }

        public void AddItem(IMenuItem i_MenuItem)
        {
            Add(i_MenuItem.ButtonLabel);
            m_Menus.Add(i_MenuItem);
            m_GameScreen.Add(i_MenuItem as CompositeDrawableComponent<IGameComponent>);
            i_MenuItem.ButtonLabel.Position =
                new Vector2
                ((Game.GraphicsDevice.Viewport.Width / 2) - (i_MenuItem.ButtonLabel.Width / 2),
                this.m_TitleLabel.Position.Y + k_ItemsSpacing + m_Menus.Count * (i_MenuItem.ButtonLabel.Height + k_ItemsSpacing));
            if (m_Menus.Count == 1)
            {
                m_FocusedMenuItemIndex = 0;
                updateActiveMenu(0);
            }
        }

        private void updateActiveMenu(int i_DirectionToMove)
        {
            m_Menus[m_FocusedMenuItemIndex].HasFocus = false;
            m_Menus[m_FocusedMenuItemIndex].ButtonTintColor = Color.White;
            m_FocusedMenuItemIndex = (m_FocusedMenuItemIndex + i_DirectionToMove) % m_Menus.Count;
            m_Menus[m_FocusedMenuItemIndex].ButtonTintColor = Color.Yellow;
            m_Menus[m_FocusedMenuItemIndex].HasFocus = true;
        }

        private void enableItems()
        {
            foreach (IMenuItem menuItem in m_Menus)
            {
                if (!(menuItem is SubMenu))
                {
                    (menuItem as MenuItem).Enabled = true;
                }
            }
        }

        private void DisableItems()
        {
            foreach (IMenuItem menuItem in m_Menus)
            {
                if (!(menuItem is SubMenu))
                {
                    (menuItem as MenuItem).Enabled = false;
                }
            }
        }

        protected void OpenSubMenu(SubMenu i_SubMenu)
        {
            this.Visible = false;
            this.Enabled = false;
            m_StartedWorking = false;
            DisableItems();
            i_SubMenu.m_StartedWorking = false;
            i_SubMenu.Enabled = true;
            i_SubMenu.Visible = true;
            i_SubMenu.enableItems();
        }

        protected void ExitThisMenu()
        {
            if (m_PreviousMenu != null)
            {
                OpenSubMenu(m_PreviousMenu);
            }
            else
            {
                this.Visible = false;
                this.Enabled = false;
                m_StartedWorking = false;
                DisableItems();
            }
        }

        private void checkIfMouseOverAnItem()
        {
            MouseState mouseState = m_InputManager.MouseState;
            Point mousePosition = new Point((int)mouseState.X, (int)mouseState.Y);
            int i = 0;
            foreach (IMenuItem menuItem in m_Menus)
            {
                if (menuItem.ButtonLabel.Bounds.Contains(mousePosition) && menuItem != m_Menus[m_FocusedMenuItemIndex])
                {
                    int newActiveIndex = (i - m_FocusedMenuItemIndex) % m_Menus.Count;
                    updateActiveMenu(newActiveIndex);
                    m_FocusMoved.Invoke(m_SoundName);
                    break;
                }
                i++;
            }
        }

        private const int k_Down = 1;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (m_InputManager.KeyPressed(Keys.Down))
            {
                updateActiveMenu(k_Down);
                m_FocusMoved.Invoke(m_SoundName);
            }

            if (m_InputManager.KeyPressed(Keys.Up))
            {
                int up = m_Menus.Count - 1;
                updateActiveMenu(up);
                m_FocusMoved.Invoke(m_SoundName);
            }

            if (m_InputManager.MousePositionDelta != Vector2.Zero)
            {
                checkIfMouseOverAnItem();
            }

            if ((m_InputManager.KeyPressed(Keys.Enter) || m_InputManager.ButtonPressed(eInputButtons.Left)) && this.m_StartedWorking)
            {
                if (m_Menus.Count != 0 && m_Menus[m_FocusedMenuItemIndex] is SubMenu)
                {
                    SubMenu subMenu = m_Menus[m_FocusedMenuItemIndex] as SubMenu;
                    OpenSubMenu(subMenu);
                }
            }
            else if (m_InputManager.KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }

            if (this.Enabled == true)
            {
                m_StartedWorking = true;
            }
        }
    }
}
