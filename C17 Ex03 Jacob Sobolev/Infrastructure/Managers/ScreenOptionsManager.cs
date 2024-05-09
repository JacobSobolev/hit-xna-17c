using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel;

namespace Infrastructure.Managers
{
    public class ScreenOptionsManager : GameService
    {
        private GraphicsDeviceManager m_GraphicsDeviceManager;
        private bool m_IsFullScreen = false;
        public bool IsFullScreen
        {
            get { return m_IsFullScreen; }
        }

        private bool m_IsMouseVisable = false;
        public bool IsMouseVisable
        {
            get { return m_IsMouseVisable; }
        }

        private bool m_IsResizeAllowed = false;
        public bool IsResizeAllowed
        {
            get { return m_IsResizeAllowed; }
        }

        public ScreenOptionsManager(Game i_Game)
            : base(i_Game, int.MaxValue)
        {
            m_GraphicsDeviceManager = this.Game.Services.GetService(typeof(IGraphicsDeviceManager)) as GraphicsDeviceManager;
        }

        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(ScreenOptionsManager), this);
        }

        public void ToggleMouseVisabilty()
        {
            m_IsMouseVisable = !m_IsMouseVisable;
            Game.IsMouseVisible = !Game.IsMouseVisible;
        }

        public void ToggleAllowWindowResizing()
        {
            m_IsResizeAllowed = !m_IsResizeAllowed;
            Game.Window.AllowUserResizing = !Game.Window.AllowUserResizing;
        }

        public void ToggleFullScreen()
        {
            m_IsFullScreen = !m_IsFullScreen;
            m_GraphicsDeviceManager.ToggleFullScreen(); 
        }
    }
}
