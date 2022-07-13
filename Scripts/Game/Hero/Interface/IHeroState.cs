namespace Game.Hero
{
    /// <summary>
    /// Stateの基本機能
    /// </summary>
    public interface IHeroState
    {
        public IHeroState SelectNextStateByInput(IHero hero);
        
        /// <summary>
        /// Stateの動作を定義
        /// </summary>
        /// <param name="hero"></param>
        public void OnAction(IHero hero);
        
        /// <summary>
        /// 次に選ぶステートがどのステートにも当てはまらなかった時
        /// </summary>
        /// <param name="hero"></param>
        public IHeroState InitState(IHero hero);
        
        /// <summary>
        /// 必要ならば後処理を記述する箇所。ステートが変わる際に呼び出す。
        /// </summary>
        public void RefreshThisState(IHero hero);

        //MEMO: ステート遷移の内容をステージごとで変更するなら必要（今回はそんなに影響ないため必要ない）
        /*public void UpdateStateClassRoomStage();
        public void UpdateStateMoveOnlyStage();

        public void UpdateStateUpstairsStage();
        public void UpdateStateRoofTopStage();*/

    }
}