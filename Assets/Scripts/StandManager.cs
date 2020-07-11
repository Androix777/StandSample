using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandManager : MonoBehaviour
{
    public float rotationSpeed = 2;
    public GameObject [] models;
    public GameObject canvas, element, currentModel;
    private Transform content;
    private Text note;
    private ModelInfo[] modelInfo;
    void Start()
    {
        modelInfo = new ModelInfo [models.Length];
        content = canvas.transform.Find("LeftPanel").Find("Scroll").Find("Content");
        note = canvas.transform.Find("DownPanel").GetChild(0).GetComponent<Text>();
        int i = 0;
        foreach(GameObject model in models)
        {
            AddElement(model, i);
            i++;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            currentModel.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed, 0));
        }
    }

    private void AddElement(GameObject model, int i)
    {
        modelInfo[i] = model.transform.GetComponent<ModelInfo>();
        Transform newElementTransform = Instantiate(element).transform;
        newElementTransform.SetParent(content, false);
        newElementTransform.GetChild(0).gameObject.GetComponent<Text>().text = modelInfo[i].modelName;
        newElementTransform.GetComponent<Button>().onClick.AddListener(delegate{ShowModel(i);});
    }

    private void ShowModel(int i)
    {
        note.text = modelInfo[i].modelNote;
        Destroy(currentModel);
        currentModel = Instantiate(models[i]);
    }
}
