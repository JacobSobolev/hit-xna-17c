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
    public abstract class SoundManager : GameService
    {
        private bool m_IsSoundOn = true;
        public bool IsSoundOn
        {
            get { return m_IsSoundOn; }
        }

        private Dictionary<string, SoundEffect> m_SoundEffects;
        private float m_ActualSoundEffectVolume = 1;
        private float soundEffectVolume = 1;
        private float MusicVolume = 1;
        private SoundEffectInstance m_Music;
        public SoundEffectInstance Music
        {
            get { return m_Music; }
        }

        public SoundManager(Game i_Game)
            : base(i_Game)
        {
            m_SoundEffects = new Dictionary<string, SoundEffect>();
            initSounds();
        }

        protected abstract void initSounds();

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
            m_SoundEffects[i_SoundName].Play(soundEffectVolume, (float)0, (float)0);
        }

        public void changeMusicVolume(float i_NewVolume)
        {
            MusicVolume = i_NewVolume;
            this.m_Music.Volume = MusicVolume;
        }

        public void changeSoundEffectsVolume(float i_NewVolume)
        {
            m_ActualSoundEffectVolume = i_NewVolume;
            soundEffectVolume = m_ActualSoundEffectVolume;
        }

        public void ToggleSound()
        {
            m_IsSoundOn = !m_IsSoundOn;
            if (!m_IsSoundOn)
            {
                m_Music.Volume = 0;
                soundEffectVolume = 0;
            }
            else
            {
                m_Music.Volume = MusicVolume;
                soundEffectVolume = m_ActualSoundEffectVolume;
            }
        }
    }
}