using UnityEngine;
using Blooper.TransitionEffects;

namespace Blooper.Examples
{
	public class FadeInOnStart : MonoBehaviour
	{
		void Start()
		{
			StartCoroutine(Transition.TransitionInToScene(TransitionType.Fade,0.1f, 0.85f, Color.black));
		}
	}
}