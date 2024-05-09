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
    public static class CollisionManager 
    {
        private const float k_JumpMul = 0.95f;
        private static int enemiesKilled;

        static CollisionManager()
        {
            enemiesKilled = 0;
        }

        public static bool CheckCollision(Sprite i_SpriteA, Sprite i_SpriteB)
        {
            bool isACollision = false;
            Rectangle SpriteASpace = new Rectangle((int)i_SpriteA.Position.X, (int)i_SpriteA.Position.Y, i_SpriteA.Texture.Width, i_SpriteA.Texture.Height);
            Rectangle SpriteBSpace = new Rectangle((int)i_SpriteB.Position.X, (int)i_SpriteB.Position.Y, i_SpriteB.Texture.Width, i_SpriteB.Texture.Height);
            if (SpriteASpace.Intersects(SpriteBSpace))
            {
                isACollision = true;
            }

            return isACollision;
        }

        public static void CheckEnemyHit(EnemyFleet i_EnemyFleet, Player i_Player)
        {
            for (int i = 0; i < i_Player.Shots.Count; ++i)
            {
                Shot shot = i_Player.Shots[i];
                for (int j = 0; j < i_EnemyFleet.Enemies.Count; ++j)
                {
                    Enemy enemy = i_EnemyFleet.Enemies[j];
                    if (CheckCollision(shot, enemy))
                    {
                        i_Player.Score += enemy.Reward;
                        i_EnemyFleet.Enemies.RemoveAt(j);
                        if (enemiesKilled != 4)
                        {
                            enemiesKilled++;
                        }
                        else
                        {
                            enemiesKilled = 0;
                            i_EnemyFleet.JumpFreq *= k_JumpMul;
                        }

                        i_Player.Shots.RemoveAt(i);
                        
                        --i;
                        --j;
                        i_EnemyFleet.UpdateFleetBounds();
                        break;
                    }
                }
            }
        }

        public static bool CheckIfEnemyCrashesPlayer(EnemyFleet i_Fleet, Player i_Player)
        {
            bool isACrash = false;
            foreach (Enemy enemy in i_Fleet.Enemies)
            {
                if (CheckCollision(enemy, i_Player))
                {
                    isACrash = true;
                    break;
                }
            }

            return isACrash;
        }

        public static void CheckPlayerHit(EnemyFleet i_EnemyFleet, Player i_Player)
        {
            for (int i = 0; i < i_EnemyFleet.Shots.Count; ++i)
            {
                Shot shot = i_EnemyFleet.Shots[i];
                Player player = i_Player;
                if (CheckCollision(shot, player))
                {
                    i_Player.GoToStartingPosition();
                    i_EnemyFleet.Shots.RemoveAt(i);
                    --i;
                    i_Player.TakeHit();
                    break;
                }
            }
        }

        public static void CheckMotherShipHit(MotherShip i_MotherShip, Player i_Player)
        {
            for (int i = 0; i < i_Player.Shots.Count; ++i)
            {
                if (CheckCollision(i_MotherShip, i_Player.Shots[i]))
                {
                    i_MotherShip.Dissappear();
                    i_Player.Shots.RemoveAt(i);
                    i_Player.Score += i_MotherShip.Reward;
                    break;
                }
            }
        }
    }
}