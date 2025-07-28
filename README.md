# 🕹️ Sparta Metaverse - 2D 캐릭터 기반 미니 게임 프로젝트

## 🔧 주요 기능

### 🚶 캐릭터 이동 및 맵 탐색
- WASD 를 통해 캐릭터가 자유롭게 맵을 이동
- Tilemap 기반의 맵 위를 자연스럽게 탐험 가능

### 🗺️ 맵 설계 및 상호작용 영역
- 간단한 2D 맵을 설계하여 플레이어의 이동 경로 제공
- 특정 영역에 진입 시 상호작용 이벤트 발생 (예: 미니게임 씬으로 전환)

### 🎮 미니 게임 점수 시스템
- 미니게임 진행 중 점수가 실시간 UI에 반영됨

### 🔁 게임 종료 및 복귀
- 미니게임이 종료되면 메인 맵으로 자동 복귀
- 결과 UI를 통해 현재 점수 및 최고 기록 동시 출력

### 🎥 카메라 추적 기능
- 플레이어 이동에 따라 카메라가 자연스럽게 따라오는 추적 시스템 구현
- 맵 경계를 벗어나지 않도록 제한 처리 포함

### 🧑‍🤝‍🧑 NPC와의 대화 시스템
- NPC 근처에 접근하면 대화창이 활성화됨
- `E` 키 입력 시 대화 시작

### 🎨 커스텀 캐릭터 변경
- 특정 NPC와의 상호작용을 통해 플레이어 캐릭터 변경 가능

---

## 🧩 트러블슈팅

### 1. 비활성화된 GameObject에서 코루틴 실행 실패

#### 🧭 배경
UI 전환을 위해 코루틴을 사용하던 중, 비활성화된 오브젝트에서 코루틴이 정상적으로 실행되지 않는 현상이 발생했습니다.

#### 🚨 문제
`SetActive(false)`로 GameObject를 비활성화한 후 코루틴을 실행하려 하면,  
내부 함수가 호출되지 않거나 실행이 중단됩니다.

```csharp
// UI.cs
gameObject.SetActive(false);
StartCoroutine(SomeCoroutine()); // 
IEnumerator SomeCoroutine()
{
    // ❌ 실행되지 않음
    yield return new WaitForSeconds(1f);
    Debug.Log("코루틴 내부 로직");
}
```
#### ✅ 해결
- 코루틴은 활성화된 GameObject에서만 안정적으로 실행됩니다.
- 따라서 코루틴 실행은 별도의 Manager 객체에서 처리하도록 구조를 분리하였습니다.
- 해당 경험을 통해, 기능별 책임 분리와 GameObject 생명주기 관리의 중요성을 인식하게 되었습니다.

### 2. 캐릭터 변경 시 SpriteRenderer 및 Animator가 교체되지 않음

#### 🧭 배경
게임 도중 NPC와 대화를 통해 플레이어의 외형을 커스터마이징할 수 있는 기능을 구현 중, 다른 캐릭터 프리팹에서 SpriteRenderer와 Animator를 가져와 현재 플레이어에 적용하려 하였습니다.

#### 🚨 시도
프리팹을 참조하여 내부에 포함된 SpriteRenderer 및 Animator 컴포넌트를 현재 플레이어 오브젝트에 직접 할당하는 방식으로 구현하였습니다.

```csharp
// 프리팹 charBase에 캐릭터의 스프라이트 렌더러, 애니메이터 정보 설정
SpriteRenderer tmpSP = GetComponentInChildren<SpriteRenderer>();
tmpSP = charBase.characterRenderer; // ❌ 변수만 바뀜, 실제 적용 안 됨
Animator tmpAnim = GetComponentInChildren<Animator>();
tmpAnim = charBase.characterAnimator; //  ❌ 변수만 바뀜, 실제 적용 안 됨
```

#### 🔍 문제 발견
- 위 코드는 단순히 로컬 변수 tmpSP, tmpAnim에 다른 컴포넌트를 대입했을 뿐,
플레이어 오브젝트의 실제 SpriteRenderer 및 Animator 컴포넌트에는 아무런 변화가 없었습니다.
- 즉, Unity에서 컴포넌트 자체는 직접 교체할 수 없고, 내부 속성만 변경 가능하다는 점을 간과했던 것입니다.

#### ✅ 해결
- 기존의 SpriteRenderer와 Animator 컴포넌트를 그대로 유지하면서,
내부 속성만 덮어써서 외형과 애니메이션을 정상적으로 적용하였습니다:

```csharp
SpriteRenderer playerRenderer = GetComponentInChildren<SpriteRenderer>();
playerRenderer.sprite = charBase.characterRenderer.sprite;

Animator playerAnimator = GetComponentInChildren<Animator>();
playerAnimator.runtimeAnimatorController = charBase.characterAnimator.runtimeAnimatorController;
```

#### 🏁 결말
- 컴포넌트 자체를 교체할 수 없다는 Unity의 구조적 제약을 이해하고, 속성만 교체하는 방식으로 외형과 애니메이션을 정상적으로 적용할 수 있었습니다.
- 해당 구조를 기준으로 커스터마이징 기능을 안정화하였습니다.

---

## 🛠️ 기술 스택
- Unity 2022.3.17f1
- C#

---
