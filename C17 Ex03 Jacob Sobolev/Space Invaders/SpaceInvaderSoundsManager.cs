using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Managers;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders
{
    public delegate void SoundToggled();

    public class SpaceInvaderSoundsManager : SoundManager
    {
        public event SoundToggled m_SoundToggled;

        public SpaceInvaderSoundsManager(Game i_Game)
            : base(i_Game)
        { }

        protected override void initSounds()
        {
            string soundFilesPath = @"C:/temp/XNA_Assets/Ex03/Sounds/";
            SetMusic(soundFilesPath + "BGMusic");
            AddSound("PlayerShot", soundFilesPath + "SSGunShot");
            AddSound("EnemyShot", soundFilesPath + "EnemyGunShot");
            AddSound("BarrierHit", soundFilesPath + "BarrierHit");
            AddSound("EnemyKill", soundFilesPath + "EnemyKill");
            AddSound("GameOver", soundFilesPath + "GameOver");
            AddSound("LevelWin", soundFilesPath + "LevelWin");
            AddSound("LifeDie", soundFilesPath + "LifeDie");
            AddSound("MenuMove", soundFilesPath + "MenuMove");
            AddSound("MotherShipKill", soundFilesPath + "MotherShipKill");
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if((Game.Services.GetService(typeof(IInputManager)) as InputManager).KeyPressed(Keys.M))
            {
                m_SoundToggled.Invoke();
            }
        }
    }
}
