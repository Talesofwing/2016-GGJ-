using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum NODE_TYPE
{
	DIALOG,
	SCENE,
	ACTION,
	COUNT
}

public enum CONDITION_TYPE
{
	NODE_COLLECTED,
	COUNT
}

public class StoryNode
{
	public int m_id;
	public int m_node_type;
	public int m_action;
	public string m_name;
	public List<Dictionary<int, int>> m_conditions_list;
	public List<int> m_next_nodes;
}
