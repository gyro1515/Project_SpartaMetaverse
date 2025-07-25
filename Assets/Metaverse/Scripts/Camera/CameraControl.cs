using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class CameraControl
{
    public static void CameraClamp(Camera cam, Bounds mapBounds)
    {
        Vector3 camPos = cam.transform.position;
        float camHalfY = cam.orthographicSize;   // Y축 방향 절반
        float camHalfX = camHalfY * cam.aspect; // X축 방향 절반 = orthographicSize * 화면비(16:9 = 1.7777)

        // 카메라가 볼 수 있는 영역 고려해서 클램핑
        float minX = mapBounds.min.x + camHalfX;
        float maxX = mapBounds.max.x - camHalfX; // 오차 왜? -> 해당 타일맵 컴포넌트의 콘텍스트 메뉴의 'Compress Tilemap Bounds'를 실행
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

        // 카메라 시야 절반 크기 계산
        float camHalfY = cam.orthographicSize;
        float camHalfX = camHalfY * cam.aspect;

        // 셀 경계 (정수 좌표)
        BoundsInt cellBounds = tilemap.cellBounds;

        // 좌측 하단 셀 위치 → 월드 좌표
        Vector3 minWorld = tilemap.CellToWorld(cellBounds.min);
        // 우측 상단 셀 위치 + Vector3.one → 셀 끝 바깥쪽까지 포함
        Vector3 maxWorld = tilemap.CellToWorld(cellBounds.max);

        // Clamp 계산
        float minX = minWorld.x + camHalfX;
        float maxX = maxWorld.x - camHalfX;
        float minY = minWorld.y + camHalfY;
        float maxY = maxWorld.y - camHalfY;

        // 카메라 위치 제한
        camPos.x = Mathf.Clamp(camPos.x, minX, maxX);
        camPos.y = Mathf.Clamp(camPos.y, minY, maxY);
        cam.transform.position = new Vector3(camPos.x, camPos.y, cam.transform.position.z);


    }
}
