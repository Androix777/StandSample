using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandManager : MonoBehaviour
{
    [System.Serializable]
    public class Model
    {
        public GameObject eventType;
        public string name;
        public string note;
    }
    public Model [] models;
    public GameObject canvas, element;
    private Transform content;
    private Text note;
    void Start()
    {
        content = canvas.transform.Find("LeftPanel").Find("Scroll").Find("Content");
        note = canvas.transform.Find("DownPanel").GetChild(0).GetComponent<Text>();
        foreach(Model model in models)
        {
            AddElement(model);
        }
    }

    void Update()
    {
        
    }
    private void AddElement(Model model)
    {
        Transform newElementTransform = Instantiate(element).transform;
        newElementTransform.SetParent(content, false);
        newElementTransform.GetChild(0).gameObject.GetComponent<Text>().text = model.name;
    }
}
