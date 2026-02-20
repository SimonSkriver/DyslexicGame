using UnityEngine;

public class LetterSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] letter;
    [SerializeField] Transform hands;
    [SerializeField] Material transparentMat;
    public GameObject spawnedLetter;
    public GameObject ghostLetter;


    public void SpawnLetter()
    {
        spawnedLetter = Instantiate(letter[Random.Range(0, letter.Length)], hands.position, hands.rotation);
        ghostLetter = spawnedLetter;
        ghostLetter = Instantiate(ghostLetter, hands.position, hands.rotation);
        ghostLetter.SetActive(false);
        ghostLetter.GetComponent<BoxCollider>().enabled = false;
        ghostLetter.GetComponent<Renderer>().material = transparentMat;
        spawnedLetter.transform.SetParent(hands);
    }
}