using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.Managers;
using Infrastructure.ServiceInterfaces;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ObjectModel;

namespace SpaceInvaders.Menus
{
    public delegate void SoundToggled();

    public class SoundOptionsMenu : SubMenu
    {
        private SoundManager m_SoundManager;

        public SoundOptionsMenu(Game i_Game, GameScreen i_Screen)
            : base(i_Game, "Sound Options", i_Screen)
        {
            m_SoundManager = this.Game.Services.GetService(typeof(SpaceInvaderSoundsManager)) as SpaceInvaderSoundsManager;
        }

        public override void Initialize()
        {
            base.Initialize();
            PercentSettingMenuItem BackGroundMusicItem = new PercentSettingMenuItem(Game, string.Format("Background Music Volume"), (int)m_SoundManager.Music.Volume);
            BackGroundMusicItem.m_SettingChanged += m_SoundManager.changeMusicVolume;
            BackGroundMusicItem.PrevoiusMenu = this;
            AddItem(BackGroundMusicItem);
            ToggleMenuItem toggleSoundItem = new ToggleMenuItem(Game, "Toggle Sound", m_SoundManager.IsSoundOn);
            (m_SoundManager as SpaceInvaderSoundsManager).m_SoundToggled += toggleSoundItem.Toggle;
            toggleSoundItem.m_ToggleDone += m_SoundManager.ToggleSound;
            toggleSoundItem.PrevoiusMenu = this;
            AddItem(toggleSoundItem);
            PercentSettingMenuItem SoundEffectVolume = new PercentSettingMenuItem(Game, string.Format("Sound Effects Volume"), (int)m_SoundManager.Music.Volume);
            SoundEffectVolume.m_SettingChanged += m_SoundManager.changeSoundEffectsVolume;
            SoundEffectVolume.PrevoiusMenu = this;
            AddItem(SoundEffectVolume);
            ActionMenuItem doneItem = new ActionMenuItem(Game, "Done");
            doneItem.PrevoiusMenu = this;
            doneItem.m_ActionDone += ExitThisMenu;
            AddItem(doneItem);
        }
    }
}