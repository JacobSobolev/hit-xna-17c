using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace C17Ex01
{
    public class Background : Sprite
    {
        private const string k_TexturePath = @"Assets\BG_Space01_1024x768";

        public Background(BaseGame i_BaseGame) : base(i_BaseGame, k_TexturePath)
        {
            TintColor = Color.White;
        }

        protected override void InitializePosition()
        {
            Position = new Vector2(0, 0);
        }

        public override void Initialize()
        {
            base.Initialize();
            InitializePosition();
        }
    }
}
