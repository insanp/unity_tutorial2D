using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float respawnDelay = 2f;
    [SerializeField] Text coinsText;

    public PlayerController gamePlayer;
    public int coins = 0;

    // Start is called before the first frame update
    void Start()
    {
        gamePlayer = FindObjectOfType<PlayerController>();    
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "Coins : " + coins.ToString();
    }

    public void AddCoins(int numOfCoins)
    {
        Debug.Log(numOfCoins);
        coins += numOfCoins;
    }

    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }

    public IEnumerator RespawnCoroutine()
    {
        gamePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);
        gamePlayer.transform.position = gamePlayer.respawnPoint;
        gamePlayer.gameObject.SetActive(true);
    }
}
