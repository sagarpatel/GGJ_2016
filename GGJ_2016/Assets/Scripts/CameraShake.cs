using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    Vector3 m_originalPosition;
    Quaternion m_originalRotation;

    void Start()
    {
        m_originalPosition = transform.position;
        m_originalRotation = transform.rotation;
    }

    public void LaunchCameraShake(float duration, float amplitude)
    {
        StartCoroutine(ShakeCamera(duration, amplitude));
    }

    IEnumerator ShakeCamera(float duration, float amplitude)
    {
        float timeCounter = 0;

        while(timeCounter < duration)
        {
            Vector3 offsetPos = Random.Range(-amplitude, amplitude) * transform.right + Random.Range(-amplitude, amplitude) * transform.up;
            transform.position = m_originalPosition + offsetPos;

            timeCounter += Time.deltaTime;
            yield return null;
        }


        transform.position = m_originalPosition;
        transform.rotation = m_originalRotation;

    }


}
