using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace PGGE
{
    // The base class for all third-person camera controllers
    public abstract class TPCBase
    {
        protected Transform mCameraTransform;
        protected Transform mPlayerTransform;
        RaycastHit hit;
        Vector3 rayStart;
        Vector3 rayDir;
        float rayDistance;

        public Transform CameraTransform
        {
            get
            {
                return mCameraTransform;
            }
        }
        public Transform PlayerTransform
        {
            get
            {
                return mPlayerTransform;
            }
        }

        public TPCBase(Transform cameraTransform, Transform playerTransform)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
        }


        public void RepositionCamera()
        {
            //-------------------------------------------------------------------
            // Implement here.
            //-------------------------------------------------------------------
            //-------------------------------------------------------------------
            // Hints:
            //-------------------------------------------------------------------
            // check collision between camera and the player.
            // find the nearest collision point to the player
            // shift the camera position to the nearest intersected point
            //-------------------------------------------------------------------

            //creating varibles for the raycast
            //origin of the ray will be the player's position + 2 units on the y axis so the start of the ray will be at the
            //head of the character instead of his feet
            rayStart = mPlayerTransform.position + (2 * Vector3.up);

            //direction of the ray will be the ((camera's position - player's position) - 2 units on the y axis ).normalised
            //this is because we will need to subtract 2 units from the y axis since we added 2 on the origin this is so the 
            //vector will be pointing at the camera instead of above it. normalise because its a direction vector.
            rayDir = ((mCameraTransform.position - mPlayerTransform.position) - (2 * Vector3.up)).normalized;

            //distance of the ray will be the magnitude of the vector camera's position - player's position
            rayDistance = (mCameraTransform.position - mPlayerTransform.position).magnitude * 1.2f;

            //if the raycast hit an object on the opaque layermask
            if (Physics.Raycast(rayStart, rayDir, out hit, rayDistance, LayerMask.GetMask("Opaque")))
            {
                Debug.DrawRay(rayStart, rayDir * rayDistance, Color.red, 0.01f);
                Debug.Log("blocking");
                //set camera position to the hit.point which is the point the raycast hit the wall -
                //rayDir * 0.5f to offset the camera off the wall slightly so that it wont clip through
                //the walls at smaller angles. i used rayDir * 0.5 because its convinently a small value
                //and we want to offset the camera towards the player.  
                mCameraTransform.position = hit.point - rayDir * 0.5f;
                    
                    
                    ////((new Vector3((mCameraTransform.position.x 
                    //- mPlayerTransform.position.x), mCameraTransform.position.y - (mPlayerTransform.position.y + 2), 
                    //(mCameraTransform.position.z - mPlayerTransform.position.z)).normalized)*0.5f);
            }
            //if the raycast didnt hit anything
            else
            {
                Debug.DrawRay(rayStart, rayDir * rayDistance, Color.green, 0.01f);
                Debug.Log("Not blocking");
            }
        }

        public abstract void Update();
    }
}
