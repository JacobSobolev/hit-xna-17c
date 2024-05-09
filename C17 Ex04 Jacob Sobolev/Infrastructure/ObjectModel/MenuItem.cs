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
    public class MenuItem : CompositeDrawableComponent<IGameComponent>, IMenuItem
    {
        protected bool m_HasFocus = false;
        protected SpriteLabel m_ButtonLabel;
        protected IInputManager m_InputManager;
        protected SubMenu m_PreviousMenu;
        public MenuItem(Game i_Game, string i_Title)
            : base(i_Game)
        {   
            m_ButtonLabel = new SpriteLabel(@"Fonts\Consolas", i_Game);
            m_ButtonLabel.Text = i_Title;
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            Enabled = false;
        }

        public SpriteLabel ButtonLabel
        {
            get { return m_ButtonLabel; }
        }

        public Color ButtonTintColor
        {
            set { m_ButtonLabel.TintColor = value; }
        }

        public bool HasFocus
        {
            set { m_HasFocus = value; }
        }

        public SubMenu PrevoiusMenu
        {
            set { m_PreviousMenu = value; }
        }
    }
}
