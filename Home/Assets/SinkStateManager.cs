using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkStateManager : MonoBehaviour {

    // Controling rotation axis
    private Vector3 point;
    private Vector3 axis;
    private float angle;

    // State manager
    private int counter;
    private static float stepSize = 1f;
    private int numStepsToFold;
    private int numStepsToWall;
    private int numStepsToSink;
    public bool Moving;
    public bool toFold;
    public bool toWall;
    public bool toSink;
    public string currentState = "";

	void Start () {
        point = new Vector3(
            transform.localPosition.x + transform.localScale.x,
            transform.localPosition.y,
            transform.localPosition.z + transform.localScale.y
        );
        axis = Vector3.right;
        angle = 180f;

        numStepsToFold = 0;
        numStepsToWall = (int)(angle / 2 / stepSize);
        numStepsToSink = (int)(angle / stepSize);

        currentState = "FOLD";
	}
	
	// Update is called once per frame
	void Update () {
        if (Moving) {
            if (toFold && !currentState.Equals("FOLD")) {
                ToFold();
            }
            else if (toWall && !currentState.Equals("WALL")) {
                ToWall();
            }
            else if (!currentState.Equals("SINK")){
                ToSink();
            }
            else {
                Debug.Log("Was already in selected state: " + currentState);
            }
        }
	}

    public void ToFold()
    {
        if (currentState.Equals("FOLD")) { return; }
        Moving = true; toFold = true; toWall = false; toSink = false;

        transform.RotateAround(point, axis, -stepSize);
        counter--;

        // Check end condition
        if (counter == numStepsToFold) {
            currentState = "FOLD";
            Moving = false;
        }
    }

    public void ToWall()
    {
        if (currentState.Equals("WALL")) { return; }

        // Determine which direction to move
        int direction;
        if (counter > numStepsToWall) { direction = -1; }
        else { direction = 1; }

        Moving = true; toFold = false; toWall = true; toSink = false;

        transform.RotateAround(point, axis, direction*stepSize);
        counter += direction;

        // Check end condition
        if (counter == numStepsToWall) {
            currentState = "WALL";
            Moving = false;
        }
    }

    public void ToSink() {
        if (currentState.Equals("SINK")) { return; }
        Moving = true; toFold = false; toWall = false; toSink = true;

        transform.RotateAround(point, axis, stepSize);
        counter++;

        // Check end condition
        if (counter == numStepsToSink) {
            currentState = "SINK";
            Moving = false;
        }
    }
}
