using UnityEngine;
using System.Collections;

public class PlayerBall : MonoBehaviour
{
    PlayersManager m_playersManager;
    AudioSource m_audioSource;
    AudioDirector m_audioDirector;

    void Start()
    {
        m_playersManager = FindObjectOfType<PlayersManager>();
        m_audioSource = GetComponent<AudioSource>();
        m_audioDirector = FindObjectOfType<AudioDirector>();
    }

    void OnCollisionEnter(Collision collision)
    {
        m_audioSource.PlayOneShot(m_audioDirector.RandomPPSFX());

        if (collision.transform.CompareTag("Floor"))
        {            
            m_playersManager.KillPlayer(transform.root);
        }
            
    }

}
