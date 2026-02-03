using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int playerScore;
    public TextMeshProUGUI playerScoreTMP;
    void Start()
    {
        playerScore = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple"))
        {
            playerScore++;
            playerScoreTMP.text = ":" + playerScore;
            Destroy(collision.gameObject);
        }
    }
}
