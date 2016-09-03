using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageGenerators : MonoBehaviour {

	const int StageTipSize = 30;

	int currentTipIndex;

	public Transform character;
	public GameObject[] stageTips;
	public int startTipsIndex;    //自動生成開始インデックス
	public int preInstantiate;   //生成先読み個数
	public List<GameObject> generateStageList = new List<GameObject>(); //生成済みステージチップ保持リスト


	void Start () {
		currentTipIndex = startTipsIndex - 1;
		UpdateStage (preInstantiate);

	}

	void Update () {

		//キャラクターの位置から現在のステージチップのインデックスを計算
		int charaPositionIndex = (int)(character.position.z / StageTipSize);

		//次のステージチップに入ったらステージの更新処理をおこなう
		if (charaPositionIndex + preInstantiate > currentTipIndex) {
			UpdateStage (charaPositionIndex + preInstantiate);
		}
	}

	//指定のIndexまでのステージチップを生成して、管理下におく
	void UpdateStage(int toTipIndex)
	{
		if (toTipIndex <= currentTipIndex)
			return;

		//指定のステージチップまでを作成
		for (int i = currentTipIndex + 1; i <= toTipIndex; i++) {
			GameObject stageObject = GenerateStage(i);

			//生成したステージチップを管理リストに追加し
			generateStageList.Add (stageObject);
		}

		//古いステージを削除
		while (generateStageList.Count > preInstantiate + 2)
			DestroyOldestStage ();

		currentTipIndex = toTipIndex;

	}

	//指定のインデックス位置にStageオブジェクトをランダムに生成
	GameObject GenerateStage(int tipIndex)
	{
		int nextStageTip = Random.Range (0, stageTips.Length);

		GameObject stageObject = (GameObject)Instantiate (
			                         stageTips [nextStageTip],
			                         new Vector3 (0, 0, tipIndex * StageTipSize),
			                         Quaternion.identity
		                         );
		return stageObject;
	}

	//一番古いステージを削除
	void DestroyOldestStage()
	{
		GameObject oldStage = generateStageList [0];
		generateStageList.RemoveAt (0);
		Destroy (oldStage);
	}


}
