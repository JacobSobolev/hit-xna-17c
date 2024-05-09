using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Infrastructure.ServiceInterfaces;
using Infrastructure.Managers;

namespace Infrastructure.ObjectModel
{
    public  abstract class Model3D : LoadableDrawableComponent
    {
        protected Vector3 m_Position;
        public Vector3 Position
        {
            set { m_Position = value; }
        }
        protected Vector3 m_Rotation = Vector3.Zero;
        protected Vector3 m_Scales = Vector3.One;
        protected Matrix m_WorldMatrix = Matrix.Identity;
        protected VertexPositionTexture[] m_Vertices;
        protected BasicEffect m_BasicEffect;
        protected RasterizerState m_RasterizerState;
        protected Texture2D m_Texture;
        protected InputManager m_InputManager;
        protected CameraManager m_CameraManager;

        public Model3D(string i_AssetName, Game i_Game)
            : base(i_AssetName, i_Game, int.MaxValue)
        { }

        protected override void InitBounds()
        {
            //TO DO 
        }

        protected override void DrawBoundingBox()
        { }

        public override void Initialize()
        {
            base.Initialize();
            m_CameraManager = Game.Services.GetService(typeof(ICameraManager)) as CameraManager;
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as InputManager;
            LoadContent();
        }

        protected override void LoadContent()
        {
            m_Texture = Game.Content.Load<Texture2D>(m_AssetName);

            if (m_BasicEffect == null)
            {
                m_BasicEffect =
                    Game.Services.GetService(typeof(BasicEffect)) as BasicEffect;

                if (m_BasicEffect == null)
                {
                    m_BasicEffect = new BasicEffect(Game.GraphicsDevice);
                }
            }

            if (m_RasterizerState == null)
            {
                m_RasterizerState =
                    Game.Services.GetService(typeof(RasterizerState)) as RasterizerState;

                if (m_RasterizerState == null)
                {
                    m_RasterizerState = new RasterizerState();
                }
            }
            SamplerState samplerState = new SamplerState();
            samplerState.AddressU = TextureAddressMode.Border;
            samplerState.AddressV = TextureAddressMode.Border;
            m_BasicEffect.GraphicsDevice.SamplerStates[0] = samplerState;
            base.LoadContent();
        }

        private void buildWorldMatrix()
        {
            m_WorldMatrix = Matrix.Identity * Matrix.CreateScale(m_Scales) * Matrix.CreateRotationX(m_Rotation.X) * Matrix.CreateRotationY(m_Rotation.Y) * Matrix.CreateRotationZ(m_Rotation.Z) * Matrix.CreateTranslation(m_Position);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            buildWorldMatrix();
        }
        public override void Draw(GameTime gameTime)
        {  
            m_BasicEffect.View = m_CameraManager.CameraState;
            m_BasicEffect.Projection = m_CameraManager.CameraSettings;
            m_BasicEffect.World = m_WorldMatrix;
            base.Draw(gameTime);
        }

    }
}
