using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateLevelText : MonoBehaviour
{
    [SerializeField]
    private GameObject ElementHolder;

    [SerializeField]
    private Button TextElement;

    [SerializeField]
    private GameObject Selector;

    public GameObject Color;

    [SerializeField]
    private Button AddColor;

    private int activateSelectorIndex = -1;

    private List<string> colorsList = new List<string>
    {
        "white","black","red","green","blue"
    };

    private List<GameObject> Selectors = new List<GameObject>();

    private List<Button> elements = new List<Button>();

    private List<string> levelOne = new List<string>
    {
        "Title","Second Title","Paragraph"
    };

    private List<string> levelOneSelector = new List<string>
    {
        ".title",".second-title",".paragraph"
    };

    private void Start()
    {
        createText();
        createSelector();
        addColorAtribut();
        

    }
    private void Update()
    {
    }


    public void createText()
    {
        foreach ( string text in levelOne)
        {
            Button element = addText(text);
            element.onClick.AddListener(() =>
            {
                activateSelectorIndex = levelOne.FindIndex(e => e.Contains(text));
                foreach(GameObject selector in Selectors)
                {
                        selector.SetActive(false);
                }
                Selectors[activateSelectorIndex].SetActive(true);
            });
            elements.Add(element);
        }
    }
    public void createSelector()
    {
        foreach (string text in levelOneSelector)
        {
            GameObject element = addselector(text + " {");
            element.SetActive(false);
            Selectors.Add(element);
        }
    }

    public Button addText(string text)
    {
        //Instantiate the element prefab
        Button element = Instantiate(TextElement);
        element.transform.SetParent(ElementHolder.transform, false);

        //change the text in the element prefab

        element.GetComponentInChildren<TMP_Text>().text = text;


        return element;
    }

    public GameObject addselector(string text)
    {
        GameObject codeWindow = GameObject.Find("Code Window");

        //Instantiate the element prefab
        GameObject selector = Instantiate(Selector);
        selector.transform.SetParent(codeWindow.transform, false);

        //change the text in the element prefab
        TMP_Text className = selector.GetComponentInChildren<TMP_Text>();
        className.text = text;
        

        return selector;
    }

    public void ColorAtrribut(GameObject color)
    {
        Dropdown ColorDropdown = color.transform.GetChild(1).GetComponent<Dropdown>();
        

        ColorDropdown.ClearOptions();

        foreach (string col in colorsList)
        {
            ColorDropdown.options.Add(new Dropdown.OptionData() { text = col });
        }
        Debug.Log(ColorDropdown.GetInstanceID());
        TMP_Text value = color.transform.GetChild(0).GetComponent<TMP_Text>();
        value.text = "color : " + ColorDropdown.options[ColorDropdown.value].text;
        ColorDropdown.onValueChanged.AddListener(delegate
        {
            ChangeColor( ColorDropdown);
            value.text = "color : " + ColorDropdown.options[ColorDropdown.value].text;
        });
        
    }

    public void ChangeColor(Dropdown color)
    {
        if (color.options[color.value].text == "white")
        {
            elements[activateSelectorIndex].GetComponentInChildren<TMP_Text>().color = new Color(1f, 1f, 1f);
        }
        else if (color.options[color.value].text == "red")
        {
            Debug.Log("red");
            elements[activateSelectorIndex].GetComponentInChildren<TMP_Text>().color = new Color(1f, 0f, 0f);
        }
        else if (color.options[color.value].text == "black")
        {
            elements[activateSelectorIndex].GetComponentInChildren<TMP_Text>().color = new Color(0f, 0f, 0f);
        }
        else if (color.options[color.value].text == "green")
        {
            elements[activateSelectorIndex].GetComponentInChildren<TMP_Text>().color = new Color(0f, 1f, 0f);
        }
        else if (color.options[color.value].text == "blue")
        {
            elements[activateSelectorIndex].GetComponentInChildren<TMP_Text>().color = new Color(0f, 0f, 1f);
        }
    } 

    public void addColorAtribut()
    {
        AddColor.onClick.AddListener(() =>
        {
            GameObject color = Instantiate(Color);
            VerticalLayoutGroup content = Selectors[activateSelectorIndex].GetComponentInChildren<VerticalLayoutGroup>();
            color.transform.SetParent(content.transform, false);

            ColorAtrribut(color);

            
        });
    }
}
