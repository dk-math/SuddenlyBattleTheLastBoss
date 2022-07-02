using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    public Collider rightLegCollider;
    public Collider rightArmCollider;
    public Collider bodyCollider;
    public PlayerUIManager playerUIManager;
    public GameObject gameOverText;
    public GameObject restartButton;
    public float moveSpeed;
    float x;
    float z;
    public int maxHp = 100;
    int hp;
    bool isDie;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        playerUIManager.UpdateHP(hp);
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        HideRightArmColliderHit();
        HideRightLegColliderHit();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie) {
            animator.SetTrigger("Die");
            return;
        }

        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            Attack();
        }
        
    }

    void FixedUpdate() {
        Run(x, z);
    }

    private void Run(float x, float z) {
        // Vector3 direction = transform.position + new Vector3(x, 0, z);
        // transform.LookAt(direction);
        // rb.velocity = new Vector3(x, 0, z) * moveSpeed;
        // animator.SetFloat("Speed", rb.velocity.magnitude);

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * z + Camera.main.transform.right * x;
        rb.velocity = moveForward * moveSpeed*2 + new Vector3(0, rb.velocity.y, 0);
        if (moveForward != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
        animator.SetFloat("Speed", rb.velocity.magnitude);

    }

    private void Jump() {
        animator.SetTrigger("Jump");
    }

    private void Attack() {
        animator.SetTrigger("Attack");
    }

    public void ShowRightLegColliderHit() {
        rightLegCollider.enabled = true;
    }

    public void HideRightLegColliderHit() {
        rightLegCollider.enabled = false;
    }

    public void ShowRightArmColliderHit() {
        rightArmCollider.enabled = true;
    }

    public void HideRightArmColliderHit() {
        rightArmCollider.enabled = false;
    }

    public void HideBodyCollider() {
        bodyCollider.enabled = false;
    }

    public void AnimationStop() {
        this.GetComponent<Animator>().enabled = false;
    }

    void Damage(int damage) {
        hp -= damage;
        if(hp <= 0) {
            hp = 0;
            isDie = true;
            HideBodyCollider();
            rb.velocity = Vector3.zero;
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
        }
        playerUIManager.UpdateHP(hp);
        Debug.Log("Player残りHP: " + hp);
    }

    private void OnTriggerEnter(Collider other) {
        Damager damager = other.GetComponent<Damager>();
        if (damager != null) {
            animator.SetTrigger("Hurt");
            Damage(damager.damage);
        }
    }
}
