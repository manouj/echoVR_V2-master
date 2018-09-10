using UnityEngine;
using System.Collections;

public class Scannable : MonoBehaviour
{

    public Material redMat;
 

    public void Start()
    {
        
    }

    public void Ping()
    {
        Invoke("m1", 2f);
    }

   void m1()
    {
        GetComponent<Renderer>().material = redMat;
    }

}
