using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osuTK;

namespace TemplateGame.Game
{
    public partial class Camera : CompositeDrawable
    {
        public Mapa Map;
        public Vector2 CameraPos;
        public Container Box;
        private Player player;
        private bool collision = false;
        private int i;
        private int lastObjectIndex;
        private bool lastObject = false;

        public Camera()
        {
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopLeft;
            AutoSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {

            i = 0;
            InternalChild = Box = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    Map = new Mapa
                    {
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                    },
                    player = new Player
                    {
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.Centre,
                    },
                }
            };
            lastObjectIndex = Map.LastObject();
        }

        protected override void LoadComplete()
        {
        }

        protected override void Update()
        {
            base.Update();
            if (i != lastObjectIndex)
            {
                if (Map.Objects[i].CheckCollision(player.CollisionQuad))
                {
                    collision = true;
                    //text.Text = "Kolize";
                }
                else
                {
                    collision = false;
                    //text.Text = "NeKolize :(";
                }

            }
            else { lastObject = true; }
        }


        protected override bool OnKeyDown(KeyDownEvent e)
        {
            bool already_Switched = false;

            if (e.Key == osuTK.Input.Key.N && lastObject == false)
            {
                if (player.Koul1.CanMove && collision)
                {
                    player.Koul1.CanMove = false;
                    //CameraPos = new Vector2(Box.Position.X, Box.Position.Y);
                    player.Koul2.Change(player.Koul1.Angle);
                    player.Koul1.Position = new Vector2(0, 0);
                    player.Position = Center;
                    player.Koul2.CanMove = true;
                    already_Switched = true;
                }

                if (player.Koul2.CanMove && already_Switched == false && collision)
                {
                    player.Koul2.CanMove = false;
                    //CameraPos = new Vector2(Box.Position.X, Box.Position.Y);
                    player.Koul1.Change(player.Koul2.Angle);
                    player.Koul2.Position = new Vector2(0, 0);
                    player.Position = Center;
                    player.Koul1.CanMove = true;
                    already_Switched = true;
                    /*
                     stary code ktery nefunguje s delay
                     player.Koul2.canMove = false;
                     this.box.MoveTo(new Vector2(this.box.Position.X, this.box.Position.Y - 150));
                     player.Koul1.Change(player.Koul2._angle);
                     player.Koul2.Position = Center;
                     player.Koul1.center = Center;
                     i++;
                     player.Koul1.canMove = true;
                    */
                }

                if (already_Switched)
                {
                    switch (Map.Objects[i].Direction)
                    {
                        case _Direction.Up:
                            this.MoveTo(new Vector2(this.Position.X, this.Position.Y + Map.GridSize), Map.GridSize);
                            break;

                        case _Direction.Down:
                            this.MoveTo(new Vector2(this.Position.X, this.Position.Y - Map.GridSize), Map.GridSize);
                            break;

                        case _Direction.Left:
                            this.MoveTo(new Vector2(this.Position.X + Map.GridSize, this.Position.Y), Map.GridSize);
                            break;

                        case _Direction.Right:
                            this.MoveTo(new Vector2(this.Position.X - Map.GridSize, this.Position.Y), Map.GridSize);
                            break;
                    }

                    if (lastObject == false) { i++; }
                }
            }

            return base.OnKeyDown(e);
        }

        public Vector2 Center => new(Map.Objects[i].Position.X + CameraPos.X, Map.Objects[i].Position.Y + CameraPos.Y);
    }
}
