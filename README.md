# 2024 Air Force (A 11조)
![tt](https://github.com/ZhamesK/2024-Air-Force/assets/163416259/27f2c2e6-0ba5-4ce9-b50f-30451b533a4b)

<br>

<div align="center">

# 목차

| [✈️ 프로젝트 소개(개발환경) ](#airplane-프로젝트-소개) |
| :---: |
| [✋ 팀 소개 ](#hand-팀-소개) |
| [💭 기획의도 ](#thought_balloon-기획의도) |
| [⌨ 작동법 ](#keyboard-작동법) |
| [🌟 주요기능 ](#star2-주요기능) |
| [⏲️ 프로젝트 수행 절차 ](#timer_clock-프로젝트-수행-절차) |
| [🕹️ 기술 스택 ](#joystick-기술-스택) |
| [🕸️ 와이어프레임 ](#spider_web-와이어프레임) |
| [📓 UML ](#notebook-통합모델링-다이어그램) |
| [☑️ 트러블 슈팅 ](#ballot_box_with_check-트러블-슈팅) |

</div>

<br><br>

## :airplane: 프로젝트 소개

#### Team Notion을 클릭하시면, 프로젝트 진행 과정을 확인하실 수 있습니다!
### [🤝Team Notion](https://teamsparta.notion.site/1-1-11-89ba3d5eca2642fcb9c71d1eeb1a56c7)

<br>

#### ${\textsf{\color{red}보스가 등장하는 2024 슈팅게임}}$
### 적들에게 포위당한 플레이어를 구해주세요!
![Sparta](https://github.com/ZhamesK/2024-Air-Force/assets/108499207/03fd72fc-9233-4deb-8528-fff047bd0382)

<br>

<div align="center">

| 게임명 | **${\textsf{\color{blue}2024 Air Force}}$** |
| :---: | :---: |
| 장르 | 아케이드 & 슈팅게임 |
| 개발 언어 | C# |
| 프레임워크 | .Net 4.8.04084 |
| 개발 환경 | Unity 2022.3.17f1 <br/> Visual Studio Community 2022 |
| 타겟 플랫폼 | Android |
| 개발 기간 | 2024.05.16 ~ 2024.05.22 |

</div>

<br>

[:ringed_planet: 목차로 돌아가기](#목차)

<br><br>

## :hand: 팀 소개

| 직급 | 이름 | 기능 구현 | 깃허브 주소 | 블로그 |
| :---: | :---: | :---: | :---: | :---: |
| 팀장 | 김재혁 | 아이템 | https://github.com/ZhamesK | https://jhk97.tistory.com/manage/posts/ |
| 팀원 | 최재원 | 게임 UI | https://github.com/wodnjsdl93 | https://blog.naver.com/ccjjww15 |
| 팀원 | 이유신 | Enemy | https://github.com/shinmegan | https://unity4th-shin.tistory.com/ |
| 팀원 | 윤기범 | Player | https://github.com/Yoonwoojoo | https://yoonwoojoo.tistory.com/ |

<br>

[:ringed_planet: 목차로 돌아가기](#목차)

<br><br>

## :thought_balloon: 기획의도

### 1. 주제 선정 배경
3가지 게임 중에서 우리가 학습한 내용들을 최대한 활용해 볼 수 있는 게임은 과연 무엇일까 생각하 보았고, 이에 가장 적합한 게임은 닷지라고 판단되어 주제로 선정

### 2. 기존 유사 서비스와 차별화된 내용
적이 위에서 내려오는 기존의 갤러그 게임 형태인 방식이 아닌 사방에 적이 출현하도록 하여 보다 더 긴장감을 갖고 몰입할 수 있는 방식으로 기획

<br>

[:ringed_planet: 목차로 돌아가기](#목차)

<br><br>

## :keyboard: 작동법

![ControllerGuide](https://github.com/ZhamesK/2024-Air-Force/assets/108499207/0c5e486d-81d6-4f6e-9099-b535828658f7)

<br>

[:ringed_planet: 목차로 돌아가기](#목차)

<br><br>

## :star2: 주요기능

### 1. 플레이어
<table>
  <tr>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/167274465/5254bba7-b6a7-461e-b74e-abd10269cdb6" alt="WASD"></a></td>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/167274465/6c58f3e4-dd7b-4a5c-8893-5bb039b84812" alt="Aim"></a></td>
  </tr>
</table>

   - 키보드 A/W/S/D 를 이용하여 캐릭터가 움직입니다.
   - 캐릭터는 상하좌우 및 대각선으로 움직일 수 있습니다.
   - 캐릭터는 마우스 위치에 따라 회전합니다.

<br>

***
     
### 2. 적 및 스테이지  
<table>
  <tr>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/108499207/85bd197b-abbb-449e-8b47-cdd0ed83d920" alt="stage1To2"></a></td>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/108499207/5c6d9423-2b61-4a0d-9869-f5831f14da58" alt="Stage2To3"></a></td>
  </tr>
</table>

   - 적은 상하좌우 랜덤한 방향으로 움직입니다.
   - 시선은 플레이어를 쫒아가며, 조준 공격을 합니다.
   - 스테이지는 총 3단계로, 1단계는 중형 크기, 2단계는 소형 크기, 3단계는 대형 크기(보스)가 등장합니다.
   - 각 스테이지에 따라 적의 수, 배치구도, 능력치, 총알발사 방식이 다릅니다.  

<br>

***
  
### 3. 플레이어 공격 기능
<table>
  <tr>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/108499207/a189267f-d6b9-4ba4-aec6-021d03c3ea0e" alt="PlayerNormalAttack"></a></td>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/108499207/e6058f79-b86c-4c40-8365-ca04fc4b764f" alt="PurpleItemMultishot"></a></td>
  </tr>
</table>

   - 플레이어의 입력에 따라 발사체를 생성하고 조준하는 로직을 구현합니다.
   - Aim 메서드로 조준 방향을 업데이트 하고, Shoot 메서드로 공격을 실행 하게 만들고, AttackSO는 발사체의 속성을 정의합니다.
   - 아이템 정보에 접근하여, 아이템 획득 시 다양한 형태의 공격이 가능합니다.

<br>

***
        
### 4. 아이템 생성
<table>
  <tr>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/108499207/6b213882-af39-4d1d-98ac-715b3fbb41d4" alt="ItemAppearDisappear"></a></td>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/108499207/4e86a9f2-b220-4d42-bef3-73fb866069d1" alt="SpeedUp"></a></td>
  </tr>
</table>

   - 스테이지가 시작되면, 10초 마다 맵의 랜덤한 위치에 아이템이 생성됩니다.
   - 아이템 종류는 총 4가지로, 각각 체력회복, 공격력 증가, 멀티샷, 이속 증가 효과를 가지고 있습니다.
   - 아이템은 생성 후 5초 뒤에 자동으로 파괴되어, 플레이어가 획득하지 못하더라도 게임 씬에 하나 이상 존재하지 않습니다.
   - 애니메이션 효과를 추가하여, 아이템은 생성 후 5초 간 제자리를 빙글빙글 돕니다. 

<br>

***
    
### 5. 이미지 및 콜라이더 벽 추가
<table>
  <tr>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/108499207/4958fc81-77d1-4029-a380-be86583245fd" alt="CannotGetOut"></a></td>
  </tr>
</table>

   - 게임을 조금 더 재미있게 즐기기 위해 게임 주제와 어울리는 맵 배경, 캐릭터, 총알 이미지를 추가 했습니다.
   - 플레이어와 적이 게임화면을 벗어나는 것을 막기 위해, 화면 주위에 Tilemap Collider2D를 추가한 벽을 생성하였습니다.

<br>

***
    
### 6. Scene 생성 및 전환
<table>
  <tr>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/108499207/254fe3fb-6d7d-4ed6-be2d-4a940c913b4e" alt="ClickStartBtn"></a></td>
  </tr>
</table>

   - Start Scene와 Game Scene을 생성하여 게임의 완성도를 높였습니다.
   - Start scene에서 Start 버튼을 누르면 Game Scene로 넘어가 게임을 플레이 할 수 있습니다.
   - Start scene에서 Off 버튼을 누르면 게임이 종료됩니다.

<br>

***
        
### 7. Sound 효과
   - 게임 진행 내내 BGM이 흘러나와, 게임의 페이스와 리듬을 유지할 수 있습니다.  
   - 플레이어나 적이 제거될 때마다, 효과음을 추가하여, 게임의 몰입감과 즐거움을 높였습니다.

<br>

***
        
### 8. UI

<div align="center">

<table>
  <tr>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/108499207/13936440-77bd-4b27-8659-1ea416e4b550" alt="GotItemImageClear"></a></td>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/108499207/9efdf665-af92-4bb7-8bcf-dd8facf279c0" alt="RetryAfterClear"></a></td>
  </tr>
</table>

<table>
  <tr>
    <td><a href=""><img src="https://github.com/ZhamesK/2024-Air-Force/assets/108499207/7a405ce9-a850-4da9-8282-6b7f7e769cc7" alt="MainAfterFail"></a></td>
  </tr>
</table>

</div>

   - Start Scene에 [Start], [Off] 버튼 생성
   - 게임 화면 오른쪽 상단에 플레이어 HP Bar 추가
   - 중앙 상단에 아이템 획득 여부를 표시해주는 이미지 추가(획득 시 이미지가 더 선명해짐)
   - 왼쪽 상단에 해당 스테이지 정보 표시
   - 게임 클리어 / 실패 시 결과 화면 출력, <br> 결과 화면에 [다시하기] [메인화면] 버튼 UI 생성, 버튼 클릭 시 씬 전환
   - 플레이 시 왼쪽 하단에 [?] 버튼 생성, <br> 조작법, 스테이지 정보, 아이템 정보를 볼 수 있어서, 누구나 쉽게 플레이 가능

<br>

[:ringed_planet: 목차로 돌아가기](#목차)

<br><br>

## :timer_clock: 프로젝트 수행 절차

| 구분 | 기간 | 활동 | 비고 |
| :---: | :---: | :---: | :---: |
| 사전 기획 | 05.16(목) | 프로젝트 주제 선정 및 와이어프레임 작성 | 아이디어 선정 |
| 역할 분담 | 05.16(목) | 스크립트 분류 및 배정 | 머지 충돌 최소화 |
| 프로젝트 <br> 수행 | 05.16(목) ~ 05.21(금) | 필수사항 및 선택사항 구현 | 하루 단위로 Merge |
| 프로젝트 <br> 완료 | 05.21(금) | 밸런스 패치, 스크립트 간소화, <br> 최종 버그 수정 | dev에서 main으로 merge |
| 발표 준비 | 05.22(수) | 와이어프레임 수정, ReadMe 수정, <br> PPT 제작 | UML 추가 |

<br>

[:ringed_planet: 목차로 돌아가기](#목차)

<br><br>

## :joystick: 기술 스택

![TechStack](https://github.com/ZhamesK/2024-Air-Force/assets/167274465/52d9c045-c684-4282-bb6d-8fc178b4915f)

<br>

[:ringed_planet: 목차로 돌아가기](#목차)

<br><br>

## :spider_web: 와이어프레임

![와이어프레임](https://github.com/ZhamesK/2024-Air-Force/assets/108499207/41c4f51b-cb2d-4f96-925f-ac9a81b4483a)

<br>

[:ringed_planet: 목차로 돌아가기](#목차)

<br><br>

## :notebook: 통합모델링 다이어그램

![2024AirForceUML](https://github.com/ZhamesK/2024-Air-Force/assets/167274465/dc177d99-1175-41e1-bf2c-d614bdc018c8)

<br>

[:ringed_planet: 목차로 돌아가기](#목차)

<br><br>

## :ballot_box_with_check: 트러블 슈팅

![t1](https://github.com/ZhamesK/2024-Air-Force/assets/167274465/5bd55c64-4ae2-4788-af5c-d271f3a5592f)

<br>

![image](https://github.com/ZhamesK/2024-Air-Force/assets/167274465/ca3f392a-4145-4718-9f05-7c95ffe734c4)

<br>

![t3](https://github.com/ZhamesK/2024-Air-Force/assets/167274465/c051e4cd-1ef6-4fbf-88c5-f52eecd15769)

<br>

![t4](https://github.com/ZhamesK/2024-Air-Force/assets/167274465/c0590ca3-b0c1-4811-b19a-c8f7ff141179)

<br>

[:ringed_planet: 목차로 돌아가기](#목차)

<br><br>
