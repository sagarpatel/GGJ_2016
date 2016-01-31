using UnityEngine;
using System.Collections;

public class PlayersManager : MonoBehaviour
{

    public void KillPlayer(Transform playerTransform)
    {
        playerTransform.GetComponent<PlayerExplode>().Explode();

    }

}
