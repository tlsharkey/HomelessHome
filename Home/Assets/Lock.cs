using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{

    private Vector3 point;
    private Vector3 axis;
    private float angle;

    private bool unlocked = true;
    public bool Moving = false;

    private int counter;

    void Start()
    {
        point = new Vector3(
            transform.position.x,// - 1 / 2 * transform.localScale.x,
            transform.position.y + transform.localScale.z,
            transform.position.z
        );
        axis = Vector3.right;
        angle = 100f; //deg

    }

    // Update is called once per frame
    void Update()
    {
        if (Moving) {
            if (unlocked) {
                UnlockHome();
            }
            else {
                LockHome();
            }
        }
    }

    public void Toggle(bool unlock)
    {
        if (unlock)
        {
            Moving = true;
            unlocked = true;
        }
        else
        {
            Moving = true;
            unlocked = false;
        }
    }

    private void LockHome() {
        transform.RotateAround(point, axis, angle / 100);
        counter--;

        if (counter == 0) {
            Moving = false;
        }
    }

    private void UnlockHome() {
        transform.RotateAround(point, axis, -angle / 100);
        counter++;

        if (counter == 100)
        {
            Moving = false;
        }
    }
}
