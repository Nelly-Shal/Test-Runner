using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 targetPos;
    float lineOffset = 4;
    float lineChangeSpeed = 6;
    float timeElapsed;
    float lerpDuration = 0.5f;

    bool keyMove;
    bool dragMove;


    [Space]
    public bool LocalMovement;

    void Start()
    {
        if (PlayerPrefs.HasKey("Control"))
        {
            ChangeCtrl(PlayerPrefs.GetInt("Control"));
        }
        targetPos = transform.position;
        SwipeManager.instance.MoveEvent += SwipeMove;
    }

    void Update()
    {
        if (keyMove == true)
            KeyMove();
        else if (dragMove == true)
            DragMove();

    }

    private void FixedUpdate()
    {
      

            if (timeElapsed < lerpDuration)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                transform.position = targetPos;
            }
    }
    public void MoveHorizontal(float speed)
    {
        
        timeElapsed = 0;
        targetPos = new Vector3(targetPos.x+speed, transform.position.y, transform.position.z);
    }

    void SwipeMove(bool[] swipes)
    {
        if (swipes[(int)SwipeManager.Direction.Left] && targetPos.x > -lineOffset)
        {
            MoveHorizontal(-lineChangeSpeed);
        }
        if (swipes[(int)SwipeManager.Direction.Right] && targetPos.x < lineOffset)
        {
            MoveHorizontal(lineChangeSpeed);
        }
    }

    void KeyMove()
    {
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && targetPos.x > -lineOffset)
        {
            MoveHorizontal(-lineChangeSpeed);
        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && targetPos.x < lineOffset)
        {
            MoveHorizontal(lineChangeSpeed);
        }
    }

    void DragMove()
    {
        

    }


    public void ChangeCtrl(int ctrl) //1-key 2-swipe 3-drag
    {
        if (ctrl == 1)
        {
            SwipeManager.instance.enabled = false;
            keyMove = true;
            dragMove = false;
            SaveCtrl(ctrl);
        }
        else if (ctrl == 2)
        {
            SwipeManager.instance.enabled = true;
            keyMove = false;
            dragMove = false;
            SaveCtrl(ctrl);
        }
        else if (ctrl == 3)
        {
            SwipeManager.instance.enabled = false;
            keyMove = false;
            dragMove = true;
            SaveCtrl(ctrl);
        }
    }
    void SaveCtrl(int ctrl)
    {
        PlayerPrefs.SetInt("Control", ctrl);
        PlayerPrefs.Save();
    }

    public void ResetCtrl()
    {
        PlayerPrefs.DeleteKey("Control");
    }
}
