using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    #region Health
    public Slider healthBar;
    public float maxHealth = 100;
    private float currentHealth;
    #endregion

    public float speed = 5f;
    public float mouseSensitivity = 100f;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Transform playerCamera;
    private float xRotation = 0f;
    private Animator anm;
    private CharacterController characterController;
    public CinemachineCamera thirdPersonFollow;
    private PlayerInput inputActions;
    public Transform firePos;
    public Transform characterFollow, aimFollow;
    public float bulletVelocity = 32;
    public float fireRate = 2;
    public LayerMask aimColliderLayerMask, takeableObjects;
    private Vector3 mouseWorldPos;
    public bool isAiming;
    public float itemsCount;
    public float aimDistance=2, followDistance=4;


    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Attack.started += ctx => StartFire();
        inputActions.Player.Attack.canceled += ctx => StopFire();
        inputActions.Player.Aim.performed += ctx => AimControl();
        inputActions.Player.Interact.started += ctx => InteractObject();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Attack.started -= ctx => StartFire();
        inputActions.Player.Attack.canceled -= ctx => StopFire();
        inputActions.Player.Aim.performed -= ctx => AimControl();
        inputActions.Player.Interact.started -= ctx => InteractObject();
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        playerCamera = Camera.main.transform;
        inputActions = new PlayerInput();
        anm = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TakeDamage()
    {
        currentHealth -= 10;
        healthBar.value = currentHealth / maxHealth;
        if (currentHealth <= 0) Death();
    }

    public void Death()
    {
        currentHealth = 0;
        Time.timeScale = 0;
    }

    public void AimControl()
    {
        isAiming = !isAiming;

        if (isAiming)
        {
            thirdPersonFollow.Follow = aimFollow;
            thirdPersonFollow.LookAt = aimFollow;
            thirdPersonFollow.GetComponent<CinemachineThirdPersonFollow>().CameraDistance = aimDistance;
        }
        else
        {
            thirdPersonFollow.Follow = characterFollow;
            thirdPersonFollow.LookAt = characterFollow;
            thirdPersonFollow.GetComponent<CinemachineThirdPersonFollow>().CameraDistance = followDistance;
        }
    }

    public void StartFire()
    {
        InvokeRepeating(nameof(Fire), 0f, fireRate);
    }

    private void StopFire()
    {
        CancelInvoke(nameof(Fire));
    }

    private void Fire()
    {
        GameObject bullet = ObjectPool.instance.GetBullet();
        Vector3 aimDir = (mouseWorldPos - firePos.position).normalized;

        if (bullet != null)
        {
            bullet.transform.position = firePos.position;
            bullet.transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.linearVelocity = aimDir * bulletVelocity;
            bullet.SetActive(true);
        }
    }

    private void Update()
    {
        HandleMovement();

        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        lookInput = inputActions.Player.Look.ReadValue<Vector2>();
        Vector2 screen = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screen);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayerMask))
        {
            mouseWorldPos = hit.point;
        }
    }

    public void HandleMovement()
    {
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, -15f, 15f);

        RotateCamera(mouseY);
        transform.Rotate(Vector3.up * mouseX);
        transform.position += moveDirection * speed * Time.deltaTime;
        anm.SetFloat("MoveX", moveInput.x);
        anm.SetFloat("MoveY", moveInput.y);
    }

    void RotateCamera(float mouseY)
    {
        float currentXRotation = characterFollow.localEulerAngles.x > 180f
            ? characterFollow.localEulerAngles.x - 360f
            : characterFollow.localEulerAngles.x;
        float clampedRotationX = Mathf.Clamp(currentXRotation - mouseY, -60f, 60f);
        characterFollow.localEulerAngles = new Vector3(clampedRotationX, characterFollow.localEulerAngles.y, characterFollow.localEulerAngles.z);

        float currentRotation = aimFollow.localEulerAngles.x > 180f
            ? aimFollow.localEulerAngles.x - 360f
            : aimFollow.localEulerAngles.x;
        float clampedRotation = Mathf.Clamp(currentRotation - mouseY, -60f, 60f);
        aimFollow.localEulerAngles = new Vector3(clampedRotation, aimFollow.localEulerAngles.y, aimFollow.localEulerAngles.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack")) TakeDamage();
    }

    public void InteractObject()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f))
        {
            
            switch (hit.collider.tag)
            {

                case "PowerItem":
                    Destroy(hit.collider.gameObject);
                    itemsCount++;
                    GameManager.Instance.IncreaseAmount();
                    break;
            }
            
        }
    }
}
