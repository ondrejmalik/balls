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
using Color4 = osuTK.Graphics.Color4;

namespace TemplateGame.Game
{
    public partial class MainScreen : Screen
    {
        private Camera camera;
        private Video bg;
        private Track track;
        private SpriteText text;

        [BackgroundDependencyLoader]
        private void load(TextureStore textures, ITrackStore tracks)
        {
            string videoPath = "BadApple.mp4";
            Stream videoStream = textures.GetStream(videoPath);
            InternalChildren = new Drawable[]
            {
                bg = new Video(videoStream, false)
                {
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Loop = true,
                    IsPlaying = false,
                },
                camera = new Camera
                {
                    Position = new Vector2(0, 0)
                },
                text = new SpriteText
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Position = new Vector2(0, 0),
                    Text = "",
                    Colour = Color4.White,
                    Font = new FontUsage(size: 100),
                }
            };
            track = tracks.Get("Audio.mp3");
            track.Volume.Value = 0;
            track.Start();
            bg.IsPlaying = false;
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            return base.OnKeyDown(e);
        }
    }
}
