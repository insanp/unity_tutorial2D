using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] Sprite redFlag;
    [SerializeField] Sprite greenFlag;

    private SpriteRenderer checkpointSpriteRenderer;
    public bool checkpointReached;

    // Start is called before the first frame update
    void Start()
    {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitBox" && !checkpointReached)
        {
            // do something
            checkpointSpriteRenderer.sprite = greenFlag;
            checkpointReached = true;
        }
    }

}
