using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Infrastructure.Managers;

namespace Dreidels
{
    public class DreidelsGame : Game
    {
        private BasicEffect m_BasicEffect;
        private RasterizerState m_RasterizerState;
        private InputManager m_InputManager;
        private CameraManager m_CameraManager;
        private GraphicsDeviceManager m_Graphics;

        public DreidelsGame()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            m_BasicEffect = new BasicEffect(this.GraphicsDevice);
            m_BasicEffect.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            m_RasterizerState = new RasterizerState();
            m_InputManager = new InputManager(this);
            m_CameraManager = new CameraManager(this);
            Services.AddService(typeof(BasicEffect), m_BasicEffect);
            Services.AddService(typeof(RasterizerState), m_RasterizerState);
            Components.Add(new DreidelsManager(this));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_RasterizerState.CullMode = CullMode.CullCounterClockwiseFace;
        }

        protected override void UnloadContent()
        {
            if (m_BasicEffect != null)
            {
                m_BasicEffect.Dispose();
                m_BasicEffect = null;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            m_Graphics.GraphicsDevice.Clear(Color.DeepSkyBlue);
            BlendState blendState = new BlendState();
            SamplerState samplerState = new SamplerState();
            samplerState.AddressU = TextureAddressMode.Border;
            samplerState.AddressV = TextureAddressMode.Border;
            m_BasicEffect.GraphicsDevice.SamplerStates[0] = samplerState;
            base.Draw(gameTime);
        }
    }
}