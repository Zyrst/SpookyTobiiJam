//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Tobii EyeX/Pannable")]
public class PannableComponent : EyeXGameObjectInteractorBase
{
    public EyeXPanDirection availableDirections = EyeXPanDirection.All;
    public EyeXPanningProfile profile = EyeXPanningProfile.Radial;

    public Vector2 Step { get; private set; }
    public float StepDuration { get; private set; }
    public Vector2 Velocity { get; private set; }

    protected override void Update()
    {
        base.Update();

        Step = GameObjectInteractor.GetPanStep();
        StepDuration = GameObjectInteractor.GetPanStepDuration();
        Velocity = GameObjectInteractor.GetPanVelocity();
    }

    protected override IList<IEyeXBehavior> GetEyeXBehaviorsForGameObjectInteractor()
    {
        return new List<IEyeXBehavior>(new[] { new EyeXPannable(Host.PannableHub, availableDirections, profile) });
    }

    protected override bool AllowOverlap()
    {
        return true;
    }
}
