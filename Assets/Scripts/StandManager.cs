using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandManager : MonoBehaviour
{
    public float rotationSpeed = 2;
    public GameObject [] models;
    public GameObject canvas, element, currentModel;
    private Transform leftContent, rightContent, animationButtonTransform;
    private Button animationButton;
    private Text note, animationName;
    private ModelInfo[] modelInfo;
    void Start()
    {
        modelInfo = new ModelInfo [models.Length];
        leftContent = canvas.transform.Find("LeftPanel").Find("Scroll").Find("Content");
        rightContent = canvas.transform.Find("RightPanel").Find("Scroll").Find("Content");
        animationButtonTransform = canvas.transform.Find("AnimationButton");
        note = canvas.transform.Find("DownPanel").GetChild(0).GetComponent<Text>();
        animationName = animationButtonTransform.GetChild(0).GetComponent<Text>();
        animationButton = animationButtonTransform.gameObject.GetComponent<Button>();
        animationButton.onClick.AddListener(delegate{PlayAnimation();});
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
        newElementTransform.SetParent(leftContent, false);
        newElementTransform.GetChild(0).gameObject.GetComponent<Text>().text = modelInfo[i].modelName;
        newElementTransform.GetComponent<Button>().onClick.AddListener(delegate{ShowModel(i);});
    }

    private void ShowModel(int i)
    {
        Destroy(currentModel);
        currentModel = Instantiate(models[i]);
        note.text = modelInfo[i].modelNote;

        if(modelInfo[i].modelAnimator)
        {
            animationButton.interactable = true;
            animationName.text = modelInfo[i].animationName;
        }

        foreach (Transform child in rightContent)
        {
            Destroy(child.gameObject);
        }

        int j = 0;
        foreach (ModelPart part in modelInfo[i].modelParts)
        {
            Transform newElementTransform = Instantiate(element).transform;
            newElementTransform.SetParent(rightContent, false);
            newElementTransform.GetChild(0).gameObject.GetComponent<Text>().text = part.name;
            int j2 = j;
            newElementTransform.GetComponent<Button>().onClick.AddListener(delegate{ShowPart(i, j2);});
            j++;
        }
    }

    private void ShowPart(int i, int j)
    {
        animationButton.interactable = false;
        animationName.text = "";
        foreach(Transform child in currentModel.transform)
        {
            if (child.gameObject.name == modelInfo[i].modelParts[j].modelPart.name)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
        note.text = modelInfo[i].modelParts[j].note;
    }

    private void PlayAnimation()
    {
        Animator animator = currentModel.GetComponent<ModelInfo>().modelAnimator;
        Debug.Log(animator.enabled);
        animator.enabled = !animator.enabled;
        Debug.Log(animator.enabled);
    }
}
