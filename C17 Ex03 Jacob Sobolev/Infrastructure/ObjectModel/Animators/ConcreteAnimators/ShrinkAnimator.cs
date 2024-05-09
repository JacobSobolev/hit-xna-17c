using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class ShrinkAnimator : SpriteAnimator
    {
        private Vector2 m_ShrinkVelocity;
        public ShrinkAnimator(string i_Name, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        { }

        public override void Initialize()
        {
            base.Initialize();
            m_ShrinkVelocity = BoundSprite.Scales / (float)AnimationLength.TotalSeconds;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            BoundSprite.Scales -= m_ShrinkVelocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
        }

        protected override void RevertToOriginal()
        {
            BoundSprite.Scales = m_OriginalSpriteInfo.Scales;
        }
    }
}
