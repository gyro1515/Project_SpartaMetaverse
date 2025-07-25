using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class CameraControl
{
    public static void CameraClamp(Camera cam, Bounds mapBounds)
    {
        Vector3 camPos = cam.transform.position;
        float camHalfY = cam.orthographicSize;   // Y�� ���� ����
        float camHalfX = camHalfY * cam.aspect; // X�� ���� ���� = orthographicSize * ȭ���(16:9 = 1.7777)

        // ī�޶� �� �� �ִ� ���� ����ؼ� Ŭ����
        float minX = mapBounds.min.x + camHalfX;
        float maxX = mapBounds.max.x - camHalfX; // ���� ��? -> �ش� Ÿ�ϸ� ������Ʈ�� ���ؽ�Ʈ �޴��� 'Compress Tilemap Bounds'�� ����
        float minY = mapBounds.min.y + camHalfY;
        float maxY = mapBounds.max.y - camHalfY;

        //Debug.Log($"minX: {mapBounds.min.x} / maxX: {mapBounds.max.x} / mapBounds.size: {mapBounds.size}");

        camPos.x = Mathf.Clamp(camPos.x, minX, maxX);
        camPos.y = Mathf.Clamp(camPos.y, minY, maxY);
        cam.transform.position = new Vector3(camPos.x, camPos.y, cam.transform.position.z);


    }
    public static void CameraClamp(Camera cam, Tilemap tilemap)
    {
        Vector3 camPos = cam.transform.position;

        // ī�޶� �þ� ���� ũ�� ���
        float camHalfY = cam.orthographicSize;
        float camHalfX = camHalfY * cam.aspect;

        // �� ��� (���� ��ǥ)
        BoundsInt cellBounds = tilemap.cellBounds;

        // ���� �ϴ� �� ��ġ �� ���� ��ǥ
        Vector3 minWorld = tilemap.CellToWorld(cellBounds.min);
        // ���� ��� �� ��ġ + Vector3.one �� �� �� �ٱ��ʱ��� ����
        Vector3 maxWorld = tilemap.CellToWorld(cellBounds.max);

        // Clamp ���
        float minX = minWorld.x + camHalfX;
        float maxX = maxWorld.x - camHalfX;
        float minY = minWorld.y + camHalfY;
        float maxY = maxWorld.y - camHalfY;

        // ī�޶� ��ġ ����
        camPos.x = Mathf.Clamp(camPos.x, minX, maxX);
        camPos.y = Mathf.Clamp(camPos.y, minY, maxY);
        cam.transform.position = new Vector3(camPos.x, camPos.y, cam.transform.position.z);


    }
}
