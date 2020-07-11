using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInfo : MonoBehaviour
{
    [System.Serializable]
    public class ModelPart
    {
        public GameObject modelPart;
        public string name;
        public string note;
    }
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
