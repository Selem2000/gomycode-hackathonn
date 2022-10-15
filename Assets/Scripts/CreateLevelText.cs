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

    private int activateSelectorIndex = -1;

    private List<GameObject> Selectors = new List<GameObject>();

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

    
}
