using osu.Framework.Allocation;
using osu.Framework.Extensions.PolygonExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
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
        Right,
        Z,
        Q,
        E,
        C
    }

    public partial class Block : CompositeDrawable
    {
        public Container Box;
        public Box Hitbox;
        private _Direction direction;
        public Type Type;
        public Sprite Sprite;
        public TextureStore Textures;
        private int hitboxSize;

        public Block()
        {
            hitboxSize = 1;
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.TopLeft;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            this.Textures = textures;
            InternalChild = Box = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    Hitbox = new Box
                    {
                        Size = new Vector2(hitboxSize, hitboxSize),
                        Colour = Colour4.Cyan,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                    Sprite = new Sprite()
                    {
                        Size = new Vector2(150, 150),
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                    }
                }
            };
        }

        public _Direction Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        public void TextureSet(Sprite sprite, string textureName)
        {
            sprite.Texture = Textures.Get(textureName);
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
