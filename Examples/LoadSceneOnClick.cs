using Blooper.TransitionEffects;
using UnityEngine;

namespace Blooper.Examples
{
	public class LoadSceneOnClick : MonoBehaviour
	{
		public void LoadScene(string newScene)
		{
			StartCoroutine(Transition.LoadSceneAfterTransition(newScene, TransitionType.CircleWipe, 1f,  Color.black));
		}
	}
}