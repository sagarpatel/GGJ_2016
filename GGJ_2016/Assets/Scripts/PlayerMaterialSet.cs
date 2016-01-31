using UnityEngine;
using System.Collections;

public class PlayerMaterialSet : MonoBehaviour
{
    public Material m_playerMaterial;

    void Start()
    {
        Renderer[] renderersArray = GetComponentsInChildren<Renderer>();
        for(int i = 0; i < renderersArray.Length; i++)
        {
            renderersArray[i].material = m_playerMaterial;
        }
    }


}
