using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAnimCont : MonoBehaviour
{
    public float timeToWait;
    private Animator _animator;
    private float _maxTimeToWait;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        timeToWait = Random.Range(1, 2);
        _maxTimeToWait = timeToWait;

    }
    // Update is called once per frame
    void Update()
    {
        if(timeToWait<0)
        {
            _animator.SetTrigger("grassAnim");
            timeToWait = Random.Range(1, 1.8f);
        }
        else
        {
            timeToWait -= Time.deltaTime;
        }
    }
}
