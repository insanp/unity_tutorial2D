using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float offset = 5f;
    [SerializeField] float offsetSmoothing;

    private Vector3 playerPosition;
    private PlayerController playerScript;
    

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x,
            transform.position.y, transform.position.z);

        if (playerScript.GetFacingRight())
        {
            playerPosition = new Vector3(playerPosition.x + offset,
                playerPosition.y, playerPosition.z);
        } else
        {
            playerPosition = new Vector3(playerPosition.x - offset,
                playerPosition.y, playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
