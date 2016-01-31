using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
    Renderer[] m_renderersArray;


    public void WorldExplode()
    {
        m_renderersArray = FindObjectsOfType<Renderer>();
        for (int i = 0; i < m_renderersArray.Length; i++)
        {
            Rigidbody rb = m_renderersArray[i].GetComponent<Rigidbody>();
            if (rb == null)
                rb = m_renderersArray[i].gameObject.AddComponent<Rigidbody>();

            rb.transform.parent = null;

            rb.mass = 0.1f;
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.drag = 0.5f;
            rb.angularDrag = 0.75f;
            rb.AddExplosionForce(1000.0f, new Vector3(), 100.0f);            
        }

    }


}
