using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public GameObject[] objectsToMantain;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject count in objectsToMantain)
        {
            DontDestroyOnLoad(count);
        }
    }
}
