using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Infrastructure.Managers;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dreidels
{
    public class DreidelsManager : CompositeDrawableComponent<Dreidel>
    {
        private int m_Score = 0;
        private char m_Bet = ' ';
        private DreidelGameState m_GameState = DreidelGameState.WaitingForBet;
        private InputManager m_InputManager;
        private List<Dreidel> m_Dreidels;

        public DreidelsManager(Game i_Game)
            : base(i_Game)
        { }

        public override void Initialize()
        {
            base.Initialize();
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as InputManager;
            Random random = new Random();
            YellowHandleDreidel yellow;
            RedHandleDreidel red;
            BlueHandleDreidel blue;
            m_Dreidels = new List<Dreidel>();
            float x = -88;
            for (int i = 0; i < 2; ++i)
            {
                yellow = new YellowHandleDreidel(this.Game);
                x += 25;
                float y = (float)random.Next(-20, 10);
                float z = -(float)random.Next(-35, 10);
                yellow.Position = new Vector3(x, y, z);
                yellow.DrawOrder = (int)z;
                m_Dreidels.Add(yellow);
                Add(yellow);
                red = new RedHandleDreidel(this.Game);
                x += 25;
                y = (float)random.Next(-20, 10);
                z = -(float)random.Next(-35, 10);
                red.Position = new Vector3(x, y, z);
                red.DrawOrder = (int)z;
                m_Dreidels.Add(red);
                Add(red);
                blue = new BlueHandleDreidel(this.Game);
                x += 25;
                y = (float)random.Next(-20, 10);
                z = -(float)random.Next(-35, 10);
                blue.Position = new Vector3(x, y, z);
                blue.DrawOrder = (int)z;
                m_Dreidels.Add(blue);
                Add(blue);
            }
        }

        private void getBet()
        {
            if (m_InputManager.KeyboardState.IsKeyDown(Keys.B))
            {
                m_Bet = 'B';
                m_GameState = DreidelGameState.WaitingForSpin;
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(Keys.D))
            {
                m_Bet = 'D';
                m_GameState = DreidelGameState.WaitingForSpin;
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(Keys.V))
            {
                m_Bet = 'V';
                m_GameState = DreidelGameState.WaitingForSpin;
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(Keys.P))
            {
                m_Bet = 'P';
                m_GameState = DreidelGameState.WaitingForSpin;
            }
        }

        private void spinDreidels()
        {
            Random random = new Random();
            float spinVelocity;
            int newFrontSide;
            foreach (Dreidel dreidel in m_Dreidels)
            {
                m_GameState = DreidelGameState.Spinning;
                spinVelocity = (float)random.Next(15, 25);
                newFrontSide = random.Next(1, 5);
                dreidel.StartSpinning(spinVelocity, newFrontSide);
            }
        }

        private bool checkIfDreidelsStopedSpinning()
        {
            bool allStoped = true;
            foreach (Dreidel dreidel in m_Dreidels)
            {
                if (dreidel.IsSpinning)
                {
                    allStoped = false;
                    break;
                }
            }

            return allStoped;
        }

        private void resolveBet()
        {
            m_GameState = DreidelGameState.WaitingForBet;
            foreach (Dreidel dreidel in m_Dreidels)
            {
                if (dreidel.FrontLetter == m_Bet)
                {
                    m_Score++;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (m_GameState == DreidelGameState.WaitingForBet)
            {
                getBet();
            }
            else if (m_GameState == DreidelGameState.WaitingForSpin && m_InputManager.KeyboardState.IsKeyDown(Keys.Space))
            {
                spinDreidels();
            }
            else if (m_GameState == DreidelGameState.Spinning && checkIfDreidelsStopedSpinning() == true)
            {
                resolveBet();
            }

            Game.Window.Title = string.Format("Score: {0} Bet: {1}", m_Score, m_Bet);
        }

        public enum DreidelGameState
        {
            WaitingForBet,
            WaitingForSpin,
            Spinning
        }
    }
}
