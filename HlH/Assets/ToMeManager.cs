using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMeManager : MonoBehaviour {

    public GameObject Home;
    public UnityEngine.UI.Text deb;

	// Use this for initialization
	void Start () {
        Home.transform.position = transform.position;
        deb.text += "Started";
	}

    private void Update()
    {
        deb.text += "Update";
    }
}
