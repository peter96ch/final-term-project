using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    public float moveSpeed=1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate(){
        if(movementInput != Vector2.zero){
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x , 0));
                if (!success)
                {
                success = TryMove(new Vector2(0 , movementInput.y));
                }
            }

            animator.SetBool("isMoving", success && movementInput.x != 0);
        }
        else
        {
            animator.SetBool("isMoving", false);
            //animator.SetBool("MoveDown", false);
            //animator.SetBool("MoveUp", false);
        }

        if(movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("MoveUp", false);
            animator.SetBool("MoveDown", false);
        }else if(movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("MoveUp", false);  
            animator.SetBool("MoveDown", false);
        }else if(movementInput.x == 0)
        {
            if(movementInput.y != 0){
            if(movementInput.y > 0)
            {
                animator.SetBool("MoveUp", true);
                animator.SetBool("MoveDown", false);

            }else if(movementInput.y < 0)
            {
                animator.SetBool("MoveDown", true);
                animator.SetBool("MoveUp", false);
            }
            }
            else if(movementInput.y == 0)
            {
                animator.SetBool("MoveUp", false);
                animator.SetBool("MoveDown", false);
                animator.SetBool("isMoving", false);
            }
            
        }
        
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
        direction,
        movementFilter,
        castCollisions,
        moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if(count == 0)
        {
            rb.MovePosition(rb.position + moveSpeed * direction * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }

    }

    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2> ();
        Debug.Log("目前按下的方向：" + movementInput);
    }

}
