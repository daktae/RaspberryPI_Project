# RaspberryPI_Project
라즈베리파이를 이용한 게임기

# 프로젝트 소개
 - 닌텐도 스위치와 같이 여러 가지 센서를 활용하여 게임을 할 수 있도록 아두이노의 여러 가지 센서와 라즈베리파이를 이용한 게임기를 만들게 되었다. 게임 속 컨텐츠로는 ML-Agent를 이용한 강화학습을 통해 만들어진 AI와 겨루는 싱글플레이, 클라우드 형식의 포톤 PUN을 이용한 멀티플레이, 아두이노에서 센서값을 블루투스 모듈을 통해 센서값을 전달하고, 전달된 센서값은 플레이어의 이동 및 추가조작을 하는데 활용된다. 만들어진 게임은 오픈소스로서 Github을 통해, 자유롭게 수정 및 배포가 가능하다.

# :hammer: 기술 스택
### Environment
<img src="https://img.shields.io/badge/Visual Studio-5c2d91?style=flat&logo=VisualStudio&logoColor=white"/> <img src="https://img.shields.io/badge/Github-181717?style=flat&logo=Github&logoColor=white"/> <img src="https://img.shields.io/badge/Git-f05032?style=flat&logo=Git&logoColor=white"/>

### Language
<img src="https://img.shields.io/badge/C Sharp-239120?style=flat&logo=CSharp&logoColor=white"/>  <img src="https://img.shields.io/badge/Python-3776ab?style=flat&logo=Python&logoColor=white"/> 

### Game Engine / Server
<img src="https://img.shields.io/badge/Unity-000000?style=flat&logo=Unity&logoColor=white"/> , Photon Server

### H/W
<img src="https://img.shields.io/badge/Arduino-00979d?style=flat&logo=Arduino&logoColor=white"/> <img src="https://img.shields.io/badge/Raspberry Pi 4-a22846?style=flat&logo=RaspberryPi&logoColor=white"/> 


# 기능 구현
### :star: 싱글플레이 구현

- 싱글플레이를 위한 AI는 유니티엔진에서 제공하는 Python 기반의 Machine Learning 라이브러리를 사용
- 체크포인트는 시작 지점에서부터 골인 지점까지 번호를 부여하였고, AI가 번호 순서대로 지날 때마다 reward 함수를 통해서 보상을 얻게 된다. (머신러닝)
<p align="center">
  <img src="https://github.com/daktae/RaspberryPI_Project/assets/66005780/7731601e-5781-4263-b8d5-f10e922bf50e" width="400">
</p>

### :star: 멀티플레이 구현
- 멀티플레이를 위한 서버는 클라우드 형식의 서버 포톤PUN 서버 사용
- 포톤PUN 서버: 각 플레이어의 데이터를 PUN으로 전달 -> PUN이 플레이어의 데이터를 중계 -> 각 플레이어는 데이터를 전달받아 동기화 (릴레이방식)
<p align="center">
  <img src="https://github.com/daktae/RaspberryPI_Project/assets/66005780/e47928ad-2d06-4e75-a796-ff556226dc4e" width="400">
</p>

### :star: 조작 구현
- 아두이노의 자이로센서(MPU-6050)를 통해 플레이어의 움직임에 필요한 센서값 갖고오기
- 갖고 온 센서값을 블루투스 모듈(HC-06)을 통해서 라즈베리파이에 전달
- 아두이노의 BTSerial 함수를 통해 버퍼 형태로 센서값 전달
- 전달된 센서값은  Unity Android Bluetooth API를 통해 게임에 전달
- 전달받은 센서값을 통해서 움직임 구현
<p align="center">
  <img src="https://github.com/daktae/RaspberryPI_Project/assets/66005780/99606fb0-9ec9-4591-8b07-3e954f2aac9f" width="400">
</p>

## 게임 플레이
- [싱글플레이] https://youtu.be/L9irJsP7_VI
- [멀티플레이] https://youtu.be/qvbSIpQC-Ts

## 아쉬운 점
- 기획에 있어서 게임 실행에 있어서, 터치스크친을 통한 터치로 게임 진행을 목표로 하였다, 화면 크기로 인해 라즈베리파이 정품 터치스크린이 아닌 일반 디스플레이로 대체하여 터치가 되지 않았다. 이런 부족한 면도 있지만, 모든 활용한 소스가 오픈소스라는 점에서 만든 프로그램을 Github를 통해서 자유롭게 수정, 배포, 활용이 가능하다는 점이 장점이라고 생각한다. 

