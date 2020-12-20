using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemiesScript : MonoBehaviour
{
    public GameObject[] enemiesToActivateInThisTrigger;
    public GameObject[] enemiesToDeactivateInThisTrigger;
    private int iteratNum = 0;
    private void OnDisable()
    {
        iteratNum = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject count in enemiesToActivateInThisTrigger)
            {
                iteratNum = iteratNum + 1;
                count.SetActive(true);
            }

            if (enemiesToDeactivateInThisTrigger.Length <= 0)
            {
                gameObject.SetActive(false);
                return;
            }

            iteratNum = 0;

            foreach (GameObject count in enemiesToDeactivateInThisTrigger)
            {
                iteratNum = iteratNum + 1;
                count.SetActive(false);

                if (iteratNum >= (enemiesToDeactivateInThisTrigger.Length))
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
