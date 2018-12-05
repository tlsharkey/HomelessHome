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

    public UnityEngine.UI.Text deb;
    public Transform Vuf;

    public GameObject other;

    void Start()
    {
        point = new Vector3(
            transform.localPosition.x,// - 1 / 2 * transform.localScale.x,
            transform.localPosition.y + transform.localScale.z,
            transform.localPosition.z
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
        if (gameObject.activeInHierarchy) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }

        //if (unlock)
        //{
        //    Moving = true;
        //    unlocked = true;
        //}
        //else
        //{
        //    Moving = true;
        //    unlocked = false;
        //}
    }

    private void LockHome()
    {
        RotateAboutLocalAxis(point, axis, angle / 100);
        counter--;

        if (counter == 0) {
            Moving = false;
        }
    }

    private void UnlockHome() {
        RotateAboutLocalAxis(new Vector3(0, 0, 0), new Vector3(1, 0, 0), -angle / 100);
        counter++;

        if (counter == 100)
        {
            Moving = false;
        }
    }

    private void RotateAboutLocalAxis(Vector3 p, Vector3 ax, float ang) {
        // Rotate
        transform.Rotate(ax*ang, Space.Self);

        //// Calculate Distance to Translate
        /// l = half the lenght of the plane
        /// theta = the angle amount to rotate
        /// fi = the angle which points the rotated plane in the direction it needs to go --> solve for this
        /// r = the distance needed to travel to place plane in correct location --> solve for this
        float l = transform.localScale.z;
        float theta = 180/Mathf.PI * ang; //radians
        float fi = (Mathf.PI - theta) / 2; //PI radians in a triangle (isocelese so /2 to get single side
        float r = 2 * l * Mathf.Sin(theta / 2); //Splitting Iscoceles into right triangle, rsin(theta) = a
        float r_ref = Mathf.Sqrt(2 * Mathf.Pow(l, 2) * (1 - Mathf.Cos(theta)));

        // Converting to vector space
        Vector3 translationVec = new Vector3(
            0,
            r * Mathf.Cos(Mathf.PI / 2 - fi),
            r * Mathf.Sin(Mathf.PI / 2 - fi)
        );


        //// Degrees Version
        theta = ang; //radians
        fi = (180 - theta) / 2; //PI radians in a triangle (isocelese so /2 to get single side
        r = 2 * l * Mathf.Sin(theta / 2); //Splitting Iscoceles into right triangle, rsin(theta) = a
        r_ref = Mathf.Sqrt(2 * Mathf.Pow(l, 2) * (1 - Mathf.Cos(theta)));

        // Converting to vector space
        translationVec = new Vector3(
            0,
            r * Mathf.Cos(90 - fi),
            r * Mathf.Sin(90 - fi)
        );


        // Translate
        transform.Translate(translationVec);
        deb.text = string.Format("l: {0}\nr:{1}\nr_ref:{4}\nfi:{2}\nvec:{3}", l, r, theta, translationVec, r_ref);
    }
}
