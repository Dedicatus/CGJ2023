using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    public Wall[] walls;

    public List<GameObject> destroyBodyPrefabs = new();

    private void Awake()
    {
        walls = FindObjectsOfType<Wall>();
    }

    private void Start()
    {
        FindNearestWall(transform);
    }

    private void FindNearestWall(Transform target)
    {
        Transform nearestWall = null;
        var minDistance = Mathf.Infinity;
        var currentPosition = target.position;

        // 遍历所有墙体，找到最近的墙体
        foreach (var wall in walls)
        {
            var distance = Vector3.Distance(currentPosition, wall.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestWall = wall.transform;
            }
        }

        // 如果找到了最近的墙体，开始移动并淡出
        if (nearestWall != null)
        {
            StartCoroutine(MoveAndFade(target, nearestWall));
        }
    }

    private IEnumerator MoveAndFade(Transform target, Transform targetWall)
    {
        var duration = 2f; // 移动和淡出的持续时间
        var elapsedTime = 0f;
        var startPosition = target.position;
        // direction
        // var direction = (targetWall.position - startPosition).normalized;
        // // 确定上下左右具体方向
        // var up = Vector3.Dot(direction, Vector3.up);
        // var down = Vector3.Dot(direction, Vector3.down);
        // var left = Vector3.Dot(direction, Vector3.left);
        // var right = Vector3.Dot(direction, Vector3.right);
        // var temp = new[] { up, down, left, right }.ToList();
        // var maxIndex = temp.IndexOf(temp.Max());
        // var activeDirection = new[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right }[maxIndex];
        // var endPosition = targetWall.position + activeDirection * 0.5f;


        var allRender = GetComponentsInChildren<SpriteRenderer>();
        var allColors = allRender.ToList().Select(t => t.color).ToArray();

        while (elapsedTime < duration)
        {
            // 移动物体向目标墙体
            target.position = Vector3.Lerp(startPosition, targetWall.position, elapsedTime / duration);
            // target.position += activeDirection * (elapsedTime / duration);
            for (int i = 0; i < allColors.Length; i++)
            {
                var startColor = allColors[i];
                var newColor = Color.Lerp(startColor, Color.clear, elapsedTime / duration);
                allRender[i].color = newColor;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (var i = destroyBodyPrefabs.Count - 1; i >= 0; i--)
        {
            Destroy(destroyBodyPrefabs[i].gameObject);
        }

        Destroy(gameObject);
    }
}