using UnityEngine;
using System.Collections;

public class LifeBarManager : MonoBehaviour
{
    GameObject[] m_livesArray;
    int m_currentLives = 0;

    public void GenerateLifeBar(int lifeCount, Material barMaterial)
    {
        m_livesArray = new GameObject[lifeCount];
        for(int i = 0; i < m_livesArray.Length; i++)
        {
            m_livesArray[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            m_livesArray[i].transform.parent = transform;
            m_livesArray[i].transform.localPosition = new Vector3(0, 0, i * 1.5f);
            m_livesArray[i].GetComponent<MeshRenderer>().material = barMaterial;
            m_livesArray[i].layer = gameObject.layer;
        }

        m_currentLives = lifeCount;
    }

    public int DecrementLife()
    {
        m_currentLives -= 1;

        m_livesArray[m_currentLives].SetActive(false);

        return m_currentLives;
    }


}
