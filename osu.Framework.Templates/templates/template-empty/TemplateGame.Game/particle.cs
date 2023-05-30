using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace TemplateGame.Game
{
    public partial class Particle : CompositeDrawable
    {
        public Sprite Sprite;
        private bool isRed;

        public Particle(bool isRed)
        {
            this.isRed = isRed;
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    Sprite = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Texture = textures.Get("particleGreen"),
                    },
                }
            };
            if (isRed) Sprite.Texture = textures.Get("particleRed");
        }

        protected override void LoadComplete()
        {
            this.LifetimeEnd = Time.Current + 200;
            base.LoadComplete();
        }
    }
}
