using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders
{
    public class Player2 : Player
    {
        private const string k_AssetName = @"Assets\Ship02_32x32";
        private const bool v_UseMouseAsControl = true;

        public Player2(Game i_Game, GameScreen i_ActiveScreen) 
            : base(k_AssetName, i_Game, !v_UseMouseAsControl, i_ActiveScreen)
        {
            m_PlayersManager.Player2Lives = m_Lives;
        }

        protected override bool pressingMoveLeftControl()
        {
            bool returnValue = false;
            if (m_InputManager.KeyboardState.IsKeyDown(Keys.D))
            {
                returnValue = true;
            }

            return returnValue;
        }

        protected override bool pressingMoveRightControl()
        {
            bool returnValue = false;
            if (m_InputManager.KeyboardState.IsKeyDown(Keys.G))
            {
                returnValue = true;
            }

            return returnValue;
        }

        protected override bool pressedFireControl()
        {
            bool returnValue = false;
            if (m_InputManager.KeyPressed(Keys.R))
            {
                returnValue = true;
            }

            return returnValue;
        }

        public override void ScorePoints(float i_ScoreToAdd)
        {
            base.ScorePoints(i_ScoreToAdd);
            m_PlayersManager.Player2Score = m_Score;
        }

        protected override void takeHit()
        {
            base.takeHit();
            m_PlayersManager.Player2Lives = m_Lives;
        }

        protected override void dieAnimationFinished(object sender, EventArgs e)
        {
            base.dieAnimationFinished(sender, e);
            m_PlayersManager.Player2Lives = m_Lives;
        }
    }
}
