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
    public class ScoreSheet : RegisteredComponent
    {
        private float m_ScoreToDisplay = 0;

        public SpriteFont SpriteFont
        {
            get { return m_Label.SpriteFont; }
            set { m_Label.SpriteFont = value; }
        }

        private string m_Text;

        public string Text
        {
            set { m_Text = value; }
        }

        private Label m_Label;

        public Color Color
        {
            get { return m_Label.TintColor; }
            set { m_Label.TintColor = value; }
        }

        public Vector2 Position
        {
            get { return m_Label.Position; }
            set { m_Label.Position = value; }
        }

        public ScoreSheet(Game i_Game, string i_Text, Vector2 i_Position)
            : base(i_Game, int.MaxValue)
        {
            m_Label = new Label(@"Fonts\Consolas", i_Game);
            Position = i_Position;
            m_Text = i_Text; 
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            m_Label.Messege = string.Format("{0} {1}", m_Text, m_ScoreToDisplay);
        }

        public void Player_ScoreChanged(float i_NewScore)
        {
            m_ScoreToDisplay = i_NewScore;
        }
    }
}
