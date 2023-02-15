using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utilities : MonoBehaviour
{
    // Start is called before the first frame update
    public void hiddenObj(string objName)
    {
        GameObject tempobj = GameObject.Find(objName);
        tempobj.GetComponent<Renderer>().enabled = false;
        Debug.Log("object removed");

    }
}
