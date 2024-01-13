using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Blooper.TransitionEffects
{
	[Serializable]
	public class TransitionEffectPassSettings
	{
		[HideInInspector] public FilterMode FilterMode = FilterMode.Bilinear; //In my testing, this doesn't have an effect. Because we are doing it at screen-resolution, by definition, there's no sampling anyway.
		
		public bool Active = true;
		[FormerlySerializedAs("transitionType")] public TransitionType TransitionType;
		[FormerlySerializedAs("transition")] [Tooltip("0 has The scene completely visible. 1 Has the transition effect completely obscuring the scene.")] [Range(0, 1)]
		public float Transition;
		public Color Color; //Luckily, black is already a pretty good default value :p
		public Vector2 Center = new Vector2(0.5f,0.5f);
		public Texture2D Image;
			// ...
			public string GetShaderName()
			{
				switch (TransitionType)
				{
					case TransitionType.Texture:
						return "Hidden/BloopTextureTransitionEffect";
					case TransitionType.VerticalWipe:
						return "Hidden/BloopWipeVerticalTransitionEffect";
					case TransitionType.CircleWipe:
						return "Hidden/BloopCircleTransitionEffect";
					case TransitionType.Fade:
						return "Hidden/BloopFadeTransitionEffect";
					case TransitionType.HorizontalWipe:
					default:
						return "Hidden/BloopWipeTransitionEffect";
				}
			}

			public void CopyFrom(TransitionEffectPassSettings settings)
			{
				this.Active = settings.Active;
				this.TransitionType = settings.TransitionType;
				this.Transition = settings.Transition;
				this.Color = settings.Color;
				this.Center = settings.Center;
			}
	}
}