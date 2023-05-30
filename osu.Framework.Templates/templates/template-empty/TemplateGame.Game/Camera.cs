using System;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osuTK;
using UdpTest.Game;

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
        private int LastObjectIndex;
        public bool LastObject = false;
        private SpriteText text;
        private double deathangle = 0;
        private double deathangleOposite = 0;
        public bool IsDead = false;
        private ITrackStore tracks;
        private Track hitsound;
        public Track Song;
        bool exploded = false;
        private ParticleLayer particleLayer;

        public int I => i;

        public int LastObjectIndex1 => LastObjectIndex;

        public Camera()
        {
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopLeft;
            AutoSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures, ITrackStore tracks)
        {
            this.tracks = tracks;
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
                    particleLayer = new ParticleLayer
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
            hitsound = tracks.Get("hitsound");
            hitsound.Volume.Value = GameSettings.HitsoundVolume / 100;

            Song = tracks.Get("Metronome");
            Song.Volume.Value = GameSettings.MusicVolume / 100;

            LastObjectIndex = Map.LastObject();
        }

        protected override void LoadComplete()
        {
        }

        protected override void Update()
        {
            base.Update();
            if (GameSettings.EnableParticles) particleLayer.AddParticle(player.Koul1.Position + player.Position, player.Koul2.Position + player.Position);
            //Logger.Log(collision.ToString());

            if (i != LastObjectIndex && !IsDead)
            {
                if (Map.Objects[i].CheckCollision(player.CollisionQuad))
                {
                    collision = true;
                }
                else
                {
                    collision = false;
                }

                //---------------------Check Dead---------------------
                if (player.Koul1.CanMove)
                {
                    if (player.Koul1.Angle >= deathangle || player.Koul1.Angle <= deathangleOposite)
                    {
                        if (i > 0 && !exploded)
                        {
                            exploded = true;
                            Song.Stop();
                            player.Explode();
                            IsDead = true;
                        }
                    }
                }

                if (player.Koul2.CanMove)
                {
                    if (player.Koul2.Angle >= deathangle || player.Koul2.Angle <= deathangleOposite)
                    {
                        if (i > 0 && !exploded)
                        {
                            exploded = true;
                            Song.Stop();
                            player.Explode();
                            IsDead = true;
                        }
                    }
                }
            }
            else
            {
                LastObject = true;
            }
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            bool already_Switched = false;
            bool doSwitch = true;

            if (e.Key == osuTK.Input.Key.N && LastObject == false)
            {
                if (!collision && i > 0 && !exploded)
                {
                    exploded = true;
                    Song.Stop();
                    player.Explode();
                    IsDead = true;
                }

                else
                {
                    Logger.Log(i.ToString());

                    if (player.Koul1.CanMove && collision)
                    {
                        Logger.Log(Map.Objects[i].Direction.ToString());

                        Song.Start();
                        hitsound.Restart();

                        if (Map.Objects[i].Type == null)
                        {
                            Change(true);
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

                        deathangle = player.Koul2.Angle + MathF.PI * 2;
                        deathangleOposite = player.Koul2.Angle - MathF.PI * 2;

                        if (LastObject == false) { i++; }
                    }

                    if (player.Koul2.CanMove && already_Switched == false && collision)
                    {
                        Song.Start();
                        hitsound.Restart();
                        Logger.Log(Map.Objects[i].Direction.ToString());
                        Logger.Log(GetNearestAngle(player.Koul2.Angle).ToString());

                        if (Map.Objects[i].Type == null)
                        {
                            Change(false);
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

                        deathangle = player.Koul1.Angle + MathF.PI * 2;
                        deathangleOposite = player.Koul1.Angle - MathF.PI * 2;

                        if (LastObject == false) { i++; }
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
            }

            return base.OnKeyDown(e);
        }

        public int GetNearestAngle(float angleInRadians)
        {
            // Convert angle from radians to degrees
            float angleInDegrees = angleInRadians * 180f / MathF.PI;

            // Normalize the angle to be positive
            while (angleInDegrees < 0)
                angleInDegrees += 360;
            while (angleInDegrees > 360)
                angleInDegrees -= 360;

            // Calculate the nearest angles
            int[] targetAngles = { 0, 90, 180, 270 };
            int nearestAngle = targetAngles[0];
            float minDifference = Math.Abs(angleInDegrees - targetAngles[0]);

            foreach (int targetAngle in targetAngles)
            {
                float difference = Math.Abs(angleInDegrees - targetAngle);

                if (difference < minDifference)
                {
                    minDifference = difference;
                    nearestAngle = targetAngle;
                }
            }

            return nearestAngle;
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
