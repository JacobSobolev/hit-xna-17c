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
    public class Player1 : Player
    {
        private const string k_AssetName = @"Assets\Ship01_32x32";

        private const bool v_UseMouseAsControl = true;

        public Player1(Game i_Game) 
            : base(k_AssetName, i_Game, v_UseMouseAsControl)
        {
        }

        protected override bool pressingMoveLeftControl()
        {
            bool returnValue = false;
            if (m_InputManager.KeyboardState.IsKeyDown(Keys.Left))
            {
                returnValue = true;
            }

            return returnValue;
        }

        protected override bool pressingMoveRightControl()
        {
            bool returnValue = false;
            if (m_InputManager.KeyboardState.IsKeyDown(Keys.Right))
            {
                returnValue = true;
            }

            return returnValue;
        }

        protected override bool pressedFireControl()
        {
            bool returnValue = false;
            if (m_InputManager.ButtonPressed(eInputButtons.Left) || m_InputManager.KeyPressed(Keys.Up))
            {
                returnValue = true;
            }

            return returnValue;
        }
    }
}
