using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using General;

namespace UserInterface{
	
	public class Timegauge : MonoBehaviour
	{

		public Image image;
		private bool isOver = false;
		private ParamBridge pb;
		// Start is called before the first frame update
		void Start()
		{
			pb = ParamBridge.Instance;
		}

		// Update is called once per frame
		void Update()
		{
			if (isOver)
			{
				return;
			}

			if (ParamBridge.Instance.Elapsed < 30f)
			{
				image.fillAmount = 1f - ParamBridge.Instance.Elapsed / 30f;
			}
			else
			{
				image.fillAmount = 0f;
				isOver = true;
				// GameEndを経由する場合はResult -> GameEnd
				GameFlowController.Instance.dispatch(Signal.Forward);
				Debug.Log("Game is over");
			}
		}
	}
}