using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;
using UdpTest.Game;

namespace TemplateGame.Game
{
    public partial class SettingsMenu : CompositeDrawable
    {
        private MenuButton submitButton;
        private BasicCheckbox particlesCheckbox;
        private BasicCheckbox hardModeCheckbox;
        private Container box;
        private BindableNumberWithCurrent<double> slider;
        private BindableNumberWithCurrent<double> slider2;
        private BasicSliderBar<double> soundVolume;
        private BasicSliderBar<double> hitsoundVolume;
        private SpriteText soundVolumeText;
        private SpriteText hitsoundVolumeText;
        private SpriteText hardModeText;
        private SpriteText enableParticlesText;
        private int textOffset = -500;
        private MenuButton backButton;
        public bool Exit = false;

        public SettingsMenu()
        {
            slider = new BindableNumberWithCurrent<double>(10);
            slider.MinValue = 0;
            slider.MaxValue = 100;
            slider.Precision = 1;
            slider.Value = GameSettings.MusicVolume;
            slider2 = new BindableNumberWithCurrent<double>(10);
            slider2.MinValue = 0;
            slider2.MaxValue = 100;
            slider2.Precision = 1;
            slider2.Value = GameSettings.HitsoundVolume;

            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            //gameSettings = SettingsLoader.Load();
            InternalChild = box = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Box()
                    {
                        Colour = Color4.Goldenrod,
                        RelativeSizeAxes = Axes.Both,
                    },
                    backButton = new MenuButton()
                    {
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        Size = new Vector2(50, 50),
                    },
                    soundVolumeText = new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 200),
                        Font = new FontUsage(size: 80),
                        Text = "Music Volume "
                    },
                    soundVolume = new BasicSliderBar<double>()
                    {
                        Current = slider,
                        Colour = Color4.DarkRed,
                        Origin = Anchor.TopCentre,
                        Anchor = Anchor.TopCentre,
                        Position = new Vector2(0, 200),
                        Size = new Vector2(300, 80),
                    },
                    hitsoundVolumeText = new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 300),
                        Font = new FontUsage(size: 80),
                        Text = "Hitsound Volume "
                    },
                    hitsoundVolume = new BasicSliderBar<double>()
                    {
                        Current = slider2,
                        Colour = Color4.DarkMagenta,
                        Origin = Anchor.TopCentre,
                        Anchor = Anchor.TopCentre,
                        Position = new Vector2(0, 300),
                        Size = new Vector2(300, 80),
                    },
                    hardModeText = new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 500),
                        Font = new FontUsage(size: 80),
                        Text = "Enable Hard Mode"
                    },
                    hardModeCheckbox = new BasicCheckbox()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(0, 500),
                        Current = new Bindable<bool>(GameSettings.HardMode),
                        LabelText = "",
                    },
                    enableParticlesText = new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 600),
                        Font = new FontUsage(size: 80),
                        Text = "Enable Particles"
                    },
                    particlesCheckbox = new BasicCheckbox()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(0, 600),
                        Current = new Bindable<bool>(GameSettings.EnableParticles),
                        LabelText = "",
                    },
                    submitButton = new MenuButton()
                    {
                        Text = "Submit",
                        Y = 700,
                    },
                }
            };
            slider.Value = GameSettings.MusicVolume;
            slider2.Value = GameSettings.HitsoundVolume;
            //soundVolume.Current = new BindableNumber<double>(GameSettings.MusicVolume);
            //hitsoundVolume.Current = new BindableNumber<double>(GameSettings.HitsoundVolume);

        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (submitButton.IsHovered)
            {
                submit();
            }

            if (backButton.IsHovered)
            {
                back();
            }

            return base.OnMouseDown(e);
        }

        protected override bool OnTouchDown(TouchDownEvent e)
        {
            if (submitButton.IsHovered)
            {
                submit();
            }

            if (backButton.IsHovered)
            {
                back();
            }

            return base.OnTouchDown(e);
        }

        private void submit()
        {
            GameSettings.MusicVolume = soundVolume.Current.Value;
            GameSettings.HitsoundVolume = (float)hitsoundVolume.Current.Value;
            GameSettings.EnableParticles = particlesCheckbox.Current.Value;
            GameSettings.HardMode = hardModeCheckbox.Current.Value;
            Logger.Log(GameSettings.ToString());
            //SettingsLoader.SaveSettings(GameSettings);
        }

        private void back()
        {
            Exit = true;
        }

        protected override void Update()
        {
            updateText();
        }

        private void updateText()
        {
            soundVolumeText.Text = "Music Volume " + soundVolume.Current.Value.ToString("0");
            hitsoundVolumeText.Text = "Hitsound Volume " + hitsoundVolume.Current.Value.ToString("0");
        }
    }
}
