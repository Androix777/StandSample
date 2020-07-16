using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandManager : MonoBehaviour
{
    public float rotationSpeed = 2;
    public GameObject [] models;
    public GameObject canvas, buttonElement, currentModel;
    private Transform leftContentTransform, rightContentTransform, animationButtonTransform;
    private Button animationButton;
    private Text note, animationName;
    private ModelInfo[] modelInfo;
    void Start()
    {
        //Кэширование
        modelInfo = new ModelInfo [models.Length];
        leftContentTransform = canvas.transform.Find("LeftPanel").Find("Scroll").Find("Content");
        rightContentTransform = canvas.transform.Find("RightPanel").Find("Scroll").Find("Content");
        animationButtonTransform = canvas.transform.Find("AnimationButton");
        note = canvas.transform.Find("DownPanel").GetChild(0).GetComponent<Text>();
        animationName = animationButtonTransform.GetChild(0).GetComponent<Text>();
        animationButton = animationButtonTransform.gameObject.GetComponent<Button>();
        animationButton.onClick.AddListener(delegate{PlayAnimation();});
        int i = 0;

        //Добавление кнопок моделей
        foreach(GameObject model in models)
        {
            AddButtonElement(model, i);
            i++;
        }
    }

    void Update()
    {
        //Вращение модели
        if (Input.GetMouseButton(0)) 
        {
            currentModel.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed, 0));
        }
    }

    //Добавить кнопку модели
    private void AddButtonElement(GameObject model, int i)
    {
        modelInfo[i] = model.transform.GetComponent<ModelInfo>();
        Transform newElementTransform = Instantiate(buttonElement).transform;
        newElementTransform.SetParent(leftContentTransform, false);
        newElementTransform.GetChild(0).gameObject.GetComponent<Text>().text = modelInfo[i].modelName;
        newElementTransform.GetComponent<Button>().onClick.AddListener(delegate{ShowModel(i);});
    }

    //Отобразить модель
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

        //Добавить кнопки показа частей модели
        foreach (Transform child in rightContentTransform)
        {
            Destroy(child.gameObject);
        }
        int j = 0;
        foreach (ModelPart part in modelInfo[i].modelParts)
        {
            Transform newElementTransform = Instantiate(buttonElement).transform;
            newElementTransform.SetParent(rightContentTransform, false);
            newElementTransform.GetChild(0).gameObject.GetComponent<Text>().text = part.name;
            int j2 = j;
            newElementTransform.GetComponent<Button>().onClick.AddListener(delegate{ShowPart(i, j2);});
            j++;
        }
    }

    //Отобразить часть модели
    private void ShowPart(int i, int j)
    {
        animationButton.interactable = false;
        PlayAnimation(true);
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

    //Включить, выключить анимцию
    private void PlayAnimation(bool off = false)
    {
        Animator animator = currentModel.GetComponent<ModelInfo>().modelAnimator;
        if(off)
        {
            animator.enabled = false;
        }
        else
        {
            animator.enabled = !animator.enabled;
        }
    }
}
