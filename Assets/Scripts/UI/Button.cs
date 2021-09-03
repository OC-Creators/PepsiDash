using UnityEngine;
using General;

namespace UserInterface
{
	public class Button : MonoBehaviour
	{
		public void Click(AudioClip clip, Signal signal)
		{
			GameFlowController.Instance.dispatch(signal);
			AudioManager.Instance.PlaySE(clip);
		}

	}
}