using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Vector2 movement;
    Rigidbody rb;

    public Animator anim;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 3f;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {

        // mantem o modelo do king no 0 em xyz
        //anim.transform.localPosition = Vector3.zero;

        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("isSlide", true);
        }else{
            anim.SetBool("isSlide", false);
        }
    }

    private void FixedUpdate() {
        HandleMovement();
        RotateYPlayer();
    }

    public void Move(InputAction.CallbackContext context){
        movement = context.ReadValue<Vector2>();
    }

    void HandleMovement(){

        Vector3 currentPosition = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
        Vector3 newPostion = currentPosition + moveDirection * (moveSpeed * Time.deltaTime);

        // limite horizontal de movimento do player
        newPostion.x = Mathf.Clamp(newPostion.x, -xClamp, xClamp);
        newPostion.z = Mathf.Clamp(newPostion.z, -zClamp, zClamp);

        rb.MovePosition(newPostion);
    }

    void RotateYPlayer(){

        if(anim.GetBool("isSlide")){
            transform.position = new Vector3(transform.position.x, 0.17f, transform.position.z);
            transform.eulerAngles = new Vector3(0f, 49.743f, 0f);
            return;
        }

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

        if(movement.x < 0f){
            transform.eulerAngles = new Vector3(0f, -20, 0f);
            //transform.Rotate(0f, -35f, 0f);
        }else if(movement.x > 0f){
            transform.eulerAngles = new Vector3(0f, 20, 0f);
            //transform.Rotate(0f, 35f, 0f);
        }else{
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        
    }
}
