using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;

namespace Blooper.TransitionEffects
{
	public class TransitionPass :ScriptableRenderPass
	{
		//Cache properties
		private static readonly int ColorPropID = Shader.PropertyToID("_Color");
		private static readonly int LerpPropID = Shader.PropertyToID("_Lerp");
		private static readonly int CenterPropID = Shader.PropertyToID("_Center");
		private static readonly int TransitionTexturePropID = Shader.PropertyToID("_TransitionTexture");

		private const string TransitionPassName = "TransitionEffectRenderPass";
		
		internal TextureHandle copySourceTexture;
		internal TransitionType _currentType;
		internal Material _material;
		public TransitionEffectPassSettings _settings;

		public TransitionPass(TransitionEffectPassSettings settings)
		{
			_settings = settings;
		}

		private void UpdateMaterialSettings()
		{
			if (_settings == null)
			{
				_settings = TransitionEffectPassSettings.GetDefault();
			}
			if (_currentType != _settings.TransitionType || _material == null)
			{
				//update material being used. 
				_material = CoreUtils.CreateEngineMaterial(_settings.GetShaderName());
				_currentType = _settings.TransitionType;
			}

			_material.SetColor(ColorPropID, _settings.Color);
			_material.SetFloat(LerpPropID,_settings.Transition);
			_material.SetTexture(TransitionTexturePropID, _settings.Image);
			_material.SetVector(CenterPropID, _settings.Center);
		}
		//In the RecordRenderGraph method, declare render pass inputs and outputs, but do not add commands to command buffers.
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
			using (var builder = renderGraph.AddRasterRenderPass<TransitionPassData>(passName, out var passData))
			{
				UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
				passData.SourceTexture = resourceData.activeColorTexture;

				UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();

				passData.SourceTexture = resourceData.activeColorTexture;
				
				var transitionTexDesc = resourceData.activeColorTexture.GetDescriptor(renderGraph);
				transitionTexDesc.name = "_TransitionEffectTexture";
				transitionTexDesc.depthBufferBits = 0;
				passData.DestinationTexture = renderGraph.CreateTexture(transitionTexDesc);

				//Update Material settings?
				UpdateMaterialSettings();
				passData.Material = _material;

				if (!passData.SourceTexture.IsValid() || !passData.DestinationTexture.IsValid())
				{
					Debug.Log("src or dst is invalid");
					return;
				}

				builder.UseTexture(passData.SourceTexture, AccessFlags.ReadWrite);
				builder.UseTexture(passData.DestinationTexture, AccessFlags.ReadWrite);
				
				//debug to prevent removing this if it doesn't do shit
				builder.AllowPassCulling(false);
				
				builder.SetRenderFunc((TransitionPassData data, RasterGraphContext context) => ExecutePass(data, context));
			}
			

			// // The AddBlitPass method adds a vertical blur render graph pass that blits from the source texture (camera color in this case) to the destination texture using the first shader pass (the shader pass is defined in the last parameter).
			// RenderGraphUtils.BlitMaterialParameters transition = new(src, dst, _material, 0);
			// renderGraph.AddBlitPass(transition, TransitionPassName);
			// RenderGraphUtils.BlitMaterialParameters copy = new(dst, src, null , 1);
			// renderGraph.AddBlitPass(copy, TransitionPassName+"-copy");
		}

		static void ExecutePass(TransitionPassData passData, RasterGraphContext context)
		{
			// Blitter.BlitCameraTexture(context.cmd, passData.SourceTexture, passData.DestinationTexture, passData.Material, 0);
			Blitter.BlitTexture(context.cmd, passData.SourceTexture, new Vector4(1, 1, 0, 0), passData.Material, 0);
			
		}
	}
}