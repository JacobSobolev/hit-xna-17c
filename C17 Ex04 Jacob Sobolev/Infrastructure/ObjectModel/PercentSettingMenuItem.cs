using System.Text;
using System;
using System.Threading.Tasks;
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
    public delegate void SettingChanged(float i_DataChanged);

    public class PercentSettingMenuItem : MenuItem
    {
        public event SettingChanged m_SettingChanged;
        private float m_Data;
        private string m_Text;
        public PercentSettingMenuItem(Game i_Game, string i_Title, float i_Data)
            : base(i_Game, i_Title)
        {
            m_Data = i_Data;
            m_Text = i_Title;
            m_ButtonLabel.Text = string.Format("{0}: {1}", m_Text, convertDataToPercent(m_Data));
        }

        private int convertDataToPercent (float i_Data)
        {
            float toReturn = i_Data * 10;
            toReturn = (float)Math.Round(toReturn);
            return (int)(toReturn * 10);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (m_HasFocus)
            {
                if (m_InputManager.KeyPressed(Keys.PageDown))
                {
                    ChangeData((float)-0.1);
                }
                else if (m_InputManager.KeyPressed(Keys.PageUp))
                {
                    ChangeData((float)0.1);
                }

                m_ButtonLabel.Text = string.Format("{0}: {1}", m_Text, convertDataToPercent(m_Data));
            }
        }

        private void ChangeData(float i_AmountToAdd)
        {
            m_Data += i_AmountToAdd;
            m_Data = MathHelper.Clamp(m_Data, 0, 1);
            m_SettingChanged.Invoke(m_Data);
        }
    }
}
