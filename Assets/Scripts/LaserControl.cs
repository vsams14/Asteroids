using UnityEngine;
using System.Collections;

public class LaserControl : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float inverseMoveTime = 1f / 0.1f;
    private float rot;
    private float pi = Mathf.PI;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rot = rb2D.rotation * Mathf.PI / 180 + pi / 2;
    }

    void Update()
    {
        //Keep moving in a straight line until off screen
        Vector2 start = rb2D.position;
        Vector2 end = start + new Vector2(Mathf.Cos(rot), Mathf.Sin(rot));
        Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
        rb2D.MovePosition(newPostion);
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameManager instance = GameManager.instance;
        //If anything hits the laser, destroy the laser
        if (other.name != "lasers" && other.name != "ship" && other.tag != "Planet")
        {
            Destroy(this.gameObject);
            Destroy(this.transform.parent.gameObject);
        }
        if (other.name == "Exit")
        {
            instance.saveData();
            Application.Quit();
        }
        //if other is the play asteroid, start the game
        if (other.name == "Play")
        {
            instance.prev.Add(instance.level);
            instance.level = 2;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }
        //if other is the play asteroid, start the game
        if (other.name == "Back")
        {
            instance.level = instance.prev[instance.prev.Count - 1];
            instance.prev.RemoveAt(instance.prev.Count - 1);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }
        //if other is the menu asteroid, reload, get menu options
        if (other.name == "Color")
        {
            instance.prev.Add(instance.level);
            instance.level = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }
        //if in colour select menu
        if ((other.name.Length == 1) && (other.tag == "Color"))
        {
            int color = int.Parse(other.name);
            instance.data.color = color;
        }
        if (other.tag == "Destructible")
        {
            Destroy(other.gameObject);
            Destroy(other.transform.parent.gameObject);

        }
    }
}
