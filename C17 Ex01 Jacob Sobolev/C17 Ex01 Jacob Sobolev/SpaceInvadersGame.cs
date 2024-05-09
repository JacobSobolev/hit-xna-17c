using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;

namespace C17Ex01
{
    public class SpaceInvadersGame : BaseGame
    {
        private const int k_GameWidth = 800;
        private const int k_GameHeight = 700;
        private const string k_ContentRootDirectoryPath = "Content";
        private GraphicsDeviceManager m_Graphics;
        private EnemyFleet m_EnemyFleet;
        private Player m_Player;
        private MotherShip m_MotherShip;

        public SpaceInvadersGame() : base()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = k_ContentRootDirectoryPath;
            IsMouseVisible = true;
            m_EnemyFleet = new EnemyFleet(this);
            m_Player = new Player(this);
            m_MotherShip = new MotherShip(this);
            m_Graphics.PreferredBackBufferWidth = k_GameWidth;
            m_Graphics.PreferredBackBufferHeight = k_GameHeight;
            m_Graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            Components.Add(new Background(this));
            Components.Add(m_Player);
            Components.Add(m_EnemyFleet);
            Components.Add(m_MotherShip);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        private void checkIfGameEnded()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || InputComponent.KeyBoardCurrentState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                Exit();
            }

            if (m_Player.Lives <= 0 || CollisionManager.CheckIfEnemyCrashesPlayer(m_EnemyFleet, m_Player) || m_EnemyFleet.Enemies.Count == 0)
            {
                MessageBox.Show(string.Format("Youre Score: {0}", m_Player.Score), "Game Over");
                Exit();
            }
        }

        protected override void Update(GameTime i_GameTime)
        {
            InputComponent.GetCurrentState();
            base.Update(i_GameTime);
            CollisionManager.CheckEnemyHit(m_EnemyFleet, m_Player);
            CollisionManager.CheckPlayerHit(m_EnemyFleet, m_Player);
            CollisionManager.CheckMotherShipHit(m_MotherShip, m_Player);
            checkIfGameEnded();
            InputComponent.MoveOnFromCurrentState();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SpriteBatch.Begin();
            foreach (DrawableGameComponent component in Components)
            {
                component.Draw(gameTime);
            }

            base.Draw(gameTime);
            SpriteBatch.End();
        }
    }
}
