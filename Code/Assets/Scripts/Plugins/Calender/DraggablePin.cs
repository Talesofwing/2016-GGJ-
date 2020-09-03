using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggablePin : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
	public Vector3 m_origion_pos;
	public Vector3 m_origion_rot;
	public int idx;
	void Awake()
	{
		m_origion_pos = transform.localPosition;
		m_origion_rot = transform.localEulerAngles;
	}

	public void OnDrag(PointerEventData eventData)
	{
		GetComponent<RectTransform>().pivot.Set(0, 0);
		transform.position = Input.mousePosition;
		Messenger.Broadcast<Transform>("calender_pin_drag", transform);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		transform.localScale = new Vector3(1f, 1f, 1f);
		Messenger.Broadcast<Transform>("calender_pin_down", transform);
	}

	public void ReturnOrigion()
	{
		transform.localPosition = m_origion_pos;
	}
	public void ReturnOrigionRotation()
	{
		transform.localEulerAngles = m_origion_rot;
	}
}