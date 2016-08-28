using UnityEngine;
using System.Collections;

public class ShipControl : MonoBehaviour
{

    public GameObject[] Laser;
    public float laserDistance;
    public float laserOffset;

    private Rigidbody2D rb2D;
    private float inverseMoveTime = 1f / 0.1f;
    private float pi = Mathf.PI;
    private float lr, la;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        lr = Mathf.Sqrt(laserDistance * laserDistance + laserOffset * laserOffset);
        la = Mathf.Atan(laserOffset / laserDistance);
    }

    void Update()
    {
        if (!CanvasScript.instance.keyboard_up && !CanvasScript.instance.menu_up)
        {
            float f = Input.GetAxisRaw("Vertical") / 10;
            float rotrad = rb2D.rotation * Mathf.PI / 180 + pi / 2;

            //Change ship position with w/d
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(f * Mathf.Cos(rotrad), f * Mathf.Sin(rotrad));
            if (end.x > -9.6f && end.x < 9.6f && end.y > -5.40f && end.y < 5.4f)
            {
                Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
                rb2D.MovePosition(newPostion);
            }

            //shoot lasers
            if (Input.GetKeyDown("space") || (Input.GetMouseButtonDown(0)))
            {
                Vector3 pos1 = transform.position + new Vector3(lr * Mathf.Cos(rotrad + la), lr * Mathf.Sin(rotrad + la), 0);
                Vector3 pos2 = transform.position + new Vector3(lr * Mathf.Cos(rotrad - la), lr * Mathf.Sin(rotrad - la), 0);
                Instantiate(Laser[GameManager.instance.data.color], pos1, transform.rotation);
                Instantiate(Laser[GameManager.instance.data.color], pos2, transform.rotation);
            }
        }
        //rotate towards mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
