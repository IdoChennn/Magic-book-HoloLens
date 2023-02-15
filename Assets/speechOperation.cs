using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.Windows.Speech;
using System;
using Vuforia;

public class speechOperation : MonoBehaviour

{

    KeywordRecognizer keywordObj;
    public String[] keywords_arrey;
    private GameObject cube, veinContainer,vein;
    private Vector3 scaleChange;
    private Boolean changeSize;

    public int cubesPerAxis = 8;
    public float delay = 1f;
    public float force = 300f;
    public float radius = 2f;

    public void Awake()
    {
        cube = GameObject.Find("Cube");
        veinContainer = GameObject.Find("veinContainer");
        vein = GameObject.Find("realArtery");
        scaleChange = new Vector3(1f, 1f, 1f);
    }
    private void Start()
    {
        keywordObj = new KeywordRecognizer(keywords_arrey);
        keywordObj.OnPhraseRecognized += OnKeywordsRecognized;
        keywordObj.Start();

    }
    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)

    {
        switch(args.text){

            
            case "red":
                //Debug.Log("keyword" + args.text + "; Confidence " + args.confidence);
                
                updateMesh(vein, "red");
                break;

            case "blue":

                updateMesh(vein, "blue");
                break;

            case "green":
                updateMesh(vein, "green");
                break;

            case "red cube":
                updateMesh(cube, "red");
                break;
            

            case "blue cube":
                updateMesh(cube, "blue");
                break;

            case "green cube":
                updateMesh(cube, "green");
                break;

            case "bigger cube":
                changeSize = true;
                updateSize(changeSize, cube);
                break;

            case "smaller cube":
                changeSize = false;
                updateSize(changeSize, cube);
                break;

            case "bigger":
                changeSize = true;
                updateSize(changeSize, veinContainer);
                break;

            case "smaller":
                changeSize = false;
                updateSize(changeSize,veinContainer);
                break;
            case "hit":
                Debug.Log("keyword" + args.text + "; Confidence " + args.confidence);
                for (int x = 0; x < cubesPerAxis; x++)
                {
                    for (int y = 0; y < cubesPerAxis; y++)
                    {
                        for (int z = 0; z < cubesPerAxis; z++)
                        {
                            CreateCube(new Vector3(x, y, z));
                        }
                    }
                }
                Destroy(gameObject);

                break;

            default:

            Debug.Log("keyword" + args.text + "; Confidence " + args.confidence);
                break;
        }
    }
    void CreateCube(Vector3 coordinates)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        Renderer rd = cube.GetComponent<Renderer>();
        rd.material = GetComponent<Renderer>().material;

        cube.transform.localScale = transform.localScale / cubesPerAxis;

        Vector3 firstCube = transform.position - transform.localScale / 2 + cube.transform.localScale / 2;
        cube.transform.position = firstCube + Vector3.Scale(coordinates, cube.transform.localScale);

        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.AddExplosionForce(force, transform.position, radius);

    }

    public void updateSize(Boolean bigOrSmall, GameObject wichObject)
    {
        if (bigOrSmall == true)
        {
            wichObject.transform.localScale += scaleChange;
        }
        else
        {
            wichObject.transform.localScale -= scaleChange;
        }
    }

    public void updateMesh(GameObject wichObject, String color)
    {
        Material objectColor;
        if(color == "red")
        {
            objectColor = wichObject.GetComponent<Renderer>().material;
            objectColor.SetColor("_Color", Color.red);

        }
        else if(color == "blue")
        {
            objectColor = wichObject.GetComponent<Renderer>().material;
            objectColor.SetColor("_Color", Color.blue);
        }
        else if(color == "green")
        {
            objectColor = wichObject.GetComponent<Renderer>().material;
            objectColor.SetColor("_Color", Color.green);
        }
    }


}
    

