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
    
    public static IEnumerator WipeLeftToRight(float delayBeforeStart,float time, Color color)
    {
        LazyGetRenderFeature();
        
        _transition.SetTransition(0);
        yield return new WaitForSeconds(delayBeforeStart);
        
        float t = 0;
        while (t < 1)
        {
            _transition.SetTransition(t);
            t += Time.deltaTime / time;
            yield return null;
        }
        _transition.SetTransition(1);
        yield return null;
        
        while (t >0)
        {
            _transition.SetTransition(t);
            t -= Time.deltaTime / time;
            yield return null;
        }

        _transition.SetTransition(0);
    }

    public static IEnumerator CircleWipeIn(float delayBeforeStart, float time, Color color)
    {
        LazyGetRenderFeature();

        float t = 0;
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
    
    public static IEnumerator CircleWipeOut(float delayBeforeStart, float time, Color color)
    {
        LazyGetRenderFeature();

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
    }

    private static void LazyGetRenderFeature()
    {
        
    }
}
