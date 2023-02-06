using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Blooper.TransitionEffects;
public class TransitionEffects : MonoBehaviour
{
    private static TransitionRenderFeature _transition;
    
    public static IEnumerator TransitionOutToColor(TransitionType type, float delayBeforeStart, float time, Color color)
    {
        LazyGetRenderFeature();
        _transition.SetActive(true);
        float t = 0;
        _transition.SetTransitionType(type);
        _transition.SetTransition(0);
        yield return new WaitForSeconds(delayBeforeStart);
        while (t < 1)
        {
            _transition.SetTransition(t);
            t += Time.deltaTime / time;
            yield return null;
        }

        _transition.SetTransition(1);
    }
    
    /// <summary>
    /// Transitions from completely covered by the transition to the scene.
    /// </summary>
    public static IEnumerator TransitionInToScene(float delayBeforeStart, float time, Color color)
    {
        LazyGetRenderFeature();
        _transition.SetActive(true);
        float t = 1;
        _transition.SetTransition(1);
        yield return new WaitForSeconds(delayBeforeStart);
        while (t > 0)
        {
            _transition.SetTransition(t);
            t -= Time.deltaTime / time;
            yield return null;
        }

        _transition.SetTransition(0);
        _transition.SetActive(false);

    }

    private static void LazyGetRenderFeature()
    {
        if (_transition == null)
        {
            _transition = TransitionEffectUtility.FindTransitionEffectRenderFeature();
        }
    }
}
