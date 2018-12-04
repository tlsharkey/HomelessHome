using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingSpace : MonoBehaviour {

    public bool Moving = false;
    public bool Opening = false;
    public bool Closed = true;

    public UnityEngine.UI.Toggle toggle;
    public GameObject[] children;


    protected Transform StartLocation;
    protected static float TranslateDistance = 3f; //feet - TODO: change to meters
    protected int TranslationCounter = 0;
    protected static float StepAmount = 0.05f;
    protected string Name;
    protected int numSteps;

	// Use this for initialization
	private void Start () {
        StartLocation = transform;
        Transform[] childTransforms = transform.GetComponentsInChildren<Transform>();
        children = new GameObject[childTransforms.Length];
        for (int i = 0; i < childTransforms.Length; i++) {
            children[i] = childTransforms[i].gameObject;
        }
        Name = "Sleeping Space";

        numSteps = (int) (TranslateDistance / StepAmount);
    }
	
	// Update is called once per frame
	private void Update () {
        if (Moving) {
            if (Opening) {
                Open();
            }
            else {
                Close();
            }
        }
	}

    public void Toggle(bool open) {
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

    protected void Open() {
        //transform.position.Set(transform.position.x, transform.position.y, transform.position.z + 0.01f);
        transform.localPosition = new Vector3(transform.position.x - StepAmount, transform.position.y, transform.position.z);
        TranslationCounter++;

        // End translation
        if (TranslationCounter == numSteps) {
            Moving = false;
        }
    }

    protected void Close() {
        //transform.position.Set(transform.position.x, transform.position.y, transform.position.z - 0.01f);
        transform.localPosition = new Vector3(transform.position.x + StepAmount, transform.position.y, transform.position.z);
        TranslationCounter--;

        // End translation
        if (TranslationCounter == 0)
        {
            Moving = false;
        }
    }

    protected void OverrideMove(float amount) {
        transform.localPosition = new Vector3(transform.position.x + amount, transform.position.y, transform.position.z);
    }
}
