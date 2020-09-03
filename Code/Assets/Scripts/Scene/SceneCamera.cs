using UnityEngine;
using System.Collections;

public class SceneCamera : MonoBehaviour {

    public Transform m_target_trans;
    void FixedUpdate()
    {
        if (m_target_trans != null)
        {
            transform.position = new Vector3(m_target_trans.position.x, m_target_trans.position.y, transform.position.z);
        }
    }
}
