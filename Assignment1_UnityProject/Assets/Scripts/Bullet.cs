using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hitSound;

    void Start()
    {
        // Destroy the bullet after 10 seconds if it does not hit any object.
        StartCoroutine(Coroutine_Destroy(10.0f));
    }

    void Update()
    {
    }

    IEnumerator Coroutine_Destroy(float duration)
    {
        yield return new WaitForSeconds(duration);
        audioSource.PlayOneShot(hitSound);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable obj = collision.gameObject.GetComponent<IDamageable>();
        if (obj != null)
        {
            obj.TakeDamage();
        }

        StartCoroutine(Coroutine_Destroy(0.1f));
    }
}
