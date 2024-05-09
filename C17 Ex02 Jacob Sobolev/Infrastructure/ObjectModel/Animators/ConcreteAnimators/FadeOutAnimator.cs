using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class FadeOutAnimator : SpriteAnimator
    {
        private float m_FadeSpeed;

        public FadeOutAnimator(string i_Name, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            m_FadeSpeed =  m_OriginalSpriteInfo.Opacity / (float)AnimationLength.TotalSeconds;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            if (BoundSprite.Opacity != 0)
            {
                BoundSprite.Opacity -= m_FadeSpeed * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                IsFinished = true;
            }
            
        }

        protected override void RevertToOriginal()
        {
            BoundSprite.Opacity = m_OriginalSpriteInfo.Opacity;
        }
    }
}
