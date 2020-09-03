using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryManager
{
	List<StoryNode> m_story_list;
	public void Init()
	{
		JSONObject stroy_json = Singleton<DataManager>.Instance.m_data["Story"];
		for(int i = 0, imax = stroy_json.Count; i < imax; ++i)
		{
			
		}
	}

	StoryNode ParseStroyNode(JSONObject story_json)
	{		
		StoryNode stroy_node = new StoryNode();
		stroy_node.m_node_type = (int)story_json["type"].i;
		return stroy_node;
	}
}
