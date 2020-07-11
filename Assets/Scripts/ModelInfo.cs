using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelPart
{
    public GameObject modelPart;
    public string name;
    public string note;
}

public class ModelInfo : MonoBehaviour
{
    public string modelName;
    public string modelNote;
    public ModelPart [] modelParts;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
