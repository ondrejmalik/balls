using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
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
        private SpriteText text;
        private double deathangle = 0;

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
                    text = new SpriteText()
                    {
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        Colour = Colour4.White,
                        Font = new FontUsage(size: 30),
                        Position = new Vector2(10, 10)
                    }
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
                    text.Text = "Kolize";
                }
                else
                {
                    collision = false;
                    text.Text = "NeKolize :(";
                }

                if (player.Koul1.CanMove && player.Koul1.Angle >= deathangle) { MainScreen.text.Text = "Death1" + deathangle + "  " + player.Koul1.Angle; }

                if (player.Koul2.CanMove && player.Koul2.Angle >= deathangle) { MainScreen.text.Text = "Death2" + deathangle + "  " + player.Koul1.Angle; }
            }
            else { lastObject = true; }
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            bool already_Switched = false;
            bool doSwitch = true;

            if (e.Key == osuTK.Input.Key.N && lastObject == false)
            {
                if (player.Koul1.CanMove && collision)
                {
                    if (Map.Objects[i].Type == null)
                    {
                        deathangle = player.Koul1.Angle + MathF.PI * 1.5;
                        Change(true);
                    }
                    else
                    {
                    }

                    if (Map.Objects[i].Type != null)
                    {
                        if (Map.Objects[i].Type.Change == _Change.Slow || Map.Objects[i].Type.Change == _Change.Speed)
                        {
                            if (Map.Objects[i].Type.Bpm != 0)
                            {
                                player.Koul2.Speed = Map.Objects[i].Type.Bpm;
                                player.Koul2.Multiplier = 1;
                                player.Koul1.Speed = Map.Objects[i].Type.Bpm;
                                player.Koul1.Multiplier = 1;
                            }

                            if (Map.Objects[i].Type.Ratio != 0)
                            {
                                player.Koul2.Multiplier = Map.Objects[i].Type.Ratio;
                                player.Koul1.Multiplier = Map.Objects[i].Type.Ratio;
                            }
                        }

                        if (Map.Objects[i].Type.Change == _Change.Twirl)
                        {
                            if (player.Koul1.Twirl == 1) player.Koul1.Twirl = -1;
                            else player.Koul1.Twirl = 1;

                            if (player.Koul2.Twirl == 1) player.Koul2.Twirl = -1;
                            else player.Koul2.Twirl = 1;
                        }

                        if (Map.Objects[i].Type.Change == _Change.NoClick)
                        {
                            doSwitch = false;
                        }
                        else Change(true);
                    }

                    if (doSwitch)
                    {
                        player.Koul2.CanMove = true;
                        already_Switched = true;
                    }

                    if (lastObject == false) { i++; }
                }

                if (player.Koul2.CanMove && already_Switched == false && collision)
                {
                    if (Map.Objects[i].Type == null)
                    {
                        deathangle = player.Koul1.Angle + MathF.PI * 1.5;
                        Change(false);
                    }
                    else
                    {
                    }

                    if (Map.Objects[i].Type != null)
                    {
                        if (Map.Objects[i].Type.Change == _Change.Slow || Map.Objects[i].Type.Change == _Change.Speed)
                        {
                            if (Map.Objects[i].Type.Bpm != 0)
                            {
                                player.Koul2.Speed = Map.Objects[i].Type.Bpm;
                                player.Koul2.Multiplier = 1;
                                player.Koul1.Speed = Map.Objects[i].Type.Bpm;
                                player.Koul1.Multiplier = 1;
                            }

                            if (Map.Objects[i].Type.Ratio != 0)
                            {
                                player.Koul2.Multiplier = Map.Objects[i].Type.Ratio;
                                player.Koul1.Multiplier = Map.Objects[i].Type.Ratio;
                            }
                        }

                        if (Map.Objects[i].Type.Change == _Change.Twirl)
                        {
                            if (player.Koul1.Twirl == 1) player.Koul1.Twirl = -1;
                            else player.Koul1.Twirl = 1;

                            if (player.Koul2.Twirl == 1) player.Koul2.Twirl = -1;
                            else player.Koul2.Twirl = 1;
                        }

                        if (Map.Objects[i].Type.Change == _Change.NoClick)
                        {
                            doSwitch = false;
                        }
                        else Change(false);
                    }

                    if (doSwitch)
                    {
                        player.Koul1.CanMove = true;
                        already_Switched = true;
                    }

                    if (lastObject == false) { i++; }
                }

                if (already_Switched)
                {
                    float angle45 = 0.70710678f;

                    switch (Map.Objects[i - 1].Direction)
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

                        case _Direction.Q:
                            this.MoveTo(new Vector2(this.Position.X + Map.GridSize * angle45, this.Position.Y + Map.GridSize * angle45), Map.GridSize);
                            break;

                        case _Direction.E:
                            this.MoveTo(new Vector2(this.Position.X - Map.GridSize * angle45, this.Position.Y + Map.GridSize * angle45), Map.GridSize);
                            break;

                        case _Direction.Z:
                            this.MoveTo(new Vector2(this.Position.X + Map.GridSize * angle45, this.Position.Y - Map.GridSize * angle45), Map.GridSize);
                            break;

                        case _Direction.C:
                            this.MoveTo(new Vector2(this.Position.X - Map.GridSize * angle45, this.Position.Y - Map.GridSize * angle45), Map.GridSize);
                            break;
                    }
                }
            }

            return base.OnKeyDown(e);
        }

        private void Change(bool isKoul1)
        {
            if (isKoul1)
            {
                player.Koul1.CanMove = false;
                player.Koul2.Change(player.Koul1.Angle);
                player.Koul1.Position = new Vector2(0, 0);
                player.Position = Center;
            }
            else
            {
                player.Koul2.CanMove = false;
                player.Koul1.Change(player.Koul2.Angle);
                player.Koul2.Position = new Vector2(0, 0);
                player.Position = Center;
            }
        }

        public Vector2 Center => new(Map.Objects[i].Position.X + CameraPos.X, Map.Objects[i].Position.Y + CameraPos.Y);
    }
}
