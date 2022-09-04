using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{

    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cameraMovement;
    public bool needText;
    public string placeName;
    public GameObject placeNameText;
    public Text placeText;


    // Start is called before the first frame update
    void Start()
    {
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !collider.isTrigger ) {
            cameraMovement.minPosition += cameraChange;
            cameraMovement.maxPosition += cameraChange;

            collider.transform.position += playerChange;

            if (needText) {
                StartCoroutine(placeNameCoroutine());
            }
        }
    }

    private IEnumerator placeNameCoroutine()
    {
        placeNameText.SetActive(true);
        placeText.text = placeName;
        Debug.Log("placename :" + placeName);
        yield return new WaitForSeconds(4f);
        placeNameText.SetActive(false);
    }
}
