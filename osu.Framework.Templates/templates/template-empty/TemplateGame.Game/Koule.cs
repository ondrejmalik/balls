using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;
using UdpTest.Game;

namespace TemplateGame.Game
{
    public partial class Koule : CompositeDrawable
    {
        private Container box;
        private SpriteText text;
        public float Speed = 60;
        public float Multiplier = 1;
        private float radius = 150.0f;
        public bool CanMove = false;
        public Vector2 Center;
        public float Angle;
        public Sprite Sprite;
        public int Twirl = 1;
        public Circle hitbox;
        private int hitboxSize;

        public Koule()
        {
            if (GameSettings.HardMode)
            {
                hitboxSize = 30;
            }
            else
            {
                hitboxSize = 70;
            }

            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
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
                    hitbox = new Circle()
                    {
                        Size = new Vector2(hitboxSize, hitboxSize),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                    Sprite = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(50, 50),
                    },
                    text = new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = "",
                        Colour = Colour4.White,
                        Font = new FontUsage(size: 30),
                    }
                }
            };
        }

        protected override void Update()
        {
            if (CanMove)
            {
                base.Update();
                Angle += Twirl * Multiplier * Speed * ((float)Time.Elapsed) * MathF.PI / 1000 / 60;
                var x = MathF.Cos(Angle) * radius;
                var y = MathF.Sin(Angle) * radius;
                Position = new Vector2(x, y);
                //text.Text = Angle.ToString();
            }
        }

        public void Change(float druhyAngle)
        {
            Angle = druhyAngle + MathF.PI;
        }
    }
}
