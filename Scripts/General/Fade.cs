using UnityEngine;

public class Fade : MonoBehaviour
{
  private SpriteRenderer sprite;
  private float fadeInSpeed = 0.05f;
  void Start()
  {
    sprite = GetComponent<SpriteRenderer>();
  }

  void Update()
  {

  }
  void FadeBackground()
  {
    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - fadeInSpeed);
  }
  void FadeOutBackground()
  {
    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1.5f);
  }
  private void OnTriggerStay2D(Collider2D other)
  {
    if (other.CompareTag("Player") && sprite.color.a > 0)
    {
      Invoke("FadeBackground", 0.1f);
    }
  }
  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      FadeOutBackground();
    }
  }
}