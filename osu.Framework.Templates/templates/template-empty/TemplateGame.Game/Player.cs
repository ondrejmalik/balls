using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Video;
using osuTK;
using UdpTest.Game;

namespace TemplateGame.Game
{
    public partial class Player : CompositeDrawable
    {
        public Koule Koul1;
        public Koule Koul2;
        public Video Explosion;
        public Track ExplosionAudio;
        private Container box;
        private Stream videoStream;

        public Player()
        {
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            Position = new Vector2(4 * 150, 3 * 150);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures, ITrackStore tracks)
        {
            videoStream = textures.GetStream("Explosion.gif");
            ExplosionAudio = tracks.Get("explosion");
            ExplosionAudio.Volume.Value = GameSettings.HitsoundVolume;
            InternalChild = box = new Container
            {
                Children = new Drawable[]
                {
                    Koul1 = new Koule
                    {
                        Center = Position,
                    },
                    Koul2 = new Koule
                    {
                        Position = new Vector2(0, 0),
                    }
                }
            };
            Koul1.Sprite.Texture = textures.Get("red");
            Koul2.Sprite.Texture = textures.Get("green");
        }

        public void Explode()
        {
            box.Add(Explosion = new Video(videoStream, false)
            {
                FillMode = FillMode.Fill,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Loop = false,
                IsPlaying = true,
            });
            ExplosionAudio.Restart();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Koul1.CanMove = true;
        }

        public Quad CollisionQuad
        {
            get
            {
                if (Koul1.CanMove)
                {
                    RectangleF rect = Koul1.hitbox.ScreenSpaceDrawQuad.AABBFloat;
                    return Quad.FromRectangle(rect);
                }
                else
                {
                    RectangleF rect = Koul2.hitbox.ScreenSpaceDrawQuad.AABBFloat;
                    return Quad.FromRectangle(rect);
                }
            }
        }
    }
}
