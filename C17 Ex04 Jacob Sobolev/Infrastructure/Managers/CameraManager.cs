using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ServiceInterfaces;
using Infrastructure.ObjectModel;

namespace Infrastructure.Managers
{
    public class CameraManager : GameService, ICameraManager
    {
        private Matrix m_CameraSettings;
        public Matrix CameraSettings
        {
            get { return m_CameraSettings; }
        }

        private Matrix m_CameraState;
        public Matrix CameraState
        {
            get { return m_CameraState; }
        }

        private Vector3 m_CameraLooksAt;
        public Vector3 CameraLooksAt
        {
            get { return m_CameraLooksAt; }
            set { m_CameraLooksAt = value; }
        }

        private Vector3 m_CameraLocation;
        public Vector3 CameraLocation
        {
            get { return m_CameraLocation; }
            set { m_CameraLocation = value; }
        }

        private Vector3 m_CameraUpDirection;
        public Vector3 CameraUpDirection
        {
            get { return m_CameraUpDirection; }
            set { m_CameraUpDirection = value; }
        }

        private float m_NearPlaneDistance;
        public float NearPlaneDistance
        {
            get { return m_NearPlaneDistance; }
        }

        private float m_FarPlaneDistance;
        public float FarPlaneDistance
        {
            get { return m_FarPlaneDistance; }
        }

        private float m_ViewAngle;
        public float ViewAngle
        {
            get { return m_ViewAngle; }
        }

        private InputManager m_InputManager;

        public CameraManager(Game i_Game) : base(i_Game)
        {
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(ICameraManager), this);
        }

        public void setCameraState()
        {
            m_CameraState = Matrix.CreateLookAt(
                m_CameraLocation, m_CameraLooksAt, m_CameraUpDirection);
        }

        public void setCameraSettings()
        {
            m_CameraSettings = Matrix.CreatePerspectiveFieldOfView(
                m_ViewAngle,
                Game.GraphicsDevice.Viewport.AspectRatio,
                m_NearPlaneDistance,
                m_FarPlaneDistance);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_NearPlaneDistance = 0.5f;
            m_FarPlaneDistance = 1000.0f;
            m_ViewAngle = MathHelper.PiOver2;
            m_CameraLooksAt = Vector3.Zero;
            m_CameraLocation = new Vector3(0, 15, 75);
            m_CameraUpDirection = new Vector3(0, 1, 0);
            setCameraSettings();
            setCameraState();
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as InputManager;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (m_InputManager.KeyboardState.IsKeyDown(Keys.Right))
            {
                m_CameraState.Translation -= new Vector3(30 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0, 0);
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(Keys.Left))
            {
                m_CameraState.Translation += new Vector3(30 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0, 0);
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(Keys.Up))
            {
                m_CameraState.Translation += new Vector3(0, 0, 30 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(Keys.Down))
            {
                m_CameraState.Translation -= new Vector3(0, 0, 30 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
    }
}
