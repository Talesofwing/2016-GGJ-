using UnityEngine;
using System.Collections;

public class ReActor : MonoBehaviour {
	const string REACT = "ReAct";
	void Awake()
	{
		Messenger.AddListener("input_z", ReAct);
	}
	public virtual void ReAct()
	{
//		Debug.LogError("React");
		//DialogManager.Instance.ShowDialog(REACT);
		//Singleton<DialogManager>.Instance.ShowDialog(REACT);
	}
}
