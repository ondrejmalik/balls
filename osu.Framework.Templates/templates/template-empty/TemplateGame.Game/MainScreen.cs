using System;
using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Video;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using osuTK.Input;
using Color4 = osuTK.Graphics.Color4;

namespace TemplateGame.Game
{
    public partial class MainScreen : Screen
    {
        private Camera camera;
        private Video bg;
        private Track track;
        public static SpriteText text;
        public Action pushAction;
        private bool alreadyPushing;
        private int retries = 0;
        private double firstTime;

        public MainScreen(int retries)
        {
            this.retries = retries;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures, ITrackStore tracks)
        {
            string videoPath = "BadApple.mp4";
            Stream videoStream = textures.GetStream(videoPath);
            InternalChildren = new Drawable[]
            {
                /*
                bg = new Video(videoStream, false)
                {
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Loop = true,
                    IsPlaying = false,
                },
                */
                camera = new Camera
                {
                    Position = new Vector2(-960, 0)
                },
                text = new SpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Position = new Vector2(0, 20),
                    Text = retries.ToString() + " retries",
                    Colour = Color4.White,
                    Font = new FontUsage(size: 100),
                },
            };
            firstTime = Time.Current;
            track = tracks.Get("Audio.mp3");
            track.Volume.Value = 0.00f;
            //track.Start();
            //bg.IsPlaying = false;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            pushAction = () => this.Push(new MainScreen(retries + 1));
        }

        protected override void Update()
        {
            if (Time.Current > firstTime + 5000) text.FadeOut(200);

            if (camera.I == camera.LastObjectIndex1 - 1)
            {
                text.Text = "You won!";
                text.FadeIn(20);
                pushAction = () => this.Push(new Menu(true));
            }

            if (camera.IsDead && !alreadyPushing)
            {
                alreadyPushing = true;
                Scheduler.AddDelayed(pushAction, 1000);
                camera.IsDead = false;
            }

            base.Update();
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Key == Key.Escape)
            {
                Dispose();
                this.Push(new Menu(false));
            }

            return base.OnKeyDown(e);
        }

        public void Dispose()
        {
            camera.Song.Stop();
            camera.Dispose();
        }
    }
}
