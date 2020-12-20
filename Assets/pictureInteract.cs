using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class pictureInteract : MonoBehaviour
{
    public NPCConversation myConversation;
    public bool interactDialogueBox;
    public bool destroyAtFirstEncounter;
    private GameObject _player;
    private float _verticalAxis;

    private void Awake()
    {
        _player = GameObject.Find("Player");
    }
    private void Update()
    {
        _verticalAxis = Input.GetAxisRaw("Vertical");
    }
    private void setInmortalInDialogue(GameObject collision)
    {
        PlayerHealth _playerHealth;
        _playerHealth = collision.GetComponent<PlayerHealth>();
        _playerHealth.setInmortalModeTrue();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && interactDialogueBox == false)
        {
            setInmortalInDialogue(_player);
            ConversationManager.Instance.StartConversation(myConversation);
            if (destroyAtFirstEncounter)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _verticalAxis > 0 && interactDialogueBox)
        {
            setInmortalInDialogue(_player);
            ConversationManager.Instance.StartConversation(myConversation);
            if (destroyAtFirstEncounter)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
