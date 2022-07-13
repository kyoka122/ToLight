using Game.Hero;

namespace Game.StageItem.Roof
{
    public class HeroPowerElementManager
    {
        private readonly HeroPowerElementGenerator _elementGenerator;
        private const int ElementPower = 60;//MEMO: 1秒約60フレームのため

        public HeroPowerElementManager(HeroPowerElementGenerator elementGenerator)
        {
            _elementGenerator = elementGenerator;
            _elementGenerator.Init(GetHeroPowerCallBack);
        }
        
        public void ActiveGenerator(bool active)
        {
            _elementGenerator.OnGenerator(active);
        }
        
        private void GetHeroPowerCallBack(IHero hero)
        {
            hero.SetBattlePower(ElementPower);
            _elementGenerator.DecreaseElementCount();
        }
    }
}