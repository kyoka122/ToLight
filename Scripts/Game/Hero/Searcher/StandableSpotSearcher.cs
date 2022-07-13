namespace Game.Hero.Searcher
{
    public class StandableSpotSearcher
    {
        //CHANGED: HeroRaycastに移動
        /*private RaycastHit _raycastHit;
        
        public bool OnStandableObj()
        {
            //Debug.DrawRay(SphereCastOriginTransform.position, -SphereCastOriginTransform.up, Color.red, maxDistance);
            bool isHit = Physics.SphereCast(SphereCastOriginTransform.position, raycastRadius,
                -SphereCastOriginTransform.up, out _raycastHit,
                maxDistance,LayerInfo.StandableItem);
            return isHit;
        }*/
    }
}