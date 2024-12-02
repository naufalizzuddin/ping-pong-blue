using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Racket LeftRacket, RightRacket;
    public Rigidbody2D ballRb;
    public float moveSpeed;
    public GameObject Update1, Update2;

    void Start()
    {
        ballRb.velocity = new Vector2(1, 1) * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TagMenager tagMenager = collision.gameObject.GetComponent<TagMenager>();

        GetComponent<AudioSource>().Play();

        if (tagMenager == null)
        {
            return;
        }

        Tag tag = tagMenager.wallTag;

        if (tag.Equals(Tag.leftWall))
        {
            RightRacket.GetScore();
            StartCoroutine(ShowUpdateText(Update2));
        }

        if (tag.Equals(Tag.rightWall))
        {
            LeftRacket.GetScore();
            StartCoroutine(ShowUpdateText(Update1));
        }
        if (tag.Equals(Tag.leftRacket))
        {
            wayBall(collision, 1);
        }
        if (tag.Equals(Tag.rightRacket))
        {
            wayBall(collision, -1);
        }
    }

    private void wayBall(Collision2D collision, int x)
    {
        float a = transform.position.y - collision.gameObject.transform.position.y;
        float b = collision.collider.bounds.size.y;
        float y = a / b;
        ballRb.velocity = new Vector2(x, y) * moveSpeed;
    }

    private IEnumerator ShowUpdateText(GameObject updateText)
    {
    updateText.SetActive(true);
    yield return new WaitForSeconds(1.0f);
    updateText.SetActive(false);
    }

}
