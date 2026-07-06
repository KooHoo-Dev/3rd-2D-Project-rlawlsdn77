# Scripts 검수 변경 로그

Assets/Scripts 전체 대상, 기준: (1) 불필요한 부분 제거 (2) 단순/명료화 (3) 어려운 내장 메서드 지양 (4) 디버그 로그 최소화 (5) 주석/툴팁 최소화.

## Boss 계열

- **IBossPattern.cs**: 사용하지 않는 `using System.Collections;`, `using UnityEngine;` 제거
- **BossPattern_Eruption.cs**: 할당만 되고 쓰이지 않던 `shifter` 필드 제거, "운석 낙하 시작!" 로그 제거
- **BossPattern_Rampage.cs**: 쓰이지 않던 `shifter` 필드 제거, "무차별 난사!" 로그 제거
- **BossPattern_Razer.cs**: "레이저 공격!" 로그 제거
- **BossManager.cs**: 패턴 시작/전환/그로기/사망 등 알림용 로그 8곳 제거 (설정 누락 경고 로그는 유지)
- **BossProjectile.cs**: 피격 알림 로그 제거

## 루트 스크립트

- **CameraController.cs**: 주석의 이모지 제거
- **PlatformManager.cs**: 의미 없는 이력성 주석, 불필요한 빈 줄 제거
- **SceneChanger.cs**: 사용하지 않는 `using System;` 제거. `UnityEditor.EditorApplication` 호출을 `#if UNITY_EDITOR`로 감싸서 빌드 시 컴파일 에러 나던 부분 수정 (실제 버그)
- **PressureButton.cs**: 디버깅용으로 임시로 넣었던 로그 2곳 제거

## Shifter

- **Shifter.cs**: 피격/사망 알림 로그 2곳 제거
- **Shifter Controller.cs**: 사용하지 않는 `using System;`, 어디서도 참조 안 되던 애니메이터 해시(IS_RUN/IS_JUMP/IS_DOWN) 제거, Shift 알림 로그 제거

## Step_STAGE1

- **BoxGoal.cs**: 사용하지 않는 `using System;` 제거
- **ButtonReset.cs**: 항상 참인 `if (collision != null)` 체크 제거, "문이 열립니다" 로그 제거
- **ButtontoLight.cs**: 항상 참인 null 체크 제거, if/else를 삼항 연산자로 단순화

## Step_STAGE2

- **CrusherWall.cs**: 이전 대화에서 이미 정리 완료 (디버그 로그 없음, 불필요 코드 없음)
- **LaserPuzzleManager.cs**: 디버깅용으로 임시로 넣었던 로그 2곳 제거

## UI

- **BossHealthUI.cs**: 이미 깔끔한 상태, 변경 없음
- **OnBosspattern.cs**: 불필요한 `using Unity.VisualScripting;` 제거, 필드에 잘못 쓰인 `[field: SerializeField]`를 `[SerializeField]`로 수정, for문을 foreach로 단순화, "보스 방 진입" 로그 제거
- **UIModManager.cs**: 이미 깔끔한 상태, 변경 없음

## 검수 중 발견한 점 (수정은 안 함, 참고용)

- **UIModManager.cs**: `player`가 비어있으면 `GameObject.FindWithTag("Player")`로 찾는데, 플레이어의 실제 태그는 Common/White/Black으로만 바뀌고 "Player" 태그는 쓰이지 않아서 이 fallback은 사실상 항상 실패합니다. 인스펙터에 player를 직접 연결해두면 문제없이 동작합니다.

전체 검수 완료했습니다.
