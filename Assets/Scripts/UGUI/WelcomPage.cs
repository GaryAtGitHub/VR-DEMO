using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class WelcomPage : MonoBehaviour
{
    public GameObject btn_Num;
    public GameObject btn_BackSpace;
    public GameObject btn_ClearAll;
    public float Animationgap = 0.5f;
    public DisplayManger m_DisplayManger;

    private int buttonCount = 12;
    private VRTK_InteractableObject interactableObject;
    private bool isInit = false;

    private void Awake()
    {
        m_DisplayManger = GetComponentInChildren<DisplayManger>();
        interactableObject = GetComponentInParent<VRTK_InteractableObject>();
        interactableObject.InteractableObjectGrabbed += InteractableObject_InteractableObjectGrabbed; 
    }


    private void InteractableObject_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        if (!isInit)
        {
            isInit = true;
            interactableObject.InteractableObjectGrabbed -= InteractableObject_InteractableObjectGrabbed;
            StartCoroutine(Init());          
        }        
    }

    IEnumerator Init()
    {
        for (int i = 1; i < 10; i++)
        {
            GenerateNumButton(btn_Num, i);
       
            yield return new WaitForSeconds(Animationgap);
        }
        GenerateButton(btn_BackSpace);
        yield return new WaitForSeconds(Animationgap);
        GenerateNumButton(btn_Num, 0);
        yield return new WaitForSeconds(Animationgap);
        GenerateButton(btn_ClearAll);
    }

    private void GenerateNumButton(GameObject button, int i)
    {
        GameObject go = Instantiate(button, transform.Find("Parent"));
        Btn_Num numberButton = go.GetComponent<Btn_Num>();
        go.GetComponentInChildren<Text>().text = i.ToString();
        numberButton.ButtonNum = i;
    }

    private void GenerateButton(GameObject button)
    {
        GameObject go = Instantiate(button, transform.Find("Parent"));
    }
}
