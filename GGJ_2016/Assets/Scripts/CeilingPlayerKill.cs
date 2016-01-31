using UnityEngine;
using System.Collections;

public class CeilingPlayerKill : MonoBehaviour
{
    PlayersManager m_playersManager;

    void Start()
    {
        m_playersManager = FindObjectOfType<PlayersManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if( collision.transform.root.CompareTag("Player") && !collision.transform.CompareTag("PlayerBall"))
        {
            m_playersManager.KillPlayer(collision.transform.root);
        }

    }

}
