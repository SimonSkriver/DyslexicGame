using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] Camera eyes;
    [SerializeField] float rayCastDistance = 3f;
    [SerializeField] LetterSpawn letterSpawn;
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
        if (interactAction.WasPressedThisFrame())
        {
            Debug.DrawRay(eyes.transform.position, eyes.transform.forward * rayCastDistance, Color.red);
            var ray = new Ray(eyes.transform.position, eyes.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, rayCastDistance))
            {
                if (hit.collider.CompareTag("LetterSpawn"))
                {
                    Debug.Log("Here's your letter");
                    letterSpawn.SpawnLetter();
                }
            }
        }
    }
}
