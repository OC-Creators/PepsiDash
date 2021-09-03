using UnityEngine;

public class GetWater : MonoBehaviour
{
	/// <summary>
	/// 衝突した時
	/// </summary>
	/// <param name="collision"></param>
    public static bool trigger=false;
	public static int addwater=50;





	void OnCollisionEnter(Collision collision)
	{
		// 衝突した相手にPlayerタグが付いているとき
		if (collision.gameObject.tag == "Player")
		{
			
            trigger = true;
			Water.water += addwater; 
			Destroy(gameObject);
		}
	}
}