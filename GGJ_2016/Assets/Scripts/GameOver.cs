using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOver : MonoBehaviour
{
    Renderer[] m_renderersArray;
    List<Rigidbody> m_rbList;
    List<Vector3> m_oPosList;
    List<Quaternion> m_oRotList;
    public AnimationCurve m_inwardCurve;

    public void ExplodeWorld()
    {
        StartCoroutine(WorldExplodeSqeunce());

    }


    IEnumerator WorldExplodeSqeunce()
    {
        m_renderersArray = FindObjectsOfType<Renderer>();
        m_rbList = new List<Rigidbody>();
        m_oPosList = new List<Vector3>();
        m_oRotList = new List<Quaternion>();
        for (int i = 0; i < m_renderersArray.Length; i++)
        {
            Rigidbody rb = m_renderersArray[i].GetComponent<Rigidbody>();
            if (rb == null)
                rb = m_renderersArray[i].gameObject.AddComponent<Rigidbody>();

            m_rbList.Add(rb);
            m_oPosList.Add(rb.position);
            m_oRotList.Add(rb.rotation);
            rb.transform.parent = null;
            m_rbList[i].isKinematic = true;
        }

        // animate inwards

        yield return StartCoroutine(AnimateInwards());

        for (int i = 0; i < m_renderersArray.Length; i++)
        {
            m_rbList[i].mass = 0.1f;
            m_rbList[i].isKinematic = false;
            m_rbList[i].useGravity = false;
            m_rbList[i].drag = 0.85f;
            m_rbList[i].angularDrag = 0.7f;
            m_rbList[i].AddExplosionForce(2000.0f, new Vector3(2,0,0), 100.0f);            
        }

    }

    IEnumerator AnimateInwards()
    {
        float duration = 1.50f;
        float timeCounter = 0;

        while(timeCounter < duration)
        {
            float progress = timeCounter / duration;
            float step = 0.5f * m_inwardCurve.Evaluate(progress);

            for(int i = 0; i < m_rbList.Count; i++)
            {
                m_rbList[i].position = Vector3.Lerp(m_oPosList[i], Vector3.zero, step);
                m_rbList[i].rotation = Quaternion.Lerp(m_oRotList[i], Quaternion.identity, step);
            }

            timeCounter += Time.deltaTime;
            yield return null;
        }


    }


}
