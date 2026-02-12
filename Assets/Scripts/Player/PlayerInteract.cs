using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] Camera eyes;
    [SerializeField] float rayCastDistance = 3f;
    private InputAction interactAction;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        HandleInteract();
    }

    void HandleInteract()
    {
        if (interactAction.IsPressed())
        {
            Debug.DrawRay(eyes.transform.position, eyes.transform.forward * rayCastDistance, Color.red);
            var ray = new Ray(eyes.transform.position, eyes.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, rayCastDistance))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
