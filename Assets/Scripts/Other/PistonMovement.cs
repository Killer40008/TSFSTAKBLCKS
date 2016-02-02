using UnityEngine;
using System.Collections;

public class PistonMovement : MonoBehaviour {


    public enum Movement { Up = 1, Down = -1 }
    public Movement MovementDirection;
    public float speed = 0.2f;

    void FixedUpdate()
    {
        int direction = (int)MovementDirection;
        this.transform.Translate(0, direction * speed, 0);

        const float MIN = -3.69f;
        const float MAX = 2.65f;
        float yClamped = Mathf.Clamp(this.transform.position.y, MIN, MAX);
        this.transform.position = new Vector3(this.transform.position.x, yClamped, this.transform.position.z);

        if (this.transform.position.y == MIN || this.transform.position.y == MAX)
            MovementDirection = MovementDirection == Movement.Down ? Movement.Up : Movement.Down;
    }
}
