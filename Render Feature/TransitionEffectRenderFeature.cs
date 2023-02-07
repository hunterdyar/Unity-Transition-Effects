using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Blooper.TransitionEffects
{
    public class TransitionEffectRenderFeature : ScriptableRendererFeature
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

        /// <summary>
        /// Set the curtain percentage of the settings. 0 has the game view completely visible, like normal. 1 covers the entire scene with the transition. Animating this value from 0 to 1 executes the transition.
        ///
        /// While this should be set to 0 when not in use, be sure to also SetTransitionActive(false) to skip doing the render pass entirely.
        /// </summary>
        /// <param name="transitionLerp"></param>
        public void SetTransition(float transitionLerp)
        {
            _settings.Transition = Mathf.Clamp01(transitionLerp);
        }

        /// <summary>
        /// Set the Color property of the transition. Alpha is ignored.
        /// </summary>
        public void SetColor(Color color)
        {
            _settings.Color = color;
        }

        public void SetTransitionType(TransitionType transitionType)
        {
            _settings.TransitionType = transitionType;
        }

        public void SetTransitionActive(bool active)
        {
            _settings.Active = active;
        }

        /// <summary>
        /// The Transition will use it's internal settings class, but copy the properties from the provided setting without referencing it.
        /// </summary>
        public void CopySettingsFrom(TransitionEffectPassSettings settings)
        {
            _settings.CopyFrom(settings);
        }

        /// <summary>
        /// The Transition will use the provided settings class for it's settings.
        /// </summary>
        public void SetSettings(TransitionEffectPassSettings settings)
        {
            _settings = settings;
        }
    }
}