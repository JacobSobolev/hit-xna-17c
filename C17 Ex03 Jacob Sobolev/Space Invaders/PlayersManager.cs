using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;

namespace SpaceInvaders
{
    public class PlayersManager : GameService
    {
        private const int k_TopLimitOfLevels = 6;
        private float m_Plyaer1Score;
        private float m_Plyaer2Score;
        private int m_Plyaer1Lives;
        private int m_Plyaer2Lives;

        public event Action Player1ScoreChanged;

        public event Action Player2ScoreChanged;

        public event Action Player1LivesChanged;

        public event Action Player2LivesChanged;

        public event Action LevelClearedEvent;

        public event Action MenuExitEvent;
            
        public int Level { get; set; }

        public bool CoOpMode { get; set; }

        public float Player1Score
        {
            get { return m_Plyaer1Score; }
            set
            {
                m_Plyaer1Score = value;
                if (Player1ScoreChanged != null)
                {
                    Player1ScoreChanged();
                }
            }
        }

        public float Player2Score
        {
            get { return m_Plyaer2Score; }
            set
            {
                m_Plyaer2Score = value;
                if (Player2ScoreChanged != null)
                {
                    Player2ScoreChanged();
                }
            }
        }

        public int Player1Lives
        {
            get { return m_Plyaer1Lives; }
            set
            {
                m_Plyaer1Lives = value;
                if (Player1LivesChanged != null)
                {
                    Player1LivesChanged();
                }
            }
        }

        public int Player2Lives
        {
            get { return m_Plyaer2Lives; }
            set
            {
                m_Plyaer2Lives = value;
                if (Player2LivesChanged != null)
                {
                    Player2LivesChanged();
                }
            }
        }

        public PlayersManager(Game i_Game)
            : base(i_Game)
        {
            CoOpMode = false;
            Level = 1;
        }

        public void ToggleCoOpMode()
        {
            CoOpMode = !CoOpMode;
        }

        public float GetBarrierLineSpeedMultiplayer()
        {
            float retrunValue = 0;

            if(Level % k_TopLimitOfLevels != 1)
            {
                if (Level % k_TopLimitOfLevels == 2)
                {
                    retrunValue = 1.0f;
                }
                else
                {
                    retrunValue = 1.0f - (((Level % k_TopLimitOfLevels) - 2) * 0.05f);
                }
            }

            return retrunValue;
        }

        public int GetColomnsOfEnemiesToAdd()
        {
            return (Level % k_TopLimitOfLevels) - 1;
        }

        public float GetEnemyRewardToAdd()
        {
            return (float)((Level % k_TopLimitOfLevels) - 1) * 100;
        }

        public int GetEnemyShotsToAdd()
        {
            return (Level % k_TopLimitOfLevels) - 1;
        }

        public void LevelCleared()
        {
            Level++;
            if (LevelClearedEvent != null)
            {
                LevelClearedEvent();
            }
        }

        public void ApplyMenuOptionsToObjects()
        {
            Level = 1;
            Player1Score = 0;
            Player2Score = 0;
            if (MenuExitEvent != null)
            {
                MenuExitEvent();
            }
        }
    }
}
