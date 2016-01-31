using UnityEngine;
using System.Collections;

public class AudioDirector : MonoBehaviour
{
    public AudioClip[] m_pingpongSFXArray;
    public AudioClip m_pop1;
    
    void Start()
    {
        FindObjectOfType<Camera>().GetComponent<AudioSource>().PlayDelayed(0.710f);
    }  
    
    public AudioClip RandomPPSFX()
    {
        int randomIndex = Random.Range(0, m_pingpongSFXArray.Length);
        return m_pingpongSFXArray[randomIndex];
    }

    public AudioClip ExplosionSFX()
    {
        return m_pop1;
    }

}
