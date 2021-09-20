using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gradation : MonoBehaviour
{

	//グラデーションの設定
	[SerializeField]
	private Gradient _gradient = default;

	//色を変える対象の画像
	[SerializeField]
	private Image _image = default;

	//色を変える時間と現在の時間
	private readonly float FADE_COLOR_TIME = 10.0f;
	private float _currentTime = 0;


	private void Awake() {
		//最初の色設定
		_image.color = _gradient.Evaluate(0);
	}


	private void Update() {
		//時間を進める
		_currentTime += Time.deltaTime;
		var timeRate = Mathf.Min(1f, _currentTime / FADE_COLOR_TIME);

		//色を変更
		_image.color = _gradient.Evaluate(timeRate);
	}
}
