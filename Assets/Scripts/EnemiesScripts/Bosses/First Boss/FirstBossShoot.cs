using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossShoot : InstantiateProjectile
{
    public GameObject prefabTopLeft;
    public GameObject prefabTopRight;
    private Firstbossbehavior bossBehavior;

    private void Awake()
    {
        bossBehavior = GetComponent<Firstbossbehavior>();
    }
    public override void Instantiate()
    {
        if (Instantiator.localScale.x > 0&& bossBehavior.secondPhase==false)
        {
            GameObject InstantiateObject = Instantiate(prefabLeft, point.position, Quaternion.identity);
            if (livingTime > 0f)
            {
                Destroy(InstantiateObject, livingTime);
            }
        }

        if (Instantiator.localScale.x < 0&& bossBehavior.secondPhase == false)
        {
            GameObject InstantiateObject = Instantiate(prefabRight, point.position, Quaternion.identity);
            if (livingTime > 0f)
            {
                Destroy(InstantiateObject, livingTime);
            }
        }

        if (Instantiator.localScale.x > 0 && bossBehavior.secondPhase == true)
        {
            GameObject InstantiateObject = Instantiate(prefabTopLeft, point.position, Quaternion.Euler(0, 0, -35.416f));
            if (livingTime > 0f)
            {
                Destroy(InstantiateObject, livingTime);
            }
        }

        if (Instantiator.localScale.x < 0 && bossBehavior.secondPhase == true)
        {
            GameObject InstantiateObject = Instantiate(prefabTopRight, point.position, Quaternion.Euler(0, 0, 35.416f));
            if (livingTime > 0f)
            {
                Destroy(InstantiateObject, livingTime);
            }
        }
    }
}
