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
        GameObject Storing;

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

            rayStart = mPlayerTransform.position + (2 * Vector3.up);
            rayDir = new Vector3((mCameraTransform.position.x - mPlayerTransform.position.x), 0, (mCameraTransform.position.z - mPlayerTransform.position.z)).normalized;
            rayDistance = (mCameraTransform.position - mPlayerTransform.position).magnitude;

            if (Physics.Raycast(rayStart, rayDir, out hit, rayDistance, LayerMask.GetMask("Opaque")))
            {
                Debug.DrawRay(rayStart, rayDir * rayDistance, Color.red, 0.01f);
                Debug.Log("blocking");
                mCameraTransform.position = hit.point;
            }
            else
            {
                Debug.DrawRay(rayStart, rayDir * rayDistance, Color.green, 0.01f);
                Debug.Log("Not blocking");
            }
        }

        public abstract void Update();
    }
}
