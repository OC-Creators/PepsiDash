using UnityEngine;
using UnityEngine.UI;

public class Twitter : MonoBehaviour
{
	public GameObject score_object = null; // Textオブジェクト
	public Text clearText;

	//「つぶやく」ボタンを押したときの処理
	public void OnClickTweetButton()
	{
		//Text score_text = score_object.GetComponent();
		var url = "https://twitter.com/intent/tweet?"
			+ "text=" + "今回の記録は『"
			+ clearText.text
			+ "』点";

		Application.OpenURL(url);
	}
}
