using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        Player player = null;
        player = col.GetComponentInParent<Player>();
        player?.Finish();
    }
}
