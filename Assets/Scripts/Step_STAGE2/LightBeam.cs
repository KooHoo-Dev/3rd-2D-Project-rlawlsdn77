using System.Collections.Generic;
using Study.Utilities;
using UnityEngine;

namespace SHIFTER
{
    // 조준 모드일 때만 마우스 방향으로 레이저를 발사합니다.
    // Mirror(거울)에 맞으면 입사각=반사각으로 반사하며 계속 나아가고,
    // LightSensor에 명중하면 전달합니다. LaserPuzzleManager가 IsAiming을 제어합니다.
    public class LightBeam : MonoBehaviour
    {
        [SerializeField] private LightSensor targetSensor; // 비워두면 씬에서 자동으로 찾음
        [SerializeField] private Camera aimCamera; // 마우스 좌표 변환에 사용할 카메라 (퍼즐 카메라)
        [SerializeField] private float maxDistance = 30f; // 한 구간 최대 사거리
        [SerializeField] private int maxBounces = 6; // 거울에 반사될 수 있는 최대 횟수
        [SerializeField] private float beamWidth = 0.1f;
        [SerializeField] private Material beamMaterial; // 비워두면 기본 라인 머티리얼 사용

        public bool IsAiming { get; set; } = false;

        private readonly List<LineRenderer> segments = new List<LineRenderer>();

        private void Start()
        {
            if (targetSensor == null) targetSensor = FindFirstObjectByType<LightSensor>();
        }

        private void Update()
        {
            if (!IsAiming)
            {
                HideUnusedSegments(0);
                return;
            }

            FireBeam();
        }

        private void FireBeam()
        {
            Vector2 direction = GetAimDirection();
            Vector2 origin = (Vector2)transform.position + direction * 0.05f;

            int segmentIndex = 0;
            int bounces = 0;
            int safety = 0;
            bool hitSensorThisFrame = false;

            while (bounces < maxBounces && safety < maxBounces + 5)
            {
                safety++;
                RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance);

                if (hit.collider == null)
                {
                    DrawSegment(segmentIndex++, origin, origin + direction * maxDistance);
                    break;
                }

                DrawSegment(segmentIndex++, origin, hit.point);

                // 플레이어는 레이저 안맞음
                if (hit.collider.gameObject.name == "Shifter")
                {
                    origin = hit.point + direction * 0.05f;
                    continue;
                }

                // 거울에 맞으면 반사
                if (hit.collider.GetComponent<Mirror>() != null)
                {
                    direction = Vector2.Reflect(direction, hit.normal);
                    origin = hit.point + direction * 0.05f;
                    bounces++;
                    continue;
                }

                if (hit.collider.GetComponent<LightSensor>() != null)
                {
                    hitSensorThisFrame = true;
                    break;
                }

                if (hit.collider.isTrigger)
                {
                    origin = hit.point + direction * 0.05f;
                    continue;
                }

                break; 
            }

            HideUnusedSegments(segmentIndex);

            if (targetSensor != null)
            {
                targetSensor.UpdateBeam(hitSensorThisFrame);
            }
        }

        private Vector2 GetAimDirection()
        {
            Vector3 mouseWorld = aimCamera.ScreenToWorldPoint(SimpleInput.GetMousePosition());
            mouseWorld.z = transform.position.z;
            return ((Vector2)mouseWorld - (Vector2)transform.position).normalized;
        }

        private void DrawSegment(int index, Vector3 from, Vector3 to)
        {
            LineRenderer lr = GetSegment(index);
            lr.enabled = true;
            lr.SetPosition(0, from);
            lr.SetPosition(1, to);
        }

        private void HideUnusedSegments(int usedCount)
        {
            for (int i = usedCount; i < segments.Count; i++)
            {
                segments[i].enabled = false;
            }
        }

        private LineRenderer GetSegment(int index)
        {
            while (segments.Count <= index)
            {
                GameObject segObj = new GameObject("BeamSegment_" + segments.Count);
                segObj.transform.SetParent(transform);

                LineRenderer lr = segObj.AddComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.startWidth = beamWidth;
                lr.endWidth = beamWidth;
                lr.sortingOrder = 5;
                lr.startColor = Color.red;
                lr.endColor = Color.red;
                if (beamMaterial != null) lr.material = beamMaterial;

                segments.Add(lr);
            }

            return segments[index];
        }
    }
}
