using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapFrameRate : MonoBehaviour
{
    public int FrameRate;
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = FrameRate;
    }
}
