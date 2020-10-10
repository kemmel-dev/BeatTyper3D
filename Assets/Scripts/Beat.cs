using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{
    public void Hit()
    {
        Destroy(this.gameObject);
    }

    public void Miss()
    {
        Destroy(this.gameObject);
    }


}
