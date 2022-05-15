using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public GameObject endGameText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            endGameText.SetActive(true);
            Destroy(gameObject);
        }
    }
}
