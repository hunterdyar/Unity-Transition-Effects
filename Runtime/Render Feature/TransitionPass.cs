using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Blooper.TransitionEffects
{
	public class TransitionPass : ScriptableRenderPass
	{
		private readonly TransitionEffectPassSettings _settings;
		private Material _material;

		//Cache properties
		private int _bufferID = Shader.PropertyToID("_TransitionBuffer");
		private static readonly int ColorPropID = Shader.PropertyToID("_Color");
		private static readonly int LerpPropID = Shader.PropertyToID("_Lerp");
		private RenderTargetIdentifier _bufferRenderTex;
		private TransitionType _currentType;
		private static readonly int TransitionTexturePropID = Shader.PropertyToID("_TransitionTexture");

		public TransitionPass(TransitionEffectPassSettings settings, TransitionPass _clone)
		{
			_settings = settings;
			renderPassEvent = RenderPassEvent.AfterRendering;
			_material = CoreUtils.CreateEngineMaterial(_settings.GetShaderName());
			// ...
		}

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			base.Configure(cmd, cameraTextureDescriptor);

			cmd.GetTemporaryRT(_bufferID, cameraTextureDescriptor, _settings.FilterMode);
			_bufferRenderTex = new RenderTargetIdentifier(_bufferID);
			// ...
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			base.OnCameraSetup(cmd, ref renderingData);
			// ...
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			//only render on top of the game. (ie: not reflections, scene view, VR, editor preview windows)
			if (renderingData.cameraData.cameraType != CameraType.Game || !_settings.Active)
			{
				return;
			}

			if (_settings.TransitionType != _currentType)
			{
				//update material being used. 
				_material = CoreUtils.CreateEngineMaterial(_settings.GetShaderName());
				_currentType = _settings.TransitionType;
			}

			var target = renderingData.cameraData.renderer.cameraColorTarget;

			CommandBuffer cmd = CommandBufferPool.Get();
			cmd.Clear();
			_material.SetColor(ColorPropID, _settings.Color);
			_material.SetFloat(LerpPropID, _settings.Transition);
			_material.SetTexture(TransitionTexturePropID,_settings.Image);
			Blit(cmd, target, _bufferRenderTex, _material, 0);
			Blit(cmd, _bufferRenderTex, target);
			context.ExecuteCommandBuffer(cmd);

			cmd.Clear();
			CommandBufferPool.Release(cmd);
		}

		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			base.OnCameraCleanup(cmd);
			cmd.ReleaseTemporaryRT(_bufferID);
		}
	}
}