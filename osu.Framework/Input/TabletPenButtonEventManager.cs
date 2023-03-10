// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Input.States;

namespace osu.Framework.Input
{
    public class TabletPenButtonEventManager : ButtonEventManager<TabletPenButton>
    {
        public TabletPenButtonEventManager(TabletPenButton button)
            : base(button)
        {
        }

        protected override Drawable HandleButtonDown(InputState state, List<Drawable> targets) => PropagateButtonEvent(targets, new TabletPenButtonPressEvent(state, Button));

        protected override void HandleButtonUp(InputState state, List<Drawable> targets)
        {
            if (targets == null)
                return;

            PropagateButtonEvent(targets, new TabletPenButtonReleaseEvent(state, Button));
        }
    }
}
