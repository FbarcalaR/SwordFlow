using UnityEngine;

public class CameraController : MonoBehaviour {


    public static CameraController Instance;

    public GameObject Target;
    public int Smoothvalue = 2;
    public float PosY = 1;

    void Update()
    {
        if (Target == null)
        {
            var player = FindObjectOfType<PlayerSingleton>();
            if (player == null) return;
            Target = player.GetComponentInChildren<Rigidbody2D>().gameObject;
        }
        Vector3 Targetpos = new Vector3(Target.transform.position.x, Target.transform.position.y + PosY, -100);
        transform.position = Vector3.Lerp(transform.position, Targetpos, Time.deltaTime * Smoothvalue);
    }
}
