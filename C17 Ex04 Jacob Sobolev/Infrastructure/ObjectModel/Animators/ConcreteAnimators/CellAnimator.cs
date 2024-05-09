//*** Guy Ronen © 2008-2011 ***//
using System;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class CellAnimator : SpriteAnimator
    {
        private readonly int r_SartCell = 1;
        private readonly int r_EndCell = 1;
        private TimeSpan m_CellTime;
        private TimeSpan m_TimeLeftForCell;
        private bool m_Loop = true;
        private int m_CurrCellIdx = 0;

        public TimeSpan CellTime
        {
            get { return m_CellTime; }
        }

        // CTORs
        public CellAnimator(TimeSpan i_CellTime, int i_SartCell, int i_EndCell, int i_CurrCellIdx, TimeSpan i_AnimationLength)
            : base("CelAnimation", i_AnimationLength)
        {
            m_CellTime = i_CellTime;
            m_TimeLeftForCell = i_CellTime;
            r_SartCell = i_SartCell;
            r_EndCell = i_EndCell;
            m_CurrCellIdx = i_CurrCellIdx;
            m_Loop = i_AnimationLength == TimeSpan.Zero;
        }

        private void goToNextFrame()
        {
            m_CurrCellIdx++;
            if (m_CurrCellIdx > r_EndCell)
            {
                if (m_Loop)
                {
                    m_CurrCellIdx = r_SartCell;
                }
                else
                {
                    m_CurrCellIdx = r_SartCell; /// lets stop at the last frame
                    this.IsFinished = true;
                }
            }
        }

        public void Restart(TimeSpan i_AnimationLength, TimeSpan i_CellTime)
        {
            m_Loop = i_AnimationLength == TimeSpan.Zero;
            m_CellTime = i_CellTime;
            Restart(i_AnimationLength);
        }

        protected override void RevertToOriginal()
        {
            BoundSprite.SourceRectangle = m_OriginalSpriteInfo.SourceRectangle; 
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            if (m_CellTime != TimeSpan.Zero)
            {
                m_TimeLeftForCell -= i_GameTime.ElapsedGameTime;
                if (m_TimeLeftForCell.TotalSeconds <= 0)
                {
                    /// we have elapsed, so blink
                    goToNextFrame();
                    m_TimeLeftForCell = m_CellTime;
                }
            }

            this.BoundSprite.SourceRectangle = new Rectangle(
                m_CurrCellIdx * this.BoundSprite.SourceRectangle.Width,
                this.BoundSprite.SourceRectangle.Top,
                this.BoundSprite.SourceRectangle.Width,
                this.BoundSprite.SourceRectangle.Height);
        }
    }
}
