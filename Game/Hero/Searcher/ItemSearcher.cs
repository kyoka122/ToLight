using System.Collections.Generic;
using System.Linq;
using Game.SpotLight;
using Game.StageItem;
using Game.StageItem.Interface;
using UnityEngine;

namespace Game.Hero.Searcher
{
    public class ItemSearcher
    {
        private RaycastHit[] _raycastHits;

        public IHeroHandleItem GetHandleableItem(Vector3 heroPos,ISpotLight spotLight,RaycastHit[] raycastHits)
        {
            var havingColliderObjects = GetHavingColliderObjects(spotLight,raycastHits);
            var rootObjects = GetRootObjects(havingColliderObjects);
            var closestItem = GetHeroClosestItemComponent(heroPos,rootObjects);
            var closestItemComponent = GetItemComponent<IHeroHandleItem>(closestItem);
            
            return closestItemComponent;
        }

        private T GetItemComponent<T>(List<GameObject> objects)
        {
            var closestItemComponent = objects.Select(x=>x.GetComponent<T>()).FirstOrDefault();
            return closestItemComponent;
        }
        
        private List<GameObject> GetHeroClosestItemComponent(Vector3 heroPos,List<GameObject> objects)
        {
            var itemOrderDistance = objects
                .OrderBy(item => Vector3.Distance(item.transform.position, heroPos)).ToList();

             return itemOrderDistance;
        }

        private List<GameObject> GetRootObjects(List<GameObject> objects)
        {
            var raycastedObjects = objects
                .Select(x => x.GetComponent<RaycastedObj>())
                .Where(y=>y!=null);
            
            return raycastedObjects.SelectMany(raycastedObj => raycastedObj.RaycastedRootObj).ToList();
        }

        //TODO: SpotLightとHeroFcedを分ける。SpotLightはpublicのメソッド内へ
        private List<GameObject> GetHavingColliderObjects(ISpotLight spotLight,RaycastHit[] heroFacedRaycastHits)
        {
            var inSpotLightRaycastHits=spotLight.SearchInSpotLight();

            var inSpotLightObjects = inSpotLightRaycastHits.Select(x=>x.collider.gameObject);
            var heroFacedObjects=heroFacedRaycastHits.Select(x=>x.collider.gameObject);
            var actionableRaycastHits = inSpotLightObjects.Intersect(heroFacedObjects).ToList();

            return actionableRaycastHits;
        }
        
        
    }
}