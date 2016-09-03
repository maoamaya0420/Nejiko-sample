using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageGenerators : MonoBehaviour {

	const int StagetipSize = 30;

	int currentTipIndex;

	public GameObject Nejiko;
	public GameObject[] Stagetips;
	public int startTipsIndex;    //自動生成開始インデックス
	public int preInstantiate;   //生成先読み個数
	public List<GameObject> generateStageList = new List<GameObject>(); //生成済みステージチップ保持リスト


	void Start () {
		currentTipIndex = startTipsIndex - 1;
		UpdateStage (preInstantiate);

	}

	void Update () {

		//キャラクターの位置から現在のステージチップのインデックスを計算
		int charaPositionIndex = (int)(CharacterInfo.position.z / StagetipSize);

		//次のステージチップに入ったらステージの更新処理をおこなう
		if (charaPositionIndex + preInstantiate > currentTipIndex) {
			UpdateStage (charaPositionIndex + preInstantiate);
		}
	}

	//指定のIndexまでのステージチップを生成して、管理下におく

}
