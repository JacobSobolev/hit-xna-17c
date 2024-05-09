using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel;

namespace SpaceInvaders
{
    public class Background : Sprite
    {
        private const string k_AssetName = @"Assets\BG_Space01_1024x768";

        public Background(Game i_Game) 
            : base(k_AssetName, i_Game)
        { }
    }
}
