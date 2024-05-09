using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.Managers;
using Infrastructure.ServiceInterfaces;

namespace SpaceInvaders
{
    public class BaseGame : Game
    {
        protected const string k_ContentRootDirectoryPath = "Content";
        protected GraphicsDeviceManager m_Graphics;

        public BaseGame() : base()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = k_ContentRootDirectoryPath;
            new InputManager(this);
        }
    }
}
