using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace TemplateGame.Game
{
    public partial class Player : CompositeDrawable
    {
        public Koule Koul1;
        public Koule Koul2;

        public Player()
        {
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            Position = new Vector2(3 * 150, 4 * 150);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChildren = new Drawable[]
            {
                Koul1 = new Koule
                {
                    Center = Position,
                },
                Koul2 = new Koule
                {
                    Position = Koul1.Center,
                }
            };
            Koul1.Sprite.Texture = textures.Get("red");
            Koul2.Sprite.Texture = textures.Get("green");
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
                    RectangleF rect = Koul1.ScreenSpaceDrawQuad.AABBFloat;
                    return Quad.FromRectangle(rect);
                }
                else
                {
                    RectangleF rect = Koul2.ScreenSpaceDrawQuad.AABBFloat;
                    return Quad.FromRectangle(rect);
                }
            }
        }
    }
}
