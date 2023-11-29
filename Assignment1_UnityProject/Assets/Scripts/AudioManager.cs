using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Source;

    public PlayerMovement playerMovement;

    //get all audioclips in arrays for randomization
    public AudioClip[] Concrete;
    public AudioClip[] Metal;
    public AudioClip[] Wood;
    public AudioClip[] Dirt;
    public AudioClip[] Sand;
    public AudioClip[] flying;
    public AudioClip[] bite;
    public AudioClip[] scream;

    //nested array that will store all the footstep sounds arrays
    AudioClip[][] allFootsteps = { };

    //array that will store the footstep audio clips of the material the charcter is standing on
    AudioClip[] CurrentClip;

    RaycastHit hit;
    Material mat;

    private void Start()
    {
        //add all the footstep clips arrays into a nested array 
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

    //plays a footstep sound using the a random audio clip in currentClip with random pitch and volume 
    public void PlayFootStepRandomly()
    { 
        //if the floor doesnt return a material log a warning 
        if (CurrentClip == null)
        {
            Debug.LogWarning("Material is not recognised");
            return;
        }
        //randomise the pitch and volume 
        Source.pitch = Random.Range(0.4f, 0.6f);
        Source.volume = Random.Range(0.3f, 0.7f);
        
        //randomly choose the audio clip in the array
        int i = Random.Range(0, CurrentClip.Length);

        //play the audio clip
        Source.PlayOneShot(CurrentClip[i]);
        Debug.Log("played");
    }

    //plays a random flap audio clip from the flying array with random pitch and volume 
    public void PlayFlappingRandomly()
    {
        //if the arrray is empty log a warning
        if(flying == null)
        {
            Debug.LogWarning("No Flapping Audio Clip");
            return;
        }

        //randomise the pitch and volume 
        Source.pitch = Random.Range(0.4f, 0.6f);
        Source.volume = Random.Range(0.3f, 1f);

        //randomise the clip being played
        int i = Random.Range(0, flying.Length);
        Source.PlayOneShot(flying[i]);
    }

    //plays a random bite audio clip from the bite array with random pitch and volume 
    public void PlayBiteRandomly()
    {
        //if the array is empty log a warning
        if (bite == null)
        {
            Debug.LogWarning("No Flapping Audio Clip");
            return;
        }
        //randomise the pitch and volume 
        Source.pitch = Random.Range(0.4f, 0.6f);
        Source.volume = Random.Range(0.3f, 1f);

        //randomise the clip being played
        int i = Random.Range(0, bite.Length);
        Source.PlayOneShot(bite[i]);
    }

    //plays a random scream audio clip from the scream array with random pitch and volume 
    public void PlayScreamRandomly()
    {
        //if the array is empty log a warning
        if (scream == null)
        {
            Debug.LogWarning("No Flapping Audio Clip");
            return;
        }
        //randomise the pitch and volume 
        Source.pitch = Random.Range(0.4f, 0.6f);
        Source.volume = Random.Range(0.3f, 1f);

        //randomise the clip being played
        int i = Random.Range(0, scream.Length);
        Source.PlayOneShot(scream[i]);
    }

    //this function get the floor material and sets the material's footstep array into the currentClip array
    public void CheckFloorMat()
    {
        //if player's velocity is 0 return
        if (playerMovement.mCharacterController.velocity == Vector3.zero)
            return;

        //if the raycast hit the floot
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f))
        {
            //get the material script from the floor
            mat = hit.collider.gameObject.GetComponent<Material>();

            //if there is a script
            if (mat != null)
            {
                //set the hitMaterial index to the index of the enum selected in the material script 
                int hitMaterialIndex = (int)mat.Fmaterial;
                Debug.Log("Hit Material: " + hitMaterialIndex);

                //set current clip to the correct clip using the index of the enum
                CurrentClip = allFootsteps[hitMaterialIndex];
            }
            else
            {
                Debug.LogWarning("No CustomMaterial component found on the hit object.");
            }
        }
    }
}
