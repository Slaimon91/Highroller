//This script outputs the name and length of the Animation clip played at start-up.

using UnityEngine;

public class GetCurrentAnimatorClipInfoExample : MonoBehaviour
{
    Animator m_Animator;
    string m_ClipName;
    AnimatorClipInfo[] m_CurrentClipInfo;

    float m_CurrentClipLength;

    void Start()
    {
        //Get them_Animator, which you attach to the GameObject you intend to animate.
        m_Animator = gameObject.GetComponent<Animator>();
        //Fetch the current Animation clip information for the base layer
        m_CurrentClipInfo = this.m_Animator.GetCurrentAnimatorClipInfo(0);
        //Access the current length of the clip
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        //Access the Animation clip name
        m_ClipName = m_CurrentClipInfo[0].clip.name;

        PrintInfo();
    }

    void PrintInfo()
    {
        //Output the current Animation name and length to the screen
        //GUI.Label(new Rect(0, 0, 200, 20), "Clip Name : " + m_ClipName);
        //GUI.Label(new Rect(0, 30, 200, 20), "Clip Length : " + m_CurrentClipLength);

        Debug.Log(m_ClipName);
        Debug.Log(m_CurrentClipLength);
        
    }
}