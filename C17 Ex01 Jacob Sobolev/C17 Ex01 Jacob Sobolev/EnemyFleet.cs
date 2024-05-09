using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C17Ex01
{
    public class EnemyFleet : DrawableGameComponent
    {
        private const int k_ShotDirection = 1;
        private const float k_EnemyWidth = 32;
        private const float k_EnemyHeight = 32;
        private const int k_EnemyNumberOfRows = 5;
        private const int k_EnemyNumberOfCols = 9;
        private const float k_EnemySpace = 1.6f;
        private const float k_JumpFreqStart = 0.5f;
        private const float k_JumpFreqMul = 0.93f;
        private const int k_DirectionStart = 1;
        private const string k_EnemyTexturePathPink = @"Assets\Enemy0101_32x32";
        private const string k_EnemyTexturePathBlue = @"Assets\Enemy0201_32x32";
        private const string k_EnemyTexturePathYellow = @"Assets\Enemy0301_32x32";
        private float m_FireRate;
        private int startMatrixJ;
        private int EndMatrixJ;
        private int m_Direction;
        private Vector2 m_Velocity;
        private Vector2 m_Position;
        private Vector2 m_StartPosition;
        private Vector2 m_EndPosition;
        private float m_JumpTimer;
        private float m_FireTimer;

        public List<Enemy> Enemies { get; private set; }

        public float JumpFreq { get; set; }

        public List<Shot> Shots { get; private set; }

        public EnemyFleet(BaseGame i_BaseGame)
            : base(i_BaseGame)
        {
            JumpFreq = k_JumpFreqStart;
            m_Velocity = new Vector2(k_EnemyWidth / 2, 0);
            m_Direction = k_DirectionStart;
            Enemies = new List<Enemy>();
            m_Position = new Vector2(0, k_EnemyHeight * 3);
            m_StartPosition = new Vector2(0, k_EnemyHeight * 3);
            m_EndPosition = new Vector2((k_EnemyNumberOfCols * k_EnemyWidth * k_EnemySpace) - (k_EnemyWidth / 2), k_EnemyHeight * 3);
            startMatrixJ = 0;
            EndMatrixJ = k_EnemyNumberOfCols - 1;
            setNewFireRate();
            Shots = new List<Shot>();
            m_FireTimer = 0;
            m_JumpTimer = 0;
        }

        private void setNewFireRate()
        {
            Random random = new Random();
            m_FireRate = (float)random.Next(0, 2) + (float)random.NextDouble();
        }

        public override void Initialize()
        {
            Enemy newEnemy;
            for (int i = 0; i < k_EnemyNumberOfRows; i++)
            {
                for (int j = 0; j < k_EnemyNumberOfCols; j++)
                {
                    Vector2 matrixPos = new Vector2(i, j);
                    if (i == 0)
                    {
                        newEnemy = new Enemy((BaseGame)Game, Color.Pink, 230, k_EnemyTexturePathPink, matrixPos, getEnemyPosBasedOnMatrixPos(matrixPos));
                    }
                    else if (i >= 1 && i < 3)
                    {
                        newEnemy = new Enemy((BaseGame)Game, Color.Blue, 190, k_EnemyTexturePathBlue, matrixPos, getEnemyPosBasedOnMatrixPos(matrixPos));
                    }
                    else
                    {
                        newEnemy = new Enemy((BaseGame)Game, Color.Yellow, 115, k_EnemyTexturePathYellow, matrixPos, getEnemyPosBasedOnMatrixPos(matrixPos));
                    }

                    Enemies.Add(newEnemy);
                }
            }

            base.Initialize();
        }

        private Vector2 getEnemyPosBasedOnMatrixPos(Vector2 i_MatrixPos)
        {
            Vector2 returnVec = Vector2.Zero;

            returnVec.X = i_MatrixPos.Y * k_EnemyWidth * k_EnemySpace;
            returnVec.Y = i_MatrixPos.X * k_EnemyHeight * k_EnemySpace;
            returnVec += m_Position;
            return returnVec;
        }

        private void updateEnemiesPos()
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Position = getEnemyPosBasedOnMatrixPos(enemy.MatrixPos);
            }
        }

        public void UpdateFleetBounds()
        {
            int minMatrixStartJ = EndMatrixJ;
            int maxMatrixStartJ = -1;
            foreach (Enemy enemy in Enemies)
            {
                if ((int)enemy.MatrixPos.Y < minMatrixStartJ)
                {
                    minMatrixStartJ = (int)enemy.MatrixPos.Y;
                }

                if ((int)enemy.MatrixPos.Y > maxMatrixStartJ)
                {
                    maxMatrixStartJ = (int)enemy.MatrixPos.Y;
                }
            }

            if (Math.Abs(minMatrixStartJ - startMatrixJ) != 0)
            {
                float newPosX = m_StartPosition.X + (Math.Abs(startMatrixJ - minMatrixStartJ) * k_EnemyWidth * k_EnemySpace);
                m_StartPosition = new Vector2(newPosX, m_StartPosition.Y);
                startMatrixJ = minMatrixStartJ;
            }

            if (Math.Abs(EndMatrixJ - maxMatrixStartJ) != 0)
            {
                float newPosX = m_EndPosition.X - (Math.Abs(EndMatrixJ - maxMatrixStartJ) * k_EnemyWidth * k_EnemySpace);
                m_EndPosition = new Vector2(newPosX, m_EndPosition.Y);
                EndMatrixJ = maxMatrixStartJ;
            }
        }

        private void fire()
        {
            if(m_FireTimer >= m_FireRate)
            {
                Random random = new Random();
                int EnemyToFireIndex = random.Next(0, Enemies.Count);
                m_FireTimer = 0;
                setNewFireRate();
                Shot shot = new Shot((BaseGame)this.Game, Enemies[EnemyToFireIndex].Position, k_ShotDirection, Color.Blue);
                Shots.Add(shot);
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            float timePassed = (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            m_JumpTimer += timePassed;
            m_FireTimer += timePassed;
            fire();
            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(i_GameTime);
            }

            foreach (Shot shot in Shots)
            {
                shot.Update(i_GameTime);
            }

            if (m_JumpTimer >= JumpFreq)
            {
                m_JumpTimer = 0;
                if ((m_EndPosition.X >= GraphicsDevice.Viewport.Width && m_Direction == 1) || (m_StartPosition.X <= 0 && m_Direction == -1))
                {
                    JumpFreq *= k_JumpFreqMul;
                    m_Velocity = new Vector2(0, k_EnemyHeight / 2);
                    m_Position += m_Velocity;
                    m_EndPosition += m_Velocity;
                    updateEnemiesPos();
                    m_Direction *= -1;
                    m_Velocity = new Vector2(k_EnemyWidth / 2 * m_Direction, 0);
                }
                else
                {
                    if (m_Direction == 1 && m_EndPosition.X + m_Velocity.X > GraphicsDevice.Viewport.Width)
                    {
                        m_Velocity = new Vector2(Math.Abs(GraphicsDevice.Viewport.Width - m_EndPosition.X) * m_Direction, m_Velocity.Y);
                    }

                    if (m_Direction == -1 && m_StartPosition.X + m_Velocity.X < 0)
                    {
                        m_Velocity = new Vector2(m_StartPosition.X * m_Direction, m_Velocity.Y);
                    }

                    m_Position += m_Velocity;
                    m_EndPosition += m_Velocity;
                    m_StartPosition += m_Velocity;
                    updateEnemiesPos();
                }
            }

            base.Update(i_GameTime);
        }

        public override void Draw(GameTime i_GameTime)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(i_GameTime);
            }

            foreach (Shot shot in Shots)
            {
                shot.Draw(i_GameTime);
            }

            base.Draw(i_GameTime);
        }
    }
}
