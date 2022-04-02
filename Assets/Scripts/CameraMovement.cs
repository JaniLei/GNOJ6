using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{

    private float rotationSpeed = 500f;
    private GameObject player;
    private float interactRange = 2f;
    private bool isInteracting = false;
    private float timer;

    public Text InteractionText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float r = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime * -1f;
        r += transform.localEulerAngles.x;

        transform.localEulerAngles = new Vector3(r, 0f, 0f);

        r = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        r += player.transform.localEulerAngles.y;

        player.transform.localEulerAngles = new Vector3(0f, r, 0f);

        if (!Input.GetButtonDown("Fire1")) { return; }
        // Only execute the following code if the button is pressed
        isInteracting = true;

    }

    private void FixedUpdate()
    {
        if (timer >= 0.1f)
        {
            CheckForInteractable();
            timer = 0f;
        }
        timer += Time.deltaTime;

        if (!isInteracting) { return; }
        isInteracting = false;

        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit, interactRange)) { return; }

        GameObject gameObject = hit.transform.gameObject;
        if (gameObject.GetComponent<Interactable>() == null) { return; }

        gameObject.GetComponent<Interactable>().Interact(player);
    }

    private void CheckForInteractable()
    {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit, interactRange))
        {
            InteractionText.text = "";
            return;
        }

        GameObject gameObject = hit.transform.gameObject;
        var interactable = gameObject.GetComponent<Interactable>();
        if (interactable == null)
        {
            InteractionText.text = "";
            return;
        }

        InteractionText.text = interactable.GetInteractionText();
    }
}
