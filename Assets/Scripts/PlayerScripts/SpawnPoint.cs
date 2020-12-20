using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    private GameObject player;
    private Transform _spawnPoint;
    private PlayerController _playerController;
    public bool spawnFaceRight;
    
    private void Awake()
    {
        player = GameObject.Find("Player");
        _spawnPoint = gameObject.GetComponent<Transform>();
        player.transform.position = _spawnPoint.position;
        _playerController = player.GetComponent<PlayerController>();
    }

    public void spawnPlayer()
    {
        player = GameObject.Find("Player");
        _spawnPoint = gameObject.GetComponent<Transform>();
        player.transform.position = _spawnPoint.position;
        _playerController = player.GetComponent<PlayerController>();
    }
}
