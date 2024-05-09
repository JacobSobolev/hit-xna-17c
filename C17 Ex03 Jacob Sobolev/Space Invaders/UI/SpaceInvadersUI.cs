using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;

namespace SpaceInvaders
{
    public class SpaceInvadersUI : CompositeDrawableComponent<GameComponent>
    {
        private const int k_defulatLivesNum = 3;
        private const string k_Player1AssetName = @"Assets\Ship01_32x32";
        private const string k_Player2AssetName = @"Assets\Ship02_32x32";
        private const float k_Spacing = 10;
        private const float k_SoulCounterWidth = 100;
        private const float k_SoulCounterHeight = 20;
        private const float k_ScoreSheetWidth = 100;
        private const float k_ScoreSheetHeight = 22;
        
        private LivesBarUIComponnet m_LivesBarPlayer1;
        private LivesBarUIComponnet m_LivesBarPlayer2;
        private SpriteLabel m_Player1ScoreLabel;
        private SpriteLabel m_Player2ScoreLabel;
        private PlayersManager m_PlayersManager;

        public SpaceInvadersUI(Game i_Game)
            : base(i_Game)
        {
            m_PlayersManager = Game.Services.GetService(typeof(PlayersManager)) as PlayersManager;
            m_PlayersManager.Player1ScoreChanged += Player1ScoreChangeCallback;
            m_PlayersManager.Player2ScoreChanged += Player2ScoreChangeCallback;
            m_PlayersManager.MenuExitEvent += menuExitEventCallback;
            m_Player1ScoreLabel = new SpriteLabel(@"Fonts\Consolas", Game);
            m_Player1ScoreLabel.Text = string.Empty;
            m_Player2ScoreLabel = new SpriteLabel(@"Fonts\Consolas", Game);
            m_Player2ScoreLabel.Text = string.Empty;
            Add(m_Player1ScoreLabel);
            Add(m_Player2ScoreLabel);           
        }

        public override void Initialize()
        {
            base.Initialize();
            Vector2 soulCounterPosition;
            soulCounterPosition = new Vector2(Game.GraphicsDevice.Viewport.Width - k_SoulCounterWidth - k_Spacing, k_Spacing);
            m_LivesBarPlayer1 = new LivesBarUIComponnet(k_Player1AssetName, Game, soulCounterPosition, k_defulatLivesNum);
            Add(m_LivesBarPlayer1);
            soulCounterPosition.Y += k_SoulCounterHeight;
            m_LivesBarPlayer2 = new LivesBarUIComponnet(k_Player2AssetName, Game, soulCounterPosition, k_defulatLivesNum);
            Add(m_LivesBarPlayer2);
            m_PlayersManager.Player1LivesChanged += Player1LifeLostCallback;
            m_PlayersManager.Player2LivesChanged += Player2LifeLostCallback;

            Vector2 scoreMsgPosition = new Vector2(k_Spacing, k_Spacing);
            
            m_Player1ScoreLabel.Text = string.Format("P1 Score: {0}", m_PlayersManager.Player1Score);
            m_Player1ScoreLabel.Position = scoreMsgPosition;
            m_Player1ScoreLabel.TintColor = Color.Blue;            
            scoreMsgPosition.Y += k_ScoreSheetHeight;
            m_Player2ScoreLabel.Text = string.Format("P2 Score: {0}", m_PlayersManager.Player2Score);
            m_Player2ScoreLabel.Position = scoreMsgPosition;
            m_Player2ScoreLabel.TintColor = Color.Green;

            bool isCoOp = m_PlayersManager.CoOpMode;
            m_LivesBarPlayer2.Enabled = isCoOp;
            m_LivesBarPlayer2.Visible = isCoOp;
            m_Player2ScoreLabel.Visible = isCoOp;
            m_Player2ScoreLabel.Enabled = isCoOp;
        }

        private void Player1ScoreChangeCallback()
        {
            m_Player1ScoreLabel.Text = string.Format("P1 Score: {0}", m_PlayersManager.Player1Score);
        }

        private void Player2ScoreChangeCallback()
        {
            m_Player2ScoreLabel.Text = string.Format("P2 Score: {0}", m_PlayersManager.Player2Score);
        }

        private void Player1LifeLostCallback()
        {
            m_LivesBarPlayer1.Player_LifeLost();
        }

        private void Player2LifeLostCallback()
        {
            m_LivesBarPlayer2.Player_LifeLost();
        }

        private void menuExitEventCallback()
        {
            bool isCoOp = m_PlayersManager.CoOpMode;
            m_LivesBarPlayer2.Enabled = isCoOp;
            m_LivesBarPlayer2.Visible = isCoOp;
            m_Player2ScoreLabel.Visible = isCoOp;
            m_Player2ScoreLabel.Enabled = isCoOp;
        }
    }
}
