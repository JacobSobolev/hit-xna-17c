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

namespace Infrastructure.ServiceInterfaces
{
    public interface IMenuItem
    {
        SpriteLabel ButtonLabel { get; }
        Color ButtonTintColor { set; }
        bool HasFocus { set; }
    }
}
