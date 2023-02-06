using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Blooper.TransitionEffects
{
    public class TransitionRenderFeature : ScriptableRendererFeature
    {


        [SerializeField] private TransitionEffectPassSettings _settings = new();
        private TransitionPass _pass;

        public override void Create()
        {
            _pass = new TransitionPass(_settings, _pass);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(_pass);
        }

        public void SetTransition(float transitionLerp)
        {
            _settings.transition = Mathf.Clamp01(transitionLerp);
        }

        public void SetColor(Color color)
        {
            _settings.Color = color;
        }
    }
}