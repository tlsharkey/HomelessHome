using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingSpace_roof : MonoBehaviour
{

    public bool Moving = false;
    public bool Opening = false;
    public bool Closed = true;

    public UnityEngine.UI.Toggle parentToggle;
    public UnityEngine.UI.Toggle toggle;
    public GameObject[] children;


    protected Transform StartLocation;
    protected static float TranslateDistance = 3f; //feet - TODO: change to meters
    protected static float TranslationCounter = 0f;
    protected static float StepAmount = 0.01f;
    protected string Name;

    private void Start() {
        StartLocation = transform;
        Transform[] childTransforms = transform.GetComponentsInChildren<Transform>();
        children = new GameObject[childTransforms.Length];
        for (int i = 0; i < childTransforms.Length; i++)
        {
            children[i] = childTransforms[i].gameObject;
        }
        Name = "Sleeping Space: Roof";
    }

    private void Update()
    {
        // Check parent - if moving, don't move.
        if (!transform.parent.GetComponent<SleepingSpace>().Moving)
        {
            if (Moving)
            {
                if (Opening)
                {
                    Open();
                }
                else
                {
                    Close();
                }
            }
        }
    }



    public void checkToggleState() {
        if (parentToggle.isOn != toggle.isOn) {
            toggle.isOn = false;
        }
    }


    public void Toggle(bool open)
    {


        if (!Moving)
        {
            if (open && Closed)
            {
                // Check parent
                //if (parentToggle) { if (parentToggle.isOn) { return; }}
                Debug.Log("Opening " + Name);
                Opening = true;
                Moving = true;
                Closed = false;
            }
            else if (!open && !Closed)
            {
                //if (parentToggle) { if (!parentToggle.isOn) { return; }}
                Debug.Log("Closing" + Name);
                Opening = false;
                Moving = true;
                Closed = true;
            }
            else
            {
                Debug.Log("Triggered, but already in that state");
            }
        }
        else
        {
            Debug.Log("Wait until current transform is complete");
            // TODO: Change check back

        }
    }

    protected void Open()
    {
        //transform.position.Set(transform.position.x, transform.position.y, transform.position.z + 0.01f);
        transform.localPosition = new Vector3(transform.position.x - StepAmount, transform.position.y, transform.position.z);
        TranslationCounter += StepAmount;

        // End translation
        if (TranslationCounter >= TranslateDistance)
        {
            Moving = false;
            TranslationCounter = 0;
            Debug.Log("Opened");
        }
    }

    protected void Close()
    {
        //transform.position.Set(transform.position.x, transform.position.y, transform.position.z - 0.01f);
        transform.localPosition = new Vector3(transform.position.x + StepAmount, transform.position.y, transform.position.z);
        TranslationCounter += StepAmount;

        // End translation
        if (TranslationCounter >= TranslateDistance)
        {
            Moving = false;
            TranslationCounter = 0;
            Debug.Log("Closed");
        }
    }

    protected void OverrideMove(float amount)
    {
        transform.localPosition = new Vector3(transform.position.x + amount, transform.position.y, transform.position.z);
    }
}
