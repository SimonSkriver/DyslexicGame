using UnityEngine;

public class LetterSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] letter;
    [SerializeField] Transform hands;


    public void SpawnLetter()
    {
        GameObject spawnedLetter = Instantiate(letter[Random.Range(0, letter.Length)], hands.position, hands.rotation);
        spawnedLetter.transform.SetParent(hands);
    }
}
