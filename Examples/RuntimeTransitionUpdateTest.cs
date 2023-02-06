using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blooper.TransitionEffects;

namespace Blooper.Examples
{
    public class RuntimeTransitionUpdateTest : MonoBehaviour
    {
        //We can reference this directly, instead of using the slow method below. Just make this serializer and use the search selector. If you've added it to the feature, it should pop right up
        private TransitionRenderFeature transition;

        [SerializeField] [Range(0, 1)] private float _transitionPercentage;
        [SerializeField] private Color color;

        private void Awake()
        {
            transition = TransitionEffectUtility.FindTransitionEffectRenderFeature();
        }

        void Update()
        {
            transition.SetTransition(_transitionPercentage);
            transition.SetColor(color);
        }
    }
}