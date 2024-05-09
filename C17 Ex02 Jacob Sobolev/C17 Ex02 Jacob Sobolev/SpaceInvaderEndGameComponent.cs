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
    public class SpaceInvaderEndGameComponent : RegisteredComponent
    {
        private Player1 m_Player1;
        private Player2 m_Player2;
        private EnemyFleet m_EnemyFleet;
        private bool m_GameOver = false;

        public SpaceInvaderEndGameComponent(Game i_Game, Player1 i_Player1, Player2 i_Player2, EnemyFleet i_EnemyFleet)
            : base(i_Game, int.MaxValue)
        {
            m_GameOver = false;
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            m_EnemyFleet = i_EnemyFleet;
            m_Player1.Disposed += mainObjectsDispoed;
            m_Player2.Disposed += mainObjectsDispoed;
            m_EnemyFleet.Disposed += mainObjectsDispoed;
        }

        private void mainObjectsDispoed(object sender, System.EventArgs e)
        {
            if (((m_Player1.Lives <= 0 && m_Player2.Lives <= 0) || m_EnemyFleet.EmptyFleet ) && !m_GameOver)
            {
                m_GameOver = true;
                System.Windows.Forms.MessageBox.Show(string.Format("Player 1 Score: {0}{1}Player 2 Score: {2}", m_Player1.Score, System.Environment.NewLine, m_Player2.Score), "Game Over");
                Game.Exit();
            }     
        }

        private void checkIfGameEnded()
        {
            if ((Game.Services.GetService(typeof(IInputManager)) as IInputManager).ButtonPressed(eInputButtons.Back) ||
                (Game.Services.GetService(typeof(IInputManager)) as IInputManager).KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            checkIfGameEnded();
            base.Update(i_GameTime);
        }
    }
}
