using osu.Framework.Allocation;
using osu.Framework.Extensions.PolygonExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Textures;
using osuTK;
using Box = osu.Framework.Graphics.Shapes.Box;

namespace TemplateGame.Game
{
    public enum _Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public partial class Block : CompositeDrawable
    {
        private Container box;
        public Box Hitbox;
        public _Direction Direction;

        public Block()
        {
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.TopLeft;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = box = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    Hitbox = new Box
                    {
                        Size = new Vector2(100, 100),
                        Colour = Colour4.Cyan,
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }

        public bool CheckCollision(Quad playerQuad)
        {
            if (playerQuad.Intersects(Hitbox.ScreenSpaceDrawQuad))
            {
                return true;
            }

            return false;
        }
    }
}
