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

    void Awake()
    {
        m_collidersArray = GetComponentsInChildren<BoxCollider>();
        m_hingeJoint = GetComponentInChildren<HingeJoint>();      
    }
    

    public void Explode()
    {
        if (m_hasExploded == true)
            return;

        StartCoroutine(ExplosionSequence());
    }

    IEnumerator ExplosionSequence()
    {
        yield return new WaitForSeconds(0.10f);

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
            rb.AddExplosionForce(m_explosionForce, m_explosionSource.position, m_explosionRadius);
        }
        
        m_hasExploded = true;


    }


}
