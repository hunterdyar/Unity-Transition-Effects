using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace Blooper.TransitionEffects
{
	public class TransitionPassData
	{
		public Material Material;
		public TextureHandle DestinationTexture;
		public TextureHandle SourceTexture;
	}
}