
생성/수정한 코드는 모두 Assets/MyAssets_copy 폴더에 있습니다.

작업 내용:

  1) 아이템
  * 구슬 개수 3개로 제한 - StoneGenerator.cs
  * 플레이어가 이동하며 마우스 클릭한 곳으로 구슬 던짐 - StoneGenerator.cs
  * 일반유리/강화유리에 구슬을 던졌을때 다른 소리가남 - BreakableWindow.cs, TemperedGlassController.cs
  
  2)플레이어
  * 게임 실행시 플레이어 공중에 뜨는 현상 수정
  * 플레이어와 유리 인터랙션 => 밟았을때 깨지고 떨어짐 - Work.cs

문제점(수정필요):
  * 플레이어가 일반유리 밟아서 떨어질 경우 바닥과 충돌하는 것 인식못함
