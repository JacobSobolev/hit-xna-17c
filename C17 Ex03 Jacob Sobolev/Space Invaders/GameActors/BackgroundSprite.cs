using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel;

namespace SpaceInvaders
{
    public class BackgroundSprite : Sprite
    {
        public BackgroundSprite(Game i_Game, string i_AssetName, int i_Opacity)
            : base(i_AssetName, i_Game)
        {
            this.Opacity = i_Opacity;
        }

        protected override void InitBounds()
        {
            base.InitBounds();

            this.DrawOrder = int.MinValue;
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
