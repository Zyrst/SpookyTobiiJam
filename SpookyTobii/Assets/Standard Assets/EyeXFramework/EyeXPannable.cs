//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Tobii.EyeX.Client;
using Tobii.EyeX.Framework;
using UnityEngine;

/// <summary>
/// Used for assigning the Pannable behavior to an interactor, making it respond to panning events.
/// See <see cref="EyeXInteractor.EyeXBehaviors"/>.
/// </summary>
public sealed class EyeXPannable : IEyeXBehavior
{
	private readonly EyeXPanningHub _hub;

    /// <summary>
    /// Gets or sets the available panning directions.
    /// </summary>
    public EyeXPanDirection AvailableDirections { get; set; }

    /// <summary>
    /// Gets or sets the current panning profile.
    /// </summary>
    public EyeXPanningProfile Profile { get; set; }

    /// <summary>
    /// Gets the current panning velocity.
    /// </summary>
    public Vector2 Velocity { get; internal set; }

    /// <summary>
    /// Gets the current step amount.
    /// </summary>
    public Vector2 Step { get; internal set; }

    /// <summary>
    /// Gets the current step duration.
    /// </summary>
    public float StepDuration { get; internal set; }

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="hub">The hub.</param>
    /// <param name="availableDirections">The available directions.</param>
    /// <param name="profile">The profile.</param>
	public EyeXPannable(EyeXPanningHub hub, EyeXPanDirection availableDirections, EyeXPanningProfile profile)
    {
        _hub = hub;

        AvailableDirections = availableDirections;
        Profile = profile;        

        Velocity = Vector2.zero;
        Step = Vector2.zero;
        StepDuration = 0.0f;
    }

	#region IEyeXBehavior interface

	public void AssignBehavior (Interactor interactor)
	{
		// Create the parameters used to create the pannable behavior.
		var param = new PannableParams
		{
			IsHandsFreeEnabled = EyeXBoolean.False,
			PanDirectionsAvailable = (PanDirection)AvailableDirections,
			Profile = (PanningProfile)Profile
		};
		
		// Create and associate the pannable behavior with the interactor.
		interactor.CreatePannableBehavior(ref param);
	}

	public void HandleEvent (string interactorId, IEnumerable<Behavior> behaviors)
	{
		_hub.Handle(this, behaviors);
	}

	#endregion
}

[Flags]
public enum EyeXPanDirection
{
    None = 0,
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8,
    All = Left | Right | Up | Down
}

public enum EyeXPanningProfile
{
    None = 1,
    Reading = 2,
    Horizontal = 3,
    Vertical = 4,
    VerticalFirstThenHorizontal = 5,
    Radial = 6,
    HorizontalFirstThenVertical = 7,
}

public static class EyeXPannableInteractorExtensions
{
    /// <summary>
    /// Gets the current panning velocity.
    /// </summary>
    /// <param name="interactor"></param>
    /// <returns></returns>
    public static Vector2 GetPanVelocity(this EyeXInteractor interactor)
    {
        var behavior = GetPannableBehavior(interactor);
        return behavior == null ? Vector2.zero : behavior.Velocity;
    }

    /// <summary>
    /// Gets the current step amount when panning.
    /// </summary>
    /// <param name="interactor"></param>
    /// <returns></returns>
    public static Vector2 GetPanStep(this EyeXInteractor interactor)
    {
        var behavior = GetPannableBehavior(interactor);
        return behavior == null ? Vector2.zero : behavior.Step;
    }

    /// <summary>
    /// Gets the current step duration when panning.
    /// </summary>
    /// <param name="interactor"></param>
    /// <returns></returns>
    public static float GetPanStepDuration(this EyeXInteractor interactor)
    {
        var behavior = GetPannableBehavior(interactor);
        return behavior == null ? 0.0f : behavior.StepDuration;
    }

    /// <summary>
    /// Gets the <see cref="EyeXPannable"/> behavior for the specified interactor.
    /// </summary>
    /// <param name="interactor">The interactor.</param>
    /// <returns>The <see cref="EyeXPannable"/> for the specified interactor.</returns>
    public static EyeXPannable GetPannableBehavior(EyeXInteractor interactor)
    {
        foreach (var behavior in interactor.EyeXBehaviors)
        {
            var gazeAwareBehavior = behavior as EyeXPannable;
            if (gazeAwareBehavior != null)
            {
                return gazeAwareBehavior;
            }
        }
        return null;
    }
}