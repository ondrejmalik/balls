using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace TemplateGame.Game
{
    public partial class Mapa : CompositeDrawable
    {
        public Container Box;
        public Block[] Objects = new Block[100];
        public int GridSize = 150;
        private string map = "rrrrrrrrrrrrrrrrrrrrrrrru";

        public Mapa()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            AutoSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = Box = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                }
            };
            int x = 4;
            int y = 3;
            Box.Add(Objects[0] = new Block
            {
                Position = new Vector2(x * GridSize, y * GridSize),
                Direction = _Direction.Left,
            });
            char[] directions = map.ToCharArray();

            for (int i = 1; i < map.Length; i++)
            {
                switch (directions[i])
                {
                    case 'r':
                        x++;
                        addBox(i, x, y, _Direction.Right);
                        break;

                    case 'l':
                        x--;
                        addBox(i, x, y, _Direction.Left);
                        break;

                    case 'u':
                        y--;
                        addBox(i, x, y, _Direction.Up);
                        break;

                    case 'd':
                        y++;
                        addBox(i, x, y, _Direction.Down);
                        break;
                }
            }
        }

        private void addBox(int i, int x, int y, _Direction direction)
        {
            Box.Add(Objects[i] = new Block
            {
                Position = new Vector2(x * GridSize, y * GridSize),
                Direction = direction,
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }

        protected override void Update()
        {
        }
        public int LastObject()
        {
            int i = 0;
            foreach (Block b in Objects)
            {
                if (b != null) i++; /////////////////////////////////////////////mejrä poradil :D
            }
            return i;
        }
    }
}
