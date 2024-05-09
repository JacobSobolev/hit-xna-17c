using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Infrastructure.ServiceInterfaces;
using Infrastructure.ObjectModel;

namespace Infrastructure.Managers
{
    public class SoundManager : GameService
    {
        Dictionary<string, SoundEffect> m_SoundEffects;
        SoundEffectInstance m_Music;
        public SoundManager(Game i_Game)
            :base (i_Game)
        {
            m_SoundEffects = new Dictionary<string, SoundEffect>();
        }

        public void AddSound(string i_SoundName, string i_SoundPath)
        {
            m_SoundEffects.Add(i_SoundName, this.Game.Content.Load<SoundEffect>(i_SoundPath));
        }

        public void SetMusic (string i_SoundPath)
        {
            SoundEffect soundEffect = this.Game.Content.Load<SoundEffect>(i_SoundPath);
            m_Music = soundEffect.CreateInstance();
            m_Music.IsLooped = true;
            m_Music.Play();
        }

        public void PlaySound(string i_SoundName)
        {
            m_SoundEffects[i_SoundName].Play();
        }
         
        
    }
}
