using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.IO;
using UnityEngine;
using Input = UnityEngine.Input;

public class MovableObject : MonoBehaviour
{
    const string FILE_DIR = "/SAVE_DATA/";

    string FILE_NAME = "<name>.json";
    //carets mean itll be replaced by something else

    string FILE_PATH;

    // Start is called before the first frame update
    void Start()
    {

        FILE_NAME = FILE_NAME.Replace("<name>", name);
        //replaces selected string in file name with specified variable (in this case, name of object)

        FILE_PATH = Application.dataPath + FILE_DIR + FILE_NAME;
        //have to do this once the program starts so unity knows where the thing is

        if (File.Exists(FILE_PATH))
            //if file already exists
        {
            string jsonStr = File.ReadAllText(FILE_PATH);
            //have to use json string since this is a json file - define string as jsonStr

            JsonUtility.FromJson<Vector3>(jsonStr);
            //this specifies what you want to grab from the json file - take string, turn into vector3

            transform.position = JsonUtility.FromJson<Vector3>(jsonStr);
            //set positions of cubes to the values pulled from json

        }
    }
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition();

    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 result = Input.mousePosition;

        result.z = Camera.main.WorldToScreenPoint(transform.position).z;
        //wtsp: where is this world spot on the screen
        //prevents depth changes

        result = Camera.main.ScreenToWorldPoint(result);
        //wtsp: where is this screen spot in the world

        return result;

    }
        
    void OnApplicationQuit()
    {
        string fileContent = JsonUtility.ToJson(transform.position, true);
        //json utility command specifically for .json files

        Debug.Log(fileContent);

        File.WriteAllText(FILE_PATH, fileContent);
    }
}




