using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.Screens
{
    public class PauseScreen : GameScreen
    {
        private SpriteLabel m_ScreenTitleLabel;
        private SpriteLabel m_InstractionsLabel;

        public PauseScreen(Game i_Game) 
            : base(i_Game)
        {
            m_ScreenTitleLabel = new SpriteLabel(@"Fonts\Consolas", i_Game);
            m_ScreenTitleLabel.Text = "Puase";
            m_InstractionsLabel = new SpriteLabel(@"Fonts\Consolas", i_Game);
            m_InstractionsLabel.Text = "To Resmue Press R";
            Add(m_ScreenTitleLabel);
            Add(m_InstractionsLabel);
            this.IsModal = true;
            this.IsOverlayed = true;
            this.UseGradientBackground = true;
            this.BlackTintAlpha = 0.65f;
        }

        public override void Initialize()
        {
            base.Initialize();
            m_ScreenTitleLabel.Scales = new Vector2(3);
            m_ScreenTitleLabel.PositionOrigin = m_ScreenTitleLabel.TextCenter;
            m_ScreenTitleLabel.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2 - 150);
            m_InstractionsLabel.PositionOrigin = m_InstractionsLabel.TextCenter;
            m_InstractionsLabel.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2 + 80);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputManager.KeyPressed(Keys.R))
            {
                ExitScreen();
            }
        }
    }
}