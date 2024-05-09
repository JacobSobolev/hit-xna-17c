using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.Managers;
using Infrastructure.ServiceInterfaces;

namespace SpaceInvaders
{
    public class GameSpaceInvaders : Game
    {
        private const string k_ContentRootDirectoryPath = "Content";
        private GraphicsDeviceManager m_Graphics;
        private MotherShip m_MotherShip;
        private SpaceInvadersUI m_SpaceInvadersUI;
        private IInputManager m_InputManager = null;
        private SoundManager m_SoundManager = null;
        public EnemyFleet EnemyFleet { get; private set; }

        public Player1 Player1 { get; private set; }

        public Player2 Player2 { get; private set; }

        public GameSpaceInvaders() : base()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = k_ContentRootDirectoryPath;
            new PerPixelCollisionManager(this);
            m_InputManager = new InputManager(this);
            CollisionsManager collisionsManager = new CollisionsManager(this);
            new Background(this);
            EnemyFleet = new EnemyFleet(this);
            Player1 = new Player1(this);
            Player2 = new Player2(this);
            m_MotherShip = new MotherShip(this);
            m_SpaceInvadersUI = new SpaceInvadersUI(this, Player1, Player2);
            new BarrierLine(this);
            new SpaceInvaderEndGameComponent(this, Player1, Player2, EnemyFleet);
            m_SoundManager = new SoundManager(this);
            string soundFilesPath = @"C:/temp/XNA_Assets/Ex03/Sounds/";
            m_SoundManager.SetMusic(soundFilesPath + "BGMusic");
            m_SoundManager.AddSound("PlayerShot" , soundFilesPath + "SSGunShot");
            m_SoundManager.AddSound("EnemyShot", soundFilesPath + "EnemyGunShot");
            m_SoundManager.AddSound("BarrierHit", soundFilesPath + "BarrierHit");
            m_SoundManager.AddSound("EnemyKill", soundFilesPath + "EnemyKill");
            m_SoundManager.AddSound("GameOver", soundFilesPath + "GameOver");
            m_SoundManager.AddSound("LevelWin", soundFilesPath + "LevelWin");
            m_SoundManager.AddSound("LifeDie", soundFilesPath + "LifeDie");
            m_SoundManager.AddSound("MenuMove", soundFilesPath + "MenuMove");
            m_SoundManager.AddSound("MotherShipKill", soundFilesPath + "MotherShipKill");


        }

        protected override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
