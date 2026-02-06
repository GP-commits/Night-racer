using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Speed")]
    public float maxForwardSpeed = 22f;
    public float maxReverseSpeed = 10f;
    public float acceleration = 10f;
    public float brakeForce = 6f;

    [Header("CHAOS DRIFT")]
    public float chaosSpeed = 7f;
    public float sideSlipPower = 16f;
    public float spinPower = 420f;
    public float gripRecovery = 0.35f;

    float currentSpeed = 0f;
    float slip = 0f;            
    float spinVelocity = 0f;   

    bool forwardPressed;
    bool reversePressed;
    bool leftPressed;
    bool rightPressed;

    void Update()
    {
   
        if (forwardPressed)
            currentSpeed += acceleration * Time.deltaTime;
        else if (reversePressed)
            currentSpeed -= acceleration * Time.deltaTime;
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, brakeForce * Time.deltaTime);

        currentSpeed = Mathf.Clamp(currentSpeed, -maxReverseSpeed, maxForwardSpeed);

      
        float steer = 0f;
        if (leftPressed) steer -= 1f;
        if (rightPressed) steer += 1f;

        bool highSpeed = Mathf.Abs(currentSpeed) > chaosSpeed;

     
        if (highSpeed && steer != 0f)
        {
 
            slip += steer * Time.deltaTime * 7f;

            slip += Random.Range(-0.4f, 0.4f);

            float moveDir = Mathf.Sign(currentSpeed);
            spinVelocity += steer * spinPower * moveDir * Time.deltaTime;
        }
        else
        {
            slip = Mathf.MoveTowards(slip, 0f, gripRecovery * Time.deltaTime);
            spinVelocity = Mathf.MoveTowards(spinVelocity, 0f, gripRecovery * 80f * Time.deltaTime);
        }

        slip = Mathf.Clamp(slip, -3f, 3f);
        spinVelocity = Mathf.Clamp(spinVelocity, -900f, 900f);

        Vector3 forwardMove = transform.forward * currentSpeed * Time.deltaTime;

        Vector3 sidewaysMove = transform.right * slip * sideSlipPower * Time.deltaTime;

        transform.position += forwardMove + sidewaysMove;

        transform.Rotate(Vector3.up, spinVelocity * Time.deltaTime);
    }

    public void ForwardDown() { forwardPressed = true; }
    public void ForwardUp() { forwardPressed = false; }

    public void ReverseDown() { reversePressed = true; }
    public void ReverseUp() { reversePressed = false; }

    public void LeftDown() { leftPressed = true; }
    public void LeftUp() { leftPressed = false; }

    public void RightDown() { rightPressed = true; }
    public void RightUp() { rightPressed = false; }
}
