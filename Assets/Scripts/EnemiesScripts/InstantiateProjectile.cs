using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateProjectile : MonoBehaviour
{
    public Transform Instantiator;
    public GameObject prefabLeft;
    public GameObject prefabRight;
    public Transform point;
    public float livingTime;

    virtual public void Instantiate()
    {
        if(Instantiator.localScale.x>0)
        {
            GameObject InstantiateObject = Instantiate(prefabLeft, point.position, Quaternion.identity);
            if (livingTime > 0f)
            {
                Destroy(InstantiateObject, livingTime);
            }
        }
        else if(Instantiator.localScale.x<0)
        {
            GameObject InstantiateObject = Instantiate(prefabRight, point.position, Quaternion.identity);
            if (livingTime > 0f)
            {
                Destroy(InstantiateObject, livingTime);
            }
        }

    }
}
