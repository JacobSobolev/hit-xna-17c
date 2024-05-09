using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.CompositeActors
{
    public class InstractionComposite : CompositeDrawableComponent<SpriteLabel>
    {
        private SpriteLabel m_TitleLabel;
        private SpriteLabel m_InstractionLabelBegin;
        private SpriteLabel m_InstractionLabelMenu;
        private SpriteLabel m_InstractionLabelExit;

        public InstractionComposite(Game i_Game, string i_TitleLabel, string i_BeginGameKey, string i_OptionsKey, string i_ExitKey) 
            : base(i_Game)
        {
            m_TitleLabel = new SpriteLabel(@"Fonts\Consolas", i_Game);
            m_TitleLabel.Text = i_TitleLabel;
            m_InstractionLabelBegin = new SpriteLabel(@"Fonts\Consolas", i_Game);
            m_InstractionLabelBegin.Text = string.Format("To Begin Press {0}", i_BeginGameKey);
            m_InstractionLabelMenu = new SpriteLabel(@"Fonts\Consolas", i_Game);
            m_InstractionLabelMenu.Text = string.Format("For Option Press {0}", i_OptionsKey);
            m_InstractionLabelExit = new SpriteLabel(@"Fonts\Consolas", i_Game);
            m_InstractionLabelExit.Text = string.Format("To Exit Press {0}", i_ExitKey);
            Add(m_TitleLabel);
            Add(m_InstractionLabelBegin);
            Add(m_InstractionLabelMenu);
            Add(m_InstractionLabelExit);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_TitleLabel.Scales = new Vector2(3);
            m_TitleLabel.PositionOrigin = m_TitleLabel.TextCenter;
            m_TitleLabel.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2 - 150);
            m_InstractionLabelBegin.PositionOrigin = m_InstractionLabelBegin.TextCenter;
            m_InstractionLabelBegin.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2 + 80);
            m_InstractionLabelMenu.PositionOrigin = m_InstractionLabelMenu.TextCenter;
            m_InstractionLabelMenu.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2 + 130);
            m_InstractionLabelExit.PositionOrigin = m_InstractionLabelExit.TextCenter;
            m_InstractionLabelExit.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2 + 180);
        }
    }
}
