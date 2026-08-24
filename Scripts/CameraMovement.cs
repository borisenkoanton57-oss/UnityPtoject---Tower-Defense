using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

    
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed, rotationSpeed, maxHeight, minHeight;
    [SerializeField] private Transform cameraHolder;
    private Rigidbody rigidbodyPlayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll > 0 && transform .position.y < minHeight)
        {
            scroll = 0;
        }
        else if(scroll < 0 && transform.position.y > maxHeight)
        {
            scroll = 0;
        }
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * Time.deltaTime;

        if(moveX != 0 || moveY != 0 || scroll != 0)
        {
            rigidbodyPlayer.AddForce(cameraHolder.transform.forward * moveY * movementSpeed + cameraHolder.transform.right *moveX * movementSpeed
            + Camera.main.transform.forward * scroll * movementSpeed);
        }
        float rotation = Input.GetAxis("Tactical") * Time.deltaTime;
        cameraHolder.transform.Rotate(0, rotation * rotationSpeed, 0);
    }
}
