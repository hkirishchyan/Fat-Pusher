using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    [Header("Direction Booleans")]
    public bool isDragging = false;
    public bool press, leftSwipe, rightSwipe, upSwipe, downSwipe;
    public Vector2 touchOrigin, swipeDistance;
    void Update()
    {
        press = leftSwipe=rightSwipe=upSwipe=downSwipe=false;
        #region PressDetection
        if (Input.GetMouseButtonDown(0))
        {
            press = true;
            isDragging = true;
            touchOrigin = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset(); 
        }
        #endregion
        #region DragDetection
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDragging = true;
                press = true;
                touchOrigin = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended||Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        #endregion
        #region SwipeDetection
        swipeDistance = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDistance = Input.touches[0].position - touchOrigin;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDistance = (Vector2)Input.mousePosition - touchOrigin;
            }
        }
        #endregion
        #region SwipeDirection
        if (swipeDistance.magnitude > 100)
        {
            float x = swipeDistance.x;
            float y = swipeDistance.y;
            if(Mathf.Abs(x)> Mathf.Abs(y))
            {
                if (x < 0)
                    leftSwipe = true;
                else
                    rightSwipe = true;
            }
            else
            {
                if (y < 0)
                    downSwipe = true;
                else
                    upSwipe = true;
            }

            Reset();
        }
        #endregion
    }
    private void Reset()
    {
        touchOrigin = swipeDistance = Vector2.zero;
        isDragging = false;
    }
}
