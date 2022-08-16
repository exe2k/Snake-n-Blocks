using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Obstacle : Collectable
{
    [SerializeField] AudioClip destroySound;

    protected override void Init()
    {
        Validate();
        UpdateVisual();
        isDestroyAfterSoundPlayed = false;
    }

    protected override void CollectExtraCode()
    {
        Player.instance.TakeDamage();
        points--;
        if (points <= 0)
        {
            var sfx = new GameObject();
            sfx.transform.position = transform.position;
            AudioSource sfx_as = sfx.AddComponent<AudioSource>();
            sfx_as.spatialBlend = _as.spatialBlend;
            sfx_as.PlayOneShot(destroySound);
            Destroy(gameObject);
        }
    }

    protected override void SetPoints()
    {
        points = Random.Range(1, CONST.OBSTACLE_MAX_POINTS);
    }

    private void Validate()
    {
        var objs = transform.parent.GetComponentsInChildren<Obstacle>();
        if (objs.Length > 2 && Random.Range(1,10)>5)
        {
           objs[0].points = Mathf.Clamp(points, 1, CONST.OBSTACLE_MAX_POINTS / 5);
        }
    }


    private void UpdateVisual()
    {
        var mat = GetComponent<MeshRenderer>().material;
        mat.SetFloat("_Points", points);
        mat.SetFloat("_MaxPoints", CONST.OBSTACLE_MAX_POINTS);

        UpdateText();
    }
}
