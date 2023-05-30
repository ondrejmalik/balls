// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;

namespace TemplateGame.Game;

public partial class MenuButton : Button
{
    private Sprite sprite;
    private Box background;
    public SpriteText SpriteText;

    public MenuButton()
    {
        Origin = Anchor.TopCentre;
        Anchor = Anchor.TopCentre;
        Size = new osuTK.Vector2(400, 100);

        AddRange(new Drawable[]
        {
            background = new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Colour = FrameworkColour.BlueGreen
            },
            SpriteText = new SpriteText()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Text = Text,
                Font = new FontUsage(size: this.Height * 0.6f)
            }
        });
    }

    public LocalisableString Text
    {
        get => SpriteText?.Text ?? default;
        set
        {
            if (SpriteText != null)
                SpriteText.Text = value;
        }
    }
}
