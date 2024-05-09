//*** Guy Ronen © 2008-2011 ***//
using System;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel
{
	public class RegisteredComponent : GameComponent
	{
        public event EventHandler<EventArgs> Disposed;
        
        public RegisteredComponent(Game i_Game, int i_UpdateOrder)
			: base(i_Game)
		{
			this.UpdateOrder = i_UpdateOrder;
			Game.Components.Add(this); // self-register as a coponent
		}

        protected virtual void OnDisposed(object sender, EventArgs args)
        {
            if (Disposed != null)
            {
                Disposed.Invoke(sender, args);
            }
            if (Game.Components != null)
            {
                Game.Components.Remove(this);
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            OnDisposed(this, EventArgs.Empty);
        }

        public RegisteredComponent(Game i_Game)
			: this(i_Game, int.MaxValue)
		{ }
	}
}