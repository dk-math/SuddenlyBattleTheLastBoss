using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public EnemyUIManager enemyUIManager;
    public Transform target;
    public GameObject enemyUIObject;
    public Collider enemyWeaponCollider;
    public Collider enemyBodyCollider;
    public GameObject gameClearText;
    public GameObject restartButton;
    public int maxHp = 100;
    int hp;
    bool isDie;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        enemyUIManager.UpdateHP(hp);
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie) {
            animator.SetTrigger("Die");
            HideCollider();
            Destroy(gameObject, 5f);
            Destroy(enemyUIObject, 5f);
            gameClearText.SetActive(true);
            restartButton.SetActive(true);
            return;
        }


        agent.destination = target.position;
        animator.SetFloat("Distance", agent.remainingDistance);
    }

    public void LookAtTarget() {
        transform.LookAt(target);
    }

    void Damage(int damage) {
        hp -= damage;
        if(hp <= 0) {
            hp = 0;
            isDie = true;
        }
        enemyUIManager.UpdateHP(hp);
        Debug.Log("Enemy残りHP: " + hp);
    }

    public void HideCollider() {
        enemyBodyCollider.enabled = false;
        enemyWeaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        Damager damager = other.GetComponent<Damager>();
        if (damager != null) {
            animator.SetTrigger("Hurt");
            Damage(damager.damage);
        }
    }
}
