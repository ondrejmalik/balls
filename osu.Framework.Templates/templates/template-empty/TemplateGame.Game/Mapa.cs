using System.Collections.Generic;
using Newtonsoft.Json.Linq;
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
        private List<JToken> actions = new List<JToken>();

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
            actions = mapLoader.LoadActions();
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
                b = new Block();

                switch (directions[i])
                {
                    case 'R':
                        x++;
                        b.Direction = _Direction.Right;
                        break;

                    case 'L':
                        x--;
                        b.Direction = _Direction.Left;
                        break;

                    case 'U':
                        y--;
                        b.Direction = _Direction.Up;
                        break;

                    case 'D':
                        y++;
                        b.Direction = _Direction.Down;
                        break;
                }

                addBox(i, x, y, b);
            }

            foreach (var action in actions)
            {
                int i = (int)action["floor"];
                string eventType = (string)action["eventType"];
                string speedType = (string)action["speedType"];
                float bpm = (float)action["beatsPerMinute"];
                float bpmMultiplier = (float)action["bpmMultiplier"];

                if (speedType == "Multiplier")
                {
                    Objects[i].Type = new Type(_Change.Slow, bpmMultiplier);
                }
            }
        }

        private void addBox(int i, int x, int y, Block b)
        {
            b.X = x * GridSize;
            b.Y = y * GridSize;
            //b.Type.Change = _Change.Normal;
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
