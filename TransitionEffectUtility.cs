using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Blooper.TransitionEffects
{
	public class TransitionEffectUtility
	{
		public static TransitionEffectRenderFeature FindTransitionEffectRenderFeature()
		{
			TransitionEffectRenderFeature transitionEffect = null;

			//if universal render pipeline...
			//if HDRP...
			
			var renderer = ((UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline).GetRenderer(0);
			var property = typeof(ScriptableRenderer).GetProperty("rendererFeatures", BindingFlags.NonPublic | BindingFlags.Instance);
			List<ScriptableRendererFeature> features = property.GetValue(renderer) as List<ScriptableRendererFeature>;
			foreach (var feature in features)
			{
				if (feature is TransitionEffectRenderFeature transitionFeature)
				{
					transitionEffect = transitionFeature;
					return transitionEffect;
				}
			}

			//if null...
			Debug.LogWarning("You need to add a Transition Render Feature to the current render pipeline settings.");
			return null;
		}
	}
}