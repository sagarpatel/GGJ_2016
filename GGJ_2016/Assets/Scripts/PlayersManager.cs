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

    int m_maxLives = 5;

    public LifeBarManager m_lifeBarManager_1;
    public LifeBarManager m_lifeBarManager_2;

    GameOver m_gameOver;

    void Start()
    {
        SpawnPlayers();

        m_lifeBarManager_1.GenerateLifeBar(m_maxLives, m_playerMaterial_1);
        m_lifeBarManager_2.GenerateLifeBar(m_maxLives, m_playerMaterial_2);
        m_gameOver = FindObjectOfType<GameOver>();
    }

    void SpawnPlayers()
    {
        m_player_1 = (GameObject)Instantiate(m_playerPrefab, m_playerSpawnPos_1.position, Quaternion.identity);
        m_player_1.GetComponent<PlayerMaterialSet>().m_aliveMaterial = m_playerMaterial_1;
        m_player_1.GetComponent<PlayerMaterialSet>().m_deadMaterial = m_playerMaterial_Dead_1;
        m_player_1.GetComponentInChildren<PlayerMove>().m_playerIndex = 0;
        m_player_1.GetComponent<PlayerData>().m_playerIndex = 0;
        m_player_1.name = "Player_1";


        m_player_2 = (GameObject)Instantiate(m_playerPrefab, m_playerSpawnPos_2.position, Quaternion.identity);
        m_player_2.GetComponent<PlayerMaterialSet>().m_aliveMaterial = m_playerMaterial_2;
        m_player_2.GetComponent<PlayerMaterialSet>().m_deadMaterial = m_playerMaterial_Dead_2;
        m_player_2.GetComponentInChildren<PlayerMove>().m_playerIndex = 1;
        m_player_2.GetComponent<PlayerData>().m_playerIndex = 1;
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
        int pIndex = firstKill.GetComponent<PlayerData>().m_playerIndex;
        int remainingLife = 10000;
        if (pIndex == 0)
            remainingLife = m_lifeBarManager_1.DecrementLife();
        else if (pIndex == 1)
            remainingLife = m_lifeBarManager_2.DecrementLife();

        if(remainingLife == 0)
        {
            StartCoroutine(GameOverSequence());
            yield break;                
        }

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

    IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(3.0f);

        m_gameOver.WorldExplode();

        yield return new WaitForSeconds(5.0f);

        Application.LoadLevel(Application.loadedLevel);

    }


}
