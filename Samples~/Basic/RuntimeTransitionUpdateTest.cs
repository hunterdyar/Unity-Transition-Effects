using UnityEngine;
using Blooper.TransitionEffects;

namespace Blooper.Examples
{
    public class RuntimeTransitionUpdateTest : MonoBehaviour
    {
        //We can reference this directly, instead of using the slow method below. Just make this serializer and use the search selector. If you've added it to the feature, it should pop right up
        private TransitionEffectRenderFeature _transitionEffect;

        [SerializeField] [Range(0, 1)] private float _transitionPercentage;
        [SerializeField] private Color color;

        private void Awake()
        {
            _transitionEffect = TransitionEffectUtility.FindTransitionEffectRenderFeature();
        }

        void Update()
        {
            _transitionEffect.SetTransition(_transitionPercentage);
            _transitionEffect.SetColor(color);
        }
    }
}