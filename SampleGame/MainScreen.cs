using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace insaneFps.Game
{
    public partial class MainScreen : Screen
    {
        [Resolved]
        private GameHost host { get; set; } = null!;

        SpriteText text;
        private double DrawFps;
        private double UpdateFps;
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Violet,
                    RelativeSizeAxes = Axes.Both,
                },
                text = new SpriteText
                {
                    Y = 20,
                    Text = "Main Screen",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 40)
                },
                new SpinningBox
                {
                    Anchor = Anchor.Centre,
                },
            };

            host.AllowBenchmarkUnlimitedFrames = true;
            host.MaximumDrawHz = 0;
            host.MaximumUpdateHz = 0;
            //host.InputThread.ActiveHz = 0;
            //frameworkConfigManager.GetBindable<RendererType>(FrameworkSetting.Renderer);
            osu.Framework.Logging.Logger.Log("balls");
        }
        protected override void Update()
        {
            base.Update();
                DrawFps = host.DrawThread.Clock.FramesPerSecond;
                UpdateFps = host.UpdateThread.Clock.FramesPerSecond;
                text.Text = $"Draw FPS: {DrawFps}\nUpdate FPS: {UpdateFps}";
        }
    }
}
