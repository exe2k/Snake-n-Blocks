using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Obstacle : Collectable
{
    [SerializeField] AudioClip destroySound;
    int rnd_object;

    protected override void Init()
    {
        rnd_object = Random.Range(0, 3);
       // Validate();
        UpdateVisual();
        isDestroyAfterSoundPlayed = false;
    }

    protected override void CollectExtraCode()
    {
        Player.instance.TakeDamage();
        points--;
        UpdateVisual();
        if (points <= 0)
        {
            var sfx = new GameObject();
            sfx.transform.position = transform.position;
            AudioSource sfx_as = sfx.AddComponent<AudioSource>();
            sfx_as.spatialBlend = _as.spatialBlend;
            sfx_as.PlayOneShot(destroySound);
            TimeKiller sfx_as_timekiller = sfx_as.gameObject.AddComponent<TimeKiller>();
            sfx_as_timekiller.time = 3;
            Destroy(gameObject);
        }
        else
        {
            Player.instance.HitBack();
        }
    }

    protected override void SetPoints()
    {
        points = Random.Range(1, CONST.OBSTACLE_MAX_POINTS);

    }

    private void Validate()
    {
        var objs = transform.parent.GetComponentsInChildren<Obstacle>();
        if (objs.Length == 3)
        {
            objs[rnd_object].points = Mathf.Clamp(points, 1, CONST.OBSTACLE_MAX_POINTS / 5);
            objs[rnd_object].UpdateVisual();
        }

    }


    private void UpdateVisual()
    {
        var mat = GetComponent<MeshRenderer>().material;
        mat.SetFloat("_Points", points);
        mat.SetFloat("_MaxPoints", CONST.OBSTACLE_MAX_POINTS);

        UpdateText();
    }

    private void OnEnable()
    {
        try { UpdateVisual(); }
        catch { };
    }


}
