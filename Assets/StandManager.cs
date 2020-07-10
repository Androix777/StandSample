using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandManager : MonoBehaviour
{
    [System.Serializable]
    public class Model
    {
        public GameObject model;
        public string name;
        public string note;
    }
    public Model [] models;
    public GameObject canvas, element, currentModel;
    private Transform content;
    private Text note;
    void Start()
    {
        int i = 0;
        content = canvas.transform.Find("LeftPanel").Find("Scroll").Find("Content");
        note = canvas.transform.Find("DownPanel").GetChild(0).GetComponent<Text>();
        foreach(Model model in models)
        {
            AddElement(model, i);
            i++;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            currentModel.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
        }
    }
    private void AddElement(Model model, int i)
    {
        Transform newElementTransform = Instantiate(element).transform;
        newElementTransform.SetParent(content, false);
        newElementTransform.GetChild(0).gameObject.GetComponent<Text>().text = model.name;
        newElementTransform.GetComponent<Button>().onClick.AddListener(delegate{ShowModel(i);});
    }

    private void ShowModel(int i)
    {
        note.text = models[i].note;
        Destroy(currentModel);
        currentModel = Instantiate(models[i].model);
    }
}
