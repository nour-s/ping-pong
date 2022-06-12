using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DrawLine : NetworkBehaviour
{
    public GameObject lineRendererPrefab;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!lineRenderer)
        {
            lineRenderer = GameObject.Instantiate(lineRendererPrefab).GetComponent<LineRenderer>();
        }
        Vector2 curPos = transform.position;
        var rb = GetComponent<Rigidbody2D>();
        var vel = rb.velocity;

        // Debug.DrawRay(ballPrevPos.normalized * 10, curPos.normalized, Color.red);
        // Debug.DrawRay(curPos, vel.normalized * 5, Color.red);
        lineRenderer.SetPosition(0, curPos);
        lineRenderer.SetPosition(1, curPos + vel.normalized * 5);
        lineRenderer.material.mainTextureScale = new Vector2(1f / lineRenderer.startWidth, 1.0f);

        // Vector2 newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(Time.timeSinceLevelLoad * 4f, 0f));
        lineRenderer.material.SetTextureScale("_MainTex", new Vector2(curPos.magnitude, 1f));
    }
}
