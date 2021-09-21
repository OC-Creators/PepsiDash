using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class ButtonPush : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

	Animator _animator;

	Button _button;

	void Start()
	{
		_animator = GetComponent<Animator>();
		_button = GetComponent<Button>();
	}

	void Update()
	{

	}


	public void OnPointerEnter(PointerEventData eventData)
	{
		_animator.SetTrigger("Highlighted");
		//マウスカーソルが重なったらボタンが大きくなる
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_animator.SetTrigger("Normal");
		//マウスカーソルがボタンから離れたら通常の大きさになる
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_animator.SetTrigger("Pressed");
		//ボタンをクリックしたらボタンが沈む
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_animator.SetTrigger("Highlighted");
		//マウスボタンが離された時の処理
	}
}
