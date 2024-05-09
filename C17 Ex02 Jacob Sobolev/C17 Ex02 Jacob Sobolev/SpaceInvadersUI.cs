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
    public class SpaceInvadersUI : RegisteredComponent
    {
        private const string k_Player1AssetName = @"Assets\Ship01_32x32";
        private const string k_Player2AssetName = @"Assets\Ship02_32x32";
        private const float k_Spacing = 10;
        private const float k_SoulCounterWidth = 100;
        private const float k_SoulCounterHeight = 20;
        private const float k_ScoreSheetWidth = 100;
        private const float k_ScoreSheetHeight = 22;
        private Player1 m_Player1;
        private Player2 m_Player2;
        private Vector2 m_SoulCounterPosition;
        private LivesBarUIComponnet m_LivesBarPlayer1;
        private LivesBarUIComponnet m_LivesBarPlayer2;

        public SpaceInvadersUI(Game i_Game, Player1 i_Player1, Player2 i_Player2)
            : base(i_Game, int.MaxValue )
        {
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
        }

        public override void Initialize()
        {
            Vector2 scoreMsgPosition = new Vector2(k_Spacing, k_Spacing);
            ScoreSheet scoreSheet = new ScoreSheet(Game, "P1 Score: ", scoreMsgPosition);
            m_Player1.m_ScoreChanged += scoreSheet.Player_ScoreChanged;
            scoreSheet.Color = Color.Blue;
            scoreMsgPosition = new Vector2(k_Spacing, k_Spacing + k_ScoreSheetHeight);
            scoreSheet = new ScoreSheet(Game, "P2 Score: ", scoreMsgPosition);
            scoreSheet.Color = Color.Green;
            m_Player2.m_ScoreChanged += scoreSheet.Player_ScoreChanged;
            m_SoulCounterPosition = new Vector2(Game.GraphicsDevice.Viewport.Width - k_SoulCounterWidth - k_Spacing, k_Spacing);
            m_LivesBarPlayer1 = new LivesBarUIComponnet(k_Player1AssetName, Game, m_SoulCounterPosition);
            m_Player1.m_LifeLost += m_LivesBarPlayer1.Player_LifeLost;
            m_SoulCounterPosition = new Vector2(Game.GraphicsDevice.Viewport.Width - k_SoulCounterWidth - k_Spacing, k_Spacing + k_SoulCounterHeight);
            m_LivesBarPlayer2 = new LivesBarUIComponnet(k_Player2AssetName, Game, m_SoulCounterPosition);
            m_Player2.m_LifeLost += m_LivesBarPlayer2.Player_LifeLost;
        }
    }
}
