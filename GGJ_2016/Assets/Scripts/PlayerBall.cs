using UnityEngine;
using System.Collections;

public class PlayerBall : MonoBehaviour
{
    PlayersManager m_playersManager;

    void Start()
    {
        m_playersManager = FindObjectOfType<PlayersManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {            
            m_playersManager.KillPlayer(transform.root);
        }
            
    }

}
