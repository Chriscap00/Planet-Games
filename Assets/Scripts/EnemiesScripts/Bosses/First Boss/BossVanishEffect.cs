using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVanishEffect : MonoBehaviour
{
    public GameObject boss;
    private Firstbossbehavior bossBehavior;
    private Animator _animator;
    // Update is called once per frame
    private void Awake()
    {
        bossBehavior = boss.GetComponent<Firstbossbehavior>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
    }
}
