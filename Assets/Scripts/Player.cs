using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput input;
    Vector2 movement;
    Vector3 direction;
    public float turnSpeed;
    public float speed;
    Rigidbody rb;
    CharacterController characterController;
    public Transform firePos;
    public float bulletVelocity = 15;
    //
    public bool dusmanMenzil;

    private void OnEnable()
    {
        input.Enable();
        input.Player.Attack.performed += ctx => Fire();
    }
    private void OnDisable()
    {
        input.Disable();
        input.Player.Attack.performed -= ctx => Fire();
    }
    void Awake()
    {
        input = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }



    private void FixedUpdate()
    {
        Movement();


    }

    public void Fire()
    {

        GameObject bullet = ObjectPool.instance.GetBullet();

        if (bullet != null)
        {
            bullet.transform.position = firePos.position;
            bullet.transform.rotation = firePos.rotation;
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.linearVelocity = firePos.forward * bulletVelocity;
            bullet.SetActive(true);
        }
    }


    private void Movement()
    {
        movement = input.Player.Move.ReadValue<Vector2>();
        direction = new Vector3(movement.x, 0, movement.y).normalized;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        forward = forward * direction.z;
        right = right * direction.x;

        if (direction.x != 0 || direction.z != 0)
        {
            float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);

        }



        Vector3 verticalDirection = Vector3.up * direction.y;
        Vector3 horizontalDirection = forward + right;
        Vector3 move = verticalDirection + horizontalDirection;


        characterController.Move(speed * Time.deltaTime * move);
        //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, Time.deltaTime);
        //transform.rotation = Quaternion.Euler(0, angle, 0);
    }


    private void Rotation()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        
        forward = forward * direction.z;
        right = right * direction.x;

        if (direction.x != 0 || direction.x != 0)
        {
            float targetAngle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            
        }
    }


    void Update()
    {

    }
}
