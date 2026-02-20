using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] Camera eyes;
    [SerializeField] float pickUpReach = 3f;
    [SerializeField] float placementReach = 3f;
    [SerializeField] LetterSpawn letterSpawn;
    private InputAction interactAction;
    private InputAction placeAction;
    public bool isCarrying;
    private bool ghostLetterIsSpawned;
    

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        placeAction = InputSystem.actions.FindAction("Place");
    }

    void Update()
    {
        HandleInteract();
        HandlePlacing();
        PlacementPreview();
    }

    void HandleInteract()
    {
        if (interactAction.WasPressedThisFrame())
        {
            Debug.DrawRay(eyes.transform.position, eyes.transform.forward * pickUpReach, Color.red);
            if (Physics.Raycast(eyes.transform.position, eyes.transform.forward, out RaycastHit hit, pickUpReach))
            {
                if (hit.collider.CompareTag("LetterSpawn") && !isCarrying)
                {
                    Debug.Log("Here's your letter");
                    letterSpawn.SpawnLetter();
                    isCarrying = true;
                }
            }
        }
    }

    void HandlePlacing()
    {
        if (placeAction.WasPressedThisFrame())
        {
            isCarrying = false;
            bool ray = Physics.Raycast(eyes.transform.position, eyes.transform.forward, out RaycastHit hit, placementReach);
            if (ray == false || !hit.collider.CompareTag("Page"))
            {
                Destroy(letterSpawn.spawnedLetter);
            }
            else
            {
                letterSpawn.spawnedLetter.transform.parent = null;
                letterSpawn.spawnedLetter.transform.position = hit.point;
                letterSpawn.spawnedLetter.transform.rotation = Quaternion.Euler(0f, -30.838f, letterSpawn.spawnedLetter.transform.rotation.z);
                letterSpawn.spawnedLetter = null;
                Destroy(letterSpawn.ghostLetter);
            }
        }
    }

    void PlacementPreview()
    {
        if (isCarrying)
        {
            Physics.Raycast(eyes.transform.position, eyes.transform.forward, out RaycastHit hit, placementReach);
            Debug.DrawRay(eyes.transform.position, eyes.transform.forward * pickUpReach, Color.red);
            if (hit.collider.CompareTag("Page"))
            {
                letterSpawn.ghostLetter.SetActive(true);
                letterSpawn.ghostLetter.transform.position = hit.point;
                letterSpawn.ghostLetter.transform.rotation = Quaternion.Euler(0f, -30.838f, letterSpawn.ghostLetter.transform.rotation.z);
            }
        }
    }
}