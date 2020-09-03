using UnityEngine;
using System.Collections;

public class titleWindow : WindowBase
{

	public void GameStart()
	{
		Active(false);
		Messenger.Broadcast<int>("scene_set", (int)SCENE_TYPE.START);
		//Messenger.Broadcast("scene_set");
	}
}