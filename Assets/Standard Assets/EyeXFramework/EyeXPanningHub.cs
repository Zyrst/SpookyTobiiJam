//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Tobii.EyeX.Client;
using Tobii.EyeX.Framework;
using UnityEngine;

/// <summary>
/// Aggregates panning related events from the EyeX Engine so that they appear consistently within rendering frames.
/// </summary>
public class EyeXPanningHub
{
    private readonly List<Action> _actions;

    /// <summary>
    /// Creates a new instance.
    /// </summary>
	public EyeXPanningHub()
    {
        _actions = new List<Action>();
    }

    /// <summary>
    /// Handles an event belonging to <param name="owner">the specified owner</param>.
    /// </summary>
    /// <param name="owner">The owner of the event.</param>
    /// <param name="behaviors">The behaviors to be handled</param>
    public void Handle(EyeXPannable owner, IEnumerable<Behavior> behaviors)
    {
        foreach (var behavior in behaviors)
        {
            if (behavior.BehaviorType != BehaviorType.Pannable)
            {
                continue;
            }

            PannableEventType eventType;
            if (behavior.TryGetPannableEventType(out eventType))
            {
                if (eventType == PannableEventType.Pan)
                {
                    PannablePanEventParams param;
                    if (behavior.TryGetPannablePanEventParams(out param))
                    {
                        owner.Velocity = new Vector2((float)param.PanVelocityX, (float)param.PanVelocityY);
                    }
                }
                else if (eventType == PannableEventType.Step)
                {
                    PannableStepEventParams param;
                    if (behavior.TryGetPannableStepEventParams(out param))
                    {
                        owner.Step = new Vector2((float)param.PanStepX, (float)param.PanStepY);
                        owner.StepDuration = (float)param.PanStepDuration;

                        _actions.Add(() =>
                        {
                            owner.Step = Vector2.zero;
                            owner.StepDuration = 0f;
                        });
                    }
                }
            }
        }
    }

    /// <summary>
    /// Called at the end of each frame.
    /// </summary>
    public void EndFrame()
    {
        foreach (var action in _actions)
        {
            action();
        }
        _actions.Clear();
    }
}
