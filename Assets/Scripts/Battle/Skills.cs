using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    public Animator animator;
    public Transform Skill1FirePoint;
    public Transform Skill3LaserFirePoint;
    public GameObject bulletPrefab;
    private GameObject currentBullet;
    public GameObject laserPrefab;
    private GameObject currentLaser;

    public enum SkillState { Ready, Charging, Attacking, SwordAttack, LaserAttack, Cancelled }
    public enum SwordState { NoSword, SwordUp, SwordDown }
    public enum PlayerState { Nobody, Luana }
    public PlayerState characterName;
    public SwordState swordState;

    private SkillState currentState = SkillState.Ready;   

    private GameObject battleSystem;
    private Vector3 enemyPosition;
    public int Rythm;

    private GameObject rythmScript;

    public GameObject SwordRythmPrefab;
    public GameObject currentSwordRythmPrefab;
    public Vector3 SwordRythmPrefabPosition { get; private set; }
    public GameObject SwordSlice;


    [System.Obsolete]
    void Start()
    {
        battleSystem = GameObject.FindWithTag("battlesystem");
        rythmScript = GameObject.FindWithTag("RythmusScript");
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(currentState == SkillState.Charging)
        {
            if (Input.GetButtonDown("Timing") && rythmScript.GetComponent<RythmScript>().CorrectTiming == 1)
            {
                Attack();
            }
        }

        if (currentState == SkillState.SwordAttack)
        {
            if (Input.GetButtonDown("Timing") && rythmScript.GetComponent<RythmScript>().CorrectTiming == 1)
            {
                Attack();
                currentSwordRythmPrefab.GetComponent<Animator>().Play("SwordCorrect");
            }
        }

        if(currentState == SkillState.LaserAttack)
        {

        }
    }

    public void SkillCharging()
    {
            currentState = SkillState.Charging;
            animator.Play("Luana Stand Left Skill Attack Weapon Charge");
    }                                                                                                    

    public void Attack()
    {
        if (currentState == SkillState.Charging)
        {
            animator.Play("Luana Stand Left Skill Attack Weapon Shoot");
            StartCoroutine(MoveBulletToEnemy(currentBullet, battleSystem.GetComponent<BattleSystem>().enemyPosition.position, 1f));
            battleSystem.GetComponent<BattleSystem>().PlayerAttack();
        }
        if (currentState == SkillState.SwordAttack)
        {
            if (swordState == SwordState.SwordDown)
            {
                animator.Play("Luana Swort Attack Up");
                battleSystem.GetComponent<BattleSystem>().PlayerAttack();
                swordState = SwordState.SwordUp;
                StartCoroutine(battleSystem.GetComponent<BattleSystem>().PerformAttack(battleSystem.GetComponent<BattleSystem>().playerUnit, battleSystem.GetComponent<BattleSystem>().SelectedEnemyUnit, battleSystem.GetComponent<BattleSystem>().SelectedEnemyHPSlider));
            }

            else
            {
                animator.Play("Luana Swort Attack Down");
                battleSystem.GetComponent<BattleSystem>().PlayerAttack();
                swordState = SwordState.SwordDown;
                StartCoroutine(battleSystem.GetComponent<BattleSystem>().PerformAttack(battleSystem.GetComponent<BattleSystem>().playerUnit, battleSystem.GetComponent<BattleSystem>().SelectedEnemyUnit, battleSystem.GetComponent<BattleSystem>().SelectedEnemyHPSlider));
            }
        }
    }


    public void CancelAttack()
    {
        if (currentState == SkillState.Charging)
        {
            Destroy(currentBullet);
            animator.Play("Luana Stand Left Skill Attack Weapon Fail");
            battleSystem.GetComponent<BattleSystem>().CheckEnemyStatusAndContinue();
        }
        if(currentState == SkillState.SwordAttack)
        {
            Destroy(currentSwordRythmPrefab);
            animator.Play("Luana Sword Cancel");
            swordState = SwordState.NoSword;
        }
        currentState = SkillState.Ready;
    }

    private IEnumerator MoveBulletToEnemy(GameObject bullet, Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = bullet.transform.position;

        while (time < duration)
        {
            bullet.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        bullet.transform.position = targetPosition;

        yield return StartCoroutine(battleSystem.GetComponent<BattleSystem>().PerformAttack(battleSystem.GetComponent<BattleSystem>().playerUnit, battleSystem.GetComponent<BattleSystem>().SelectedEnemyUnit, battleSystem.GetComponent<BattleSystem>().SelectedEnemyHPSlider));


        yield return new WaitForSeconds(0.5f);
        Animator bulletAnimator = bullet.GetComponent<Animator>();
        if (bulletAnimator != null)
        {
            bulletAnimator.Play("ShootExplode");
        }

        Destroy(bullet, 0.5f);
    }

    public void MoveToTheLeft()
    {
        Debug.Log("Player moves to the left");
        animator.Play("Luana Move Left Battle Prepare Attack");
        Vector3 enemyPosition = battleSystem.GetComponent<BattleSystem>().enemyPosition.position;
        StartCoroutine(MovePlayerToEnemy(this.gameObject, battleSystem.GetComponent<BattleSystem>().SelectedEnemyVariables, 1f));
    }

    public void MoveToTheRight()
    {
        Vector3 originalPosition = battleSystem.GetComponent<BattleSystem>().playerPosition.position;
        StartCoroutine(MovePlayerToOriginalPosition(this.gameObject, originalPosition, 1f));
        Debug.Log("Player moves to the right");
    }

    private IEnumerator MovePlayerToOriginalPosition(GameObject player, Vector3 originalPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = player.transform.position;

        while (time < duration)
        {
            player.transform.position = Vector3.Lerp(startPosition, originalPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        player.transform.position = originalPosition;
    }



    private IEnumerator MovePlayerToEnemy(GameObject player, GameObject enemy, float duration)
    {
        SpriteRenderer enemySpriteRenderer = enemy.GetComponent<SpriteRenderer>();
        if (enemySpriteRenderer == null)
        {
            Debug.LogError("Enemy does not have a SpriteRenderer component.");
            yield break;
        }

        float time = 0;
        Vector3 startPosition = player.transform.position;
        Vector3 targetPosition = enemy.transform.position;
        float stopDistance = enemySpriteRenderer.bounds.size.x;
        Vector3 direction = (targetPosition - startPosition).normalized;
        targetPosition -= direction * stopDistance;

        while (time < duration)
        {
            float t = time / duration;
            float smoothT = Mathf.SmoothStep(0.0f, 1.0f, t);
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, smoothT);
            time += Time.deltaTime;
            yield return null;
        }

        player.transform.position = targetPosition;

        if (currentState == SkillState.SwordAttack)
        {
            animator.Play("Luana Sword Charging");
            swordState = SwordState.SwordDown;
        }
    }



    public void SkillSword()
    {
        currentState = SkillState.SwordAttack;
        if (swordState == SwordState.NoSword)
        {
            swordState = SwordState.SwordDown;
        }
    }

    public void LaserAttack()
    {
        currentLaser = Instantiate(laserPrefab, Skill1FirePoint.position, Skill1FirePoint.rotation);
    }

    public void BulletAppears()
    {
        currentBullet = Instantiate(bulletPrefab, Skill3LaserFirePoint.position, Skill3LaserFirePoint.rotation);
    }

    public void SwordSliceAppears()
    {
        currentSwordRythmPrefab = Instantiate(SwordSlice, battleSystem.GetComponent<BattleSystem>().enemyPosition.position, Quaternion.identity);
    }

    public void Ready()
    {
        currentState = SkillState.Ready;
    }

    public void EnemyTurn()
    {
        battleSystem.GetComponent<BattleSystem>().CheckEnemyStatusAndContinue();
    }

    public void SwordRythmPrefabAppears()
    {
        currentSwordRythmPrefab = Instantiate(battleSystem.GetComponent<BattleSystem>().SwordRythmPrefab, battleSystem.GetComponent<BattleSystem>().enemyPosition.position, Quaternion.identity);
    }
}
