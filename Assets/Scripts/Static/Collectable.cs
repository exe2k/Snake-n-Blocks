using System.Collections;
using UnityEngine;
using TMPro;

[
    RequireComponent(typeof(AudioSource)),
    RequireComponent(typeof(BoxCollider)),
    RequireComponent(typeof(LoopMover)),
]

public abstract class Collectable : MonoBehaviour
{
    protected int points = 0;
    protected bool isCollected = false;
    public bool isMoving=false;

    TextMeshPro txt = null;

    [Header("Audio")]
    protected bool isDestroyAfterSoundPlayed = true;
    protected AudioSource _as = null;
    [SerializeField] protected AudioClip clip;


    private void Awake()
    {
        SetPoints();
        TryGetComponent<AudioSource>(out _as);
    }

    private void Start()
    {
        txt = GetComponentInChildren<TextMeshPro>();
        if (txt != null) txt.text = points.ToString();
        else print("TMPro not found in " + name);

        if (!isMoving) GetComponent<LoopMover>().enabled = false;

        Init();
    }

    protected virtual void Init()
    {
        //for additional initialization;
    }

    private void Update()
    {
        DestroyObserver();
    }

    protected abstract void SetPoints();

    public virtual int Collect() 
    {
        PlaySound();
        isCollected = true;
        CollectExtraCode();
        return points;
    }
   
    //to avoid of using :base.Collect() and prevent bugs beacause I forgot to write that.
    protected abstract void CollectExtraCode(); 

    private void PlaySound()
    {
        if (_as == null) return;
        _as.PlayOneShot(clip);
    }

    private void DestroyObserver()
    {
        if (isCollected && isDestroyAfterSoundPlayed)
            Destroy(gameObject);
    }

    protected void UpdateText()
    {
        txt.text = points.ToString();
    }

}
