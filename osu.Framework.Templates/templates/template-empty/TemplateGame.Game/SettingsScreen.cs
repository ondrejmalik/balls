using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace TemplateGame.Game
{
    public partial class SettingsScreen : Screen
    {
        SettingsMenu settingsMenu;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                settingsMenu = new SettingsMenu()
            };
        }

        protected override void Update()
        {
            if (settingsMenu.Exit)
            {
                this.Push(new Menu(false));
            }
        }
    }
}
