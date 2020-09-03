using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogWindow : WindowBase {

	public Transform content_tran = null;
	public Transform name_tran = null;
	public Text content_text = null;
	public Text name_text = null;
	// Use this for initialization
	void Start () {		
		Messenger.AddListener<DialogNode>("dialog_show", ShowDialog);
		Messenger.AddListener("dialog_hide", HideDialog);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickBox()
	{
		Messenger.Broadcast("dialog_next");
	}
	public void ShowDialog(string name, string content)
	{
		Active(true);
		Text name_text = name_tran.GetComponent<Text>();
		if (name_text != null)
		{
			name_text.text = name;
		}		
		Text content_text = content_tran.GetComponent<Text>();
		if (content_text != null)
		{
			content_text.text = content;
		}
		
	}
	public void ShowDialog(DialogNode dialog)
	{
		Active(true);

		if(!string.IsNullOrEmpty(dialog.m_name))
		{
			name_tran.gameObject.SetActive(true);
			name_text.text = dialog.m_name;
		}
		else
		{
			name_tran.gameObject.SetActive(false);

		}
		content_text.text = dialog.m_content;
	}
	public void ShowChoose(string content)
	{

	}

	public void HideDialog()
	{
		Active(false);
	}
	public void EndDialog()
	{
		Active(false);
	}
}
