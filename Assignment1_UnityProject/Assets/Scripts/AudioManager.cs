using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Source;

    public PlayerMovement playerMovement;

    public AudioClip[] Concrete;
    public AudioClip[] Metal;
    public AudioClip[] Wood;
    public AudioClip[] Dirt;
    public AudioClip[] Sand;
    public AudioClip[] flying;
    public AudioClip[] bite;
    public AudioClip[] scream;

    public AudioClip[][] allFootsteps = { };
    public AudioClip[][] allAttack = { };

    AudioClip[] CurrentClip;

    RaycastHit hit;
    Material mat;
    

    private void Start()
    {
        allFootsteps = new AudioClip[][]
        {
            Concrete,
            Metal,
            Wood,
            Dirt,
            Sand
        };

    }

    private void Update()
    { 
        CheckFloorMat(); 
    }
    public void PlayFootStepRandomly()
    {
        if (CurrentClip == null)
        {
            Debug.LogError("Material is not recognised");
            return;
        }
        Source.pitch = Random.Range(0.4f, 0.6f);
        Source.volume = Random.Range(0.3f, 0.7f);
        int i = Random.Range(0, CurrentClip.Length);
        Source.PlayOneShot(CurrentClip[i]);
    }

    public void PlayFlappingRandomly()
    {
        if(flying == null)
        {
            Debug.LogError("No Flapping Audio Clip");
            return;
        }
        Source.pitch = Random.Range(0.4f, 0.6f);
        Source.volume = Random.Range(0.3f, 0.7f);
        int i = Random.Range(0, flying.Length);
        Source.PlayOneShot(flying[i]);
    }
    public void PlayBiteRandomly()
    {
        int i = Random.Range(0, bite.Length);
        Source.PlayOneShot(bite[i]);
    }
    public void PlayScreamRandomly()
    {
        int i = Random.Range(0, scream.Length);
        Source.PlayOneShot(scream[i]);
    }

    public void CheckFloorMat()
    {
        if (playerMovement.mCharacterController.velocity == Vector3.zero)
            return;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f))
        {
            Material mat = hit.collider.gameObject.GetComponent<Material>();

            if (mat != null)
            {
                int hitMaterialIndex = (int)mat.Fmaterial;
                Debug.Log("Hit Material: " + hitMaterialIndex);

                CurrentClip = allFootsteps[hitMaterialIndex];
                
            }
            else
            {
                Debug.LogWarning("No CustomMaterial component found on the hit object.");
            }
        }
        Debug.Log(hit.collider.gameObject.name);
    }
}
