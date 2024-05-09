using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.Managers;
using Infrastructure.ServiceInterfaces;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ObjectModel;

namespace SpaceInvaders.Actors
{
    public class BackgroundComposite : CompositeDrawableComponent<Sprite>
    {
        private const string k_AssetNameBg = @"Assets\BG_Space01_1024x768";

        public BackgroundComposite(Game i_Game) : base(i_Game)
        {
            Add(new BackgroundSprite(i_Game, k_AssetNameBg, 1));
        }
    }
}
