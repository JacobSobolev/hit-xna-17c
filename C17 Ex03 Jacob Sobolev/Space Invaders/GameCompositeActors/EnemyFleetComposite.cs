using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders
{
    public class EnemyFleetComposite : CompositeDrawableComponent<Sprite>
    {
        private const float k_JumpMul = 0.95f;
        private const float k_EnemyWidth = 32;
        private const float k_EnemyHeight = 32;
        private const int k_EnemyNumberOfRowsInit = 5;
        private const int k_EnemyNumberOfColsInit = 9;
        private const float k_EnemySpace = 1.6f;
        private const float k_JumpFreqStart = 0.5f;
        private const float k_JumpFreqMul = 0.93f;
        private const int k_DirectionStart = 1;
        private int m_EnemyNumberOfCols;
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
        private float m_JumpFreq;
        private int m_EnemiesKilled;
        private GameScreen m_ParentScreen;
        private PlayersManager m_PlayersManager;
        private SpaceInvaderSoundsManager m_SoundManager;

        public bool EmptyFleet { get; private set; }

        public Vector2 Position
        {
            get { return m_Position; }
        }

        private List<List<Enemy>> m_Enemies;

        public List<List<Enemy>> Enemies
        {
            get { return m_Enemies; }
        }

        public EnemyFleetComposite(Game i_Game, GameScreen i_ParentScreen)
            : base(i_Game)
        {
            m_SoundManager = i_Game.Services.GetService(typeof(SpaceInvaderSoundsManager)) as SpaceInvaderSoundsManager;
            m_PlayersManager = Game.Services.GetService(typeof(PlayersManager)) as PlayersManager;
            m_ParentScreen = i_ParentScreen;
            m_Enemies = new List<List<Enemy>>();
            m_PlayersManager.LevelClearedEvent += resetToLevelStart;
            m_PlayersManager.MenuExitEvent += resetToLevelStart;
        }

        private void setNewFireRate()
        {
            Random random = new Random();
            m_FireRate = (float)random.Next(0, 2) + (float)random.NextDouble();
        }

        public override void Initialize()
        {
            resetToLevelStart();
            base.Initialize();
        }

        private void resetToLevelStart()
        {
            m_EnemyNumberOfCols = k_EnemyNumberOfColsInit + m_PlayersManager.GetColomnsOfEnemiesToAdd();
            Clear();
            for (int i = 0; i < m_Enemies.Count; i++)
            {
                for (int j = 0; j < m_Enemies[i].Count; j++)
                {
                    m_Enemies[i][j].Enabled = false;
                    m_Enemies[i][j].Visible = false;
                }

                m_Enemies[i].Clear();
                m_Enemies.Remove(m_Enemies[i]);
            }

            m_Enemies.Clear();
            m_JumpFreq = k_JumpFreqStart;
            m_Velocity = new Vector2(k_EnemyWidth / 2, 0);
            m_Direction = k_DirectionStart;
            m_Position = new Vector2(0, k_EnemyHeight * 3);
            m_StartPosition = new Vector2(0, k_EnemyHeight * 3);
            m_EndPosition = new Vector2((m_EnemyNumberOfCols * k_EnemyWidth * k_EnemySpace) - (k_EnemyWidth / 2), k_EnemyHeight * 3);
            startMatrixJ = 0;
            EndMatrixJ = m_EnemyNumberOfCols - 1;
            setNewFireRate();
            m_FireTimer = 0;
            m_JumpTimer = 0;
            m_EnemiesKilled = 0;
            Enemy newEnemy;
            Vector2 newPosition = Vector2.Zero;
            for (int i = 0; i < k_EnemyNumberOfRowsInit; i++)
            {
                m_Enemies.Add(new List<Enemy>());
                for (int j = 0; j < m_EnemyNumberOfCols; j++)
                {
                    newPosition = Vector2.Zero;
                    newPosition.X = j * k_EnemyWidth * k_EnemySpace;
                    newPosition.Y = i * k_EnemyHeight * k_EnemySpace;
                    newPosition += m_Position;
                    if (i == 0)
                    {
                        newEnemy = new Enemy(Game, Color.Pink, 230, newPosition, EnemyType.Top, 0, updateCelEnemyAnimationCallback, m_ParentScreen);
                    }
                    else if (i >= 1 && i < 3)
                    {
                        newEnemy = new Enemy(Game, Color.LightBlue, 190, newPosition, EnemyType.Center, (i + 1) % 2, updateCelEnemyAnimationCallback, m_ParentScreen);
                    }
                    else
                    {
                        newEnemy = new Enemy(Game, Color.LightYellow, 115, newPosition, EnemyType.Bottom, (i + 1) % 2, updateCelEnemyAnimationCallback, m_ParentScreen);
                    }

                    newEnemy.Disposed += UpdateFleetBounds;
                    newEnemy.Disposed += updateJumpTime;
                    newEnemy.Disposed += removeEnemyFromFiringList;
                    m_Enemies[i].Add(newEnemy);
                    Add(newEnemy);
                    newEnemy.Initialize();
                }
            }
        }

        private void removeEnemyFromFiringList(object sender, EventArgs e)
        {
            Enemy enemy = sender as Enemy;
            enemy.Enabled = false;
            enemy.Visible = false;
            Remove(sender as Enemy);
            for (int i = 0; i < m_Enemies.Count; i++)
            {
                int index = m_Enemies[i].IndexOf(enemy);
                if (index != -1)
                {
                    m_Enemies[i][index].Enabled = false;
                }
            }

            if (m_Sprites.Count == 0)
            {
                m_PlayersManager.LevelCleared();
                m_SoundManager.PlaySound("LevelWin");
            }
        }

        private void updateEnemiesPos()
        {
            bool emptyFleet = true;
            Vector2 newPosition = Vector2.Zero;
            for (int i = 0; i < m_Enemies.Count; i++)
            {
                for (int j = 0; j < m_Enemies[i].Count; j++)
                {
                    if (m_Enemies[i][j] != null && m_Enemies[i][j].Enabled)
                    {
                        emptyFleet = false;
                        newPosition = Vector2.Zero;
                        newPosition.X = j * k_EnemyWidth * k_EnemySpace;
                        newPosition.Y = i * k_EnemyHeight * k_EnemySpace;
                        newPosition += m_Position;
                        m_Enemies[i][j].Position = newPosition;
                    }
                }
            }

            if (emptyFleet)
            {
                EmptyFleet = emptyFleet;
                Dispose();
            }
        }

        private void updateJumpTime(object sender, EventArgs args)
        {
            if (m_EnemiesKilled != 4)
            {
                m_EnemiesKilled++;
            }
            else
            {
                m_EnemiesKilled = 0;
                m_JumpFreq *= k_JumpMul;
            }
        }

        private void updateCelEnemyAnimationCallback(object sender, EventArgs args)
        {
            CellAnimator cellAnimator = sender as CellAnimator;
            if (cellAnimator.CellTime.TotalSeconds != m_JumpFreq)
            {
                cellAnimator.Restart(TimeSpan.FromSeconds(m_JumpFreq * 2), TimeSpan.FromSeconds(m_JumpFreq));
            }
            else
            {
                cellAnimator.Reset();
            }
        }

        public void UpdateFleetBounds(object sender, EventArgs args)
        {
            int minMatrixStartJ = EndMatrixJ;
            int maxMatrixStartJ = -1;
            for (int i = 0; i < m_Enemies.Count; i++)
            {
                for (int j = 0; j < m_Enemies[i].Count; j++)
                {
                    if (m_Enemies[i][j] != null && m_Enemies[i][j].Enabled)
                    {
                        if (j < minMatrixStartJ)
                        {
                            minMatrixStartJ = j;
                        }

                        if (j > maxMatrixStartJ)
                        {
                            maxMatrixStartJ = j;
                        }
                    }
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

        private void fireSingleEnemy()
        {
            if (m_FireTimer >= m_FireRate && this.m_Sprites.Count != 0)
            {
                Random random = new Random();
                int enemyToFireIndex = 0;
                enemyToFireIndex = random.Next(0, m_Sprites.Count);
                (m_Sprites[enemyToFireIndex] as Enemy).Shot();
                m_FireTimer = 0;
                setNewFireRate();
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            float timePassed = (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            m_JumpTimer += timePassed;
            m_FireTimer += timePassed;
            fireSingleEnemy();

            if (m_JumpTimer >= m_JumpFreq)
            {
                m_JumpTimer = 0;
                if ((m_EndPosition.X >= Game.GraphicsDevice.Viewport.Width && m_Direction == 1) || (m_StartPosition.X <= 0 && m_Direction == -1))
                {
                    m_JumpFreq *= k_JumpFreqMul;
                    m_Velocity = new Vector2(0, k_EnemyHeight / 2);
                    m_Position += m_Velocity;
                    m_EndPosition += m_Velocity;
                    updateEnemiesPos();
                    m_Direction *= -1;
                    m_Velocity = new Vector2(k_EnemyWidth / 2 * m_Direction, 0);
                }
                else
                {
                    if (m_Direction == 1 && m_EndPosition.X + m_Velocity.X > Game.GraphicsDevice.Viewport.Width)
                    {
                        m_Velocity = new Vector2(Math.Abs(Game.GraphicsDevice.Viewport.Width - m_EndPosition.X) * m_Direction, m_Velocity.Y);
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
        }
    }
}