boss 패턴 중 eruption에 사용되는 프리팹의 sprite가 하강할때 아래를 바라보는 것이 아닌 기본 sprite 모양인 우측을 바라보는 문제

STAGE2의 첫번째 Step 퍼즐의 첫번째인
Step_2_1 Start를 ontriggerenter2d 하면 bullet을 3회 발사. 
(발사중엔 ontriggerenter2d해도 수행중인 발사 진행. 초기화 or 중첩이 되지 않는다)
CountPivot에 맞은 bullet의 tag를 추적해 각각 white, black 두개의 int형으로 저장
Step_2_1 Start를 다시 밟을경우 저장된 값은 사라지고 다시 계산한다.

플레이어가 white, black의 개수를 유추한 후 Step_2_1 End로 ontriggerenter2d할 시 
time.Scale이 0f가 되며 Step_2_1Q UI가 열림

Step_2_1Q에서 각각 White와 Black 값을 입력한 후 버튼을 눌러 
맞추면 UI 종료 + door1 열림 (맞출 시 Step_2_1Q UI는 destroy)
못맞출시 wrongLog 출력 후 white와 black 의 일치 여부도 text로 출력
우측 상단의 버튼을 누를 시 UI 비활성화. (재시도를 목적으로 한다)

추가 : Step_2_1 Start, Step_2_1 End를 단순히 진입 시 작용하는 것이 아닌 실제 버튼이 눌리는 느낌 구현하기
(버튼이 눌려야 기능 발동)

