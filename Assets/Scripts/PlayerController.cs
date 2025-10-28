using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;

    public InputActionReference moveInput, actionInput;

    public Animator anim;

    public enum Tooltype
    {
        plough,
        wateringCan,
        seeds,
        basket
    }

    public Tooltype currentTool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIController.instance.SwitchTool((int)currentTool);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        anim.SetFloat("speed", theRB.linearVelocity.magnitude);

    }
    void HandleInput()
    {
        HandleAction();
        HandleToolSwitch();
        HandleMovement();

    }
    void HandleMovement()
    {
        //theRB.linearVelocity = new Vector2(moveSpeed, 0f);
        theRB.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;

        if (theRB.linearVelocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (theRB.linearVelocity.x > 0f)
        {
            transform.localScale = Vector3.one;
        }
    }
    void HandleToolSwitch()
    {
        bool hasSwitchedTool = false;

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            currentTool++;
            if ((int)currentTool >= 4)
            {
                currentTool = Tooltype.plough;
            }
            hasSwitchedTool = true;
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentTool = Tooltype.plough;
            hasSwitchedTool = true;
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentTool = Tooltype.wateringCan;
            hasSwitchedTool = true;
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentTool = Tooltype.seeds;
            hasSwitchedTool = true;
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            currentTool = Tooltype.basket;
            hasSwitchedTool = true;
        }

        if (hasSwitchedTool == true)
        {
            //Pas la meilleure façon de faire mais bon...
            //FindFirstObjectByType<UIController>().SwitchTool((int)currentTool);
            UIController.instance.SwitchTool((int)currentTool);
        }
    }
    void HandleAction()
    {
        if (actionInput.action.WasPressedThisFrame())
        {
            useTool();
        }
    }
    void useTool()
    {
        GrowBlock block = null;

        block = FindFirstObjectByType<GrowBlock>();

        //block.PloughSoil();
        if(block!=null)
        {

            switch (currentTool)
            {
                case Tooltype.plough:
                        block.PloughSoil();
                    break;
                case Tooltype.wateringCan:
                        
                    break;
                case Tooltype.seeds:
                        
                    break;
                case Tooltype.basket:
                    
                    break;
            }
        }
    }
}
