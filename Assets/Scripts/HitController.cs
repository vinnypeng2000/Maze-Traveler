using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HitController : MonoBehaviour
{
    public Animator boxingAnim;
    public Animator getHitAnim;
    public float interval;

    private void Start()
    {
        StartCoroutine(PlayerHitAnim());
    }

    private IEnumerator PlayerHitAnim()
    {
        boxingAnim.enabled = true;
        boxingAnim.Play("boxing");
        //boxingAnim.transform.DOMoveZ(1f, 0.5f);
        yield return new WaitForSeconds(interval);
        //boxingAnim.transform.DOMoveZ(0f, 1f);
        getHitAnim.enabled = true;
        getHitAnim.Play("get_hit");
        StartCoroutine(EndHit());
        StartCoroutine(EndBoxing());
    }

    private IEnumerator EndHit()
    {
        yield return new WaitForSeconds(1.2f);
        getHitAnim.enabled = false;
    }

    private IEnumerator EndBoxing()
    {
        yield return new WaitForSeconds(2.1f - interval);
        boxingAnim.enabled = false;
        StartCoroutine(PlayerHitAnim());
    }
}
