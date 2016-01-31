using UnityEngine;
using System.Collections;

public class PlayersManager : MonoBehaviour
{

    public GameObject m_playerPrefab;

    public Transform m_playerSpawnPos_1;
    public Transform m_playerSpawnPos_2;

    public Material m_playerMaterial_1;
    public Material m_playerMaterial_2;

    public Material m_playerMaterial_Dead_1;
    public Material m_playerMaterial_Dead_2;


    GameObject m_player_1;
    GameObject m_player_2;

    int m_roundCount = 0;
    bool m_isRespawnSequenceComplete = true;

    void Start()
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        m_player_1 = (GameObject)Instantiate(m_playerPrefab, m_playerSpawnPos_1.position, Quaternion.identity);
        m_player_1.GetComponent<PlayerMaterialSet>().m_aliveMaterial = m_playerMaterial_1;
        m_player_1.GetComponent<PlayerMaterialSet>().m_deadMaterial = m_playerMaterial_Dead_1;
        m_player_1.GetComponentInChildren<PlayerMove>().m_playerIndex = 0;
        m_player_1.name = "Player_1";


        m_player_2 = (GameObject)Instantiate(m_playerPrefab, m_playerSpawnPos_2.position, Quaternion.identity);
        m_player_2.GetComponent<PlayerMaterialSet>().m_aliveMaterial = m_playerMaterial_2;
        m_player_2.GetComponent<PlayerMaterialSet>().m_deadMaterial = m_playerMaterial_Dead_2;
        m_player_2.GetComponentInChildren<PlayerMove>().m_playerIndex = 1;
        m_player_2.name = "Player_2";

    }

    public void KillPlayer(Transform playerTransform)
    {
        if (m_isRespawnSequenceComplete == false)
            return;

        m_isRespawnSequenceComplete = false;
        StartCoroutine(KillRespawnSequence(playerTransform));
    }

    IEnumerator KillRespawnSequence(Transform firstKill)
    {
        firstKill.GetComponent<PlayerMaterialSet>().SetDeadMaterial();

        yield return new WaitForSeconds(0.25f);

        // TODO:  do slomo and cam shake here

        firstKill.GetComponent<PlayerExplode>().Explode();

        yield return new WaitForSeconds(1.0f);

        m_player_1.GetComponent<PlayerMaterialSet>().SetDeadMaterial();
        m_player_2.GetComponent<PlayerMaterialSet>().SetDeadMaterial();


        yield return new WaitForSeconds(0.25f);

        // kill both again because i can't be bothered wit id system
        m_player_1.GetComponent<PlayerExplode>().Explode();
        m_player_2.GetComponent<PlayerExplode>().Explode();

        yield return new WaitForSeconds(3.0f);

        SpawnPlayers();

        m_isRespawnSequenceComplete = true;
    }


}
