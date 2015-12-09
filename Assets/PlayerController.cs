﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;


public enum AnimationState
{
    WalkLeft,
    WalkRight,
    WalkUp,
    WalkDown,
    AttackLeft,
    AttackRight,
    AttackUp,
    AttackDown,
    Dead,
    IdleLeft,
    IdleRight,
    IdleUp,
    IdleDown
}

public enum SpriteDirection
{
    Left,
    Right,
    Up,
    Down,
}

public class PlayerController : MonoBehaviour {

    public List<Sprite> walkAnimationList;
    public List<Sprite> otherAnimationList;

    public AnimationState animationState;
    public SpriteDirection spriteDirection;

    public float animTime;
    public float animTimer;

    public SpriteRenderer playerSR;
    private int walkCounter;

    private Text debugText;

    public Rigidbody2D playerRigidBody;
   
	// Use this for initialization
	void Start () {
        animTimer = .1f;
        playerSR = gameObject.GetComponent<SpriteRenderer>();
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();

        debugText = GameObject.FindGameObjectWithTag("DebugText").GetComponent<Text>();
        LoadAnimations();
	}

    private void LoadAnimations()
    {

        walkAnimationList = Resources.LoadAll<Sprite>("DwarfWalk").ToList();
        otherAnimationList = Resources.LoadAll<Sprite>("DwarfOther").ToList();

        spriteDirection = SpriteDirection.Down;
        animationState = AnimationState.IdleDown;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateAnimTimer();
        UpdateMovement();
        //UpdateControl();

        debugText.text = animationState.ToString();
	}

    private void UpdateAnimTimer()
    {
        animTime -= Time.deltaTime;
        if (animTime <= 0)
        {
            animTime = animTimer;
            SetSprite();
        }
    }

    private void UpdateMovement()
    {
        var vel = playerRigidBody.velocity;
        var velx = Mathf.Round(vel.x);
        var vely = Mathf.Round(vel.y);

        if (velx == 0 && vely == 0)
        {
            switch (spriteDirection)
            {
                case SpriteDirection.Up:
                    animationState = AnimationState.IdleUp;
                    break;
                case SpriteDirection.Down:
                    animationState = AnimationState.IdleDown;
                    break;
                case SpriteDirection.Left:
                    animationState = AnimationState.IdleLeft;
                    break;
                case SpriteDirection.Right:
                    animationState = AnimationState.IdleRight;
                    break;
            }
        }
        else if (Mathf.Abs(vely) >= Mathf.Abs(velx))
        {
            if (vely > 0)
            {
                spriteDirection = SpriteDirection.Up;
                animationState = AnimationState.WalkUp;
            }
            else
            {
                spriteDirection = SpriteDirection.Down;
                animationState = AnimationState.WalkDown;
            }
        }
        else if (Mathf.Abs(vely) < Mathf.Abs(velx))
        {
            if (velx > 0)
            {
                spriteDirection = SpriteDirection.Right;
                animationState = AnimationState.WalkRight;
            }
            else
            {
                spriteDirection = SpriteDirection.Left;
                animationState = AnimationState.WalkLeft;
            }
        }
        else
        {
            switch (spriteDirection)
            {
                case SpriteDirection.Up:
                    animationState = AnimationState.IdleUp;
                    break;
                case SpriteDirection.Down:
                    animationState = AnimationState.IdleDown;
                    break;
                case SpriteDirection.Left:
                    animationState = AnimationState.IdleLeft;
                    break;
                case SpriteDirection.Right:
                    animationState = AnimationState.IdleRight;
                    break;
            }
        }
    }

    private void UpdateControl()
    {
        var vAxis = Input.GetAxis("Vertical");
        var hAxis = Input.GetAxis("Horizontal");

        if (Mathf.Abs(vAxis) > Mathf.Abs(hAxis))
        {
            if (vAxis > 0)
            {
                spriteDirection = SpriteDirection.Up;
                animationState = AnimationState.WalkUp;
            }
            else
            {
                spriteDirection = SpriteDirection.Down;
                animationState = AnimationState.WalkDown;
            }
        }
        else if (Mathf.Abs(vAxis) < Mathf.Abs(hAxis))
        {
            if (hAxis > 0)
            {
                spriteDirection = SpriteDirection.Right;
                animationState = AnimationState.WalkRight;
            }
            else
            {
                spriteDirection = SpriteDirection.Left;
                animationState = AnimationState.WalkLeft;
            }
        }
        else
        {
            switch (spriteDirection)
            {
                case SpriteDirection.Up:
                    animationState = AnimationState.IdleUp;
                    break;
                case SpriteDirection.Down:
                    animationState = AnimationState.IdleDown;
                    break;
                case SpriteDirection.Left:
                    animationState = AnimationState.IdleLeft;
                    break;
                case SpriteDirection.Right:
                    animationState = AnimationState.IdleRight;
                    break;

            }
          
        }

        if (Input.GetKey(KeyCode.Space))
        {
            switch (spriteDirection)
            {
                case SpriteDirection.Up:
                    animationState = AnimationState.AttackUp;
                    break;
                case SpriteDirection.Down:
                    animationState = AnimationState.AttackDown;
                    break;
                case SpriteDirection.Left:
                    animationState = AnimationState.AttackLeft;
                    break;
                case SpriteDirection.Right:
                    animationState = AnimationState.AttackRight;
                    break;
            }
        }
           
    }

    
    private void SetSprite()
    {
        switch (animationState)
        {
            case AnimationState.IdleDown:
                playerSR.sprite = otherAnimationList[0];
                break;
            case AnimationState.IdleUp:
                playerSR.sprite = otherAnimationList[9];
                break;
            case AnimationState.IdleLeft:
                playerSR.sprite = otherAnimationList[6];
                break;
            case AnimationState.IdleRight:
                playerSR.sprite = otherAnimationList[3];
                break;

            case AnimationState.AttackUp:
                playerSR.sprite = otherAnimationList[10];
                break;
            case AnimationState.AttackDown:
                playerSR.sprite = otherAnimationList[1];
                break;
            case AnimationState.AttackLeft:
                playerSR.sprite = otherAnimationList[7];
                break;
            case AnimationState.AttackRight:
                playerSR.sprite = otherAnimationList[4];
                break;

            case AnimationState.WalkLeft:
                if (walkCounter != 3 && walkCounter != 4 && walkCounter != 5)
                {
                    walkCounter = 5;
                }
                setWalkSprite();
                break;
            case AnimationState.WalkRight:
                if (walkCounter != 6 && walkCounter != 7 && walkCounter != 8)
                {
                    walkCounter = 6;
                }
                setWalkSprite();
                break;
            case AnimationState.WalkUp:
                if (walkCounter != 9 && walkCounter != 10 && walkCounter != 11)
                {
                    walkCounter = 9;
                }
                setWalkSprite();
                break;
            case AnimationState.WalkDown:
                if (walkCounter != 0 && walkCounter != 1 && walkCounter != 2)
                {
                    walkCounter = 0;
                }
                setWalkSprite();
                break;
            default:
                playerSR.sprite = otherAnimationList[0];
                break;
                     
        }
    }

    private void setWalkSprite()
    {
        switch (walkCounter)
        {
            case 0:
                walkCounter = 1;
                break;
            case 1:
                walkCounter = 2;
                break;
            case 2:
                walkCounter = 0;
                break;
            case 3:
                walkCounter = 5;
                break;
            case 4:
                walkCounter = 3;
                break;
            case 5:
                walkCounter = 4;
                break;
            case 6:
                walkCounter = 7;
                break;
            case 7:
                walkCounter = 8;
                break;
            case 8:
                walkCounter = 6;
                break;
            case 9:
                walkCounter = 10;
                break;
            case 10:
                walkCounter = 11;
                break;
            case 11:
                walkCounter = 9;
                break;
            default:
                walkCounter = 0;
                break;

        }
        playerSR.sprite = walkAnimationList[walkCounter];
    }
}
