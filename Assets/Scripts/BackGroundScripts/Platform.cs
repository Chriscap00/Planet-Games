using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float FallTimer = 0.05f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float Verticalinput = Input.GetAxisRaw("Vertical");
        if (Verticalinput < 0)
        {
            StartCoroutine(fallTimer());
        }
    }
    IEnumerator fallTimer()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(FallTimer);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
