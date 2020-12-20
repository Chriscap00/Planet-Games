using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpPlayerSceneManager : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerHealth>().onCharge();
    }

    public void setSceneToReply()
    {
        player.SetActive(true);
    }
}
