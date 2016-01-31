using UnityEngine;
using System.Collections;

public class AudioDirector : MonoBehaviour
{
    public AudioClip[] m_pingpongSFXArray;

    
    public AudioClip RandomPPSFX()
    {
        int randomIndex = Random.Range(0, m_pingpongSFXArray.Length);
        return m_pingpongSFXArray[randomIndex];
    }

}
