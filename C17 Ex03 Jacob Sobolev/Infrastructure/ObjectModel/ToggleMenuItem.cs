using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.Managers;
using Infrastructure.ServiceInterfaces;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ObjectModel;

namespace Infrastructure.ObjectModel
{
    public class ToggleMenuItem : MenuItem
    {
        public event ActionDone m_ToggleDone;
        private string m_Text;
        private string m_TrueText = "On";
        public string TrueText
        {
            set { m_TrueText = value; }
        }

        private string m_FalseText = "Off";
        public string FalseText
        {
            set { m_FalseText = value; }
        }

        private bool m_IsTrue;
        public bool IsTrue
        {
            set { m_IsTrue = value; }
        }

        public ToggleMenuItem(Game i_Game, string i_Title, bool i_IsTrue)
            : base(i_Game, i_Title)
        {
            m_IsTrue = i_IsTrue;
            m_Text = i_Title;
            this.UpdateOrder = int.MinValue;
            updateText();
        }

        private void updateText()
        {
            if (m_IsTrue)
            {
                m_ButtonLabel.Text = m_Text + ": " + m_TrueText;
            }
            else
            {
                m_ButtonLabel.Text = m_Text + ": " + m_FalseText;
            }
        }

        public void Toggle()
        {
            m_ToggleDone.Invoke();
            m_IsTrue = !m_IsTrue;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (m_HasFocus && (m_InputManager.KeyPressed(Keys.PageDown) || m_HasFocus && m_InputManager.KeyPressed(Keys.PageUp) || m_InputManager.ScrollWheelDelta != 0))
            {
                Toggle();
            }
            updateText();
        }
    }
}
