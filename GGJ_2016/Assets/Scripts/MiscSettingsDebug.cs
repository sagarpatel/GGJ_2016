using UnityEngine;
using System.Collections;

public class MiscSettingsDebug : MonoBehaviour
{

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel(Application.loadedLevel)
;
    }

}
