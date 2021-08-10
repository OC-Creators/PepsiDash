using UnityEngine;
using UnityEngine.UI;
 
public class GaugeSpeed : MonoBehaviour
{
    [SerializeField] Image gauge;
    [SerializeField] Text timeText;
    [SerializeField] Text gaugeText;
 
    float second; // 秒数
 
    // Start is called before the first frame update
    void Start()
    {
        gauge.fillAmount = 0f;
        timeText.text = "";
        gaugeText.text = "";
    }
 
    // Update is called once per frame
    void Update()
    {
        if (second <= 8)
        {
 
            // ゲージを毎秒0.125増やす
            gauge.fillAmount += 0.125f * Time.deltaTime;
 
            // ゲージ量を表示
            gaugeText.text = gauge.fillAmount.ToString();
 
            // 秒数をカウント
            second += Time.deltaTime;
 
            // 秒数を表示
            timeText.text = second + "秒";
 
        }
    }
}