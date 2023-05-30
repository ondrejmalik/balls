using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using UdpTest.Game;

namespace TemplateGame.Game
{
    public partial class Menu : Screen
    {
        private MenuButton button1;
        private MenuButton button2;
        private MenuButton settings;
        private Circle finishBox1;
        private bool isLevel1Finished;

        public Menu(bool isLevel1Finished)
        {
            this.isLevel1Finished = isLevel1Finished;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Goldenrod,
                    RelativeSizeAxes = Axes.Both,
                },
                finishBox1 = new Circle()
                {
                    Colour = Color4.Transparent,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Size = new Vector2(80, 80),
                    Position = new Vector2(-300, 100)
                },
                button1 = new MenuButton()
                {
                    Y = 100,
                    Text = "Play Test",
                },
                button2 = new MenuButton()
                {
                    Y = 400,
                    Text = "TODO Map",
                },
                settings = new MenuButton()
                {
                    Y = 700,
                    Text = "Settings",
                },
            };
            if (GameSettings.MusicVolume == 0) GameSettings.SetDefaluts();
            Logger.Log(GameSettings.ToString());

            if (isLevel1Finished)
            {
                finishBox1.Colour = Color4.Green;
                button1.Text = "Complete";
            }

        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            IsHovered();
            return base.OnMouseDown(e);
        }

        protected override bool OnTouchDown(TouchDownEvent e)
        {
            IsHovered();
            return base.OnTouchDown(e);
        }

        private void IsHovered()
        {
            if (button1.IsHovered)
            {
                this.Push(new MainScreen(0));
            }

            if (button2.IsHovered)
            {
                //this.Push(new MainScreen(0));
            }

            if (settings.IsHovered)
            {
                this.Push(new SettingsScreen());
            }
        }
    }
}
