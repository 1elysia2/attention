using UnityEngine;

public class AttentionEventHub : MonoBehaviour
{
    public static AttentionEventHub Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OnNoteHit(Judgment judgment)
    {
        if (AttentionStats.Instance != null)
        {
            AttentionStats.Instance.RecordHit(judgment);
        }
    }

    public void OnNoteMiss()
    {
        if (AttentionStats.Instance != null)
        {
            AttentionStats.Instance.RecordMiss();
        }
    }
}
