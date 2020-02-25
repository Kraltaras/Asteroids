using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float cameraWidth = Camera.main.orthographicSize * 2 * Screen.width / Screen.height; //screen width in unity points
        float screenPosition = Input.mousePosition.x - Screen.width / 2;
        float newPos = screenPosition * cameraWidth / Screen.width;
        transform.position = new Vector3(newPos, transform.position.y, transform.position.z);

        UIController ui = FindObjectOfType<UIController>();
        ui.ChangeHealthPos(this.gameObject, UIController.adjustVertPos);
    }
}
