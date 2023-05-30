using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace TemplateGame.Game
{
    public partial class ParticleLayer : CompositeDrawable
    {
        private Container Box;
        public float particleFrequencyCount = 0;

        public ParticleLayer()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            InternalChild = Box = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                }
            };
        }

        public void AddParticle(Vector2 NewPosition1, Vector2 NewPosition2)
        {
            NewPosition1 -= new Vector2(710, 560);
            NewPosition2 -= new Vector2(710, 560);

            if (particleFrequencyCount > 15)
            {
                Box.Add(new Particle(true)
                {
                    Position = NewPosition1
                });
                Box.Add(new Particle(false)
                {
                    Position = NewPosition2
                });
                particleFrequencyCount = 0;
            }
            else
            {
                particleFrequencyCount++;
            }
        }
    }
}
