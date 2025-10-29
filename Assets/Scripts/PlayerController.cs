using UnityEditor.Experimental.GraphView;
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

    public float toolWaitTime = .5f;
    private float toolWaitCounter;

    public Transform toolIndicator;
    public float toolrange = 3f;
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
        
        toolIndicator.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        toolIndicator.position = new Vector3(toolIndicator.position.x, toolIndicator.position.y, 0f);

        if(Vector3.Distance(toolIndicator.position, transform.position) > toolrange)
        {
            Vector2 direction = toolIndicator.position - transform.position;
            direction = direction.normalized * toolrange;
            toolIndicator.position = transform.position + new Vector3(direction.x, direction.y, 0f);
        }

        toolIndicator.position = new Vector3(Mathf.FloorToInt(toolIndicator.position.x)+.5f, Mathf.FloorToInt(toolIndicator.position.y)+.5f, 0f);
    }
    void HandleInput()
    {
        HandleAction();
        HandleToolSwitch();
        HandleMovement();

    }
    void HandleMovement()
    {
        if (toolWaitCounter> 0)
        {
            // how long the player has to wait before being able to move again after using a tool
            toolWaitCounter -= Time.deltaTime;
            theRB.linearVelocity = Vector2.zero;
        }
        else
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

        //block = FindFirstObjectByType<GrowBlock>();

        //block.PloughSoil();

        block = GridController.instance.GetBlock(toolIndicator.position.x - .5f,toolIndicator.position.y - .5f);
        toolWaitCounter = toolWaitTime;
        if (block!=null)
        {

            switch (currentTool)
            {
                case Tooltype.plough:
                        block.PloughSoil();
                        anim.SetTrigger("usePlough");
                    break;
                case Tooltype.wateringCan:
                        block.WaterSoil();
                        anim.SetTrigger("useWateringCan");
                    break;
                case Tooltype.seeds:
                    block.PlantCrop();

                    break;
                case Tooltype.basket:
                    block.HarvestCrop();
                    break;
            }
        }
    }
}
