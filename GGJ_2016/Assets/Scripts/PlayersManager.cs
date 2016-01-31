using UnityEngine;
using System.Collections;

public class PlayersManager : MonoBehaviour
{

    public GameObject m_playerPrefab;

    public Transform m_playerSpawnPos_1;
    public Transform m_playerSpawnPos_2;

    public Material m_playerMaterial_1;
    public Material m_playerMaterial_2;

    GameObject m_player_1;
    GameObject m_player_2;

    int m_roundCount = 0;

    void Start()
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        m_player_1 = (GameObject)Instantiate(m_playerPrefab, m_playerSpawnPos_1.position, Quaternion.identity);
        m_player_1.GetComponent<PlayerMaterialSet>().m_playerMaterial = m_playerMaterial_1;
        m_player_1.GetComponentInChildren<PlayerMove>().m_playerIndex = 0;
        m_player_1.name = "Player_1";


        m_player_2 = (GameObject)Instantiate(m_playerPrefab, m_playerSpawnPos_2.position, Quaternion.identity);
        m_player_2.GetComponent<PlayerMaterialSet>().m_playerMaterial = m_playerMaterial_2;
        m_player_2.GetComponentInChildren<PlayerMove>().m_playerIndex = 1;
        m_player_2.name = "Player_2";

    }

    public void KillPlayer(Transform playerTransform)
    {
        playerTransform.GetComponent<PlayerExplode>().Explode();

    }
    
}
