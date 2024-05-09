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
using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.GameCompositeActors
{
    public class PlayersComposite : CompositeDrawableComponent<Sprite>
    {
        private Player1 m_Player1;
        private Player2 m_Player2;
        private PlayersManager m_PlayersManager;

        public PlayersComposite(Game i_Game, GameScreen i_ParentScreen) : base(i_Game)
        {
            m_PlayersManager = Game.Services.GetService(typeof(PlayersManager)) as PlayersManager;
            m_Player1 = new Player1(i_Game, i_ParentScreen);
            Add(m_Player1);
            m_Player2 = new Player2(i_Game, i_ParentScreen);
            Add(m_Player2);
            bool isCoOp = m_PlayersManager.CoOpMode;
            m_Player2.Enabled = isCoOp;
            m_Player2.Visible = isCoOp;

            BlendState = BlendState.NonPremultiplied;
            m_PlayersManager.MenuExitEvent += menuExitEventCallback;
        }

        private void menuExitEventCallback()
        {
            m_Player1.Enabled = true;
            m_Player1.Visible = true;

            bool isCoOp = m_PlayersManager.CoOpMode;
            m_Player2.Enabled = isCoOp;
            m_Player2.Visible = isCoOp;
        }
    }
}
