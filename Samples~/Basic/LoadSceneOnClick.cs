using Blooper.TransitionEffects;
using UnityEngine;

namespace Blooper.Examples
{
	public class LoadSceneOnClick : MonoBehaviour
	{
		public TransitionType type;
		public void LoadScene(string newScene)
		{
			StartCoroutine(Transition.LoadSceneAfterTransition(newScene, type, 1f,  Color.black));
		}
	}
}