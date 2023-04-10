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
        public Block[] Objects = new Block[10000];
        public int GridSize = 150;
        private string map;
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
            float x = 4;
            float y = 3;
            int noClickCount = 0;
            Box.Add(Objects[0] = new Block // prvni blok
            {
                Position = new Vector2(x * GridSize, y * GridSize),
                Direction = _Direction.Left,
            });
            char[] directions = map.ToCharArray();

            for (int i = 0; i < map.Length; i++)
            {
                b = new Block();
                bool noClickBool = false;

                switch (directions[i])
                {
                    case '!':
                        Objects[i - noClickCount - 1].Type = new Type(_Change.NoClick);
                        Objects[i - noClickCount - 1].Hitbox.Colour = Colour4.Red;
                        noClickCount++;
                        x = Objects[i - noClickCount - 1].Position.X / GridSize;
                        y = Objects[i - noClickCount - 1].Position.Y / GridSize;
                        noClickBool = true;
                        break;

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

                    case 'Z':
                        x -= 0.70710678f;
                        y += 0.70710678f;
                        b.Direction = _Direction.Z;
                        break;

                    case 'Q':
                        x -= 0.70710678f;
                        y -= 0.70710678f;
                        b.Direction = _Direction.Q;
                        break;

                    case 'E':
                        x += 0.70710678f;
                        y -= 0.70710678f;
                        b.Direction = _Direction.E;
                        break;

                    case 'C':
                        x += 0.70710678f;
                        y += 0.70710678f;
                        b.Direction = _Direction.C;
                        break;
                }

                if (noClickBool == false) { addBox(i - noClickCount, x, y, b); }
            }

            foreach (var action in actions)
            {
                int i = (int)action["floor"];
                string eventType = (string)action["eventType"];
                string speedType = null;
                float bpm = 0;
                float bpmMultiplier = 0;

                if ((string)action["speedType"] != null) { speedType = (string)action["speedType"]; }

                if ((string)action["beatsPerMinute"] != null) bpm = (float)action["beatsPerMinute"];
                if ((string)action["bpmMultiplier"] != null) bpmMultiplier = (float)action["bpmMultiplier"];

                if (speedType == "Multiplier")
                {
                    Objects[i].Type = new Type(_Change.Slow, bpmMultiplier, 0);
                    Objects[i].Hitbox.Colour = Colour4.Yellow;
                }

                if (speedType == "Bpm")
                {
                    Objects[i].Type = new Type(_Change.Slow, 0, bpm);
                    Objects[i].Hitbox.Colour = Colour4.Yellow;
                }

                if (eventType == "Twirl")
                {
                    Objects[i].Type = new Type(_Change.Twirl);
                    Objects[i].Hitbox.Colour = Colour4.Green;
                }
            }

            textureSet();
        }

        private void addBox(int i, float x, float y, Block b)
        {
            b.X = x * GridSize;
            b.Y = y * GridSize;
            //b.Type.Change = _Change.Normal;
            Box.Add(Objects[i] = b);
        }

        private void textureSet()
        {
            for (int i = 0; i < LastObject() - 1; i++)
            {
                if (Objects[i].Direction == _Direction.Right)
                {
                    if (Objects[i + 1].Direction == _Direction.Up)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("LU");
                    }

                    if (Objects[i + 1].Direction == _Direction.Down)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("LD");
                    }

                    if (Objects[i + 1].Direction == _Direction.Left)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("LR");
                    }

                    if (Objects[i + 1].Direction == _Direction.Right)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("LR");
                    }
                }

                if (Objects[i].Direction == _Direction.Down)
                {
                    if (Objects[i + 1].Direction == _Direction.Up)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("UD");
                    }

                    if (Objects[i + 1].Direction == _Direction.Down)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("UD");
                    }

                    if (Objects[i + 1].Direction == _Direction.Left)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("LU");
                    }

                    if (Objects[i + 1].Direction == _Direction.Right)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("RU");
                    }
                }

                if (Objects[i].Direction == _Direction.Left)
                {
                    if (Objects[i + 1].Direction == _Direction.Up)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("LU");
                    }

                    if (Objects[i + 1].Direction == _Direction.Down)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("RD");
                    }

                    if (Objects[i + 1].Direction == _Direction.Left)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("LR");
                    }

                    if (Objects[i + 1].Direction == _Direction.Right)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("LR");
                    }
                }

                if (Objects[i].Direction == _Direction.Up)
                {
                    if (Objects[i + 1].Direction == _Direction.Up)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("UD");
                    }

                    if (Objects[i + 1].Direction == _Direction.Down)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("UD");
                    }

                    if (Objects[i + 1].Direction == _Direction.Left)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("LD");
                    }

                    if (Objects[i + 1].Direction == _Direction.Right)
                    {
                        Objects[i].Sprite.Texture = Objects[i].Textures.Get("RU");
                    }
                }
            }
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
