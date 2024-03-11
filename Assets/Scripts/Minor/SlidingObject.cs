using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingObject : MonoBehaviour
{
    Vector2 InitialPos; // Gotten Position.
    public Vector2 DesiredPos; // Desired to be in Position.
    public float TransitionSpeed;
    [Space]
    public bool InDesiredPos = false;
    private Vector3 Pos;

    public void Start() {
        InitialPos = this.transform.position;
    }


    Vector2 CurrentPos;
    // Update is called once per frame
    void LateUpdate()
    {
        if(InDesiredPos == false) { // In Initial
            CurrentPos = Vector2.Lerp(this.transform.position, DesiredPos, TransitionSpeed * Time.deltaTime);
        } else { // End Position
            CurrentPos = Vector2.Lerp(this.transform.position, InitialPos, TransitionSpeed * Time.deltaTime);
        }

        if(InDesiredPos != true) {
            if(CurrentPos == DesiredPos) {
                InDesiredPos = true;
            }
        } else {
            if (CurrentPos == InitialPos) {
                InDesiredPos = false;
            }
        }

        this.transform.position = CurrentPos;
    }
}
