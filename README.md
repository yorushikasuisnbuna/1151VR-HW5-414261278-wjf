







1151VR 作業五 專案製作說明



































一、製作步驟
1. 建立專案與場景
建立 Unity 3D（URP）專案，使用 Stylized Nature Lite 資源包搭建場景
以 Terrain 工具繪製草地與起伏的山，並放置樹木
2. 匯入模型
訓練家：Marine（.dae）
寶可夢：Glaceon（.dae）
精靈球：ob0204_00_monsterball
3. 修復材質（粉紅色問題）
匯入的模型使用舊版 Built-in Shader，在 URP 下顯示為粉紅色，處理步驟：
選取模型檔，Inspector → Materials 分頁 → Extract Materials 抽出材質
全選材質 → Edit → Rendering → Materials → Convert Selected Built-in Materials to URP
將對應貼圖拖入材質的 Base Map 欄位
4. 調整位置與大小
Marine（訓練家）
Position：98.5, 0, 123.5
Rotation：0, 90, 0
Scale：0.02
精靈球
Position：99, 2.4, 123.5
Rotation：0, 0, 0
Scale：0.05
Glaceon（寶可夢）
Position：102, 0.25, 123.5
Rotation：0, -90, 0
Scale：0.015
補充說明：
將 Marine 從精靈球的子物件拖出，避免移動精靈球時連動
三者 Z 軸相同，精靈球飛行路徑為由左（X 小）至右（X 大）的直線
Camera1 位於 (100, 1.5, 120)，朝 +Z 方向拍攝
5. 建立收服特效
Hierarchy 右鍵建立 Particle System，命名為 CaptureEffect，設定如下：
Duration = 1，取消 Looping 與 Play On Awake
Start Lifetime = 1、Start Speed = 3、Start Size = 0.1
Emission：Rate over Time = 0，新增 Burst（Count = 60）
Shape：Sphere，Radius = 0.2
Renderer 材質使用 URP 相容材質，避免顯示粉紅色
6. 撰寫拋出與收服腳本
建立 PokeballThrow.cs 並掛在精靈球上，使用 Coroutine 依序執行：
等待 1 秒後開始
精靈球沿拋物線由左至右飛向寶可夢，飛行時旋轉
命中後在寶可夢位置播放 CaptureEffect
寶可夢縮小並消失，表示被收服
核心邏輯：
位置：用 Vector3.Lerp(起點, 終點, t) 計算水平移動，再加上 Mathf.Sin(t × π) × arcHeight 形成拋物線
旋轉：用 transform.Rotate 讓球飛行時自轉
收服：用 Vector3.Lerp(原大小, 零, s) 讓寶可夢逐漸縮小，最後 SetActive(false)
7. 連接腳本欄位
選取精靈球，在 Inspector 的 Pokeball Throw 腳本中：
Pokemon：拖入 Glaceon_ColladaMax
Capture Effect：拖入 CaptureEffect

二、腳本參數說明
Start Delay = 1：Play 後延遲拋出的秒數
Flight Time = 1.5：飛行時間
Arc Height = 1.5：拋物線高度
Spin Speed = 720：球旋轉速度（度/秒）
Hit Height Offset = 0.4：命中點高於寶可夢腳底的距離
Shrink Time = 0.6：寶可夢縮小消失時間

三、執行結果
按下 Play 後：精靈球由訓練家手中向右拋出，沿拋物線旋轉飛行，命中 Glaceon 後產生粒子特效，Glaceon 縮小消失，完成收服。
<img width="959" height="499" alt="螢幕擷取畫面 2026-11-22 235931" src="https://github.com/user-attachments/assets/749fa4d6-ecea-4a13-9515-0b7e7495ef09" />


影片連結: https://youtu.be/oqhpOoO2N8c
Github連結: https://github.com/yorushikasuisnbuna/1151VR-HW5-414261278-wjf
