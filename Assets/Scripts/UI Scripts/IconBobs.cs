using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IconBobs : MonoBehaviour
{
    public AnimationCurve bobAnim;

    void Update()
    {
        BobAnim();
    }

    void BobAnim()
    {
        transform.position = new Vector3(transform.position.x, bobAnim.Evaluate((Time.time % bobAnim.length)), transform.position.z);
    }
}