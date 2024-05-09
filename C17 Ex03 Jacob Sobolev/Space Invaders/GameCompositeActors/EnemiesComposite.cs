using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.GameCompositeActors
{
    public class EnemiesComposite : CompositeDrawableComponent<GameComponent>
    {
        public EnemiesComposite(Game i_Game, GameScreen i_ParentScreen) 
            : base(i_Game)
        {
            Add(new EnemyFleetComposite(i_Game, i_ParentScreen));
            Add(new MotherShip(i_Game));
        }
    }
}
