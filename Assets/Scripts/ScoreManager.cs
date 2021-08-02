using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;  // 追加しましょう

public class ScoreManager : MonoBehaviour {


	public GameObject score_object = null; // Textオブジェクト
	public int score_num = 0; // スコア変数

	// 初期化
	void Start () {
	}

	// 更新
	void Update () {
		// オブジェクトからTextコンポーネントを取得
		Text score_text = score_object.GetComponent<Text> ();
		// テキストの表示を入れ替える
		score_text.text = "Score:" + score_num;

		if(Input.GetMouseButtonDown(0)){
		score_num += 1; // とりあえず1加算し続けてみる
		}
	}
}
