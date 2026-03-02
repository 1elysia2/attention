using UnityEngine;
using UnityEngine.UI;
public class Note : MonoBehaviour
{
    public float targetTime; // ������Ŀ������ʱ��
    private NoteMovement movement;
    public string noteDirection;
    public bool isError;
    [SerializeField] private Transform arrowVisual; // ָ���ͷ��������

    void Start()
    {
        movement = GetComponent<NoteMovement>(); 

    }


    public void Initialize(string direction,float moveSpeed)
    { 
        noteDirection = direction;
        if(movement==null)
        {
              movement = GetComponent<NoteMovement>(); 
        }
        movement.speed =moveSpeed;
        UpdateVisual();
    }
    public void ChangeSpeed(float newSpeed)
    {
        movement.speed = newSpeed;
    }
    private void UpdateVisual()
    {
        switch (noteDirection)
        {
            case "Up":
                Debug.Log("Up");
                arrowVisual.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case "Down":
                Debug.Log("Down");
                arrowVisual.localEulerAngles = new Vector3(0, 0, 180);
                break;
            case "Left":
                Debug.Log("Left");
                arrowVisual.localEulerAngles = new Vector3(0, 0, 90);
                break;
            case "Right":
                Debug.Log("Right");
                arrowVisual.localEulerAngles = new Vector3(0, 0, -90);
                break;
        }
    }

}