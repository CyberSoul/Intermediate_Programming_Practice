using UnityEngine;

public delegate void OutOfBpundsHandler();

public class ProjectileController : MonoBehaviour
{
    #region Field Declarations

    public Vector2 projectileDirection;
    public float projectileSpeed;
    public bool isPlayers;

    #endregion

    public event OutOfBpundsHandler ProjectileOutOfBounds;

    #region Movement

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.Translate(projectileDirection * Time.deltaTime * projectileSpeed, Space.World);

        if (ScreenBounds.OutOfBounds(transform.position))
        {
            if (ProjectileOutOfBounds != null && isPlayers)
            {
                ProjectileOutOfBounds.Invoke();
            }
                Destroy(gameObject);
        }
    }

    #endregion
}
