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
    public delegate void ActionDone();

    public class ActionMenuItem : MenuItem
    {
        public event ActionDone m_ActionDone;
        public ActionMenuItem(Game i_Game, string i_Title)
            : base(i_Game, i_Title)
        {
            this.Enabled = true;
            this.UpdateOrder = int.MinValue;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (m_HasFocus && (m_InputManager.KeyPressed(Keys.Enter) || m_InputManager.ButtonPressed(eInputButtons.Left)) && (m_PreviousMenu == null || m_PreviousMenu.StartedWorking ))
            {
                m_ActionDone.Invoke();
            }
        }
    }
}