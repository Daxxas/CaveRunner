using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour
{
    
    protected float skinWidth = .015f;
    [SerializeField] float distBetweenRays = .025f;
    protected int horizontalRayCount;
    protected int verticalRayCount;
    
    public LayerMask collisionMask;
    
    [HideInInspector] protected float horizontalRaySpacing;
    [HideInInspector] protected float verticalRaySpacing;
    
    [HideInInspector] protected BoxCollider2D collider;
    [HideInInspector] protected RaycastOrigins raycastOrigins;
    
    public virtual void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        
    }

    public virtual void Start()
    {
        CalculateRaySpacing();
    }
    
    protected void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);
        
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    protected void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand (skinWidth * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;
        
        
        
        horizontalRayCount = Mathf.RoundToInt(boundsHeight / distBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / distBetweenRays);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
    
    protected struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
