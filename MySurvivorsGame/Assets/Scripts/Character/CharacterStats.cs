using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public DataManager dataManager;
    public UnityData unityData;

    Rigidbody2D rb_;
    public bool isFacingRight_ = true;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        dataManager = GameContainer.Get<DataManager>();
        unityData = GameContainer.Get<UnityData>();
        rb_ = gameObject.GetComponent<Rigidbody2D>();
        SetInitValue();
    }

    protected virtual void Update()
    {        
        Move();
        TryFlip();
    }

    protected virtual void SetInitValue()
    {
        SetInitSkill();
    }

    protected virtual void SetInitSkill()
    {
        
    }

    protected virtual void Move()
    {

    }

    protected virtual void TryFlip()
    {
        Vector2 velocity = rb_.velocity;
        
        if ((velocity.x < 0 && isFacingRight_) || (velocity.x > 0 && !isFacingRight_))
        {
            Flip();
        }
    }

    protected virtual void Flip()
    {
        isFacingRight_ = !isFacingRight_;

        Vector3 newScale = transform.localScale;
        newScale.x *= -1; 
        transform.localScale = newScale;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {

    }

    protected virtual void TryAttack()
    {

    }

    public virtual void TakeDamage(float damage)
    {
        //HP -= damage;
        //if(HP <= 0)
        //{
        //    Dead();
        //}
    }

    protected virtual void Dead()
    {

    }
}
