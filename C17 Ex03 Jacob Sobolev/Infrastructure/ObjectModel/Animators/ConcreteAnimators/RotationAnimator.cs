using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class RotationAnimator : SpriteAnimator
    {
        private float m_RotationSpeed;

        public RotationAnimator(string i_Name, float i_NumRotationsInSec, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
            m_RotationSpeed = MathHelper.TwoPi * i_NumRotationsInSec;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            BoundSprite.Rotation += m_RotationSpeed * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
        }

        protected override void RevertToOriginal()
        {
            BoundSprite.Rotation = m_OriginalSpriteInfo.Rotation;
        }
    }
}
