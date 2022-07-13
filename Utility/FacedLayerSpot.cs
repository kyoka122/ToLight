using System.Collections.Generic;
using System.Linq;
using Game.SpotLight;
using UnityEngine;

namespace Utility
{
    public static class FacedLayerSpot
    {
        public static bool HadFacedLayerSpot(ISpotLight spotLight,RaycastHit[] heroFacedSpots,out RaycastHit facedSpot,int layer)
        {
            var standableSpots = TryGetSpotsInLayer(spotLight,heroFacedSpots,layer);
            if (standableSpots.Count==0)
            {
                facedSpot = new RaycastHit();
                return false;
            }
            
            facedSpot = GetHigherSpot(standableSpots);
            return true;
        }
        
        //MEMO: TryGetStandableSpots()の返り値ListのCountを事前にチェックして、raycastHitsのCountが0で無い事を保証している。
        private static RaycastHit GetHigherSpot(List<RaycastHit> raycastHits)
        {
            //Rayが当たった中で一番高い位置を取得
            var actionableRaycastHits = raycastHits.OrderBy(y => y.point.y).FirstOrDefault();
            return actionableRaycastHits;
        }

        private static List<RaycastHit> TryGetSpotsInLayer(ISpotLight spotLight,RaycastHit[] heroFacedSpots,int layer)
        {
            var raycastHitInSpotLights = spotLight.SearchInSpotLight();
            List<RaycastHit> heroAndLightRaycastHits=new List<RaycastHit>();
            
            foreach (var heroFacedSpot in heroFacedSpots)
            {
                //Debug.Log($"heroFacedSpot:{heroFacedSpot}");
                foreach (var raycastHitInSpotLight in raycastHitInSpotLights)
                {
                    if (heroFacedSpot.collider.gameObject==raycastHitInSpotLight.collider.gameObject)
                    {
                        heroAndLightRaycastHits.Add(heroFacedSpot);
                        break;
                    }
                }
            }
            var standableSpots =
                heroAndLightRaycastHits.Where(x => x.collider.gameObject.layer==layer).ToList();
            
            return standableSpots;
        }
    }
}