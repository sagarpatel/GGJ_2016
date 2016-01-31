using UnityEngine;
using System.Collections;

public class PlayerExplode : MonoBehaviour
{
    BoxCollider[] m_collidersArray;
    HingeJoint m_hingeJoint;
    
    public Transform m_explosionSource;
    float m_explosionForce = 4200.0f;
    float m_explosionRadius = 10.0f;

    bool m_hasExploded = false;

    GameObject m_playerBall;

    AudioSource m_audioSource;
    AudioDirector m_audioDirector;

    void Awake()
    {
        m_collidersArray = GetComponentsInChildren<BoxCollider>();
        m_hingeJoint = GetComponentInChildren<HingeJoint>();
        m_playerBall = GetComponentInChildren<PlayerBall>().gameObject;

        m_audioSource = GetComponentInChildren<AudioSource>();
        m_audioDirector = FindObjectOfType<AudioDirector>();
    }
    

    public void Explode()
    {
        if (m_hasExploded == true)
            return;

        StartCoroutine(ExplosionSequence());
    }

    IEnumerator ExplosionSequence()
    {
        gameObject.tag = "DeadPlayer";

        Destroy(m_playerBall);

        yield return new WaitForSeconds(0.010f);

        m_audioSource.pitch = Random.Range(0.5f, 1.5f);
        m_audioSource.PlayOneShot(m_audioDirector.ExplosionSFX());

        Destroy(m_hingeJoint);
        
        for (int i = 0; i < m_collidersArray.Length; i++)
        {            
            Rigidbody rb = m_collidersArray[i].GetComponent<Rigidbody>();
            if (rb == null)
            {
                m_collidersArray[i].gameObject.AddComponent<Rigidbody>();
                rb = m_collidersArray[i].GetComponent<Rigidbody>();
                rb.mass = 0.1f;
            }
            rb.mass = 0.10f;
            rb.transform.parent = null;
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.drag = 0.2f;
            rb.angularDrag = 0.1f;
            rb.AddExplosionForce(m_explosionForce, m_explosionSource.position, m_explosionRadius);
        }
        
        m_hasExploded = true;


    }


}
