using UnityEngine;

public class Finish : MonoBehaviour
{

    [SerializeField]ParticleSystem paricleSystem;

    private void Start()
    {
        paricleSystem.Stop();
    }
    private void OnTriggerEnter(Collider col)
    {
        Player player = null;
        player = col.GetComponentInParent<Player>();
        player?.Finish();

        paricleSystem.Play();
    }
}
