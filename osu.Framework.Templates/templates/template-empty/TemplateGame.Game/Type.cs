// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace TemplateGame.Game;

public enum _Change
{
    Normal,
    Speed,
    Slow,
    Twirl,
}

public class Type
{
    public _Change? Change;
    public float Ratio = 1;
    public float Bpm = 0;

    public Type(_Change change, float ratio, float bpm)
    {
        Change = change;
        Ratio = ratio;
    }

    public Type(_Change change)
    {
        Change = change;
    }
}
