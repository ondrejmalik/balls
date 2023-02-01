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
        private string map = "rnrnrnrnrndudndndnldlnlnur";
        private Block b;
        private MapLoader mapLoader = new MapLoader();

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
            map = mapLoader.LoadMap();
            int x = 4;
            int y = 3;
            Box.Add(Objects[0] = new Block
            {
                Position = new Vector2(x * GridSize, y * GridSize),
                Direction = _Direction.Left,
            });
            char[] directions = map.ToCharArray();

            for (int i = 0; i < map.Length; i++)
            {
                if (i % 2 == 0)
                {
                    b = new Block();

                    switch (directions[i])
                    {
                        case 'r':
                            x++;
                            b.Direction = _Direction.Right;
                            break;

                        case 'l':
                            x--;
                            b.Direction = _Direction.Left;
                            break;

                        case 'u':
                            y--;
                            b.Direction = _Direction.Up;
                            break;

                        case 'd':
                            y++;
                            b.Direction = _Direction.Down;
                            break;
                    }
                }
                else
                {
                    switch (directions[i])
                    {
                        case 'u':
                            b.Type = new Type(_Change.Speed);
                            break;

                        case 'd':
                            b.Type = new Type(_Change.Slow);
                            break;

                        case 'r':
                            b.Type = new Type(_Change.Reverse);
                            break;

                        case 'n':
                            b.Type = new Type(_Change.Normal);
                            break;
                    }

                    addBox(i / 2, x, y, b);
                }
            }
        }

        private void addBox(int i, int x, int y, Block b)
        {
            b.X = x * GridSize;
            b.Y = y * GridSize;
            Box.Add(Objects[i] = b);
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
