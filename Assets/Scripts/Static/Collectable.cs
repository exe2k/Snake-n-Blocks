using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public abstract class Collectable : MonoBehaviour
{
    protected int points = 0;
    protected bool isCollected = false;

    [Header("Audio")]
    protected bool isDestroyAfterSoundPlayed = true;
    protected AudioSource _as = null;
    [SerializeField] protected AudioClip clip;


    private void Awake()
    {
        SetPoints();
        TryGetComponent<AudioSource>(out _as);
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
        if (_as.clip != null && !_as.isPlaying && isCollected && isDestroyAfterSoundPlayed)
            Destroy(gameObject);
    }

}
