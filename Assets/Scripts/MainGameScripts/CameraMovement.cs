using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Position Variables")]
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition; //15x 19.5y
    public Vector2 minPosition; //-15 -19.5

    [Header("Animator")]
    public Animator animator;

    [Header("Position Reset")]
    public VectorValue cameraMin;
    public VectorValue cameraMax;


    // Start is called before the first frame update
    void Start()
    {
        minPosition = cameraMin.initialValue;
        maxPosition = cameraMax.initialValue;
        
        animator = GetComponent<Animator>();
        transform.position = new Vector3( target.position.x, target.position.y, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform != null && transform.position != target.position) {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            //Bind the camera "hard-coded"
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }

    public void BeginKick()
    {
        animator.SetBool("KickActive", true);
        StartCoroutine(KickCoRoutine());
    }

    public IEnumerator KickCoRoutine()
    {
        yield return null;
        animator.SetBool("KickActive", false);
    }
}