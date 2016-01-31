using UnityEngine;
using System.Collections;

public class PlayerMaterialSet : MonoBehaviour
{
    public Material m_aliveMaterial;
    public Material m_deadMaterial;

    void Start()
    {
        SetPlayerMaterial(m_aliveMaterial);
    }

    public void SetDeadMaterial()
    {
        SetPlayerMaterial(m_deadMaterial);
    }

    void SetPlayerMaterial(Material playerMaterial)
    {
        Renderer[] renderersArray = GetComponentsInChildren<Renderer>();
        for(int i = 0; i < renderersArray.Length; i++)
        {
            renderersArray[i].sharedMaterial = playerMaterial;
        }

        Light ballLight = GetComponentInChildren<Light>();
        if(ballLight != null)
            ballLight.color = playerMaterial.color;
    }



}
