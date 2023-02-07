using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Blooper.TransitionEffects;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    private static TransitionEffectRenderFeature _transitionEffect;
    
    public static IEnumerator TransitionOutToColor(TransitionType type, float delayBeforeStart, float time, Color color)
    {
        LazyGetRenderFeature();
        _transitionEffect.SetActive(true);
        float t = 0;
        _transitionEffect.SetTransitionType(type);
        _transitionEffect.SetTransition(0);
        yield return new WaitForSeconds(delayBeforeStart);
        while (t < 1)
        {
            _transitionEffect.SetTransition(t);
            t += Time.deltaTime / time;
            yield return null;
        }

        _transitionEffect.SetTransition(1);
    }
    
    /// <summary>
    /// Transitions from completely covered by the transition to the scene.
    /// </summary>
    public static IEnumerator TransitionInToScene(TransitionType type,float delayBeforeStart, float time, Color color)
    {
        LazyGetRenderFeature();
        _transitionEffect.SetActive(true);
        _transitionEffect.SetTransition(1);
        _transitionEffect.SetTransitionType(type);
        _transitionEffect.SetColor(color);
        float t = 1;
        yield return new WaitForSeconds(delayBeforeStart);
        while (t > 0)
        {
            _transitionEffect.SetTransition(t);
            t -= Time.deltaTime / time;
            yield return null;
        }

        _transitionEffect.SetTransition(0);
        _transitionEffect.SetActive(false);

    }
    
    /// <returns></returns>
    public static IEnumerator LoadSceneAfterTransition(string sceneName, TransitionType outType, float outTime, Color color)
    {
        LazyGetRenderFeature();
        _transitionEffect.SetActive(true);
        _transitionEffect.SetColor(color);
        _transitionEffect.SetTransitionType(outType);
        _transitionEffect.SetTransition(0);
        
        //pause at least one frame.
        yield return null;
        
        float t = 0;
        while (t < 1)
        {
            _transitionEffect.SetTransition(t);
            t += Time.deltaTime / outTime;
            yield return null;
        }
        _transitionEffect.SetTransition(1);
        //Now we are on solid Color.
        
        //load scene and wait for it to finish loading before disabling the transition effect.
        
        var operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }
        _transitionEffect.SetActive(false);

    }
    
    private static void LazyGetRenderFeature()
    {
        if (_transitionEffect == null)
        {
            _transitionEffect = TransitionEffectUtility.FindTransitionEffectRenderFeature();
        }
    }
    
}
