using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.Managers;
using Infrastructure.ServiceInterfaces;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Screens;

namespace SpaceInvaders
{
    public class GameSpaceInvaders : BaseGame
    {
        public GameSpaceInvaders() : base()
        {
            new ScreenOptionsManager(this);
            new CollisionsManager(this);
            new PerPixelCollisionManager(this);
            new SpaceInvaderSoundsManager(this);
            new PlayersManager(this);
            ScreensMananger screensMananger = new ScreensMananger(this);
            screensMananger.Push(new PlayScreen(this));
            screensMananger.Push(new LevelTransitionScreen(this));
            screensMananger.SetCurrentScreen(new WelcomeScreen(this));
        }

        protected override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
