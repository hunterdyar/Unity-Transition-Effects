using System;
using UnityEngine;

namespace Blooper.TransitionEffects
{
	[Serializable]
	public class TransitionEffectPassSettings
	{
		[HideInInspector] public FilterMode FilterMode = FilterMode.Bilinear; //In my testing, this doesn't have an effect. Because we are doing it at screen-resolution, by definition, there's no sampling anyway.
			public TransitionType transitionType;

			[Tooltip("0 has The scene completely visible. 1 Has the transition effect completely obscuring the scene.")] [Range(0, 1)]
			public float transition;

			public Color Color; //Luckily, black is already a pretty good default value :p

			// ...
			public string GetShaderName()
			{
				switch (transitionType)
				{
					case TransitionType.VerticalWipe:
						return "Hidden/BloopWipeVerticalTransitionEffect";
					case TransitionType.CircleWipe:
						return "Hidden/BloopCircleTransitionEffect";
					case TransitionType.HorizontalWipe:
					default:
						return "Hidden/BloopWipeTransitionEffect";
				}
			}
		
	}
}