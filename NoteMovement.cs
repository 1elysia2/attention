using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float speed = 2f;
    public bool moveTowardsNegativeZ = true; // Ĭ���� Z �Ḻ�����ƶ�
    public Note note;
    private GameObject tipUI1;
    public GameObject rateUI;
    private void Start()
    {
        tipUI1 = GameObject.FindGameObjectWithTag("tip1");
        if (rateUI != null)
        {
            rateUI.SetActive(true);
        }
        if (tipUI1 != null)
        {
            tipUI1.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<HoldNote>(out var h ))
        {
            Destroy(gameObject);
        }
       
    }
    void Update()
    {
        if (!PicoUIController.paused)
        {   // �����ƶ�����Z ��������򸺷���
            Vector3 direction = Vector3.back;

            // �� Z ���ƶ�
            transform.Translate(direction * speed * Time.deltaTime);

            if (tipUI1 != null&&transform.position.z < 6.0f)
            {
                //if (rateUI != null)
                //{
                //    rateUI.SetActive(false);
                //}
                if(!tipUI1.transform.GetChild(0).gameObject.activeSelf) 
                tipUI1.transform.GetChild(0).gameObject.SetActive(true);

                Debug.Log("stop");
                Time.timeScale = 0.4f;
            }

            if(!note.isError && transform.position.z<2.3f)
            {
                Calculate.notHit();
                AttentionEventHub.Instance.OnNoteMiss();

                Time.timeScale = 1f;
                if(tipUI1 != null)
                {
                    tipUI1.transform.GetChild(0).gameObject.SetActive(false);
                }
                if (rateUI != null)
                {
                    rateUI.SetActive(true);
                }
                Destroy(gameObject);
            }
        }
        if (PicoUIController.Gameover)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
        if (tipUI1 != null)
        {
            tipUI1.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (rateUI != null)
        {
            rateUI.SetActive(true);
        }
    }
}

