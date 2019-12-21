using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform mount;
    [SerializeField] // Follow Speed
    private float speed = 5;

    [SerializeField]
    private float height;
    private Vector3 offset;

    [SerializeField]
    private bool lockRotation = false;
    [SerializeField]
    private bool smoothFollow;

    private Coroutine camFollow;

    private bool camFollowTrigger;
    private Vector3 mountLastKnownPosition;

    private Vector3 followPosition;

    private void Start()
    {
        offset = new Vector3(0, height, 0);
    }

    // private Coroutine CamZoomIn;

    // Start is called before the first frame update
    public void SetMount(Transform inMount)
    {
        mount = inMount;        
    }

    //Start follow and set base position and rotation
    public void CameraOn()
    {


        camFollow = StartCoroutine(StartCamFollow());
        transform.position = mount.transform.position + offset;

        if (lockRotation) transform.rotation = Quaternion.Euler(90,0,0);
    }

   

   //Lerp between current position and rotation to mount on character for a smooth follow
    //private IEnumerator StartCamZoomIn()
    //{
    //    while (true)
    //    {
    //        if (transform.position == mount.transform.position)
    //        {
    //            StartCoroutine(StartCamFollow());
    //            StopCoroutine(CamZoomIn);
    //        }

    //        transform.position = Vector3.Lerp(transform.position, mount.transform.position, Time.deltaTime * speed);

    //        transform.rotation = Quaternion.Slerp(transform.rotation, mount.transform.rotation, Time.deltaTime * speed);

    //        yield return new WaitForFixedUpdate();
    //    }
        
    //}
    private IEnumerator StartCamFollow()
    {
        while (true)
        {
            followPosition = camFollowTrigger ? mountLastKnownPosition : mount.transform.position;
            //if (camFollowTrigger) speed = 0.01f / Vector3.Distance(mountLastKnownPosition + offset, transform.position + offset); else speed = 5;
            followPosition += offset;
            if (smoothFollow)
            {
                transform.position = Vector3.Lerp(transform.position, followPosition, Time.deltaTime * speed);
            }
            else
            {
                transform.position = followPosition;
            }


            if (!lockRotation) transform.rotation = mount.transform.rotation;

            yield return new WaitForFixedUpdate();
        }

    }

    public void StopFollow()
    {
        if (!camFollowTrigger)
        {
            mountLastKnownPosition = mount.transform.position;
            camFollowTrigger = true;
        }
        if (Vector3.Distance(transform.position, mountLastKnownPosition + offset) < 5)
        {
            ForceStopFollow();
        }
    }

    public void ForceStopFollow()
    {
        camFollowTrigger = false;
        if (camFollow != null)
        {
            StopCoroutine(camFollow);
            camFollow = null;
        }
    }

    public void StartFollow()
    {
        ForceStopFollow();
        if (camFollow == null) camFollow = StartCoroutine(StartCamFollow());
    }


}
